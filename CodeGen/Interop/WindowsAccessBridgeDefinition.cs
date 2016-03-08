﻿// Copyright 2016 Google Inc. All Rights Reserved.
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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using CodeGen.Interop.NativeStructures;

namespace CodeGen.Interop {
  /// <summary>
  /// This interface defintion serves as the source defintion of the WindowsAccessBridge
  /// library definitions. It is used by the code generator to generate vaious interop
  /// classes, interfaces and utility methods.
  /// </summary>
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  public interface WindowsAccessBridgeDefinition {
    void Windows_run();

    bool IsJavaWindow(WindowHandle window);
    bool IsSameObject(int vmID, JavaObjectHandle obj1, JavaObjectHandle obj2);
    bool GetAccessibleContextFromHWND(WindowHandle window, out int vmID, out JavaObjectHandle ac);
    WindowHandle GetHWNDFromAccessibleContext(int vmID, JavaObjectHandle ac);

    /// <summary>
    /// Returns the deepest accessible context at screen location (x,y) using
    /// <paramref name="acParent"/> as the root of the search tree. The returned
    /// AccessibleContext <paramref name="ac"/> maybe be equal to <paramref
    /// name="acParent"/> in case there is no child at that location. Returns
    /// <code>false</code> in case of serious error.
    /// </summary>
    bool GetAccessibleContextAt(int vmID, JavaObjectHandle acParent, int x, int y, out JavaObjectHandle ac);

    bool GetAccessibleContextWithFocus(WindowHandle window, out int vmID, out JavaObjectHandle ac);
    bool GetAccessibleContextInfo(int vmID, JavaObjectHandle ac, out AccessibleContextInfo info);
    JavaObjectHandle GetAccessibleChildFromContext(int vmID, JavaObjectHandle ac, int i);
    JavaObjectHandle GetAccessibleParentFromContext(int vmID, JavaObjectHandle ac);

    #region AccessibleRelationSet

    bool GetAccessibleRelationSet(
      int vmID,
      JavaObjectHandle accessibleContext,
      out AccessibleRelationSetInfo relationSetInfo);

    #endregion

    #region AccessibleHypertext

    bool GetAccessibleHypertext(int vmID, JavaObjectHandle accessibleContext, out AccessibleHypertextInfo hypertextInfo);
    bool ActivateAccessibleHyperlink(int vmID, JavaObjectHandle accessibleContext, JavaObjectHandle accessibleHyperlink);
    int GetAccessibleHyperlinkCount(int vmID, JavaObjectHandle accessibleContext);

    bool GetAccessibleHypertextExt(
      int vmID,
      JavaObjectHandle accessibleContext,
      int nStartIndex,
      out AccessibleHypertextInfo hypertextInfo);

    int GetAccessibleHypertextLinkIndex(int vmID, JavaObjectHandle hypertext, int nIndex);

    bool GetAccessibleHyperlink(
      int vmID,
      JavaObjectHandle hypertext,
      int nIndex,
      out AccessibleHyperlinkInfo hyperlinkInfo);

    #endregion

    #region Accessible KeyBindings, Icons and Actions

    bool GetAccessibleKeyBindings(int vmID, JavaObjectHandle accessibleContext, out AccessibleKeyBindings keyBindings);
    bool GetAccessibleIcons(int vmID, JavaObjectHandle accessibleContext, out AccessibleIcons icons);
    bool GetAccessibleActions(int vmID, JavaObjectHandle accessibleContext, [Out] AccessibleActions actions);

    bool DoAccessibleActions(
      int vmID,
      JavaObjectHandle accessibleContext,
      ref AccessibleActionsToDo actionsToDo,
      out int failure);

    #endregion

    #region AccessibleText

    bool GetAccessibleTextInfo(int vmID, JavaObjectHandle at, out AccessibleTextInfo textInfo, int x, int y);
    bool GetAccessibleTextItems(int vmID, JavaObjectHandle at, out AccessibleTextItemsInfo textItems, int index);
    bool GetAccessibleTextSelectionInfo(int vmID, JavaObjectHandle at, out AccessibleTextSelectionInfo textSelection);

    bool GetAccessibleTextAttributes(
      int vmID,
      JavaObjectHandle at,
      int index,
      out AccessibleTextAttributesInfo attributes);

    bool GetAccessibleTextRect(int vmID, JavaObjectHandle at, out AccessibleTextRectInfo rectInfo, int index);
    bool GetAccessibleTextLineBounds(int vmID, JavaObjectHandle at, int index, out int startIndex, out int endIndex);
    bool GetAccessibleTextRange(int vmID, JavaObjectHandle at, int start, int end, StringBuilder text, short len);

    #endregion

    bool GetCurrentAccessibleValueFromContext(int vmID, JavaObjectHandle av, StringBuilder value, short len);
    bool GetMaximumAccessibleValueFromContext(int vmID, JavaObjectHandle av, StringBuilder value, short len);
    bool GetMinimumAccessibleValueFromContext(int vmID, JavaObjectHandle av, StringBuilder value, short len);

    void AddAccessibleSelectionFromContext(int vmID, JavaObjectHandle asel, int i);
    void ClearAccessibleSelectionFromContext(int vmID, JavaObjectHandle asel);
    JavaObjectHandle GetAccessibleSelectionFromContext(int vmID, JavaObjectHandle asel, int i);
    int GetAccessibleSelectionCountFromContext(int vmID, JavaObjectHandle asel);
    bool IsAccessibleChildSelectedFromContext(int vmID, JavaObjectHandle asel, int i);
    void RemoveAccessibleSelectionFromContext(int vmID, JavaObjectHandle asel, int i);
    void SelectAllAccessibleSelectionFromContext(int vmID, JavaObjectHandle asel);

    #region AccessibleTable

    bool GetAccessibleTableInfo(int vmID, JavaObjectHandle ac, out AccessibleTableInfo tableInfo);

    bool GetAccessibleTableCellInfo(
      int vmID,
      JavaObjectHandle accessibleTable,
      int row,
      int column,
      out AccessibleTableCellInfo tableCellInfo);

    bool GetAccessibleTableRowHeader(int vmID, JavaObjectHandle acParent, out AccessibleTableInfo tableInfo);
    bool GetAccessibleTableColumnHeader(int vmID, JavaObjectHandle acParent, out AccessibleTableInfo tableInfo);
    JavaObjectHandle GetAccessibleTableRowDescription(int vmID, JavaObjectHandle acParent, int row);
    JavaObjectHandle GetAccessibleTableColumnDescription(int vmID, JavaObjectHandle acParent, int column);
    int GetAccessibleTableRowSelectionCount(int vmID, JavaObjectHandle table);
    bool IsAccessibleTableRowSelected(int vmID, JavaObjectHandle table, int row);

    bool GetAccessibleTableRowSelections(
      int vmID,
      JavaObjectHandle table,
      int count,
      [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[] selections);

    int GetAccessibleTableColumnSelectionCount(int vmID, JavaObjectHandle table);
    bool IsAccessibleTableColumnSelected(int vmID, JavaObjectHandle table, int column);

    bool GetAccessibleTableColumnSelections(
      int vmID,
      JavaObjectHandle table,
      int count,
      [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[] selections);

    /// <summary>
    /// Return the row number for a cell at a given index
    /// </summary>
    int GetAccessibleTableRow(int vmID, JavaObjectHandle table, int index);

    /// <summary>
    /// Return the column number for a cell at a given index
    /// </summary>
    int GetAccessibleTableColumn(int vmID, JavaObjectHandle table, int index);

    /// <summary>
    /// Return the index of a cell at a given row and column
    /// </summary>
    int GetAccessibleTableIndex(int vmID, JavaObjectHandle table, int row, int column);

    #endregion

    #region Additional utility methods

    bool SetTextContents(int vmID, JavaObjectHandle ac, string text);
    JavaObjectHandle GetParentWithRole(int vmID, JavaObjectHandle ac, [MarshalAs(UnmanagedType.LPWStr)] string role);

    JavaObjectHandle GetParentWithRoleElseRoot(
      int vmID,
      JavaObjectHandle ac,
      [MarshalAs(UnmanagedType.LPWStr)] string role);

    JavaObjectHandle GetTopLevelObject(int vmID, JavaObjectHandle ac);
    int GetObjectDepth(int vmID, JavaObjectHandle ac);
    JavaObjectHandle GetActiveDescendent(int vmID, JavaObjectHandle ac);

    bool GetVirtualAccessibleName(int vmID, JavaObjectHandle ac, StringBuilder name, int len);

    bool GetTextAttributesInRange(
      int vmID,
      JavaObjectHandle accessibleContext,
      int startIndex,
      int endIndex,
      out AccessibleTextAttributesInfo attributes,
      out short len);

    bool GetCaretLocation(int vmID, JavaObjectHandle ac, out AccessibleTextRectInfo rectInfo, int index);

    int GetVisibleChildrenCount(int vmID, JavaObjectHandle accessibleContext);

    bool GetVisibleChildren(
      int vmID,
      JavaObjectHandle accessibleContext,
      int startIndex,
      out VisibleChildrenInfo children);

    #endregion

    bool GetVersionInfo(int vmID, out AccessBridgeVersionInfo info);

    #region Event handling routines

    event PropertyChangeEventHandler PropertyChange;

    event JavaShutdownEventHandler JavaShutdown;

    event FocusGainedEventHandler FocusGained;
    event FocusLostEventHandler FocusLost;

    event CaretUpdateEventHandler CaretUpdate;

    event MouseClickedEventHandler MouseClicked;
    event MouseEnteredEventHandler MouseEntered;
    event MouseExitedEventHandler MouseExited;
    event MousePressedEventHandler MousePressed;
    event MouseReleasedEventHandler MouseReleased;

    event MenuCanceledEventHandler MenuCanceled;
    event MenuDeselectedEventHandler MenuDeselected;
    event MenuSelectedEventHandler MenuSelected;
    event PopupMenuCanceledEventHandler PopupMenuCanceled;
    event PopupMenuWillBecomeInvisibleEventHandler PopupMenuWillBecomeInvisible;
    event PopupMenuWillBecomeVisibleEventHandler PopupMenuWillBecomeVisible;

    event PropertyNameChangeEventHandler PropertyNameChange;
    event PropertyDescriptionChangeEventHandler PropertyDescriptionChange;
    event PropertyStateChangeEventHandler PropertyStateChange;
    event PropertyValueChangeEventHandler PropertyValueChange;
    event PropertySelectionChangeEventHandler PropertySelectionChange;
    event PropertyTextChangeEventHandler PropertyTextChange;
    event PropertyCaretChangeEventHandler PropertyCaretChange;
    event PropertyVisibleDataChangeEventHandler PropertyVisibleDataChange;
    event PropertyChildChangeEventHandler PropertyChildChange;
    event PropertyActiveDescendentChangeEventHandler PropertyActiveDescendentChange;

    event PropertyTableModelChangeEventHandler PropertyTableModelChange;

    #endregion
  }

  #region Managed Event Handlers Delegate Definitions

  public delegate void PropertyChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    [MarshalAs(UnmanagedType.LPWStr)] string property,
    [MarshalAs(UnmanagedType.LPWStr)] string oldValue,
    [MarshalAs(UnmanagedType.LPWStr)] string newValue);

  public delegate void JavaShutdownEventHandler(int vmid);

  public delegate void FocusGainedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void FocusLostEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void CaretUpdateEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void MouseClickedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void MouseEnteredEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void MouseExitedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void MousePressedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void MouseReleasedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void MenuCanceledEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void MenuDeselectedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void MenuSelectedEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void PopupMenuCanceledEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void PopupMenuWillBecomeInvisibleEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);
  public delegate void PopupMenuWillBecomeVisibleEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void PropertyNameChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    [MarshalAs(UnmanagedType.LPWStr)] string oldName,
    [MarshalAs(UnmanagedType.LPWStr)] string newName);

  public delegate void PropertyDescriptionChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    [MarshalAs(UnmanagedType.LPWStr)] string oldDescription,
    [MarshalAs(UnmanagedType.LPWStr)] string newDescription);

  public delegate void PropertyStateChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    [MarshalAs(UnmanagedType.LPWStr)] string oldState,
    [MarshalAs(UnmanagedType.LPWStr)] string newState);

  public delegate void PropertyValueChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    [MarshalAs(UnmanagedType.LPWStr)] string oldValue,
    [MarshalAs(UnmanagedType.LPWStr)] string newValue);

  public delegate void PropertySelectionChangeEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void PropertyTextChangeEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void PropertyCaretChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    int oldPosition,
    int newPosition);

  public delegate void PropertyVisibleDataChangeEventHandler(int vmid, JavaObjectHandle evt, JavaObjectHandle source);

  public delegate void PropertyChildChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    JavaObjectHandle oldChild,
    JavaObjectHandle newChild);

  public delegate void PropertyActiveDescendentChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle source,
    JavaObjectHandle oldActiveDescendent,
    JavaObjectHandle newActiveDescendent);

  public delegate void PropertyTableModelChangeEventHandler(
    int vmid,
    JavaObjectHandle evt,
    JavaObjectHandle src,
    [MarshalAs(UnmanagedType.LPWStr)] string oldValue,
    [MarshalAs(UnmanagedType.LPWStr)] string newValue);
  #endregion
}