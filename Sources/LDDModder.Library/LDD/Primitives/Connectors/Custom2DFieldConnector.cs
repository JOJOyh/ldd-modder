﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LDDModder.LDD.Primitives.Connectors
{
    public partial class Custom2DFieldConnector : Connector, IEnumerable<Custom2DFieldNode>
    {
        private Custom2DFieldNode[,] _NodeArray;
        private int _Width;
        private int _Height;
        private bool PreventNodeEvents;

        public event PropertyValueChangedEventHandler NodeValueChanged;

        public override ConnectorType Type => ConnectorType.Custom2DField;

        /// <summary>
        /// Width = S * 2, where S: Stud count
        /// </summary>
        public int Width
        {
            get => _Width;
            set
            {
                if ((value % 2) == 0 && value > 0)
                {
                    if (SetPropertyValue(ref _Width, value))
                        Initialize2DArray();
                }
            }
        }

        public int ArrayWidth => Width + 1;

        public int StudWidth
        {
            get => Width / 2;
            set => Width = value * 2;
        }

        /// <summary>
        /// Height = S * 2, where S: Stud count
        /// </summary>
        public int Height
        {
            get => _Height;
            set
            {
                if ((value % 2) == 0 && value > 0)
                {
                    if (SetPropertyValue(ref _Height, value))
                        Initialize2DArray();
                }
            }
        }

        public int ArrayHeight => Height + 1;

        public int StudHeight
        {
            get => Height / 2;
            set => Height = value * 2;
        }

        public Custom2DFieldNode this[int x, int y]
        {
            get => _NodeArray[x, y];
        }

        public Custom2DFieldNode this[int index]
        {
            get
            {
                var pos = IndexToPosition(index);
                return _NodeArray[pos.Item1, pos.Item2];
            }
        }

        public Custom2DFieldNode[,] NodeArray
        {
            get => _NodeArray;
            set => AssignArrayValues(value);
        }

        public Custom2DFieldConnector()
        {
            _Width = 2;
            _Height = 2;
            SubType = 23;
            Initialize2DArray();
        }

        public Custom2DFieldConnector(int subType, int width, int height)
        {
            _Width = (int)(Math.Round(width / 2d) * 2);
            _Height = (int)(Math.Round(height / 2d) * 2);
            SubType = subType;
            Initialize2DArray();
        }

        public int PositionToIndex(int x, int y)
        {
            return (y * ArrayWidth) + x;
        }

        public Tuple<int,int> IndexToPosition(int index)
        {
            int x = index % ArrayWidth;
            //int y = (int)Math.Floor(index / (double)ArrayWidth);
            int y = (index - x) / ArrayWidth;
            return new Tuple<int, int>(x, y);
        }

        public Custom2DFieldNode GetNode(int x, int y)
        {
            if (x >= 0 && x <= Width && y >= 0 && y <= Height)
                return _NodeArray[x, y];
            return null;
        }

        public Custom2DFieldNode GetNode(int index)
        {
            if (index >= 0 && index < (Width + 1) * (Height + 1))
                return this[index];
            return null;
        }

        private void Initialize2DArray()
        {
            var oldValues = _NodeArray;

            if (_NodeArray != null)
                DettachNodeEvents(_NodeArray);

            _NodeArray = new Custom2DFieldNode[Width + 1, Height + 1];
            int nodeIndex = 0;

            for (int y = 0; y < Height + 1; y++)
            {
                for (int x = 0; x < Width + 1; x++)
                {
                    _NodeArray[x, y] = new Custom2DFieldNode(x, y, nodeIndex++);
                }
            }

            if (oldValues != null)
            {
                int oldW = oldValues.GetLength(0);
                int oldH = oldValues.GetLength(1);

                for (int x = 0; x < Math.Min(oldW, Width + 1); x++)
                    for (int y = 0; y < Math.Min(oldH, Height + 1); y++)
                        _NodeArray[x, y].Values = oldValues[x, y].Values;
            }

            AttachNodeEvents(_NodeArray);
        }

        public Custom2DFieldNode[,] GetArrayCopy()
        {
            var nodeArray = new Custom2DFieldNode[Width + 1, Height + 1];
            for (int y = 0; y < ArrayHeight; y++)
            {
                for (int x = 0; x < ArrayWidth; x++)
                {
                    nodeArray[x, y] = _NodeArray[x, y].Clone();
                }
            }
            return nodeArray;
        }

        public void AssignArrayValues(Custom2DFieldNode[,] nodeArray)
        {
            int width = nodeArray.GetLength(0);
            int height = nodeArray.GetLength(1);

            if (width == ArrayWidth && height == ArrayHeight)
            {
                bool hasChanges = false;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (NodeArray[x, y].Values != nodeArray[x, y].Values)
                        {
                            hasChanges = true;
                            break;
                        }
                    }
                }

                if (!hasChanges)
                    return;

                PreventNodeEvents = true;


                var oldValue = GetArrayCopy();



                RaisePropertyValueChanging(new PropertyValueChangingEventArgs(nameof(NodeArray), oldValue, nodeArray));

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                        NodeArray[x, y].Values = nodeArray[x, y].Values;
                }

                RaisePropertyValueChanged(new PropertyValueChangedEventArgs(nameof(NodeArray), oldValue, nodeArray));
                PreventNodeEvents = false;
            }
        }

        private void AttachNodeEvents(Custom2DFieldNode[,] nodeArray)
        {
            int width = nodeArray.GetLength(0);
            int height = nodeArray.GetLength(1);
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodeArray[x, y].PropertyValueChanged += FieldNode_PropertyValueChanged;
                }
            }
        }

        private void DettachNodeEvents(Custom2DFieldNode[,] nodeArray)
        {
            int width = nodeArray.GetLength(0);
            int height = nodeArray.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    nodeArray[x, y].PropertyValueChanged -= FieldNode_PropertyValueChanged;
                }
            }
        }

        private void FieldNode_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            if (PreventNodeEvents)
                return;
            RaiseChildObjectEvent(new ForwardedEventArgs(sender, "PropertyValueChanged", e));
            NodeValueChanged?.Invoke(sender, e);
        }

        public override void LoadFromXml(XElement element)
        {
            base.LoadFromXml(element);
            _Width = element.ReadAttribute<int>("width");
            _Height = element.ReadAttribute<int>("height");
            _NodeArray = null;
            Initialize2DArray();
            int expectedValueCount = (Width + 1) * (Height + 1);

            var values = element.Value.Trim().Split(',').ToList();
            values.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            //if (values.Count != expectedValueCount)
            //    throw new Exception("Unexpected number of values!");

            for (int i = 0; i < values.Count; i++)
            {
                int rowIdx = (int)Math.Floor(i / (double)(Width + 1));
                if (rowIdx > Height + 1)
                    break;
                _NodeArray[i % (Width + 1), rowIdx].Parse(values[i]);
            }
        }

        protected override void SerializeBeforeTransform(XElement element)
        {
            element.Add(new XAttribute("width", Width));
            element.Add(new XAttribute("height", Height));
            string content = string.Empty;
            for (int y = 0; y <= Height; y++)
            {
                string line = Environment.NewLine;
                for (int x = 0; x <= Width; x++)
                    line += _NodeArray[x, y].ToString() + ",";
                content += line;
            }
            content = content.TrimEnd(',') + Environment.NewLine;
            element.Value = content;
        }

        public static bool TryParseNode(string nodeValue)
        {
            string[] values = nodeValue.Trim().Split(':');

            if (values.Length < 2 || values.Length > 3)
                return false;

            if (!int.TryParse(values[0], out _))
                return false;
            if (!int.TryParse(values[1], out _))
                return false;
            if (values.Length == 3)
            {
                if (values.Length == 3 && !int.TryParse(values[2], out _))
                    return false;
            }

            return true;
        }

        public IEnumerator<Custom2DFieldNode> GetEnumerator()
        {
            return _NodeArray.Cast<Custom2DFieldNode>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override Connector Clone()
        {
            var conn = new Custom2DFieldConnector(SubType, Width, Height);
            conn.AssignArrayValues(NodeArray);
            return conn;
        }
    }
}
