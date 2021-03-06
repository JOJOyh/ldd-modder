﻿namespace LDDModder.LifExtractor.Windows
{
    partial class LifViewerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LifViewerWindow));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.StatusToolStrip = new System.Windows.Forms.StatusStrip();
            this.CurrentFileStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LifTreeView = new System.Windows.Forms.TreeView();
            this.FolderListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ListMenu_ExtractItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ListMenu_CreateFolderItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListMenu_AddFileItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListMenu_RenameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ListMenu_DeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SmallIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.FolderListView = new BrightIdeasSoftware.DataListView();
            this.FlvNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FlvTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FlvSizeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FlvCreatedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FlvModifiedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.LargeIconImageList = new System.Windows.Forms.ImageList(this.components);
            this.MainMenuToolStrip = new System.Windows.Forms.MenuStrip();
            this.MainMenu_FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_OpenItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_NewLif = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMenu_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileMenu_ExtractItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu_ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewModeDetailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewModeSmallMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewModeLargeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ActionsMenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.ActionsMenu_Open = new System.Windows.Forms.ToolStripButton();
            this.ActionsMenu_Extract = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ActionsMenu_EnableEdit = new System.Windows.Forms.ToolStripButton();
            this.ActionsMenu_SaveLif = new System.Windows.Forms.ToolStripSplitButton();
            this.SaveMenu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenu_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.ActionsMenu_CancelEdit = new System.Windows.Forms.ToolStripButton();
            this.NavigationToolStrip = new System.Windows.Forms.ToolStrip();
            this.BackToolbarButton = new System.Windows.Forms.ToolStripButton();
            this.NextToolbarButton = new System.Windows.Forms.ToolStripButton();
            this.UpToolbarButton = new System.Windows.Forms.ToolStripButton();
            this.ToolBarFolderCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.StatusToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.FolderListContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FolderListView)).BeginInit();
            this.MainMenuToolStrip.SuspendLayout();
            this.ActionsMenuToolStrip.SuspendLayout();
            this.NavigationToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.StatusToolStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MainMenuToolStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.ActionsMenuToolStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.NavigationToolStrip);
            // 
            // StatusToolStrip
            // 
            resources.ApplyResources(this.StatusToolStrip, "StatusToolStrip");
            this.StatusToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrentFileStripLabel,
            this.toolStripProgressBar1});
            this.StatusToolStrip.Name = "StatusToolStrip";
            // 
            // CurrentFileStripLabel
            // 
            this.CurrentFileStripLabel.Name = "CurrentFileStripLabel";
            resources.ApplyResources(this.CurrentFileStripLabel, "CurrentFileStripLabel");
            this.CurrentFileStripLabel.Spring = true;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LifTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FolderListView);
            // 
            // LifTreeView
            // 
            this.LifTreeView.AllowDrop = true;
            this.LifTreeView.ContextMenuStrip = this.FolderListContextMenu;
            resources.ApplyResources(this.LifTreeView, "LifTreeView");
            this.LifTreeView.FullRowSelect = true;
            this.LifTreeView.HideSelection = false;
            this.LifTreeView.HotTracking = true;
            this.LifTreeView.ImageList = this.SmallIconImageList;
            this.LifTreeView.ItemHeight = 20;
            this.LifTreeView.Name = "LifTreeView";
            this.LifTreeView.ShowLines = false;
            this.LifTreeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LifTreeView_BeforeLabelEdit);
            this.LifTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LifTreeView_AfterLabelEdit);
            this.LifTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LifTreeView_ItemDrag);
            this.LifTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LifTreeView_AfterSelect);
            this.LifTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.LifTreeView_DragDrop);
            this.LifTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.LifTreeView_DragEnter);
            this.LifTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.LifTreeView_DragOver);
            this.LifTreeView.DragLeave += new System.EventHandler(this.LifTreeView_DragLeave);
            // 
            // FolderListContextMenu
            // 
            this.FolderListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ListMenu_ExtractItem,
            this.toolStripSeparator1,
            this.ListMenu_CreateFolderItem,
            this.ListMenu_AddFileItem,
            this.ListMenu_RenameItem,
            this.ListMenu_DeleteItem});
            this.FolderListContextMenu.Name = "FolderListContextMenu";
            resources.ApplyResources(this.FolderListContextMenu, "FolderListContextMenu");
            this.FolderListContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.FolderListContextMenu_Opening);
            // 
            // ListMenu_ExtractItem
            // 
            this.ListMenu_ExtractItem.Name = "ListMenu_ExtractItem";
            resources.ApplyResources(this.ListMenu_ExtractItem, "ListMenu_ExtractItem");
            this.ListMenu_ExtractItem.Click += new System.EventHandler(this.ListMenu_ExtractItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // ListMenu_CreateFolderItem
            // 
            this.ListMenu_CreateFolderItem.Name = "ListMenu_CreateFolderItem";
            resources.ApplyResources(this.ListMenu_CreateFolderItem, "ListMenu_CreateFolderItem");
            this.ListMenu_CreateFolderItem.Click += new System.EventHandler(this.ListMenu_CreateFolderItem_Click);
            // 
            // ListMenu_AddFileItem
            // 
            this.ListMenu_AddFileItem.Name = "ListMenu_AddFileItem";
            resources.ApplyResources(this.ListMenu_AddFileItem, "ListMenu_AddFileItem");
            this.ListMenu_AddFileItem.Click += new System.EventHandler(this.ListMenu_AddFileItem_Click);
            // 
            // ListMenu_RenameItem
            // 
            this.ListMenu_RenameItem.Name = "ListMenu_RenameItem";
            resources.ApplyResources(this.ListMenu_RenameItem, "ListMenu_RenameItem");
            this.ListMenu_RenameItem.Click += new System.EventHandler(this.ListMenu_RenameItem_Click);
            // 
            // ListMenu_DeleteItem
            // 
            this.ListMenu_DeleteItem.Name = "ListMenu_DeleteItem";
            resources.ApplyResources(this.ListMenu_DeleteItem, "ListMenu_DeleteItem");
            this.ListMenu_DeleteItem.Click += new System.EventHandler(this.ListMenu_DeleteItem_Click);
            // 
            // SmallIconImageList
            // 
            this.SmallIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.SmallIconImageList, "SmallIconImageList");
            this.SmallIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FolderListView
            // 
            this.FolderListView.AllColumns.Add(this.FlvNameColumn);
            this.FolderListView.AllColumns.Add(this.FlvTypeColumn);
            this.FolderListView.AllColumns.Add(this.FlvSizeColumn);
            this.FolderListView.AllColumns.Add(this.FlvCreatedColumn);
            this.FolderListView.AllColumns.Add(this.FlvModifiedColumn);
            this.FolderListView.AllowDrop = true;
            this.FolderListView.AutoGenerateColumns = false;
            this.FolderListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.FolderListView.CellEditUseWholeCell = false;
            this.FolderListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FlvNameColumn,
            this.FlvTypeColumn,
            this.FlvSizeColumn,
            this.FlvCreatedColumn,
            this.FlvModifiedColumn});
            this.FolderListView.ContextMenuStrip = this.FolderListContextMenu;
            this.FolderListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.FolderListView.DataSource = null;
            resources.ApplyResources(this.FolderListView, "FolderListView");
            this.FolderListView.FullRowSelect = true;
            this.FolderListView.HideSelection = false;
            this.FolderListView.LargeImageList = this.LargeIconImageList;
            this.FolderListView.Name = "FolderListView";
            this.FolderListView.ShowGroups = false;
            this.FolderListView.ShowHeaderInAllViews = false;
            this.FolderListView.ShowItemToolTips = true;
            this.FolderListView.SmallImageList = this.SmallIconImageList;
            this.FolderListView.UseCompatibleStateImageBehavior = false;
            this.FolderListView.UseExplorerTheme = true;
            this.FolderListView.View = System.Windows.Forms.View.Details;
            this.FolderListView.BeforeSorting += new System.EventHandler<BrightIdeasSoftware.BeforeSortingEventArgs>(this.FolderListView_BeforeSorting);
            this.FolderListView.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.FolderListView_CanDrop);
            this.FolderListView.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.FolderListView_CellEditFinished);
            this.FolderListView.CellEditValidating += new BrightIdeasSoftware.CellEditEventHandler(this.FolderListView_CellEditValidating);
            this.FolderListView.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.FolderListView_Dropped);
            this.FolderListView.ItemActivate += new System.EventHandler(this.FolderListView_ItemActivate);
            this.FolderListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.FolderListView_ItemDrag);
            // 
            // FlvNameColumn
            // 
            this.FlvNameColumn.AspectName = "Name";
            this.FlvNameColumn.AutoCompleteEditor = false;
            this.FlvNameColumn.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.FlvNameColumn.ImageAspectName = "ItemImageKey";
            resources.ApplyResources(this.FlvNameColumn, "FlvNameColumn");
            // 
            // FlvTypeColumn
            // 
            this.FlvTypeColumn.AspectName = "Description";
            this.FlvTypeColumn.IsEditable = false;
            resources.ApplyResources(this.FlvTypeColumn, "FlvTypeColumn");
            // 
            // FlvSizeColumn
            // 
            this.FlvSizeColumn.AspectName = "Size";
            this.FlvSizeColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.FlvSizeColumn.IsEditable = false;
            resources.ApplyResources(this.FlvSizeColumn, "FlvSizeColumn");
            // 
            // FlvCreatedColumn
            // 
            this.FlvCreatedColumn.AspectName = "CreatedDate";
            this.FlvCreatedColumn.IsEditable = false;
            resources.ApplyResources(this.FlvCreatedColumn, "FlvCreatedColumn");
            // 
            // FlvModifiedColumn
            // 
            this.FlvModifiedColumn.AspectName = "ModifiedDate";
            this.FlvModifiedColumn.IsEditable = false;
            resources.ApplyResources(this.FlvModifiedColumn, "FlvModifiedColumn");
            // 
            // LargeIconImageList
            // 
            this.LargeIconImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.LargeIconImageList, "LargeIconImageList");
            this.LargeIconImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainMenuToolStrip
            // 
            resources.ApplyResources(this.MainMenuToolStrip, "MainMenuToolStrip");
            this.MainMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenu_FileMenu,
            this.MainMenu_ViewMenu});
            this.MainMenuToolStrip.Name = "MainMenuToolStrip";
            // 
            // MainMenu_FileMenu
            // 
            this.MainMenu_FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu_OpenItem,
            this.FileMenu_NewLif,
            this.FileMenu_Close,
            this.toolStripSeparator2,
            this.FileMenu_ExtractItem});
            this.MainMenu_FileMenu.Name = "MainMenu_FileMenu";
            resources.ApplyResources(this.MainMenu_FileMenu, "MainMenu_FileMenu");
            // 
            // FileMenu_OpenItem
            // 
            this.FileMenu_OpenItem.Name = "FileMenu_OpenItem";
            resources.ApplyResources(this.FileMenu_OpenItem, "FileMenu_OpenItem");
            this.FileMenu_OpenItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // FileMenu_NewLif
            // 
            this.FileMenu_NewLif.Name = "FileMenu_NewLif";
            resources.ApplyResources(this.FileMenu_NewLif, "FileMenu_NewLif");
            this.FileMenu_NewLif.Click += new System.EventHandler(this.FileMenu_NewLif_Click);
            // 
            // FileMenu_Close
            // 
            this.FileMenu_Close.Name = "FileMenu_Close";
            resources.ApplyResources(this.FileMenu_Close, "FileMenu_Close");
            this.FileMenu_Close.Click += new System.EventHandler(this.FileMenu_Close_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // FileMenu_ExtractItem
            // 
            resources.ApplyResources(this.FileMenu_ExtractItem, "FileMenu_ExtractItem");
            this.FileMenu_ExtractItem.Name = "FileMenu_ExtractItem";
            this.FileMenu_ExtractItem.Click += new System.EventHandler(this.FileMenu_ExtractItem_Click);
            // 
            // MainMenu_ViewMenu
            // 
            this.MainMenu_ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewModeDetailsMenuItem,
            this.ViewModeSmallMenuItem,
            this.ViewModeLargeMenuItem});
            this.MainMenu_ViewMenu.Name = "MainMenu_ViewMenu";
            resources.ApplyResources(this.MainMenu_ViewMenu, "MainMenu_ViewMenu");
            // 
            // ViewModeDetailsMenuItem
            // 
            this.ViewModeDetailsMenuItem.Checked = true;
            this.ViewModeDetailsMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewModeDetailsMenuItem.Name = "ViewModeDetailsMenuItem";
            resources.ApplyResources(this.ViewModeDetailsMenuItem, "ViewModeDetailsMenuItem");
            this.ViewModeDetailsMenuItem.Click += new System.EventHandler(this.ViewModeMenuItems_Click);
            // 
            // ViewModeSmallMenuItem
            // 
            this.ViewModeSmallMenuItem.Name = "ViewModeSmallMenuItem";
            resources.ApplyResources(this.ViewModeSmallMenuItem, "ViewModeSmallMenuItem");
            this.ViewModeSmallMenuItem.Click += new System.EventHandler(this.ViewModeMenuItems_Click);
            // 
            // ViewModeLargeMenuItem
            // 
            this.ViewModeLargeMenuItem.Name = "ViewModeLargeMenuItem";
            resources.ApplyResources(this.ViewModeLargeMenuItem, "ViewModeLargeMenuItem");
            this.ViewModeLargeMenuItem.Click += new System.EventHandler(this.ViewModeMenuItems_Click);
            // 
            // ActionsMenuToolStrip
            // 
            resources.ApplyResources(this.ActionsMenuToolStrip, "ActionsMenuToolStrip");
            this.ActionsMenuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ActionsMenuToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ActionsMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ActionsMenu_Open,
            this.ActionsMenu_Extract,
            this.toolStripSeparator3,
            this.ActionsMenu_EnableEdit,
            this.ActionsMenu_SaveLif,
            this.ActionsMenu_CancelEdit});
            this.ActionsMenuToolStrip.Name = "ActionsMenuToolStrip";
            this.ActionsMenuToolStrip.Stretch = true;
            // 
            // ActionsMenu_Open
            // 
            this.ActionsMenu_Open.Image = global::LDDModder.LifExtractor.Properties.Resources.Open_32x32;
            resources.ApplyResources(this.ActionsMenu_Open, "ActionsMenu_Open");
            this.ActionsMenu_Open.Name = "ActionsMenu_Open";
            this.ActionsMenu_Open.Click += new System.EventHandler(this.ActionsMenu_Open_Click);
            // 
            // ActionsMenu_Extract
            // 
            resources.ApplyResources(this.ActionsMenu_Extract, "ActionsMenu_Extract");
            this.ActionsMenu_Extract.Image = global::LDDModder.LifExtractor.Properties.Resources.Extract_32x32;
            this.ActionsMenu_Extract.Name = "ActionsMenu_Extract";
            this.ActionsMenu_Extract.Click += new System.EventHandler(this.ActionsMenu_Extract_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // ActionsMenu_EnableEdit
            // 
            this.ActionsMenu_EnableEdit.Image = global::LDDModder.LifExtractor.Properties.Resources.Edit_32x32;
            resources.ApplyResources(this.ActionsMenu_EnableEdit, "ActionsMenu_EnableEdit");
            this.ActionsMenu_EnableEdit.Name = "ActionsMenu_EnableEdit";
            this.ActionsMenu_EnableEdit.Click += new System.EventHandler(this.ActionsMenu_EnableEdit_Click);
            // 
            // ActionsMenu_SaveLif
            // 
            this.ActionsMenu_SaveLif.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMenu_Save,
            this.SaveMenu_SaveAs});
            this.ActionsMenu_SaveLif.Image = global::LDDModder.LifExtractor.Properties.Resources.Save_32x32;
            resources.ApplyResources(this.ActionsMenu_SaveLif, "ActionsMenu_SaveLif");
            this.ActionsMenu_SaveLif.Name = "ActionsMenu_SaveLif";
            // 
            // SaveMenu_Save
            // 
            this.SaveMenu_Save.Name = "SaveMenu_Save";
            resources.ApplyResources(this.SaveMenu_Save, "SaveMenu_Save");
            this.SaveMenu_Save.Click += new System.EventHandler(this.SaveMenu_Save_Click);
            // 
            // SaveMenu_SaveAs
            // 
            this.SaveMenu_SaveAs.Name = "SaveMenu_SaveAs";
            resources.ApplyResources(this.SaveMenu_SaveAs, "SaveMenu_SaveAs");
            this.SaveMenu_SaveAs.Click += new System.EventHandler(this.SaveMenu_SaveAs_Click);
            // 
            // ActionsMenu_CancelEdit
            // 
            this.ActionsMenu_CancelEdit.Image = global::LDDModder.LifExtractor.Properties.Resources.Undo_32x32;
            resources.ApplyResources(this.ActionsMenu_CancelEdit, "ActionsMenu_CancelEdit");
            this.ActionsMenu_CancelEdit.Name = "ActionsMenu_CancelEdit";
            this.ActionsMenu_CancelEdit.Click += new System.EventHandler(this.ActionsMenu_CancelEdit_Click);
            // 
            // NavigationToolStrip
            // 
            resources.ApplyResources(this.NavigationToolStrip, "NavigationToolStrip");
            this.NavigationToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.NavigationToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BackToolbarButton,
            this.NextToolbarButton,
            this.UpToolbarButton,
            this.ToolBarFolderCombo});
            this.NavigationToolStrip.Name = "NavigationToolStrip";
            this.NavigationToolStrip.Stretch = true;
            // 
            // BackToolbarButton
            // 
            this.BackToolbarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BackToolbarButton, "BackToolbarButton");
            this.BackToolbarButton.Image = global::LDDModder.LifExtractor.Properties.Resources.Arrow_Left_16x16_Black;
            this.BackToolbarButton.Name = "BackToolbarButton";
            this.BackToolbarButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.BackToolbarButton.Click += new System.EventHandler(this.BackToolbarButton_Click);
            // 
            // NextToolbarButton
            // 
            this.NextToolbarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.NextToolbarButton, "NextToolbarButton");
            this.NextToolbarButton.Image = global::LDDModder.LifExtractor.Properties.Resources.Arrow_Right_16x16_Black;
            this.NextToolbarButton.Name = "NextToolbarButton";
            this.NextToolbarButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.NextToolbarButton.Click += new System.EventHandler(this.NextToolbarButton_Click);
            // 
            // UpToolbarButton
            // 
            this.UpToolbarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.UpToolbarButton, "UpToolbarButton");
            this.UpToolbarButton.Image = global::LDDModder.LifExtractor.Properties.Resources.Arrow_Up_16x16_Black;
            this.UpToolbarButton.Name = "UpToolbarButton";
            this.UpToolbarButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.UpToolbarButton.Click += new System.EventHandler(this.UpToolbarButton_Click);
            // 
            // ToolBarFolderCombo
            // 
            resources.ApplyResources(this.ToolBarFolderCombo, "ToolBarFolderCombo");
            this.ToolBarFolderCombo.Name = "ToolBarFolderCombo";
            this.ToolBarFolderCombo.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            // 
            // LifViewerWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "LifViewerWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LifViewerWindow_FormClosing);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.StatusToolStrip.ResumeLayout(false);
            this.StatusToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.FolderListContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FolderListView)).EndInit();
            this.MainMenuToolStrip.ResumeLayout(false);
            this.MainMenuToolStrip.PerformLayout();
            this.ActionsMenuToolStrip.ResumeLayout(false);
            this.ActionsMenuToolStrip.PerformLayout();
            this.NavigationToolStrip.ResumeLayout(false);
            this.NavigationToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip StatusToolStrip;
        private System.Windows.Forms.MenuStrip MainMenuToolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripStatusLabel CurrentFileStripLabel;
        private System.Windows.Forms.ToolStrip NavigationToolStrip;
        private System.Windows.Forms.ToolStripButton BackToolbarButton;
        private System.Windows.Forms.ToolStripButton UpToolbarButton;
        private System.Windows.Forms.ToolStripComboBox ToolBarFolderCombo;
        private System.Windows.Forms.ImageList SmallIconImageList;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_FileMenu;
        private System.Windows.Forms.ToolStripButton NextToolbarButton;
        private System.Windows.Forms.ImageList LargeIconImageList;
        private BrightIdeasSoftware.DataListView FolderListView;
        private BrightIdeasSoftware.OLVColumn FlvSizeColumn;
        private BrightIdeasSoftware.OLVColumn FlvNameColumn;
        private BrightIdeasSoftware.OLVColumn FlvTypeColumn;
        private BrightIdeasSoftware.OLVColumn FlvCreatedColumn;
        private BrightIdeasSoftware.OLVColumn FlvModifiedColumn;
        private System.Windows.Forms.ToolStripMenuItem MainMenu_ViewMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewModeDetailsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewModeSmallMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewModeLargeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_OpenItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_ExtractItem;
        private System.Windows.Forms.ContextMenuStrip FolderListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem ListMenu_ExtractItem;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_NewLif;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ListMenu_CreateFolderItem;
        private System.Windows.Forms.ToolStripMenuItem ListMenu_AddFileItem;
        private System.Windows.Forms.ToolStripMenuItem ListMenu_RenameItem;
        private System.Windows.Forms.ToolStripMenuItem ListMenu_DeleteItem;
        private System.Windows.Forms.TreeView LifTreeView;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip ActionsMenuToolStrip;
        private System.Windows.Forms.ToolStripButton ActionsMenu_Extract;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ActionsMenu_EnableEdit;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripButton ActionsMenu_Open;
        private System.Windows.Forms.ToolStripButton ActionsMenu_CancelEdit;
        private System.Windows.Forms.ToolStripSplitButton ActionsMenu_SaveLif;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu_Save;
        private System.Windows.Forms.ToolStripMenuItem SaveMenu_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem FileMenu_Close;
    }
}