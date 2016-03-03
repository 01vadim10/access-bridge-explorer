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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AccessBridgeExplorer {
  public partial class ExplorerForm : Form, IExplorerFormView, IMessageQueue {
    /// <summary>
    /// Capture the "Ctrl+\" keyboard combination
    /// Note: OemPipe: Used for miscellaneous characters; it can vary by keyboard.
    /// For the US standard keyboard, the '\|' key
    /// </summary>
    private const Keys CaptureKey = Keys.Control | Keys.OemPipe;
    private readonly WindowsHotKeyHandler _hotKeyHandler;
    private readonly ExplorerFormController _controller;
    private readonly PropertyListView _accessibleComponentPropertyListView;
    private bool _capturing;
    private int _navigationVersion = int.MinValue;

    public ExplorerForm() {
      InitializeComponent();
      mainToolStrip.Renderer = new OverlayButtonRenderer(this);
      _accessibleComponentPropertyListView = new PropertyListView(_accessibleContextPropertyList, _propertyImageList);
      _controller = new ExplorerFormController(this);
      _hotKeyHandler = new WindowsHotKeyHandler();
      _hotKeyHandler.KeyPressed += HotKeyHandlerOnKeyPressed;
      SetDoubleBuffered(_accessibilityTree, true);
      SetDoubleBuffered(_messageList, true);
      SetDoubleBuffered(_accessibleContextPropertyList, true);
      SetDoubleBuffered(_topLevelTabControl, true);
      SetDoubleBuffered(_accessibleComponentTabControl, true);
      SetDoubleBuffered(_bottomTabControl, true);

      overlayEnableButton_Click(_overlayEnableButton, new EventArgs());
    }

    private void MainForm_Shown(object sender, EventArgs e) {
      InvokeLater(() => {
        _controller.LogIntroMessages();
        _controller.Initialize();
        try {
          _hotKeyHandler.Register(this, 1, CaptureKey);
        } catch (Exception ex) {
          _controller.LogErrorMessage(ex);
        }
        Application.Idle += ApplicationOnIdle;
      });
    }

    private void ApplicationOnIdle(object sender, EventArgs eventArgs) {
      UpdateNavigationState();
      UpdateNotificationMenu();
    }

    private void UpdateNotificationMenu() {
      showNotificationMenuItem.Checked = notificationPanel.NotificationShown;
      showNotificationMenuItem.Enabled = notificationPanel.NotificationText.Length > 0;
    }

    private void UpdateNavigationState() {
      if (_navigationVersion == _controller.Navigation.Version)
        return;
      _navigationVersion = _controller.Navigation.Version;

      navigateBackwardButton.Enabled = _controller.Navigation.BackwardAvailable;
      navigateBackwardMenuItem.Enabled = _controller.Navigation.BackwardAvailable;
      AddDropDownEntries(navigateBackwardButton.DropDownItems, _controller.Navigation.BackwardEntries);

      navigateForwardButton.Enabled = _controller.Navigation.ForwardAvailable;
      navigateForwardMenuItem.Enabled = _controller.Navigation.ForwardAvailable;
      AddDropDownEntries(navigateForwardButton.DropDownItems, _controller.Navigation.ForwardEntries);
    }

    private void AddDropDownEntries(ToolStripItemCollection items, IEnumerable<NavigationEntry> entries) {
      items.Clear();
      var index = 0;
      entries.ForEach(entry => {
        if (index++ >= 20)
          return;

        var item = items.Add(entry.Description);
        item.Click += (sender, args) => {
          _controller.Navigation.NavigateTo(entry);
          UpdateNavigationState();
        };
      });
    }

    private void HotKeyHandlerOnKeyPressed(object sender, EventArgs eventArgs) {
      _controller.RefreshTree();

      var screenPoint = MousePosition;
      _controller.SelectNodeAtPoint(screenPoint);
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
      _controller.Dispose();
      Close();
    }

    private void refreshToolStripMenuItem_Click(object sender, EventArgs e) {
      _controller.RefreshTree();
    }

    private void refreshButton_Click(object sender, EventArgs e) {
      _controller.RefreshTree();
    }

    private void overlayEnableButton_Click(object sender, EventArgs e) {
      var button = (ToolStripButton)sender;
      var enable = !button.Checked;
      button.Checked = enable;
      showOverlayMenuItem.Checked = enable;
      if (enable) {
        button.ForeColor = Color.FromArgb(128, 255, 128);
      } else {
        button.ForeColor = SystemColors.InactiveCaption;
      }
      _controller.EnableOverlayWindow(enable);
    }

    private class OverlayButtonRenderer : ToolStripProfessionalRenderer {
      private readonly ExplorerForm _explorerForm;

      public OverlayButtonRenderer(ExplorerForm explorerForm) {
        _explorerForm = explorerForm;
      }

      protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e) {
        base.OnRenderButtonBackground(e);

        if (ReferenceEquals(e.Item, _explorerForm._overlayEnableButton)) {
          var bounds = new Rectangle(Point.Empty, e.Item.Size);
          bounds.Inflate(-1, -1);
          e.Graphics.FillRectangle(new SolidBrush(e.Item.ForeColor), bounds);
        }
      }
    }

    private void showOverlayMenuItem_Click(object sender, EventArgs e) {
      overlayEnableButton_Click(_overlayEnableButton, new EventArgs());
    }

    private void catpureButton_MouseDown(object sender, MouseEventArgs e) {
      _controller.ClearSelectedNode();
      _controller.RefreshTree();
      _capturing = true;
      Cursor = Cursors.Cross;
      Capture = true;
    }

    private void MainForm_MouseCaptureChanged(object sender, EventArgs e) {
      Cursor = Cursors.Default;
      _capturing = false;
      _controller.HideToolTip();
    }

    private void MainForm_MouseMove(object sender, MouseEventArgs e) {
      var screenPoint = PointToScreen(e.Location);
      var nodePath = _controller.GetNodePathAt(screenPoint);
      if (nodePath != null) {
        _controller.ShowToolTip(screenPoint, nodePath);
        _controller.ShowOverlayForNodePath(nodePath);
      } else {
        _controller.HideOverlayWindow();
        _controller.HideToolTip();
      }
    }

    private void MainForm_MouseUp(object sender, MouseEventArgs e) {
      if (!_capturing)
        return;

      var screenPoint = PointToScreen(e.Location);
      _controller.SelectNodeAtPoint(screenPoint);
      Capture = false;
    }

    private void refreshTimer_Tick(object sender, EventArgs e) {
      _controller.RefreshTick();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
      Application.Idle -= ApplicationOnIdle;
      _controller.Dispose();
      _hotKeyHandler.Dispose();
    }

    private void MainForm_Activated(object sender, EventArgs e) {
      _controller.OnFocusGained();
    }

    private void MainForm_Deactivate(object sender, EventArgs e) {
      _controller.OnFocusLost();
    }

    private void clearEventsButton_Click(object sender, EventArgs e) {
      _eventList.Items.Clear();
    }

    private void clearMessagesButton_Click(object sender, EventArgs e) {
      _messageList.Items.Clear();
    }

    private void showHelpButton_Click(object sender, EventArgs e) {
      ShowHelp();
    }

    private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e) {
      ShowHelp();
    }

    private void ShowHelp() {
      _controller.ShowHelp();
    }

    public static void SetDoubleBuffered(Control control, bool enable) {
      var doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
      doubleBufferPropertyInfo.SetValue(control, enable, null);
    }

    public void Invoke(Action action) {
      base.Invoke(action);
    }

    public void InvokeLater(Action action) {
      BeginInvoke(action);
    }

    public T Compute<T>(Func<T> function) {
      return (T)base.Invoke(function);
    }

    private void navigateForwardButton_Click(object sender, EventArgs e) {
      _controller.Navigation.NavigateForward();
      UpdateNavigationState();
    }

    private void navigateForwardToolStripMenuItem_Click(object sender, EventArgs e) {
      _controller.Navigation.NavigateForward();
      UpdateNavigationState();
    }

    private void navigateBackwardButton_Click(object sender, EventArgs e) {
      _controller.Navigation.NavigateBackward();
      UpdateNavigationState();
    }

    private void navigateBackwardToolStripMenuItem_Click(object sender, EventArgs e) {
      _controller.Navigation.NavigateBackward();
      UpdateNavigationState();
    }

    #region IExplorerFormView

    string IExplorerFormView.Caption {
      get { return Text; }
    }

    IMessageQueue IExplorerFormView.MessageQueue {
      get { return this; }
    }

    ToolStripButton IExplorerFormView.RefreshButton {
      get { return _refreshButton; }
    }

    ToolStripButton IExplorerFormView.FindComponentButton {
      get { return _findComponentButton; }
    }

    TabPage IExplorerFormView.AccessibilityTreePage {
      get { return _accessibilityTreePage; }
    }

    TreeView IExplorerFormView.AccessibilityTree {
      get { return _accessibilityTree; }
    }

    PropertyListView IExplorerFormView.AccessibleComponentPropertyList {
      get { return _accessibleComponentPropertyListView; }
    }

    TabPage IExplorerFormView.MessageListPage {
      get { return _messageListPage; }
    }

    ListView IExplorerFormView.MessageList {
      get { return _messageList; }
    }

    TabPage IExplorerFormView.EventListPage {
      get { return _eventListPage; }
    }

    ListView IExplorerFormView.EventList {
      get { return _eventList; }
    }

    ToolStripMenuItem IExplorerFormView.PropertiesMenu {
      get { return _propertiesMenu; }
    }

    ToolStripMenuItem IExplorerFormView.EventsMenu {
      get { return _eventsMenu; }
    }

    ToolStripStatusLabel IExplorerFormView.StatusLabel {
      get { return _statusLabel; }
    }

    void IExplorerFormView.ShowMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon) {
      MessageBox.Show(this, message, title, buttons, icon);
    }

    void IExplorerFormView.ShowDialog(Form form) {
      form.ShowDialog(this);
    }

    void IExplorerFormView.FocusMessageList() {
      _bottomTabControl.SelectedTab = _messageListPage;
      _messageList.Focus();
    }

    #endregion

    private void aboutMenuItem_Click(object sender, EventArgs e) {
      _controller.ShowAbout();
    }

    private void showNotificationMenuItem_Click(object sender, EventArgs e) {
      if (showNotificationMenuItem.Checked) {
        notificationPanel.HideNotification();
        showNotificationMenuItem.Checked = false;
      } else {
        notificationPanel.ShowNotification();
        showNotificationMenuItem.Checked = true;
      }
    }
  }
}
