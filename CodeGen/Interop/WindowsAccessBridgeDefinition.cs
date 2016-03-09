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
    bool IsSameObject(int vmid, JavaObjectHandle obj1, JavaObjectHandle obj2);
    bool GetAccessibleContextFromHWND(WindowHandle window, out int vmid, out JavaObjectHandle ac);
    WindowHandle GetHWNDFromAccessibleContext(int vmid, JavaObjectHandle ac);

    /// <summary>
    /// Returns the deepest accessible context at screen location (x,y) using
    /// <paramref name="acParent"/> as the root of the search tree. The returned
    /// AccessibleContext <paramref name="ac"/> maybe be equal to <paramref
    /// name="acParent"/> in case there is no child at that location. Returns
    /// <code>false</code> in case of serious error.
    /// </summary>
    bool GetAccessibleContextAt(int vmid, JavaObjectHandle acParent, int x, int y, out JavaObjectHandle ac);

    bool GetAccessibleContextWithFocus(WindowHandle window, out int vmid, out JavaObjectHandle ac);
    bool GetAccessibleContextInfo(int vmid, JavaObjectHandle ac, out AccessibleContextInfo info);
    JavaObjectHandle GetAccessibleChildFromContext(int vmid, JavaObjectHandle ac, int i);
    JavaObjectHandle GetAccessibleParentFromContext(int vmid, JavaObjectHandle ac);

    #region AccessibleRelationSet

    bool GetAccessibleRelationSet(
      int vmid,
      JavaObjectHandle accessibleContext,
      out AccessibleRelationSetInfo relationSetInfo);

    #endregion

    #region AccessibleHypertext

    bool GetAccessibleHypertext(int vmid, JavaObjectHandle accessibleContext, out AccessibleHypertextInfo hypertextInfo);
    bool ActivateAccessibleHyperlink(int vmid, JavaObjectHandle accessibleContext, JavaObjectHandle accessibleHyperlink);
    int GetAccessibleHyperlinkCount(int vmid, JavaObjectHandle accessibleContext);

    bool GetAccessibleHypertextExt(
      int vmid,
      JavaObjectHandle accessibleContext,
      int nStartIndex,
      out AccessibleHypertextInfo hypertextInfo);

    int GetAccessibleHypertextLinkIndex(int vmid, JavaObjectHandle hypertext, int nIndex);

    bool GetAccessibleHyperlink(
      int vmid,
      JavaObjectHandle hypertext,
      int nIndex,
      out AccessibleHyperlinkInfo hyperlinkInfo);

    #endregion

    #region Accessible KeyBindings, Icons and Actions

    bool GetAccessibleKeyBindings(int vmid, JavaObjectHandle accessibleContext, out AccessibleKeyBindings keyBindings);
    bool GetAccessibleIcons(int vmid, JavaObjectHandle accessibleContext, out AccessibleIcons icons);
    bool GetAccessibleActions(int vmid, JavaObjectHandle accessibleContext, [Out] AccessibleActions actions);

    bool DoAccessibleActions(
      int vmid,
      JavaObjectHandle accessibleContext,
      ref AccessibleActionsToDo actionsToDo,
      out int failure);

    #endregion

    #region AccessibleText

    bool GetAccessibleTextInfo(int vmid, JavaObjectHandle at, out AccessibleTextInfo textInfo, int x, int y);
    bool GetAccessibleTextItems(int vmid, JavaObjectHandle at, out AccessibleTextItemsInfo textItems, int index);
    bool GetAccessibleTextSelectionInfo(int vmid, JavaObjectHandle at, out AccessibleTextSelectionInfo textSelection);

    bool GetAccessibleTextAttributes(
      int vmid,
      JavaObjectHandle at,
      int index,
      out AccessibleTextAttributesInfo attributes);

    bool GetAccessibleTextRect(int vmid, JavaObjectHandle at, out AccessibleTextRectInfo rectInfo, int index);
    bool GetAccessibleTextLineBounds(int vmid, JavaObjectHandle at, int index, out int startIndex, out int endIndex);
    bool GetAccessibleTextRange(int vmid, JavaObjectHandle at, int start, int end, StringBuilder text, short len);

    #endregion

    bool GetCurrentAccessibleValueFromContext(int vmid, JavaObjectHandle av, StringBuilder value, short len);
    bool GetMaximumAccessibleValueFromContext(int vmid, JavaObjectHandle av, StringBuilder value, short len);
    bool GetMinimumAccessibleValueFromContext(int vmid, JavaObjectHandle av, StringBuilder value, short len);

    void AddAccessibleSelectionFromContext(int vmid, JavaObjectHandle asel, int i);
    void ClearAccessibleSelectionFromContext(int vmid, JavaObjectHandle asel);
    JavaObjectHandle GetAccessibleSelectionFromContext(int vmid, JavaObjectHandle asel, int i);
    int GetAccessibleSelectionCountFromContext(int vmid, JavaObjectHandle asel);
    bool IsAccessibleChildSelectedFromContext(int vmid, JavaObjectHandle asel, int i);
    void RemoveAccessibleSelectionFromContext(int vmid, JavaObjectHandle asel, int i);
    void SelectAllAccessibleSelectionFromContext(int vmid, JavaObjectHandle asel);

    #region AccessibleTable

    bool GetAccessibleTableInfo(int vmid, JavaObjectHandle ac, out AccessibleTableInfo tableInfo);

    bool GetAccessibleTableCellInfo(
      int vmid,
      JavaObjectHandle accessibleTable,
      int row,
      int column,
      out AccessibleTableCellInfo tableCellInfo);

    bool GetAccessibleTableRowHeader(int vmid, JavaObjectHandle acParent, out AccessibleTableInfo tableInfo);
    bool GetAccessibleTableColumnHeader(int vmid, JavaObjectHandle acParent, out AccessibleTableInfo tableInfo);
    JavaObjectHandle GetAccessibleTableRowDescription(int vmid, JavaObjectHandle acParent, int row);
    JavaObjectHandle GetAccessibleTableColumnDescription(int vmid, JavaObjectHandle acParent, int column);
    int GetAccessibleTableRowSelectionCount(int vmid, JavaObjectHandle table);
    bool IsAccessibleTableRowSelected(int vmid, JavaObjectHandle table, int row);

    bool GetAccessibleTableRowSelections(
      int vmid,
      JavaObjectHandle table,
      int count,
      [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[] selections);

    int GetAccessibleTableColumnSelectionCount(int vmid, JavaObjectHandle table);
    bool IsAccessibleTableColumnSelected(int vmid, JavaObjectHandle table, int column);

    bool GetAccessibleTableColumnSelections(
      int vmid,
      JavaObjectHandle table,
      int count,
      [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] int[] selections);

    /// <summary>
    /// Return the row number for a cell at a given index
    /// </summary>
    int GetAccessibleTableRow(int vmid, JavaObjectHandle table, int index);

    /// <summary>
    /// Return the column number for a cell at a given index
    /// </summary>
    int GetAccessibleTableColumn(int vmid, JavaObjectHandle table, int index);

    /// <summary>
    /// Return the index of a cell at a given row and column
    /// </summary>
    int GetAccessibleTableIndex(int vmid, JavaObjectHandle table, int row, int column);

    #endregion

    #region Additional utility methods

    bool SetTextContents(int vmid, JavaObjectHandle ac, string text);
    JavaObjectHandle GetParentWithRole(int vmid, JavaObjectHandle ac, [MarshalAs(UnmanagedType.LPWStr)] string role);

    JavaObjectHandle GetParentWithRoleElseRoot(
      int vmid,
      JavaObjectHandle ac,
      [MarshalAs(UnmanagedType.LPWStr)] string role);

    JavaObjectHandle GetTopLevelObject(int vmid, JavaObjectHandle ac);
    int GetObjectDepth(int vmid, JavaObjectHandle ac);
    JavaObjectHandle GetActiveDescendent(int vmid, JavaObjectHandle ac);

    bool GetVirtualAccessibleName(int vmid, JavaObjectHandle ac, StringBuilder name, int len);

    bool GetTextAttributesInRange(
      int vmid,
      JavaObjectHandle accessibleContext,
      int startIndex,
      int endIndex,
      out AccessibleTextAttributesInfo attributes,
      out short len);

    bool GetCaretLocation(int vmid, JavaObjectHandle ac, out AccessibleTextRectInfo rectInfo, int index);

    int GetVisibleChildrenCount(int vmid, JavaObjectHandle accessibleContext);

    bool GetVisibleChildren(
      int vmid,
      JavaObjectHandle accessibleContext,
      int startIndex,
      out VisibleChildrenInfo children);

    #endregion

    bool GetVersionInfo(int vmid, out AccessBridgeVersionInfo info);

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