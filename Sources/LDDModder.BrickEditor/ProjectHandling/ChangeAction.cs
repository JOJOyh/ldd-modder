﻿using LDDModder.Modding.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LDDModder.BrickEditor.ProjectHandling
{
    public class ChangeAction
    {
        internal long ChangeID;

        public virtual void Undo()
        {

        }

        public virtual void Redo()
        {

        }
    }

    public class CollectionChangeAction : ChangeAction
    {
        public ElementCollectionChangedEventArgs Data { get; }

        public CollectionChangeAction(ElementCollectionChangedEventArgs data)
        {
            Data = data;
        }

        public override void Redo()
        {
            if (Data.Action == System.ComponentModel.CollectionChangeAction.Remove)
            {
                foreach (var item in Data.RemovedElements)
                    Data.Collection.Remove(item);
            }
            else if (Data.Action == System.ComponentModel.CollectionChangeAction.Add)
            {
                foreach (var item in Data.AddedElements)
                    Data.Collection.Add(item);
            }
        }

        public override void Undo()
        {
            if (Data.Action == System.ComponentModel.CollectionChangeAction.Remove)
            {
                foreach (var item in Data.RemovedElements)
                    Data.Collection.Add(item);
            }
            else if (Data.Action == System.ComponentModel.CollectionChangeAction.Add)
            {
                foreach (var item in Data.AddedElements)
                    Data.Collection.Remove(item);
            }
        }
    }

    public class PropertyChangeAction : ChangeAction
    {
        public ElementValueChangedEventArgs Data { get; }

        public PropertyChangeAction(ElementValueChangedEventArgs data)
        {
            Data = data;
        }

        private void AssignPropertyValue(object value)
        {
            object objToAssign = Data.ChildProperty ?? Data.Element;
            var propInfo = objToAssign.GetType().GetProperty(Data.PropertyName);
            if (propInfo != null)
                propInfo.SetValue(objToAssign, value);
        }

        public override void Undo()
        {
            AssignPropertyValue(Data.OldValue);
        }

        public override void Redo()
        {
            AssignPropertyValue(Data.NewValue);
        }
    }

    public class BatchChangeAction : ChangeAction
    {
        public List<ChangeAction> Actions { get; }

        public BatchChangeAction(IEnumerable<ChangeAction> actions)
        {
            Actions = actions.ToList();
        }

        public override void Undo()
        {
            var reversed = Actions.ToList();
            reversed.Reverse();
            foreach (var action in reversed)
                action.Undo();
        }

        public override void Redo()
        {
            foreach (var action in Actions)
                action.Redo();
        }
    }
}