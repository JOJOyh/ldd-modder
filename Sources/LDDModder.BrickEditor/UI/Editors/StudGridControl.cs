﻿using LDDModder.LDD.Primitives.Connectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDDModder.BrickEditor.UI.Editors
{
    public partial class StudGridControl : Control
    {
        private Custom2DFieldConnector _StudConnector;
       
        private Size _MaxGridSize;
        private System.Threading.Timer SelectionScrollTimer;

        private Point? SelectionStart;
        private Point? SelectionEnd;
        private Point? FocusedCell;
        
        private Point ScrollOffset;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Custom2DFieldConnector StudConnector
        {
            get => _StudConnector;
            set
            {
                if (_StudConnector != value)
                    BindConnector(value);
            }
        }

        public Size MaxGridSize
        {
            get => _MaxGridSize;
            set
            {
                value.Width = Math.Max(value.Width, 3);
                value.Height = Math.Max(value.Height, 3);

                if (value != _MaxGridSize)
                {
                    _MaxGridSize = value;
                    UpdateControlSize();
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Custom2DFieldNode SelectedNode => GetNodeFromCell(FocusedCell);

        public StudGridControl()
        {
            InitializeComponent();
            CausesValidation = true;
            _MaxGridSize = new Size(10, 10);

            SetStyle(ControlStyles.ResizeRedraw | 
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.Selectable, true);

            CalculateElementSizes();

            InitEditCombo();
            InitScrollBars();
            
            BindConnector(null);
        }

        protected void BindConnector(Custom2DFieldConnector connector)
        {
            SelectionStart = null;
            SelectionEnd = null;
            FocusedCell = null;
            ScrollOffset = new Point(0, 0);
            IsSelectingRange = false;

            DisableSelectionAutoScroll();

            if (_StudConnector != null)
            {
                _StudConnector.PropertyChanged -= StudConnector_PropertyChanged;
                _StudConnector.NodeValueChanged -= StudConnector_NodeValueChanged;
            }

            _StudConnector = connector ?? new Custom2DFieldConnector();

            _StudConnector.PropertyChanged += StudConnector_PropertyChanged;
            _StudConnector.NodeValueChanged += StudConnector_NodeValueChanged;

            UpdateControlSize();
            Invalidate();
        }

        private void StudConnector_NodeValueChanged(object sender, PropertyChangedEventArgs e)
        {
            Invalidate();
        }

        private void StudConnector_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateControlSize();
        }

        #region Size Calculation

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle CellGridBounds { get; private set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size GridCellSize { get; private set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size CurrentGridSize { get; private set; }

        private int ColumnHeaderHeight;
        private int RowHeaderWidth;

        private void CalculateElementSizes()
        {
            var cellSize = TextRenderer.MeasureText("15:4:96", Font, Size, TextFormatFlags.TextBoxControl);
            cellSize.Width += 24;
            cellSize.Height += 12;

            GridCellSize = cellSize;
        }

        private Size CalculateControlSize()
        {
            int gridWidth = StudConnector?.ArrayWidth ?? 3;
            int gridHeight = StudConnector?.ArrayHeight ?? 3;

            int visibleCols = Math.Min(gridWidth, MaxGridSize.Width);
            int visibleRows = Math.Min(gridHeight, MaxGridSize.Height);

            CurrentGridSize = new Size(visibleCols, visibleRows);

            int width = (GridCellSize.Width * visibleCols) + 1;
            int height = (GridCellSize.Height * visibleRows) + 1;

            CellGridBounds = new Rectangle(
                RowHeaderWidth, 
                ColumnHeaderHeight, 
                width, height);

            if (gridWidth > MaxGridSize.Width)
                height += SystemInformation.HorizontalScrollBarHeight;

            if (gridHeight > MaxGridSize.Height)
                width += SystemInformation.VerticalScrollBarWidth;

            return new Size(
                width + RowHeaderWidth, 
                height + ColumnHeaderHeight
            );
        }

        private void UpdateControlSize()
        {
            Size = CalculateControlSize();
            UpdateScrollbars();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            CalculateElementSizes();
            UpdateControlSize();
        }

        #endregion

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (specified.HasFlag(BoundsSpecified.Width) ||
                specified.HasFlag(BoundsSpecified.Height))
            {
                var size = CalculateControlSize();
                width = size.Width;
                height = size.Height;
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }

        #region Drawing

        protected override void OnPaint(PaintEventArgs pe)
        {
            var g = pe.Graphics;
            g.Clear(Color.White);

            if (StudConnector == null)
                return;

            DrawGridBackground(pe.Graphics);
            var evenColor = Color.FromArgb(80, 180, 180, 180);

            var columnRects = new List<RectangleF>();
            var rowRects = new List<RectangleF>();

            int visibleRows = Math.Min(StudConnector.ArrayHeight, MaxGridSize.Height);
            int visibleCols = Math.Min(StudConnector.ArrayWidth, MaxGridSize.Width);

            for (int x = 0; x < visibleCols; x++)
            {
                var colRect = new RectangleF(GridCellSize.Width * x, 0, GridCellSize.Width, GridCellSize.Height * visibleRows);
                columnRects.Add(colRect);
            }

            for (int y = 0; y < visibleRows; y++)
            {
                var rowRect = new RectangleF(0, GridCellSize.Height * y, GridCellSize.Width * visibleCols, GridCellSize.Height);
                rowRects.Add(rowRect);
            }

            void DrawBackground(List<RectangleF> rects, int offset)
            {
                for (int i = 0; i < rects.Count; i++)
                {
                    var colRowRect = rects[i];

                    if (((i + offset) % 2) == 0)
                    {
                        using (var brush = new SolidBrush(evenColor))
                            g.FillRectangle(brush, colRowRect);
                    }
                }
            }

            void DrawBorders(List<RectangleF> rects)
            {
                for (int i = 0; i < rects.Count; i++)
                {
                    var colRowRect = rects[i];
                    g.DrawRectangle(Pens.Black, colRowRect.X, colRowRect.Y, colRowRect.Width, colRowRect.Height);
                }
            }

            DrawBackground(columnRects, ScrollOffset.X);
            DrawBackground(rowRects, ScrollOffset.Y);

            var selectionColor = Color.FromArgb(50, 180, 180, 200);
            if (ContainsFocus || Focused)
                selectionColor = Color.FromArgb(100, 180, 180, 255);

            var selectionBrush = new SolidBrush(selectionColor);

            for (int y = 0; y < visibleRows; y++)
            {
                for (int x = 0; x < visibleCols; x++)
                {
                    int cellX = x + ScrollOffset.X;
                    int cellY = y + ScrollOffset.Y;

                    var cellRect = GetCellRect(cellX, cellY);

                    if (IsInSelection(cellX, cellY))
                        g.FillRectangle(selectionBrush, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                }
            }
            selectionBrush.Dispose();

            DrawBorders(columnRects);
            DrawBorders(rowRects);


            var sf = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            Pen focusedBorderPen = SystemPens.Highlight;

            //if (!(Focused || ContainsFocus))
            //{
            //    focusedBorderPen = SystemPens.
            //}

            for (int y = 0; y < visibleRows; y++)
            {
                for (int x = 0; x < visibleCols; x++)
                {
                    int cellX = x + ScrollOffset.X;
                    int cellY = y + ScrollOffset.Y;

                    var cellRect = GetCellRect(cellX, cellY);

                    if (IsInSelection(cellX, cellY))
                        g.DrawRectangle(focusedBorderPen, cellRect);

                    TextRenderer.DrawText(g, StudConnector[cellX, cellY].ToString(), Font, cellRect, ForeColor, 
                        TextFormatFlags.TextBoxControl | TextFormatFlags.NoClipping | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    //g.DrawString(StudConnector[cellX, cellY].ToString(), Font, Brushes.Black, cellRect, sf);
                }
            }

            sf.Dispose();
        }

        private void DrawGridBackground(Graphics g)
        {
            
        }

        #endregion

        #region Mouse Handling

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (EditCombo.Visible)
                    FinishEditNode();

                var clickedNode = GetNodeFromPosition(e.Location);
                if (clickedNode != null)
                {
                    IsSelectingRange = true;
                    FocusedCell = GetCellAddressFromPosition(e.Location);
                    SelectionStart = FocusedCell;
                    SelectionEnd = null;
                    Invalidate();
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsSelectingRange)
            {
                DisableSelectionAutoScroll();
                IsSelectingRange = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectionEnd = null;
                Invalidate();
                var curNode = GetNodeFromPosition(e.Location);
                if (curNode != null)
                {
                    BeginEditNode(curNode);
                }
            }
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsSelectingRange)
            {
                UpdateDragSelection();
                CheckSelectionAutoScroll();
            }
            base.OnMouseMove(e);
        }

        
        #endregion


        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (IsEditingNode && !FinishEditNode())
                e.Cancel = true;
            base.OnValidating(e);
        }

        #region Keyboard Handling

        static readonly Keys[] ARROW_KEYS = new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
        static readonly Keys[] MODIFIER_KEYS = new Keys[]
        {
                Keys.Shift, Keys.ShiftKey, Keys.LShiftKey, Keys.RShiftKey,
                Keys.Control, Keys.ControlKey, Keys.LControlKey, Keys.RControlKey
        };

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys activeModifiers = Keys.None;

            for (int i = 0; i < MODIFIER_KEYS.Length; i++)
            {
                if ((keyData & MODIFIER_KEYS[i]) == MODIFIER_KEYS[i])
                    activeModifiers |= MODIFIER_KEYS[i];
            }

            keyData &= ~activeModifiers;


            if (ARROW_KEYS.Contains(keyData))
            {
                ProcessArrowKeys(keyData, activeModifiers);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                CopySelectedCells();
            }
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                string clipContent = Clipboard.GetText();
                if (!string.IsNullOrEmpty(clipContent))
                    PasteContent(clipContent);
            }
            else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                SelectionStart = new Point(0, 0);
                SelectionEnd = new Point(
                    StudConnector.ArrayWidth - 1,
                    StudConnector.ArrayHeight - 1
                    );
                FocusedCell = SelectionStart;

                if (IsSelectingRange)
                {
                    IsSelectingRange = false;
                    DisableSelectionAutoScroll();
                }

                Invalidate();
            }

            if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
            {
                if (!IsEditingNode && SelectedNode != null)
                {
                    if (BeginEditNode(SelectedNode))
                    {
                        EditCombo.SelectAll();
                        EditCombo.SelectedText = ((char)e.KeyCode).ToString();
                    }
                }
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (!IsEditingNode && FocusedCell != null)
                {
                    var focusedNode = StudConnector.GetNode(FocusedCell.Value.X, FocusedCell.Value.Y);
                    if (focusedNode != null)
                        BeginEditNode(focusedNode);
                }
            }


            if (e.KeyCode == Keys.Escape && IsEditingNode)
            {
                CancelEditNode();
            }

            base.OnKeyDown(e);
        }

        private void ProcessArrowKeys(Keys key, Keys modifier)
        {
            if (SelectedNode != null)
            {
                var newPos = FocusedCell.Value;

                switch (key)
                {
                    case Keys.Up:
                        newPos.Y -= 1; break;
                    case Keys.Down:
                        newPos.Y += 1; break;
                    case Keys.Left:
                        newPos.X -= 1; break;
                    case Keys.Right:
                        newPos.X += 1; break;
                }

                if (newPos.X >= 0 && newPos.Y >= 0 &&
                    newPos.X < StudConnector.ArrayWidth &&
                    newPos.Y < StudConnector.ArrayHeight)
                {
                    FocusedCell = newPos;
                    if (modifier.HasFlag(Keys.Shift))
                    {
                        SelectionEnd = newPos;
                    }
                    else
                    {
                        SelectionStart = newPos;
                        SelectionEnd = null;
                    }
                    ScrollIntoView(FocusedCell.Value);
                    Invalidate();
                }
            }
        }


        #endregion

        #region Copy / Pasting

        private void CopySelectedCells()
        {
            var cells = GetSelectedNodes();
            if (cells.Any())
            {
                string strContent = string.Empty;
                foreach (var nodeRow in cells.GroupBy(x => x.Y))
                {
                    strContent += string.Join("\t", nodeRow.Select(x => x.ToString()));
                    strContent += "\r\n";
                }
                strContent = strContent.TrimEnd();
                Clipboard.SetText(strContent);
            }
        }

        private void PasteContent(string content)
        {
            var lines = content.Split('\r', '\n').ToList();
            lines.RemoveAll(x => string.IsNullOrWhiteSpace(x?.Trim()));


            for (int i = 0; i < lines.Count; i++)
            {
                var rowValues = lines[i].Trim().Split(',', ';', '\t');
            }
        }

        #endregion


        #region Selection Handling

        private bool IsSelectingRange;

        private void UpdateDragSelection()
        {
            var mousePos = PointToClient(MousePosition);
            var curCell = GetCellAddressFromPosition(mousePos);

            var end = SelectionEnd ?? SelectionStart.Value;
            if (!end.Equals(curCell))
            {
                SelectionEnd = curCell;
                FocusedCell = curCell;
                Invalidate();
            }
        }

        public bool IsInSelection(int x, int y)
        {
            if (SelectionStart != null)
            {
                var start = SelectionStart.Value;
                var end = SelectionEnd ?? SelectionStart.Value;

                int minX = Math.Min(start.X, end.X);
                int maxX = Math.Max(start.X, end.X);
                int minY = Math.Min(start.Y, end.Y);
                int maxY = Math.Max(start.Y, end.Y);

                return x >= minX && x <= maxX && y >= minY && y <= maxY;
            }

            return false;
        }

        #endregion

        #region ScrollBars

        private HScrollBar HScrollBar;
        private VScrollBar VScrollBar;
        private bool InternalScroll;
        private bool AutoScrollTimerActive;
        private Point AutoScrollSpeed;

        private void InitScrollBars()
        {
            HScrollBar = new HScrollBar()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right,
                Left = 0,
                Top = Height - SystemInformation.HorizontalScrollBarHeight,
                Width = Width,
                SmallChange = 1,
                LargeChange = 3,
                Visible = false
            };
            HScrollBar.Scroll += HScrollBar_Scroll;
            Controls.Add(HScrollBar);


            VScrollBar = new VScrollBar()
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                Top = 0,
                Left = Width - SystemInformation.VerticalScrollBarWidth,
                Height = Height,
                SmallChange = 1,
                LargeChange = 3,
                Visible = false
            };
            VScrollBar.Scroll += VScrollBar_Scroll;
            Controls.Add(VScrollBar);

            AutoScrollSpeed = new Point(1, 1);
            SelectionScrollTimer = new System.Threading.Timer(OnSelectionScroll);
            SelectionScrollTimer.Change(System.Threading.Timeout.Infinite, 500);
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (InternalScroll)
                return;
            ScrollOffset.X = e.NewValue;
            Invalidate();
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (InternalScroll)
                return;
            ScrollOffset.Y = e.NewValue;
            Invalidate();
        }

        private void EnableSelectionAutoScroll()
        {
            if (!AutoScrollTimerActive)
            {
                SelectionScrollTimer.Change(200, 120);
                AutoScrollTimerActive = true;
            }
        }

        private void DisableSelectionAutoScroll()
        {
            if (AutoScrollTimerActive)
            {
                SelectionScrollTimer.Change(System.Threading.Timeout.Infinite, 150);
                AutoScrollTimerActive = false;
            }
        }

        private void OnSelectionScroll(object state)
        {
            BeginInvoke((Action)(() =>
            {
                var mousePos = PointToClient(MousePosition);
                var oldOffset = ScrollOffset;

                if (HScrollBar.Visible)
                {
                    int maxScroll = HScrollBar.Maximum - HScrollBar.LargeChange + 1;
                    if (mousePos.X < CellGridBounds.Left && ScrollOffset.X > 0)
                        ScrollOffset.X -= AutoScrollSpeed.X;
                    else if (mousePos.X > CellGridBounds.Right && ScrollOffset.X < maxScroll)
                        ScrollOffset.X += AutoScrollSpeed.X;

                    ScrollOffset.X = Math.Max(0, Math.Min(maxScroll, ScrollOffset.X));
                }

                if (VScrollBar.Visible)
                {
                    int maxScroll = VScrollBar.Maximum - VScrollBar.LargeChange + 1;
                    if (mousePos.Y < CellGridBounds.Top && ScrollOffset.Y > 0)
                        ScrollOffset.Y -= AutoScrollSpeed.Y;
                    else if (mousePos.Y > CellGridBounds.Bottom && ScrollOffset.Y < maxScroll)
                        ScrollOffset.Y += AutoScrollSpeed.Y;

                    ScrollOffset.Y = Math.Max(0, Math.Min(maxScroll, ScrollOffset.Y));
                }

                if (oldOffset != ScrollOffset)
                {
                    UpdateScrollBarsValues();
                    UpdateDragSelection();
                }
            }));
            
        }

        private Size GetVisibleGridSize()
        {
            int visibleCols = Math.Min(StudConnector.ArrayWidth, MaxGridSize.Width);
            int visibleRows = Math.Min(StudConnector.ArrayHeight, MaxGridSize.Height);
            return new Size(visibleCols, visibleRows);
        }

        private void UpdateScrollbars()
        {
            int visibleCols = Math.Min(StudConnector.ArrayWidth, MaxGridSize.Width);
            int visibleRows = Math.Min(StudConnector.ArrayHeight, MaxGridSize.Height);

            InternalScroll = true;
            
            bool hScrollBarVisible = visibleCols < StudConnector.ArrayWidth;
            bool vScrollBarVisible = visibleRows < StudConnector.ArrayHeight;

            HScrollBar.Visible = hScrollBarVisible;
            VScrollBar.Visible = vScrollBarVisible;

            if (hScrollBarVisible)
            {
                int remainingCols = StudConnector.ArrayWidth - visibleCols;
                HScrollBar.Value = 0;
                HScrollBar.Maximum = remainingCols + HScrollBar.LargeChange - 1;
                HScrollBar.Value = ScrollOffset.X;

                HScrollBar.Width = Width - (vScrollBarVisible ? VScrollBar.Width : 0);
                HScrollBar.Top = Height - HScrollBar.Height;    
            }

            if (vScrollBarVisible)
            {
                int remainingRows = StudConnector.ArrayHeight - visibleRows;
                VScrollBar.Value = 0;
                VScrollBar.Maximum = remainingRows + VScrollBar.LargeChange - 1;
                VScrollBar.Value = ScrollOffset.Y;

                VScrollBar.Height   = Height - (hScrollBarVisible ? HScrollBar.Height : 0);
                VScrollBar.Left = Width - VScrollBar.Width;
            }

            InternalScroll = false;
        }

        private void UpdateScrollBarsValues()
        {
            InternalScroll = true;

            if (HScrollBar.Visible)
                HScrollBar.Value = ScrollOffset.X;
            if (VScrollBar.Visible)
                VScrollBar.Value = ScrollOffset.Y;

            InternalScroll = false;
        }

        private void CheckSelectionAutoScroll()
        {
            var mousePos = PointToClient(MousePosition);

            if ((HScrollBar.Visible || VScrollBar.Visible) &&
                !CellGridBounds.Contains(mousePos))
            {
                AutoScrollSpeed = new Point(1, 1);

                int distX = mousePos.X >= CellGridBounds.Right ?
                    mousePos.X - CellGridBounds.Right :
                    CellGridBounds.Left - mousePos.X;
                int distY = mousePos.Y >= CellGridBounds.Bottom ?
                    mousePos.Y - CellGridBounds.Bottom :
                    CellGridBounds.Top - mousePos.Y;

                AutoScrollSpeed.X = (int)(distX / (double)(GridCellSize.Width));
                AutoScrollSpeed.X = Math.Max(1, AutoScrollSpeed.X);

                AutoScrollSpeed.Y = (int)(distY / (GridCellSize.Height * 1.5d));
                AutoScrollSpeed.Y = Math.Max(1, AutoScrollSpeed.Y);

                EnableSelectionAutoScroll();
            }
            else if (AutoScrollTimerActive)
                DisableSelectionAutoScroll();

        }

        private void ScrollIntoView(Point cellPos)
        {
            var gridSize = GetVisibleGridSize();

            var adjustedScroll = ScrollOffset;

            while ( cellPos.X < adjustedScroll.X || 
                    cellPos.X >= gridSize.Width + adjustedScroll.X ||
                    cellPos.Y < adjustedScroll.Y || 
                    cellPos.Y >= gridSize.Height + adjustedScroll.Y)
            {
                if (cellPos.X < adjustedScroll.X)
                    adjustedScroll.X--;
                else if (cellPos.X >= gridSize.Width + adjustedScroll.X)
                    adjustedScroll.X++;

                if (cellPos.Y < adjustedScroll.Y)
                    adjustedScroll.Y--;
                else if (cellPos.Y >= gridSize.Height + adjustedScroll.Y)
                    adjustedScroll.Y++;
            }

            if (adjustedScroll != ScrollOffset)
            {
                ScrollOffset = adjustedScroll;
                UpdateScrollBarsValues();
                Invalidate();
            }
        }

        #endregion

        #region Edit ComboBox Handling

        private ComboBox EditCombo;
        private bool IsEditingNode;
        private bool NodeValueChanged;
        private bool LoadingCombobox;
        private bool IsCancelingEdit;

        class NodeTypeInfo
        {
            public string ID { get; set; }
            public int Affinity { get; set; }

            public NodeTypeInfo(string iD, int affinity)
            {
                ID = iD;
                Affinity = affinity;
            }
        }

        private void InitEditCombo()
        {
            EditCombo = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDown,
                Visible = false,
                FlatStyle = FlatStyle.Flat,
                AutoSize = false
            };

            Controls.Add(EditCombo);

            var nodeTypes = new List<NodeTypeInfo>()
            {
                new NodeTypeInfo("0:4", 0),
                new NodeTypeInfo("1:4", 0),
                new NodeTypeInfo("2:4", 0),
                new NodeTypeInfo("3:4", 0),
                new NodeTypeInfo("4:4", 0),
                new NodeTypeInfo("5:4", 1),
                new NodeTypeInfo("7:4", 1),
                new NodeTypeInfo("8:4", 1),
                new NodeTypeInfo("9:4", 1),
                new NodeTypeInfo("10:4", 1),
                new NodeTypeInfo("11:4", 1),
                new NodeTypeInfo("12:4", 1),
                new NodeTypeInfo("13:4", 1),
                new NodeTypeInfo("14:4", 1),
                new NodeTypeInfo("15:4", 1),
                new NodeTypeInfo("16:4", 1),
                new NodeTypeInfo("17:4", 1),
                new NodeTypeInfo("18:1", 0),
                new NodeTypeInfo("18:2", 0),
                new NodeTypeInfo("18:3", 0),
                new NodeTypeInfo("18:4", 0),
                new NodeTypeInfo("19:4", 0),
                new NodeTypeInfo("21:4", 1),
                new NodeTypeInfo("22:1", 1),
                new NodeTypeInfo("22:2", 1),
                new NodeTypeInfo("22:3", 1),
                new NodeTypeInfo("23:4", 2),
                new NodeTypeInfo("24:4", 1),
                new NodeTypeInfo("25:4", 0),
                new NodeTypeInfo("26:4", 0),
                new NodeTypeInfo("27:4", 1),
                new NodeTypeInfo("28:4", 1),
                new NodeTypeInfo("29:0", 2),
            };

            EditCombo.DataSource = nodeTypes;
            EditCombo.DisplayMember = "ID";
            EditCombo.Validating += EditCombo_Validating;
            EditCombo.Validated += EditCombo_Validated;
            EditCombo.LostFocus += EditCombo_LostFocus;
            EditCombo.TextChanged += EditCombo_TextChanged;
            EditCombo.SelectedValueChanged += EditCombo_SelectedValueChanged;
            EditCombo.KeyDown += EditCombo_KeyDown;
        }

        private bool BeginEditNode(Custom2DFieldNode node)
        {
            if (IsEditingNode && !FinishEditNode())
                return false;

            SelectionStart = new Point(node.X, node.Y);
            SelectionEnd = null;
            IsSelectingRange = false;

            if (node != SelectedNode)
                FocusedCell = SelectionStart;

            ScrollIntoView(FocusedCell.Value);

            var rectangle = GetCellRect(node.X, node.Y);

            LoadingCombobox = true;
            
            EditCombo.Width = rectangle.Width - 1;
            EditCombo.Left = rectangle.X + 1;
            EditCombo.Top = rectangle.Y + (rectangle.Height - EditCombo.Height) / 2;
            EditCombo.Visible = true;

            EditCombo.SelectedIndex = -1;
            EditCombo.Text = node.ToString();
            EditCombo.Focus();
            EditCombo.SelectionStart = 0;
            EditCombo.SelectionLength = 0;

            LoadingCombobox = false;

            IsEditingNode = true;

            Invalidate();

            return true;
        }

        private void CancelEditNode()
        {
            if (IsEditingNode || EditCombo.Visible)
            {
                IsCancelingEdit = true;
                Focus();
                EditCombo.Hide();
                IsCancelingEdit = false;
                IsEditingNode = false;
                NodeValueChanged = false;
            }
        }

        private bool FinishEditNode()
        {
            if (IsEditingNode || EditCombo.Visible)
            {
                if (!EditCombo.Focused)
                    EditCombo.Focus();
                this.Focus();
                if (NodeValueChanged)
                    return false;
            }

            if (EditCombo.Visible)
            {
                EditCombo.Visible = false;
            }
            return true;
        }

        private void EditCombo_TextChanged(object sender, EventArgs e)
        {
            if (IsEditingNode && !LoadingCombobox)
            {
                NodeValueChanged = true;
            }
        }

        private void EditCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (IsEditingNode && !LoadingCombobox)
            {
                NodeValueChanged = true;
            }
        }

        private void EditCombo_Validating(object sender, CancelEventArgs e)
        {
            if (IsCancelingEdit)
                return;

            if (!Custom2DFieldConnector.TryParseNode(EditCombo.Text))
            {
                e.Cancel = true;
            }
        }

        private void EditCombo_Validated(object sender, EventArgs e)
        {
            if (IsEditingNode && SelectedNode != null && !IsCancelingEdit)
            {
                SelectedNode.Parse(EditCombo.Text);
                NodeValueChanged = false;
            }
        }

        private void EditCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && IsEditingNode)
            {
                CancelEditNode();
            }
        }

        private void EditCombo_LostFocus(object sender, EventArgs e)
        {
            if (!NodeValueChanged && IsEditingNode)
            {
                EditCombo.Visible = false;
                IsEditingNode = false;
                NodeValueChanged = false;
            }
        }

        #endregion

        private Point GetCellAddressFromPosition(Point point)
        {
            point.X -= CellGridBounds.Left;
            point.Y -= CellGridBounds.Top;
            point.X += ScrollOffset.X * GridCellSize.Width;
            point.Y += ScrollOffset.Y * GridCellSize.Height;
            int x = point.X / GridCellSize.Width;
            int y = point.Y / GridCellSize.Height;
            return new Point(x, y);
        }

        private Custom2DFieldNode GetNodeFromCell(Point? cellPos)
        {
            if (cellPos.HasValue)
                return GetNodeFromCell(cellPos.Value);
            return null;
        }

        private Custom2DFieldNode GetNodeFromCell(Point cellPos)
        {
            return StudConnector.GetNode(cellPos.X, cellPos.Y);
        }

        private Custom2DFieldNode GetNodeFromPosition(Point point)
        {
            var cellPos = GetCellAddressFromPosition(point);
            return GetNodeFromCell(cellPos);
        }

        private Rectangle GetCellRect(int x, int y)
        {
            int offsetX = (ScrollOffset.X * GridCellSize.Width) + CellGridBounds.Left;
            int offsetY = (ScrollOffset.Y * GridCellSize.Height) + CellGridBounds.Top;
            return new Rectangle(
                        (GridCellSize.Width * x) - offsetX, (GridCellSize.Height * y) - offsetY,
                        GridCellSize.Width, GridCellSize.Height);
        }
    
        public IEnumerable<Custom2DFieldNode> GetSelectedNodes()
        {
            if (SelectionStart != null)
            {
                var start = SelectionStart.Value;
                var end = SelectionEnd ?? SelectionStart.Value;
                int minX = Math.Min(start.X, end.X);
                int maxX = Math.Max(start.X, end.X);
                int minY = Math.Min(start.Y, end.Y);
                int maxY = Math.Max(start.Y, end.Y);

                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        yield return StudConnector.GetNode(x, y);
                    }
                }
            }

            yield break;
        }
    }
}
