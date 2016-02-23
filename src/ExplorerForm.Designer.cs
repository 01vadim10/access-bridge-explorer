﻿namespace AccessBridgeExplorer {
  partial class ExplorerForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerForm));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.navigateForwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.navigateBackwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.eventsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusBarStrip = new System.Windows.Forms.StatusStrip();
      this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.mainToolStrip = new System.Windows.Forms.ToolStrip();
      this.navigateBackwardButton = new System.Windows.Forms.ToolStripButton();
      this.navigateForwardButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.refreshButton = new System.Windows.Forms.ToolStripButton();
      this.findComponentButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.overlayEnableButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.showHelpButton = new System.Windows.Forms.ToolStripButton();
      this.refreshTimer = new System.Windows.Forms.Timer(this.components);
      this.topSplitContainer = new System.Windows.Forms.SplitContainer();
      this.topLevelTabControl = new System.Windows.Forms.TabControl();
      this.accessibilityTreePage = new System.Windows.Forms.TabPage();
      this.accessibilityTree = new System.Windows.Forms.TreeView();
      this.accessibleComponentTabControl = new System.Windows.Forms.TabControl();
      this.accessibleComponentTabPage = new System.Windows.Forms.TabPage();
      this.accessibleContextPropertyList = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.propertyImageList = new System.Windows.Forms.ImageList(this.components);
      this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
      this.bottomTabControl = new System.Windows.Forms.TabControl();
      this.messagesPage = new System.Windows.Forms.TabPage();
      this.messageList = new System.Windows.Forms.ListView();
      this.messageIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.messageTimeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.messageTextColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.messagesToolStrip = new System.Windows.Forms.ToolStrip();
      this.clearMessagesButton = new System.Windows.Forms.ToolStripButton();
      this.eventsPage = new System.Windows.Forms.TabPage();
      this.eventList = new System.Windows.Forms.ListView();
      this.eventId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventJvmId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventOldValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventNewValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.eventsToolStrip = new System.Windows.Forms.ToolStrip();
      this.clearEventsButton = new System.Windows.Forms.ToolStripButton();
      this.menuStrip1.SuspendLayout();
      this.statusBarStrip.SuspendLayout();
      this.mainToolStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).BeginInit();
      this.topSplitContainer.Panel1.SuspendLayout();
      this.topSplitContainer.Panel2.SuspendLayout();
      this.topSplitContainer.SuspendLayout();
      this.topLevelTabControl.SuspendLayout();
      this.accessibilityTreePage.SuspendLayout();
      this.accessibleComponentTabControl.SuspendLayout();
      this.accessibleComponentTabPage.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
      this.mainSplitContainer.Panel1.SuspendLayout();
      this.mainSplitContainer.Panel2.SuspendLayout();
      this.mainSplitContainer.SuspendLayout();
      this.bottomTabControl.SuspendLayout();
      this.messagesPage.SuspendLayout();
      this.messagesToolStrip.SuspendLayout();
      this.eventsPage.SuspendLayout();
      this.eventsToolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.eventsMenu,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(1181, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // viewToolStripMenuItem
      // 
      this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigateForwardToolStripMenuItem,
            this.navigateBackwardToolStripMenuItem,
            this.toolStripMenuItem2,
            this.refreshToolStripMenuItem});
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      // 
      // navigateForwardToolStripMenuItem
      // 
      this.navigateForwardToolStripMenuItem.Name = "navigateForwardToolStripMenuItem";
      this.navigateForwardToolStripMenuItem.ShortcutKeyDisplayString = "";
      this.navigateForwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Right)));
      this.navigateForwardToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
      this.navigateForwardToolStripMenuItem.Text = "Navigate Forward";
      this.navigateForwardToolStripMenuItem.Click += new System.EventHandler(this.navigateForwardToolStripMenuItem_Click);
      // 
      // navigateBackwardToolStripMenuItem
      // 
      this.navigateBackwardToolStripMenuItem.Name = "navigateBackwardToolStripMenuItem";
      this.navigateBackwardToolStripMenuItem.ShortcutKeyDisplayString = "";
      this.navigateBackwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Left)));
      this.navigateBackwardToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
      this.navigateBackwardToolStripMenuItem.Text = "Navigate Backward";
      this.navigateBackwardToolStripMenuItem.Click += new System.EventHandler(this.navigateBackwardToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(222, 6);
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
      this.refreshToolStripMenuItem.Text = "Refresh";
      this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
      // 
      // optionsToolStripMenuItem
      // 
      this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
      this.optionsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
      this.optionsToolStripMenuItem.Text = "Properties";
      // 
      // eventsMenu
      // 
      this.eventsMenu.Name = "eventsMenu";
      this.eventsMenu.Size = new System.Drawing.Size(53, 20);
      this.eventsMenu.Text = "&Events";
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      // 
      // viewHelpToolStripMenuItem
      // 
      this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
      this.viewHelpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
      this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
      this.viewHelpToolStripMenuItem.Text = "View Help";
      this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
      // 
      // statusBarStrip
      // 
      this.statusBarStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.statusBarStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
      this.statusBarStrip.Location = new System.Drawing.Point(0, 682);
      this.statusBarStrip.Name = "statusBarStrip";
      this.statusBarStrip.Padding = new System.Windows.Forms.Padding(1, 0, 17, 0);
      this.statusBarStrip.Size = new System.Drawing.Size(1181, 22);
      this.statusBarStrip.TabIndex = 1;
      this.statusBarStrip.Text = "statusStrip1";
      // 
      // statusLabel
      // 
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(42, 17);
      this.statusLabel.Text = "Ready.";
      // 
      // mainToolStrip
      // 
      this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.navigateBackwardButton,
            this.navigateForwardButton,
            this.toolStripSeparator3,
            this.refreshButton,
            this.findComponentButton,
            this.toolStripSeparator2,
            this.overlayEnableButton,
            this.toolStripSeparator1,
            this.showHelpButton});
      this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
      this.mainToolStrip.Name = "mainToolStrip";
      this.mainToolStrip.Size = new System.Drawing.Size(1181, 27);
      this.mainToolStrip.TabIndex = 3;
      this.mainToolStrip.Text = "toolStrip1";
      // 
      // navigateBackwardButton
      // 
      this.navigateBackwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.navigateBackwardButton.Image = ((System.Drawing.Image)(resources.GetObject("navigateBackwardButton.Image")));
      this.navigateBackwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.navigateBackwardButton.Name = "navigateBackwardButton";
      this.navigateBackwardButton.Size = new System.Drawing.Size(24, 24);
      this.navigateBackwardButton.Text = "Navigate Backward";
      this.navigateBackwardButton.ToolTipText = "Navigate Backward";
      this.navigateBackwardButton.Click += new System.EventHandler(this.navigateBackwardButton_Click);
      // 
      // navigateForwardButton
      // 
      this.navigateForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.navigateForwardButton.Image = ((System.Drawing.Image)(resources.GetObject("navigateForwardButton.Image")));
      this.navigateForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.navigateForwardButton.Name = "navigateForwardButton";
      this.navigateForwardButton.Size = new System.Drawing.Size(24, 24);
      this.navigateForwardButton.Text = "Nagivate Forward";
      this.navigateForwardButton.ToolTipText = "Nagivate Forward";
      this.navigateForwardButton.Click += new System.EventHandler(this.navigateForwardButton_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
      // 
      // refreshButton
      // 
      this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
      this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(24, 24);
      this.refreshButton.Text = "Refresh";
      this.refreshButton.ToolTipText = "Refresh the list of accessible windows";
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // findComponentButton
      // 
      this.findComponentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.findComponentButton.Image = global::AccessBridgeExplorer.Properties.Resources.Crosshair;
      this.findComponentButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.findComponentButton.Name = "findComponentButton";
      this.findComponentButton.Size = new System.Drawing.Size(24, 24);
      this.findComponentButton.Text = "Find Component";
      this.findComponentButton.ToolTipText = "Find accessibility elements using the mouse pointer";
      this.findComponentButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.catpureButton_MouseDown);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
      // 
      // overlayEnableButton
      // 
      this.overlayEnableButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.overlayEnableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
      this.overlayEnableButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
      this.overlayEnableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.overlayEnableButton.Name = "overlayEnableButton";
      this.overlayEnableButton.Size = new System.Drawing.Size(23, 24);
      this.overlayEnableButton.Text = "Overlay";
      this.overlayEnableButton.ToolTipText = "Enable/Disable the accessibility window overlay";
      this.overlayEnableButton.Click += new System.EventHandler(this.overlayEnableButton_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
      // 
      // showHelpButton
      // 
      this.showHelpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.showHelpButton.Image = ((System.Drawing.Image)(resources.GetObject("showHelpButton.Image")));
      this.showHelpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.showHelpButton.Name = "showHelpButton";
      this.showHelpButton.Size = new System.Drawing.Size(24, 24);
      this.showHelpButton.Text = "Help";
      this.showHelpButton.Click += new System.EventHandler(this.showHelpButton_Click);
      // 
      // refreshTimer
      // 
      this.refreshTimer.Enabled = true;
      this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
      // 
      // topSplitContainer
      // 
      this.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.topSplitContainer.Location = new System.Drawing.Point(0, 0);
      this.topSplitContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.topSplitContainer.Name = "topSplitContainer";
      // 
      // topSplitContainer.Panel1
      // 
      this.topSplitContainer.Panel1.Controls.Add(this.topLevelTabControl);
      // 
      // topSplitContainer.Panel2
      // 
      this.topSplitContainer.Panel2.Controls.Add(this.accessibleComponentTabControl);
      this.topSplitContainer.Size = new System.Drawing.Size(1181, 400);
      this.topSplitContainer.SplitterDistance = 546;
      this.topSplitContainer.TabIndex = 0;
      // 
      // topLevelTabControl
      // 
      this.topLevelTabControl.Controls.Add(this.accessibilityTreePage);
      this.topLevelTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.topLevelTabControl.Location = new System.Drawing.Point(0, 0);
      this.topLevelTabControl.Name = "topLevelTabControl";
      this.topLevelTabControl.SelectedIndex = 0;
      this.topLevelTabControl.Size = new System.Drawing.Size(546, 400);
      this.topLevelTabControl.TabIndex = 0;
      // 
      // accessibilityTreePage
      // 
      this.accessibilityTreePage.Controls.Add(this.accessibilityTree);
      this.accessibilityTreePage.Location = new System.Drawing.Point(4, 24);
      this.accessibilityTreePage.Name = "accessibilityTreePage";
      this.accessibilityTreePage.Size = new System.Drawing.Size(538, 372);
      this.accessibilityTreePage.TabIndex = 0;
      this.accessibilityTreePage.Text = "Accessibility Tree";
      this.accessibilityTreePage.UseVisualStyleBackColor = true;
      // 
      // accessibilityTree
      // 
      this.accessibilityTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.accessibilityTree.FullRowSelect = true;
      this.accessibilityTree.HideSelection = false;
      this.accessibilityTree.Location = new System.Drawing.Point(0, 0);
      this.accessibilityTree.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.accessibilityTree.Name = "accessibilityTree";
      this.accessibilityTree.Size = new System.Drawing.Size(538, 372);
      this.accessibilityTree.TabIndex = 2;
      // 
      // accessibleComponentTabControl
      // 
      this.accessibleComponentTabControl.Controls.Add(this.accessibleComponentTabPage);
      this.accessibleComponentTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.accessibleComponentTabControl.Location = new System.Drawing.Point(0, 0);
      this.accessibleComponentTabControl.Name = "accessibleComponentTabControl";
      this.accessibleComponentTabControl.SelectedIndex = 0;
      this.accessibleComponentTabControl.Size = new System.Drawing.Size(631, 400);
      this.accessibleComponentTabControl.TabIndex = 0;
      // 
      // accessibleComponentTabPage
      // 
      this.accessibleComponentTabPage.Controls.Add(this.accessibleContextPropertyList);
      this.accessibleComponentTabPage.Location = new System.Drawing.Point(4, 24);
      this.accessibleComponentTabPage.Name = "accessibleComponentTabPage";
      this.accessibleComponentTabPage.Size = new System.Drawing.Size(623, 372);
      this.accessibleComponentTabPage.TabIndex = 0;
      this.accessibleComponentTabPage.Text = "Accessible Component";
      this.accessibleComponentTabPage.UseVisualStyleBackColor = true;
      // 
      // accessibleContextPropertyList
      // 
      this.accessibleContextPropertyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.accessibleContextPropertyList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.accessibleContextPropertyList.FullRowSelect = true;
      this.accessibleContextPropertyList.GridLines = true;
      this.accessibleContextPropertyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.accessibleContextPropertyList.Location = new System.Drawing.Point(0, 0);
      this.accessibleContextPropertyList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.accessibleContextPropertyList.MultiSelect = false;
      this.accessibleContextPropertyList.Name = "accessibleContextPropertyList";
      this.accessibleContextPropertyList.Size = new System.Drawing.Size(623, 372);
      this.accessibleContextPropertyList.SmallImageList = this.propertyImageList;
      this.accessibleContextPropertyList.TabIndex = 1;
      this.accessibleContextPropertyList.UseCompatibleStateImageBehavior = false;
      this.accessibleContextPropertyList.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Property";
      this.columnHeader1.Width = 200;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Value";
      this.columnHeader2.Width = 300;
      // 
      // propertyImageList
      // 
      this.propertyImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("propertyImageList.ImageStream")));
      this.propertyImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.propertyImageList.Images.SetKeyName(0, "Plus.png");
      this.propertyImageList.Images.SetKeyName(1, "Minus.png");
      // 
      // mainSplitContainer
      // 
      this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainSplitContainer.Location = new System.Drawing.Point(0, 51);
      this.mainSplitContainer.Name = "mainSplitContainer";
      this.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // mainSplitContainer.Panel1
      // 
      this.mainSplitContainer.Panel1.Controls.Add(this.topSplitContainer);
      // 
      // mainSplitContainer.Panel2
      // 
      this.mainSplitContainer.Panel2.Controls.Add(this.bottomTabControl);
      this.mainSplitContainer.Size = new System.Drawing.Size(1181, 631);
      this.mainSplitContainer.SplitterDistance = 400;
      this.mainSplitContainer.TabIndex = 4;
      // 
      // bottomTabControl
      // 
      this.bottomTabControl.Controls.Add(this.messagesPage);
      this.bottomTabControl.Controls.Add(this.eventsPage);
      this.bottomTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bottomTabControl.Location = new System.Drawing.Point(0, 0);
      this.bottomTabControl.Name = "bottomTabControl";
      this.bottomTabControl.SelectedIndex = 0;
      this.bottomTabControl.Size = new System.Drawing.Size(1181, 227);
      this.bottomTabControl.TabIndex = 0;
      // 
      // messagesPage
      // 
      this.messagesPage.Controls.Add(this.messageList);
      this.messagesPage.Controls.Add(this.messagesToolStrip);
      this.messagesPage.Location = new System.Drawing.Point(4, 24);
      this.messagesPage.Name = "messagesPage";
      this.messagesPage.Size = new System.Drawing.Size(1173, 199);
      this.messagesPage.TabIndex = 1;
      this.messagesPage.Text = "Messages";
      this.messagesPage.UseVisualStyleBackColor = true;
      // 
      // messageList
      // 
      this.messageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.messageIdColumn,
            this.messageTimeColumn,
            this.messageTextColumn});
      this.messageList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.messageList.FullRowSelect = true;
      this.messageList.GridLines = true;
      this.messageList.Location = new System.Drawing.Point(25, 0);
      this.messageList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.messageList.Name = "messageList";
      this.messageList.Size = new System.Drawing.Size(1148, 199);
      this.messageList.TabIndex = 7;
      this.messageList.UseCompatibleStateImageBehavior = false;
      this.messageList.View = System.Windows.Forms.View.Details;
      // 
      // messageIdColumn
      // 
      this.messageIdColumn.Text = "Id";
      // 
      // messageTimeColumn
      // 
      this.messageTimeColumn.Text = "Time";
      this.messageTimeColumn.Width = 100;
      // 
      // messageTextColumn
      // 
      this.messageTextColumn.Text = "Message";
      this.messageTextColumn.Width = 900;
      // 
      // messagesToolStrip
      // 
      this.messagesToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
      this.messagesToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.messagesToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.messagesToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearMessagesButton});
      this.messagesToolStrip.Location = new System.Drawing.Point(0, 0);
      this.messagesToolStrip.Name = "messagesToolStrip";
      this.messagesToolStrip.Size = new System.Drawing.Size(25, 199);
      this.messagesToolStrip.TabIndex = 6;
      this.messagesToolStrip.Text = "toolStrip3";
      this.messagesToolStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
      // 
      // clearMessagesButton
      // 
      this.clearMessagesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.clearMessagesButton.Image = ((System.Drawing.Image)(resources.GetObject("clearMessagesButton.Image")));
      this.clearMessagesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.clearMessagesButton.Name = "clearMessagesButton";
      this.clearMessagesButton.Size = new System.Drawing.Size(22, 24);
      this.clearMessagesButton.Text = "Clear";
      this.clearMessagesButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.clearMessagesButton.Click += new System.EventHandler(this.clearMessagesButton_Click);
      // 
      // eventsPage
      // 
      this.eventsPage.Controls.Add(this.eventList);
      this.eventsPage.Controls.Add(this.eventsToolStrip);
      this.eventsPage.Location = new System.Drawing.Point(4, 22);
      this.eventsPage.Name = "eventsPage";
      this.eventsPage.Size = new System.Drawing.Size(1173, 201);
      this.eventsPage.TabIndex = 0;
      this.eventsPage.Text = "Accessibility events";
      this.eventsPage.UseVisualStyleBackColor = true;
      // 
      // eventList
      // 
      this.eventList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.eventId,
            this.eventTime,
            this.eventJvmId,
            this.eventName,
            this.eventSource,
            this.eventOldValue,
            this.eventNewValue});
      this.eventList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.eventList.FullRowSelect = true;
      this.eventList.GridLines = true;
      this.eventList.Location = new System.Drawing.Point(25, 0);
      this.eventList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.eventList.Name = "eventList";
      this.eventList.Size = new System.Drawing.Size(1148, 201);
      this.eventList.TabIndex = 5;
      this.eventList.UseCompatibleStateImageBehavior = false;
      this.eventList.View = System.Windows.Forms.View.Details;
      // 
      // eventId
      // 
      this.eventId.Text = "Id";
      // 
      // eventTime
      // 
      this.eventTime.Text = "Time";
      this.eventTime.Width = 100;
      // 
      // eventJvmId
      // 
      this.eventJvmId.Text = "JvmID";
      this.eventJvmId.Width = 100;
      // 
      // eventName
      // 
      this.eventName.Text = "Name";
      this.eventName.Width = 220;
      // 
      // eventSource
      // 
      this.eventSource.Text = "Source";
      this.eventSource.Width = 200;
      // 
      // eventOldValue
      // 
      this.eventOldValue.Text = "Old Value";
      this.eventOldValue.Width = 120;
      // 
      // eventNewValue
      // 
      this.eventNewValue.Text = "NewValue";
      this.eventNewValue.Width = 120;
      // 
      // eventsToolStrip
      // 
      this.eventsToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
      this.eventsToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.eventsToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.eventsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearEventsButton});
      this.eventsToolStrip.Location = new System.Drawing.Point(0, 0);
      this.eventsToolStrip.Name = "eventsToolStrip";
      this.eventsToolStrip.Size = new System.Drawing.Size(25, 201);
      this.eventsToolStrip.TabIndex = 0;
      this.eventsToolStrip.Text = "toolStrip2";
      this.eventsToolStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
      // 
      // clearEventsButton
      // 
      this.clearEventsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.clearEventsButton.Image = ((System.Drawing.Image)(resources.GetObject("clearEventsButton.Image")));
      this.clearEventsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.clearEventsButton.Name = "clearEventsButton";
      this.clearEventsButton.Size = new System.Drawing.Size(22, 24);
      this.clearEventsButton.Text = "Clear";
      this.clearEventsButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.clearEventsButton.Click += new System.EventHandler(this.clearEventsButton_Click);
      // 
      // ExplorerForm
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(1181, 704);
      this.Controls.Add(this.mainSplitContainer);
      this.Controls.Add(this.mainToolStrip);
      this.Controls.Add(this.statusBarStrip);
      this.Controls.Add(this.menuStrip1);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Candara", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "ExplorerForm";
      this.Text = "Access Bridge Explorer";
      this.Activated += new System.EventHandler(this.MainForm_Activated);
      this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.Shown += new System.EventHandler(this.MainForm_Shown);
      this.MouseCaptureChanged += new System.EventHandler(this.MainForm_MouseCaptureChanged);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.statusBarStrip.ResumeLayout(false);
      this.statusBarStrip.PerformLayout();
      this.mainToolStrip.ResumeLayout(false);
      this.mainToolStrip.PerformLayout();
      this.topSplitContainer.Panel1.ResumeLayout(false);
      this.topSplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.topSplitContainer)).EndInit();
      this.topSplitContainer.ResumeLayout(false);
      this.topLevelTabControl.ResumeLayout(false);
      this.accessibilityTreePage.ResumeLayout(false);
      this.accessibleComponentTabControl.ResumeLayout(false);
      this.accessibleComponentTabPage.ResumeLayout(false);
      this.mainSplitContainer.Panel1.ResumeLayout(false);
      this.mainSplitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
      this.mainSplitContainer.ResumeLayout(false);
      this.bottomTabControl.ResumeLayout(false);
      this.messagesPage.ResumeLayout(false);
      this.messagesPage.PerformLayout();
      this.messagesToolStrip.ResumeLayout(false);
      this.messagesToolStrip.PerformLayout();
      this.eventsPage.ResumeLayout(false);
      this.eventsPage.PerformLayout();
      this.eventsToolStrip.ResumeLayout(false);
      this.eventsToolStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusBarStrip;
    private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    private System.Windows.Forms.ToolStrip mainToolStrip;
    private System.Windows.Forms.ToolStripButton refreshButton;
    private System.Windows.Forms.ToolStripButton overlayEnableButton;
    private System.Windows.Forms.ToolStripButton findComponentButton;
    private System.Windows.Forms.Timer refreshTimer;
    private System.Windows.Forms.SplitContainer topSplitContainer;
    private System.Windows.Forms.SplitContainer mainSplitContainer;
    private System.Windows.Forms.ToolStripMenuItem eventsMenu;
    private System.Windows.Forms.TabControl bottomTabControl;
    private System.Windows.Forms.TabPage eventsPage;
    private System.Windows.Forms.ListView eventList;
    private System.Windows.Forms.ColumnHeader eventId;
    private System.Windows.Forms.ColumnHeader eventTime;
    private System.Windows.Forms.ColumnHeader eventJvmId;
    private System.Windows.Forms.ColumnHeader eventName;
    private System.Windows.Forms.ColumnHeader eventSource;
    private System.Windows.Forms.ColumnHeader eventOldValue;
    private System.Windows.Forms.ColumnHeader eventNewValue;
    private System.Windows.Forms.ToolStrip eventsToolStrip;
    private System.Windows.Forms.ToolStripButton clearEventsButton;
    private System.Windows.Forms.TabPage messagesPage;
    private System.Windows.Forms.ListView messageList;
    private System.Windows.Forms.ColumnHeader messageIdColumn;
    private System.Windows.Forms.ColumnHeader messageTimeColumn;
    private System.Windows.Forms.ColumnHeader messageTextColumn;
    private System.Windows.Forms.ToolStrip messagesToolStrip;
    private System.Windows.Forms.ToolStripButton clearMessagesButton;
    private System.Windows.Forms.TabControl topLevelTabControl;
    private System.Windows.Forms.TabPage accessibilityTreePage;
    private System.Windows.Forms.TreeView accessibilityTree;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton showHelpButton;
    private System.Windows.Forms.TabControl accessibleComponentTabControl;
    private System.Windows.Forms.TabPage accessibleComponentTabPage;
    private System.Windows.Forms.ListView accessibleContextPropertyList;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    private System.Windows.Forms.ImageList propertyImageList;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem navigateForwardToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem navigateBackwardToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton navigateBackwardButton;
    private System.Windows.Forms.ToolStripButton navigateForwardButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
  }
}

