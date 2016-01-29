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
    /// The representation of the Parameter class
    /// </summary>
    [XmlNamespaceAttribute("http://nmf.codeplex.com/nmeta/")]
    [XmlNamespacePrefixAttribute("nmeta")]
    [ModelRepresentationClassAttribute("http://nmf.codeplex.com/nmeta/#//Parameter/")]
    [DebuggerDisplayAttribute("Parameter {Name}")]
    public class Parameter : TypedElement, IParameter, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Direction property
        /// </summary>
        private Direction _direction;
        
        /// <summary>
        /// The Direction property
        /// </summary>
        [XmlAttributeAttribute(true)]
        public virtual Direction Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                if ((value != this._direction))
                {
                    this._direction = value;
                    this.OnDirectionChanged(EventArgs.Empty);
                    this.OnPropertyChanged("Direction");
                }
            }
        }
        
        /// <summary>
        /// The Operation property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        public virtual IOperation Operation
        {
            get
            {
                return ModelHelper.CastAs<IOperation>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new ParameterReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets fired when the Direction property changed its value
        /// </summary>
        public event EventHandler DirectionChanged;
        
        /// <summary>
        /// Gets fired when the Operation property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> OperationChanged;
        
        /// <summary>
        /// Raises the DirectionChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnDirectionChanged(EventArgs eventArgs)
        {
            EventHandler handler = this.DirectionChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the OperationChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnOperationChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.OperationChanged;
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
            IOperation oldOperation = ModelHelper.CastAs<IOperation>(oldParent);
            IOperation newOperation = ModelHelper.CastAs<IOperation>(newParent);
            if ((oldOperation != null))
            {
                oldOperation.Parameters.Remove(this);
            }
            if ((newOperation != null))
            {
                newOperation.Parameters.Add(this);
            }
            this.OnPropertyChanged("Operation");
            this.OnOperationChanged(new ValueChangedEventArgs(oldOperation, newOperation));
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of the current model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            return NMF.Models.Repository.MetaRepository.Instance.ResolveClass("http://nmf.codeplex.com/nmeta/#//Parameter/");
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Parameter class
        /// </summary>
        public class ParameterReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Parameter _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public ParameterReferencedElementsCollection(Parameter parent)
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
                    if ((this._parent.Operation != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.OperationChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.OperationChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Operation == null))
                {
                    IOperation operationCasted = item.As<IOperation>();
                    if ((operationCasted != null))
                    {
                        this._parent.Operation = operationCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Operation = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Operation))
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
                if ((this._parent.Operation != null))
                {
                    array[arrayIndex] = this._parent.Operation;
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
                if ((this._parent.Operation == item))
                {
                    this._parent.Operation = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Operation).GetEnumerator();
            }
        }
    }
}
