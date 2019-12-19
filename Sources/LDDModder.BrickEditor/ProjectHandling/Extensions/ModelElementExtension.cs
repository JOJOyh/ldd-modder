﻿using LDDModder.BrickEditor.ProjectHandling;
using LDDModder.Modding.Editing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LDDModder.BrickEditor.ProjectHandling
{
    public class ModelElementExtension : IElementExtender/* : INotifyPropertyChanged*/
    {
        public IProjectManager Manager { get; internal set; }

        public PartElement Element { get; }

        private bool _IsHidden;
        private bool _IsVisible;
        private bool visbilityDirty;

        public bool IsHidden
        {
            get => _IsHidden;
            set
            {
                if (_IsHidden != value)
                {
                    _IsHidden = value;
                    InvalidateVisibility();
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                if (visbilityDirty)
                    CalculateVisibility();
                return _IsVisible;
            }
            //protected set => _IsVisible = value;
        }

        public bool HasInitialized { get; private set; }

        public event EventHandler VisibilityChanged;

        public ModelElementExtension(PartElement element)
        {
            Element = element;
            _IsVisible = true;
            visbilityDirty = true;
        }

        internal void AssignManager(IProjectManager manager)
        {
            if (Manager != manager)
            {
                Manager = manager;
                visbilityDirty = true;
            }
        }

        public bool IsParentVisible()
        {
            if (Element.Parent != null)
            {
                var parentExt = Element.Parent.GetExtension<ModelElementExtension>();
                if (parentExt != null)
                    return parentExt.IsVisible;
            }
            return true;
        }

        public bool IsHiddenByConfigs()
        {
            if (Manager != null)
            {
                if (Element is PartSurface)
                    return !Manager.ShowPartModels;

                if (Element is PartCollision)
                    return !Manager.ShowCollisions;

                if (Element is PartConnection)
                    return !Manager.ShowConnections;
            }

            return false;
        }

        public virtual bool IsHiddenByParent()
        {
            if (Element.Parent != null)
            {
                var parentExt = Element.Parent.GetExtension<ModelElementExtension>();
                if (parentExt != null)
                    return parentExt.OverrideChildVisibility(Element);
            }
            return false;
        }

        protected virtual bool OverrideChildVisibility(PartElement element)
        {
            return false;
        }

        public bool IsHiddenOverride()
        {
            bool isParentVisible = IsParentVisible();
            bool isHiddenByConfigs = IsHiddenByConfigs();
            bool isHiddenByParent = IsHiddenByParent();

            return !isParentVisible || isHiddenByConfigs || isHiddenByParent;
        }

        public void CalculateVisibility()
        {
            bool wasVisible = _IsVisible;

            _IsVisible = !IsHidden && !IsHiddenOverride();
            
            visbilityDirty = false;

            if (_IsVisible != wasVisible)
                VisibilityChanged?.Invoke(this, EventArgs.Empty);
        }

        public void InvalidateVisibility(bool childrensOnly = false)
        {
            foreach (var element in Element.GetChildsHierarchy(!childrensOnly))
            {
                var modelExt = element.GetExtension<ModelElementExtension>();
                if (modelExt != null)
                    modelExt.visbilityDirty = true;
            }
        }
    }
}