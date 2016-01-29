//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using NMF.Collections.Generic;
using NMF.Collections.ObjectModel;
using NMF.Expressions;
using NMF.Expressions.Linq;
using NMF.Models;
using NMF.Models.Collections;
using NMF.Serialization;
using NMF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace NMF.Models.Meta
{
    
    
    /// <summary>
    /// The representation of the Operation class
    /// </summary>
    [XmlNamespaceAttribute("http://nmf.codeplex.com/nmeta/")]
    [XmlNamespacePrefixAttribute("nmeta")]
    [ModelRepresentationClassAttribute("http://nmf.codeplex.com/nmeta/#//Operation/")]
    [DebuggerDisplayAttribute("Operation {Name}")]
    public class Operation : TypedElement, IOperation, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Parameters property
        /// </summary>
        private OperationParametersCollection _parameters;
        
        /// <summary>
        /// The backing field for the Refines property
        /// </summary>
        private IOperation _refines;
        
        public Operation()
        {
            this._parameters = new OperationParametersCollection(this);
            this._parameters.CollectionChanged += this.ParametersCollectionChanged;
        }
        
        /// <summary>
        /// The Parameters property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public virtual ICollectionExpression<IParameter> Parameters
        {
            get
            {
                return this._parameters;
            }
        }
        
        /// <summary>
        /// The DeclaringType property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        public virtual IStructuredType DeclaringType
        {
            get
            {
                return ModelHelper.CastAs<IStructuredType>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// The Refines property
        /// </summary>
        [XmlAttributeAttribute(true)]
        public virtual IOperation Refines
        {
            get
            {
                return this._refines;
            }
            set
            {
                if ((this._refines != value))
                {
                    IOperation old = this._refines;
                    this._refines = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetRefines;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetRefines;
                    }
                    this.OnPropertyChanged("Refines");
                    this.OnRefinesChanged(new ValueChangedEventArgs(old, value));
                }
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new OperationChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new OperationReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets fired when the DeclaringType property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> DeclaringTypeChanged;
        
        /// <summary>
        /// Gets fired when the Refines property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> RefinesChanged;
        
        /// <summary>
        /// Forwards change notifications for the Parameters property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ParametersCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Parameters", e);
        }
        
        /// <summary>
        /// Raises the DeclaringTypeChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnDeclaringTypeChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.DeclaringTypeChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Gets called when the parent model element of the current model element changes
        /// </summary>
        /// <param name="oldParent">The old parent model element</param>
        /// <param name="newParent">The new parent model element</param>
        protected override void OnParentChanged(IModelElement newParent, IModelElement oldParent)
        {
            IStructuredType oldDeclaringType = ModelHelper.CastAs<IStructuredType>(oldParent);
            IStructuredType newDeclaringType = ModelHelper.CastAs<IStructuredType>(newParent);
            if ((oldDeclaringType != null))
            {
                oldDeclaringType.Operations.Remove(this);
            }
            if ((newDeclaringType != null))
            {
                newDeclaringType.Operations.Add(this);
            }
            this.OnPropertyChanged("DeclaringType");
            this.OnDeclaringTypeChanged(new ValueChangedEventArgs(oldDeclaringType, newDeclaringType));
        }
        
        /// <summary>
        /// Raises the RefinesChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnRefinesChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.RefinesChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Refines property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetRefines(object sender, EventArgs eventArgs)
        {
            this.Refines = null;
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of the current model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            return NMF.Models.Repository.MetaRepository.Instance.ResolveClass("http://nmf.codeplex.com/nmeta/#//Operation/");
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Operation class
        /// </summary>
        public class OperationChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Operation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public OperationChildrenCollection(Operation parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    count = (count + this._parent.Parameters.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Parameters.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Parameters.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IParameter parametersCasted = item.As<IParameter>();
                if ((parametersCasted != null))
                {
                    this._parent.Parameters.Add(parametersCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Parameters.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Parameters.Contains(item))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                IEnumerator<IModelElement> parametersEnumerator = this._parent.Parameters.GetEnumerator();
                try
                {
                    for (
                    ; parametersEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = parametersEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    parametersEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IParameter parameterItem = item.As<IParameter>();
                if (((parameterItem != null) 
                            && this._parent.Parameters.Remove(parameterItem)))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Parameters).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Operation class
        /// </summary>
        public class OperationReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Operation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public OperationReferencedElementsCollection(Operation parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    count = (count + this._parent.Parameters.Count);
                    if ((this._parent.DeclaringType != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Refines != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Parameters.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.DeclaringTypeChanged += this.PropagateValueChanges;
                this._parent.RefinesChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Parameters.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.DeclaringTypeChanged -= this.PropagateValueChanges;
                this._parent.RefinesChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IParameter parametersCasted = item.As<IParameter>();
                if ((parametersCasted != null))
                {
                    this._parent.Parameters.Add(parametersCasted);
                }
                if ((this._parent.DeclaringType == null))
                {
                    IStructuredType declaringTypeCasted = item.As<IStructuredType>();
                    if ((declaringTypeCasted != null))
                    {
                        this._parent.DeclaringType = declaringTypeCasted;
                        return;
                    }
                }
                if ((this._parent.Refines == null))
                {
                    IOperation refinesCasted = item.As<IOperation>();
                    if ((refinesCasted != null))
                    {
                        this._parent.Refines = refinesCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Parameters.Clear();
                this._parent.DeclaringType = null;
                this._parent.Refines = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Parameters.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.DeclaringType))
                {
                    return true;
                }
                if ((item == this._parent.Refines))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                IEnumerator<IModelElement> parametersEnumerator = this._parent.Parameters.GetEnumerator();
                try
                {
                    for (
                    ; parametersEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = parametersEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    parametersEnumerator.Dispose();
                }
                if ((this._parent.DeclaringType != null))
                {
                    array[arrayIndex] = this._parent.DeclaringType;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Refines != null))
                {
                    array[arrayIndex] = this._parent.Refines;
                    arrayIndex = (arrayIndex + 1);
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IParameter parameterItem = item.As<IParameter>();
                if (((parameterItem != null) 
                            && this._parent.Parameters.Remove(parameterItem)))
                {
                    return true;
                }
                if ((this._parent.DeclaringType == item))
                {
                    this._parent.DeclaringType = null;
                    return true;
                }
                if ((this._parent.Refines == item))
                {
                    this._parent.Refines = null;
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Parameters).Concat(this._parent.DeclaringType).Concat(this._parent.Refines).GetEnumerator();
            }
        }
    }
}
