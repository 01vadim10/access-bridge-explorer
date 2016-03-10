﻿// Copyright 2015 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using AccessBridgeExplorer.Model;

namespace AccessBridgeExplorer.WindowsAccessBridge {
  /// <summary>
  /// Wrapper for an accessible context returned by the Java Access Bridge.
  /// </summary>
  public class AccessibleContextNode : AccessibleNode {
    const int TextBufferSize = 256;
    private readonly JavaObjectHandle _ac;
    private Lazy<AccessibleContextInfo> _info;
    private readonly List<Lazy<AccessibleNode>> _childList = new List<Lazy<AccessibleNode>>();
    private bool _isManagedDescendant;

    public AccessibleContextNode(AccessBridge accessBridge, JavaObjectHandle ac) : base(accessBridge) {
      _ac = ac;
      _info = new Lazy<AccessibleContextInfo>(FetchNodeInfo);
    }

    public override int JvmId {
      get { return _ac.JvmId; }
    }

    public override bool IsManagedDescendant {
      get { return _isManagedDescendant; }
    }

    public void SetManagedDescendant(bool value) {
      _isManagedDescendant = value;
    }

    public override void Dispose() {
      _ac.Dispose();
    }

    public override AccessibleNode GetParent() {
      ThrowIfDisposed();
      var parentAc = AccessBridge.Functions.GetAccessibleParentFromContext(JvmId, _ac);
      if (parentAc.IsNull) {
        return null;
      }

      var hwnd = AccessBridge.Functions.GetHWNDFromAccessibleContext(JvmId, parentAc);
      if (hwnd != IntPtr.Zero) {
        return new AccessibleWindow(AccessBridge, hwnd, parentAc);
      }

      return new AccessibleContextNode(AccessBridge, parentAc);
    }

    private void ThrowIfDisposed() {
      if (_ac.IsClosed)
        throw new ObjectDisposedException("accessibility context");
    }

    private int LimitSize(int count) {
      return Math.Min(AccessBridge.CollectionSizeLimit, count);
    }

    /// <summary>
    /// Limit the values of count1 and count2 by the same factor so that
    /// their product is less than or equal to the maximum collection size.
    /// </summary>
    private KeyValuePair<int, int> LimitSize(int count1, int count2) {
      if (count1 <= 0 || count2 <= 0)
        return new KeyValuePair<int, int>(0, 0);

      double c1 = count1;
      double c2 = count2;
      double limit = AccessBridge.CollectionSizeLimit;
      int c1Max = (int)Math.Round(Math.Sqrt(limit * c2 / c1));
      int c2Max = (int)Math.Round(limit / c1Max);
      return new KeyValuePair<int, int>(Math.Min(c1Max, count1), Math.Min(c2Max, count2));
    }

    public AccessibleContextInfo FetchNodeInfo() {
      ThrowIfDisposed();
      _childList.Clear();

      AccessibleContextInfo info;
      if (Failed(AccessBridge.Functions.GetAccessibleContextInfo(JvmId, _ac, out info))) {
        throw new ApplicationException("Error retrieving accessible context info");
      }
      _childList.AddRange(
        Enumerable
        .Range(0, LimitSize(info.childrenCount))
        .Select(i => new Lazy<AccessibleNode>(() => FetchChildNode(i))));

      return info;
    }

    public AccessibleNode FetchChildNode(int i) {
      ThrowIfDisposed();
#if false
      var nodeInfo = _info.Value;
      if ((nodeInfo.accessibleInterfaces & AccessibleInterfaces.cAccessibleTableInterface) != 0) {
        // Note: The default implementation for table is incorrect due to an
        // defect in JavaAccessBridge: instead of returning the
        // AccessbleJTableCell, the AccessBridge implemenentation, for some
        // reason, has a hard-coded reference to JTable and calls
        // "getCellRendered" directly instead of calling
        // AccessibleContext.GetAccessibleChild(). So we need to call a custom
        // method for tables.
        return FetchTableChildNode(i);
      }
#endif

      var childhandle = AccessBridge.Functions.GetAccessibleChildFromContext(JvmId, _ac, i);
      if (childhandle.IsNull) {
        throw new ApplicationException(string.Format("Error retrieving accessible context for child {0}", i));
      }

      var result = new AccessibleContextNode(AccessBridge, childhandle);
      if (_isManagedDescendant || _info.Value.states.Contains("manages descendants")) {
        result.SetManagedDescendant(true);
      }
      return result;
    }

    /// <summary>
    /// The only way we found to get the right info for a cell in a table is to
    /// set the selection to a single cell, then to get the accessible context
    /// for the selection. A side effect of this, of course, is that the table
    /// selection will change in the Java Application.
    /// </summary>
    private AccessibleNode FetchTableChildNode(int childIndex) {
      // Get the current selection, just in case it contains "childIndex".
      var childNode = FindNodeInSelection(childIndex);
      if (childNode != null)
        return childNode;

      // Note that if the table only supports entire row selection, this call
      // may end up selecting an entire row.
      AccessBridge.Functions.ClearAccessibleSelectionFromContext(JvmId, _ac);
      AccessBridge.Functions.AddAccessibleSelectionFromContext(JvmId, _ac, childIndex);
      childNode = FindNodeInSelection(childIndex);
      if (childNode != null)
        return childNode;

      var row = AccessBridge.Functions.GetAccessibleTableRow(JvmId, _ac, childIndex);
      var col = AccessBridge.Functions.GetAccessibleTableColumn(JvmId, _ac, childIndex);

      throw new ApplicationException(string.Format("Error retrieving accessible context for cell [{0},{1}]", row, col));
    }

    /// <summary>
    /// Return the <see cref="AccessibleContextNode"/> of the current selection
    /// that has its "indexInParent" equal to <paramref name="childIndex"/>. Returns
    /// <code>null</code> if there is no such child in the selection.
    /// </summary>
    private AccessibleContextNode FindNodeInSelection(int childIndex) {
      var selCount = AccessBridge.Functions.GetAccessibleSelectionCountFromContext(JvmId, _ac);
      if (selCount > 0) {
        for (var selIndex = 0; selIndex < LimitSize(selCount); selIndex++) {
          var selectedContext = AccessBridge.Functions.GetAccessibleSelectionFromContext(JvmId, _ac, selIndex);
          if (!selectedContext.IsNull) {
            var selectedNode = new AccessibleContextNode(AccessBridge, selectedContext);
            if (selectedNode.GetInfo().indexInParent == childIndex) {
              return selectedNode;
            }
          }
        }
      }
      return null;
    }

    public override void Refresh() {
      _info = new Lazy<AccessibleContextInfo>(FetchNodeInfo);
    }

    public AccessibleContextInfo GetInfo() {
      return _info.Value;
    }

    protected override int GetChildrenCount() {
      // We limit to 256 to avoid (almost) infinite loop when # of children
      // is really huge (e.g. an app exposing a worksheet with thousand of cells).
      return LimitSize(GetInfo().childrenCount);
    }

    protected override AccessibleNode GetChildAt(int i) {
      ThrowIfDisposed();
      return _childList[i].Value;
    }

    public override Rectangle? GetScreenRectangle() {
      var info = GetInfo();
      if (info.width >= 0 && info.height >= 0) {
        return new Rectangle(info.x, info.y, info.width, info.height);
      }
      return null;
    }

    public override void FetchSubTree() {
      //GetInfo();
      //foreach (var child in GetChildren()) {
      //  child.FetchSubTree();
      //}
    }

    public override NodePath GetNodePathAt(Point screenPoint) {
      // Bail out early if the node is not visible
      var info = GetInfo();
      if (info.states != null && info.states.IndexOf("showing", StringComparison.InvariantCulture) < 0) {
        return null;
      }

      // Special case to avoid returning nodes from overlapping components:
      // If we are a viewport, we only return children contained inside our
      // containing rectangle.
      if (info.role != null && info.role.Equals("viewport", StringComparison.InvariantCulture)) {
        var rectangle = GetScreenRectangle();
        if (rectangle != null) {
          if (!rectangle.Value.Contains(screenPoint))
            return null;
        }
      }
      return base.GetNodePathAt(screenPoint);
    }

    /// <summary>
    /// Experimental implementation using <see cref="IAccessBridgeFunctions.GetAccessibleContextAt"/>
    /// </summary>
    private NodePath GetNodePathAtUsingAccessBridge(Point screenPoint) {
      JavaObjectHandle childHandle;
      if (Failed(AccessBridge.Functions.GetAccessibleContextAt(JvmId, _ac, screenPoint.X, screenPoint.Y, out childHandle))) {
        return null;
      }

      if (childHandle.IsNull) {
        return null;
      }

      var childNode = new AccessibleContextNode(AccessBridge, childHandle);
      Debug.WriteLine("Child found: {0}-{1}-{2}-{3}", childNode.GetInfo().x, childNode.GetInfo().y,
        childNode.GetInfo().width, childNode.GetInfo().height);

      var path = new NodePath();
      for (AccessibleNode node = childNode; node != null; node = node.GetParent()) {
        /*DEBUG*/
        if (node is AccessibleContextNode) {
          ((AccessibleContextNode)node).GetInfo();
        }
        path.AddParent(node);
        if (node.Equals(this))
          break;
      }
      return path;
    }

    public override string GetTitle() {
      var info = GetInfo();

      var sb = new StringBuilder();
      // Append role (if exists)
      if (!string.IsNullOrEmpty(info.role)) {
        if (sb.Length > 0)
          sb.Append(" ");
        sb.Append(info.role);
      }

      // Append name or description
      var caption = !string.IsNullOrEmpty(info.name) ? info.name : info.description;
      if (!string.IsNullOrEmpty(caption)) {
        if (sb.Length > 0)
          sb.Append(": ");
        sb.Append(caption);
      }

      // Append ephemeral state (if exists)
      if (_isManagedDescendant) {
        sb.Append("*");
      }

      return sb.ToString();
    }

    protected override void AddProperties(PropertyList list, PropertyOptions options) {
      AddContextProperties(list, options);
      AddTopLevelWindowProperties(list, options);
      AddParentContextProperties(list, options);
      AddActiveDescendentProperties(list, options);
      AddSelectionProperties(list, options);
      AddKeyBindingsProperties(list, options);
      AddIconsProperties(list, options);
      AddActionProperties(list, options);
      AddVisibleDescendentsProperties(list, options);
      AddRelationSetProperties(list, options);
      AddValueProperties(list, options);
      AddTableProperties(list, options);
      AddTextProperties(list, options);
    }

    private void AddTopLevelWindowProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.TopLevelWindowInfo) != 0) {
        var topLevel = AccessBridge.Functions.GetTopLevelObject(JvmId, _ac);
        if (!topLevel.IsNull) {
          var group = list.AddGroup("Top level window");
          group.Expanded = false;
          AddSubContextProperties(group.Children, options, topLevel);
        }
      }
    }

    private void AddParentContextProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.ParentContext) != 0) {
        var parentContext = AccessBridge.Functions.GetAccessibleParentFromContext(JvmId, _ac);
        if (!parentContext.IsNull) {
          var group = list.AddGroup("Parent");
          group.Expanded = false;
          AddSubContextProperties(group.Children, options, parentContext);
        }
      }
    }

    private void AddActiveDescendentProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.ActiveDescendent) != 0) {
        var context = AccessBridge.Functions.GetActiveDescendent(JvmId, _ac);
        if (!context.IsNull) {
          var group = list.AddGroup("Active Descendent");
          group.Expanded = false;
          AddSubContextProperties(group.Children, options, context);
        }
      }
    }

    private void AddSelectionProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleSelection) != 0) {
        var info = GetInfo();
        if (info.accessibleSelection != 0) {
          var selCount = AccessBridge.Functions.GetAccessibleSelectionCountFromContext(JvmId, _ac);
          var group = list.AddGroup("Selections", selCount);
          group.Expanded = false;
          if (selCount > 0) {
            for (var i = 0; i < LimitSize(selCount); i++) {
              var selGroup = group.AddGroup(string.Format("Selection {0} of {1}", i + 1, selCount));
              var selectedContext = AccessBridge.Functions.GetAccessibleSelectionFromContext(JvmId, _ac, i);
              if (selectedContext.IsNull) {
                selGroup.AddProperty(string.Format("Selection {0} of {1}", i + 1, selCount), "<Error>");
              } else {
                AddSubContextProperties(selGroup.Children, options, selectedContext);
              }
            }
          }
        }
      }
    }

    private void AddKeyBindingsProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleKeyBindings) != 0) {
        AccessibleKeyBindings keyBindings;
        if (Succeeded(AccessBridge.Functions.GetAccessibleKeyBindings(JvmId, _ac, out keyBindings))) {
          if (keyBindings.keyBindingsCount > 0) {
            var group = list.AddGroup("Key Bindings", keyBindings.keyBindingsCount);
            group.Expanded = false;
            for (var i = 0; i < LimitSize(keyBindings.keyBindingsCount); i++) {
              var keyGroup = group.AddGroup(string.Format("Key binding {0} of {1}", i + 1, keyBindings.keyBindingsCount));
              keyGroup.AddProperty("Character", keyBindings.keyBindingInfo[i].character);
              keyGroup.AddProperty("Modifiers", keyBindings.keyBindingInfo[i].modifiers);
            }
          }
        }
      }
    }

    private void AddIconsProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleIcons) != 0) {
        AccessibleIcons icons;
        if (Succeeded(AccessBridge.Functions.GetAccessibleIcons(JvmId, _ac, out icons))) {
          if (icons.iconsCount > 0) {
            var group = list.AddGroup("Icons", icons.iconsCount);
            group.Expanded = false;
            for (var i = 0; i < LimitSize(icons.iconsCount); i++) {
              var iconGroup = group.AddGroup(string.Format("Icon {0} of {1}", i + 1, icons.iconsCount));
              iconGroup.AddProperty("Height", icons.iconInfo[i].height);
              iconGroup.AddProperty("Width", icons.iconInfo[i].width);
            }
          }
        }
      }
    }

    private void AddActionProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleActions) != 0) {
        AccessibleActions actions = new AccessibleActions();
        if (Succeeded(AccessBridge.Functions.GetAccessibleActions(JvmId, _ac, actions))) {
          if (actions.actionsCount > 0) {
            var group = list.AddGroup("Actions", actions.actionsCount);
            group.Expanded = false;
            if (actions.actionsCount > 0) {
              for (var i = 0; i < LimitSize(actions.actionsCount); i++) {
                group.AddProperty(
                  string.Format("Action {0} of {1}", i + 1, actions.actionsCount),
                  actions.actionInfo[i].name);
              }
            }
          }
        }
      }
    }

    private void AddVisibleDescendentsProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.VisibleChildren) != 0) {
        var visibleCount = AccessBridge.Functions.GetVisibleChildrenCount(JvmId, _ac);
        if (visibleCount > 0) {
          var group = list.AddGroup("Visible Children", visibleCount);
          group.Expanded = false;
          VisibleChildrenInfo childrenInfo;
          if (Succeeded(AccessBridge.Functions.GetVisibleChildren(JvmId, _ac, 0, out childrenInfo))) {
            var handles = childrenInfo.children.Take(LimitSize(childrenInfo.returnedChildrenCount)).ToList();
            var childNodes = handles.Select(x => new AccessibleContextNode(AccessBridge, x)).ToList();
            var childIndex = 0;
            foreach (var childNode in childNodes) {
              var childGroup = group.AddGroup(string.Format("Child {0} of {1}", childIndex + 1, childNodes.Count));
              AddSubContextProperties(childGroup.Children, options, childNode);
              childIndex++;
            }
          }
        }
      }
    }

    private void AddRelationSetProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleRelationSet) != 0) {
        AccessibleRelationSetInfo relationSetInfo;
        if (Succeeded(AccessBridge.Functions.GetAccessibleRelationSet(JvmId, _ac, out relationSetInfo))) {
          if (relationSetInfo.relationCount > 0) {
            // Note: Create safe handles as soon as possible
            var handles = Enumerable
              .Range(0, relationSetInfo.relationCount)
              .SelectMany(i =>
                Enumerable
                  .Range(0, relationSetInfo.relations[i].targetCount)
                  .Select(j => relationSetInfo.relations[i].targets[j])).ToList();

            var group = list.AddGroup("Relation Set", relationSetInfo.relationCount);
            group.Expanded = false;

            var handleIndex = 0;
            for (var i = 0; i < LimitSize(relationSetInfo.relationCount); i++) {
              var relationInfo = relationSetInfo.relations[i];
              var relGroup = group.AddGroup(string.Format("Relation {0} of {1}", i + 1, relationSetInfo.relationCount));
              relGroup.AddProperty("Key", relationInfo.key);

              var relSubGroup = relGroup.AddGroup("Targets", relationInfo.targetCount);
              for (var j = 0; j < LimitSize(relationInfo.targetCount); j++) {
                var targetGroup = relSubGroup.AddGroup(string.Format("Target {0} of {1}", j + 1, relationInfo.targetCount));
                AddSubContextProperties(targetGroup.Children, options, handles[handleIndex]);
                handleIndex++;
              }
            }
          }
        }
      }
    }

    private void AddValueProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleValue) != 0) {
        var info = GetInfo();
        if ((info.accessibleInterfaces & AccessibleInterfaces.cAccessibleValueInterface) != 0) {
          var group = list.AddGroup("Value");
          group.Expanded = false;

          var sb = new StringBuilder(TextBufferSize);
          if (Succeeded(AccessBridge.Functions.GetCurrentAccessibleValueFromContext(JvmId, _ac, sb, (short)sb.Capacity))) {
            group.AddProperty("Current", sb);
          }
          if (Succeeded(AccessBridge.Functions.GetMaximumAccessibleValueFromContext(JvmId, _ac, sb, (short)sb.Capacity))) {
            group.AddProperty("Maximum", sb);
          }
          if (Succeeded(AccessBridge.Functions.GetMinimumAccessibleValueFromContext(JvmId, _ac, sb, (short)sb.Capacity))) {
            group.AddProperty("Minimum", sb);
          }
        }
      }
    }

    private void AddTableProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleTable) != 0) {
        var nodeInfo = GetInfo();
        if ((nodeInfo.accessibleInterfaces & AccessibleInterfaces.cAccessibleTableInterface) != 0) {
          var group = list.AddGroup("Table");
          group.Expanded = false;
          AccessibleTableInfo tableInfo;
          if (Failed(AccessBridge.Functions.GetAccessibleTableInfo(JvmId, _ac, out tableInfo))) {
            group.AddProperty("Error", "Error retrieving table info");
          } else {
            AddTableInfo(group, tableInfo);

#if false
            int trow = AccessBridge.Functions.GetAccessibleTableRow(JvmId, tableInfo.AccessibleTable, 3);
            appendToBuffer(buffer, bufsize, "\r\n    getAccessibleTableRow:  %d", trow);

            int tcol = getAccessibleTableColumn(vmID, tableInfo.AccessibleTable, 2);
            appendToBuffer(buffer, bufsize, "\r\n    getAccessibleTableColumn:  %d", tcol);

            int tindex = getAccessibleTableIndex(vmID, tableInfo.AccessibleTable, 2, 3);
            appendToBuffer(buffer, bufsize, "\r\n    getAccessibleTableIndex:  %d", tindex);
#endif

            // Get the column headers
            var columnHeaderGroup = group.AddGroup("Column Header");
            columnHeaderGroup.Expanded = false;
            AccessibleTableInfo columnInfo;
            if (Failed(AccessBridge.Functions.GetAccessibleTableColumnHeader(JvmId, _ac, out columnInfo))) {
              columnHeaderGroup.AddProperty("Error", "Error retrieving column header info");
            } else {
              AddTableInfo(columnHeaderGroup, columnInfo);
              var limits = LimitSize(columnInfo.rowCount, columnInfo.columnCount);
              for (var rowIndex = 0; rowIndex < limits.Key; rowIndex++) {
                for (var colunmnIndex = 0; colunmnIndex < limits.Value; colunmnIndex++) {
                  var chGroup = columnHeaderGroup.AddGroup(string.Format("Column Header[Row={0},Column={1}]", rowIndex, colunmnIndex));

                }
              }
            }

            // Get the row headers
            var rowHeaderGroup = group.AddGroup("Row Header");
            rowHeaderGroup.Expanded = false;
            AccessibleTableInfo rowInfo;
            if (Failed(AccessBridge.Functions.GetAccessibleTableRowHeader(JvmId, _ac, out rowInfo))) {
              rowHeaderGroup.AddProperty("Error", "Error retrieving column header info");
            } else {
              AddTableInfo(rowHeaderGroup, rowInfo);
            }

            // Get the selected columns
            var numColSelections = AccessBridge.Functions.GetAccessibleTableColumnSelectionCount(JvmId, tableInfo.accessibleTable);
            var selColGroup = group.AddGroup("Column selections", numColSelections);
            selColGroup.Expanded = false;
            if (numColSelections > 0) {
              var selections = new int[numColSelections];
              if (Failed(AccessBridge.Functions.GetAccessibleTableColumnSelections(JvmId, tableInfo.accessibleTable, numColSelections, selections))) {
                selColGroup.AddProperty("Error", "Error getting column selections");
              } else {
                for (var j = 0; j < LimitSize(numColSelections); j++) {
                  selColGroup.AddProperty(string.Format("Column index {0} of {1}", j + 1, numColSelections), selections[j]);
                }
              }
            }

            // Get the selected rows
            var numRowSelections = AccessBridge.Functions.GetAccessibleTableRowSelectionCount(JvmId, tableInfo.accessibleTable);
            var selRowGroup = group.AddGroup("Row selections", numRowSelections);
            selRowGroup.Expanded = false;
            if (numRowSelections > 0) {
              var selections = new int[numRowSelections];
              if (Failed(AccessBridge.Functions.GetAccessibleTableRowSelections(JvmId, tableInfo.accessibleTable, numRowSelections, selections))) {
                selRowGroup.AddProperty("Error", "Error getting row selections");
              } else {
                for (var j = 0; j < LimitSize(numRowSelections); j++) {
                  selRowGroup.AddProperty(string.Format("Row index {0} of {1}", j + 1, numRowSelections), selections[j]);
                }
              }
            }

            // Get info of all cells
            if ((options & PropertyOptions.AccessibleTableCells) != 0) {
              var cellsGroup = group.AddGroup("Cells");
              cellsGroup.Expanded = false;
              var limits = LimitSize(tableInfo.rowCount, tableInfo.columnCount);
              for (var rowIndex = 0; rowIndex < limits.Key; rowIndex++) {
                for (var colunmnIndex = 0; colunmnIndex < limits.Value; colunmnIndex++) {
                  var cellGroup = cellsGroup.AddGroup(string.Format("Cell[Row={0},Column={1}]", rowIndex, colunmnIndex));

                  AccessibleTableCellInfo tableCellInfo;
                  if (Failed(AccessBridge.Functions.GetAccessibleTableCellInfo(tableInfo.accessibleTable.JvmId,
                      tableInfo.accessibleTable, rowIndex, colunmnIndex, out tableCellInfo))) {
                    cellGroup.AddProperty("Error", "Error retrieving cell info");
                  } else {
                    var cellHandle = tableCellInfo.accessibleContext;
                    cellGroup.AddProperty("Index", tableCellInfo.index);
                    cellGroup.AddProperty("Row extent", tableCellInfo.rowExtent);
                    cellGroup.AddProperty("Column extent", tableCellInfo.columnExtent);
                    cellGroup.AddProperty("Is selected", tableCellInfo.isSelected);

                    AddSubContextProperties(cellGroup.Children, options, cellHandle);
                  }
                }
              }
            }
          }
        }
      }
    }

    private void AddTableInfo(PropertyGroup group, AccessibleTableInfo tableInfo) {
      group.AddProperty("Row count", tableInfo.rowCount);
      group.AddProperty("Column count", tableInfo.columnCount);
    }

    private void AddTextProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleText) != 0) {
        var info = GetInfo();
        if (info.accessibleText != 0) {
          int x = 0;
          int y = 0;

          var group = list.AddGroup("Accessible Text");
          group.Expanded = false;

          AccessibleTextInfo textInfo;
          if (Succeeded(AccessBridge.Functions.GetAccessibleTextInfo(JvmId, _ac, out textInfo, x, y))) {
            group.AddProperty("Mouse point at text index", textInfo.indexAtPoint);
            group.AddProperty("Caret at text index", textInfo.caretIndex);
            group.AddProperty("Char count", textInfo.charCount);
          }

          AccessibleTextSelectionInfo textSelection;
          if (Succeeded(AccessBridge.Functions.GetAccessibleTextSelectionInfo(JvmId, _ac, out textSelection))) {
            group.AddProperty("Selection start index", textSelection.selectionStartIndex);
            group.AddProperty("Selection end index", textSelection.selectionEndIndex);
            group.AddProperty("Selected text", textSelection.selectedText);
          }

          AddHyperTextProperties(group.Children, options);

          /* ===== AccessibleText information at the mouse point ===== */

          var mouseGroup = group.AddGroup(string.Format("Mouse point at index {0} attributes", textInfo.indexAtPoint));
          AddTextAttributeAtIndex(mouseGroup.Children, textInfo.indexAtPoint);

          /* ===== AccessibleText information at the caret index ===== */

          var caretGroup = group.AddGroup(string.Format("Caret at index {0} attributes", textInfo.caretIndex));
          AddTextAttributeAtIndex(caretGroup.Children, textInfo.caretIndex);
        }
      }
    }

    private void AddHyperTextProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.AccessibleText) != 0) {
        var group = list.AddGroup("Accessible Hyper Text");
        group.Expanded = false;

        AccessibleHypertextInfo hyperTextInfo;
#if true
        if (Failed(AccessBridge.Functions.GetAccessibleHypertextExt(JvmId, _ac, 0, out hyperTextInfo))) {
          group.AddProperty("Error", "No hyper text data");
          return;
        }
#else
      // Note: This call does *not* return any value in the "hyperTextInfo.links" array
      if (Failed(AccessBridge.Functions.GetAccessibleHypertext(JvmId, _ac.Handle, out hyperTextInfo))) {
        group.AddProperty("Error", "No hyper text data");
        return;
      }
#endif
        var hyperText = hyperTextInfo.accessibleHypertext;
        var linksGroup = group.AddGroup("Hyperlinks", hyperTextInfo.linkCount);
        for (var i = 0; i < LimitSize(hyperTextInfo.linkCount); i++) {
          var hyperlinkHandle = hyperTextInfo.links[i].accessibleHyperlink;

          var linkGroup = linksGroup.AddGroup(string.Format("Hyperlink #{0}", i + 1));
          var hyperLink = hyperTextInfo.links[i].accessibleHyperlink;
          linkGroup.AddProperty("Start index", hyperTextInfo.links[i].startIndex);
          linkGroup.AddProperty("End index", hyperTextInfo.links[i].endIndex);
          linkGroup.AddProperty("Text", hyperTextInfo.links[i].text);
        }
      }
    }

    private void AddTextAttributeAtIndex(PropertyList list, int index) {
      AccessibleTextRectInfo rectInfo;
      if (Succeeded(AccessBridge.Functions.GetAccessibleTextRect(JvmId, _ac, out rectInfo, index))) {
        list.AddProperty("Character bounding rectangle:", string.Format("[{0},{1},{2},{3}]", rectInfo.x, rectInfo.y, rectInfo.width, rectInfo.height));
      }

      int start;
      int end;
      if (Succeeded(AccessBridge.Functions.GetAccessibleTextLineBounds(JvmId, _ac, index, out start, out end))) {
        list.AddProperty("Line bounds", string.Format("[{0},{1}]", start, end));
        var sb = new StringBuilder(TextBufferSize);
        if (Succeeded(AccessBridge.Functions.GetAccessibleTextRange(JvmId, _ac, start, end, sb, (short)sb.Capacity))) {
          list.AddProperty("Line text", sb);
        }
      }

      AccessibleTextItemsInfo textItems;
      if (Succeeded(AccessBridge.Functions.GetAccessibleTextItems(JvmId, _ac, out textItems, index))) {
        list.AddProperty("Character", textItems.letter);
        list.AddProperty("Word", textItems.word);
        list.AddProperty("Sentence", textItems.sentence);
      }

      /* ===== AccessibleText attributes ===== */

      AccessibleTextAttributesInfo attributeInfo;
      if (Succeeded(AccessBridge.Functions.GetAccessibleTextAttributes(JvmId, _ac, index, out attributeInfo))) {
        list.AddProperty("Core attributes", (attributeInfo.bold != 0 ? "bold" : "not bold") + ", " +
                                            (attributeInfo.italic != 0 ? "italic" : "not italic") + ", " +
                                            (attributeInfo.underline != 0 ? "underline" : "not underline") + ", " +
                                            (attributeInfo.strikethrough != 0 ? "strikethrough" : "not strikethrough") + ", " +
                                            (attributeInfo.superscript != 0 ? "superscript" : "not superscript") + ", " +
                                            (attributeInfo.subscript != 0 ? "subscript" : "not subscript"));

        list.AddProperty("Background color", attributeInfo.backgroundColor);
        list.AddProperty("Foreground color", attributeInfo.foregroundColor);
        list.AddProperty("Font family", attributeInfo.fontFamily);
        list.AddProperty("Font size", attributeInfo.fontSize);
        list.AddProperty("First line indent", attributeInfo.firstLineIndent);
        list.AddProperty("Left indent", attributeInfo.leftIndent);
        list.AddProperty("Right indent", attributeInfo.rightIndent);
        list.AddProperty("Line spacing", attributeInfo.lineSpacing);
        list.AddProperty("Space above", attributeInfo.spaceAbove);
        list.AddProperty("Space below", attributeInfo.spaceBelow);
        list.AddProperty("Full attribute string", attributeInfo.fullAttributesString);

        // get the attribute run length
        short runLength;
        if (Succeeded(AccessBridge.Functions.GetTextAttributesInRange(JvmId, _ac, index, index + 100, out attributeInfo, out runLength))) {
          list.AddProperty("Attribute run", runLength);
        } else {
          list.AddProperty("Attribute run", "<Error>");
        }
      }
    }

    private void AddContextProperties(PropertyList list, PropertyOptions options) {
      if ((options & PropertyOptions.ObjectDepth) != 0) {
        var depth = AccessBridge.Functions.GetObjectDepth(JvmId, _ac);
        list.AddProperty("Object Depth", depth);
      }

      if ((options & PropertyOptions.AccessibleContextInfo) != 0) {
        var info = GetInfo();
        list.AddProperty("Name", info.name ?? "-");
        list.AddProperty("Description", info.description ?? "-");
        list.AddProperty("Name (JAWS algorithm)", GetVirtualAccessibleName());
        list.AddProperty("Bounds", string.Format("[{0}, {1}, {2}, {3}]", info.x, info.y, info.width, info.height));
        list.AddProperty("Role", info.role ?? "-");
        list.AddProperty("Role_en_US", info.role_en_US ?? "-");
        list.AddProperty("States", info.states ?? "-");
        list.AddProperty("States_en_US", info.states_en_US ?? "-");
        list.AddProperty("Children count", info.childrenCount);
        list.AddProperty("Index in parent", info.indexInParent);
        list.AddProperty("AccessibleComponent supported", info.accessibleComponent);
        list.AddProperty("AccessibleAction supported", info.accessibleAction);
        list.AddProperty("AccessibleSelection supported", info.accessibleSelection);
        list.AddProperty("AccessibleText supported", info.accessibleText);
        list.AddProperty("AccessibleInterfaces supported", info.accessibleInterfaces);
      }
    }

    private void AddSubContextProperties(PropertyList list, PropertyOptions options, JavaObjectHandle contextHandle) {
      var contextNode = new AccessibleContextNode(AccessBridge, contextHandle);
      AddSubContextProperties(list, options, contextNode);
    }

    private void AddSubContextProperties(PropertyList list, PropertyOptions options, AccessibleContextNode contextNode) {
      try {
        if ((options & PropertyOptions.ObjectDepth) != 0) {
          var depth = AccessBridge.Functions.GetObjectDepth(contextNode.JvmId, contextNode._ac);
          list.AddProperty("Object Depth", depth);
        }

        var info = contextNode.GetInfo();
        list.AddProperty("Name", info.name);
        list.AddProperty("Description", info.description);
        list.AddProperty("Name (JAWS algorithm)", contextNode.GetVirtualAccessibleName());
        list.AddProperty("Bounds", string.Format("[{0}, {1}, {2}, {3}]", info.x, info.y, info.width, info.height));
        list.AddProperty("Role", info.role);
        list.AddProperty("Index in parent", info.indexInParent);
        list.AddProperty("accessibleInterfaces", info.accessibleInterfaces);

      } catch (Exception e) {
        list.AddProperty("Error", e.Message);
      }
    }

    protected override void AddToolTipProperties(PropertyList list) {
      try {
        var info = GetInfo();
        list.AddProperty("Name", info.name);
        list.AddProperty("Description", info.description);
        list.AddProperty("Name (JAWS algorithm)", GetVirtualAccessibleName());
        list.AddProperty("Bounds", string.Format("[{0}, {1}, {2}, {3}]", info.x, info.y, info.width, info.height));
        list.AddProperty("Role", info.role);
        list.AddProperty("States", info.states);
        list.AddProperty("accessibleInterfaces", info.accessibleInterfaces);
      } catch (Exception e) {
        list.AddProperty("Error", e.Message);
      }
    }

    public override bool Equals(AccessibleNode other) {
      if (!base.Equals(other))
        return false;

      if (!(other is AccessibleContextNode))
        return false;

      return AccessBridge.Functions.IsSameObject(JvmId, _ac, ((AccessibleContextNode)other)._ac);
    }

    /// <summary>
    /// Call the custom function <see
    /// cref="IAccessBridgeFunctions.GetVirtualAccessibleName"/> to retrieve the
    /// spoken name of an accessible component supposedly according to the
    /// algorithm used by the Jaws screen reader.
    /// </summary>
    private string GetVirtualAccessibleName() {
      var sb = new StringBuilder(TextBufferSize);
      if (Failed(AccessBridge.Functions.GetVirtualAccessibleName(JvmId, _ac, sb, sb.Capacity))) {
        return "<Error>";
      }
      return sb.ToString();
    }

    private static bool Failed(bool accessBridgeReturnValue) {
      return accessBridgeReturnValue == false;
    }

    private static bool Succeeded(bool accessBridgeReturnValue) {
      return !Failed(accessBridgeReturnValue);
    }

    public override string ToString() {
      try {
        var info = GetInfo();
        return string.Format("AccessibleContextNode(name={0},role={1},x={2},y={3},w={4},h={5})", info.name ?? " - ", info.role ?? " - ", info.x, info.y, info.width, info.height);
      } catch (Exception e) {
        return string.Format("AccessibleContextNode(Error={0})", e.Message);
      }
    }
    public override int GetIndexInParent() {
      return GetInfo().indexInParent;
    }
  }
}