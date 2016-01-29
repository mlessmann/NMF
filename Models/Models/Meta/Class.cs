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
    /// Represents a group of instances with common properties like attributes or references
    /// </summary>
    [XmlNamespaceAttribute("http://nmf.codeplex.com/nmeta/")]
    [XmlNamespacePrefixAttribute("nmeta")]
    [ModelRepresentationClassAttribute("http://nmf.codeplex.com/nmeta/#//Class/")]
    [DebuggerDisplayAttribute("Class {Name}")]
    public class Class : ReferenceType, IClass, IModelElement
    {
        
        /// <summary>
        /// The backing field for the IsInterface property
        /// </summary>
        private bool _isInterface = false;
        
        /// <summary>
        /// The backing field for the IsAbstract property
        /// </summary>
        private bool _isAbstract = false;
        
        /// <summary>
        /// The backing field for the BaseTypes property
        /// </summary>
        private ObservableAssociationList<IClass> _baseTypes;
        
        /// <summary>
        /// The backing field for the InstanceOf property
        /// </summary>
        private IClass _instanceOf;
        
        /// <summary>
        /// The backing field for the Identifier property
        /// </summary>
        private IAttribute _identifier;
        
        /// <summary>
        /// The backing field for the AttributeConstraints property
        /// </summary>
        private ClassAttributeConstraintsCollection _attributeConstraints;
        
        /// <summary>
        /// The backing field for the ReferenceConstraints property
        /// </summary>
        private ClassReferenceConstraintsCollection _referenceConstraints;
        
        public Class()
        {
            this._baseTypes = new ObservableAssociationList<IClass>();
            this._baseTypes.CollectionChanged += this.BaseTypesCollectionChanged;
            this._attributeConstraints = new ClassAttributeConstraintsCollection(this);
            this._attributeConstraints.CollectionChanged += this.AttributeConstraintsCollectionChanged;
            this._referenceConstraints = new ClassReferenceConstraintsCollection(this);
            this._referenceConstraints.CollectionChanged += this.ReferenceConstraintsCollectionChanged;
        }
        
        /// <summary>
        /// Determines whether this class is an interface
        /// </summary>
        [DefaultValueAttribute(false)]
        [XmlAttributeAttribute(true)]
        public virtual bool IsInterface
        {
            get
            {
                return this._isInterface;
            }
            set
            {
                if ((value != this._isInterface))
                {
                    this._isInterface = value;
                    this.OnIsInterfaceChanged(EventArgs.Empty);
                    this.OnPropertyChanged("IsInterface");
                }
            }
        }
        
        /// <summary>
        /// The IsAbstract property
        /// </summary>
        [DefaultValueAttribute(false)]
        [XmlAttributeAttribute(true)]
        public virtual bool IsAbstract
        {
            get
            {
                return this._isAbstract;
            }
            set
            {
                if ((value != this._isAbstract))
                {
                    this._isAbstract = value;
                    this.OnIsAbstractChanged(EventArgs.Empty);
                    this.OnPropertyChanged("IsAbstract");
                }
            }
        }
        
        /// <summary>
        /// The BaseTypes property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlAttributeAttribute(true)]
        public virtual ICollectionExpression<IClass> BaseTypes
        {
            get
            {
                return this._baseTypes;
            }
        }
        
        /// <summary>
        /// The InstanceOf property
        /// </summary>
        [XmlAttributeAttribute(true)]
        public virtual IClass InstanceOf
        {
            get
            {
                return this._instanceOf;
            }
            set
            {
                if ((this._instanceOf != value))
                {
                    IClass old = this._instanceOf;
                    this._instanceOf = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetInstanceOf;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetInstanceOf;
                    }
                    this.OnPropertyChanged("InstanceOf");
                    this.OnInstanceOfChanged(new ValueChangedEventArgs(old, value));
                }
            }
        }
        
        /// <summary>
        /// The Identifier property
        /// </summary>
        [XmlAttributeAttribute(true)]
        public virtual IAttribute Identifier
        {
            get
            {
                return this._identifier;
            }
            set
            {
                if ((this._identifier != value))
                {
                    IAttribute old = this._identifier;
                    this._identifier = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetIdentifier;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetIdentifier;
                    }
                    this.OnPropertyChanged("Identifier");
                    this.OnIdentifierChanged(new ValueChangedEventArgs(old, value));
                }
            }
        }
        
        /// <summary>
        /// The AttributeConstraints property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public virtual ICollectionExpression<IAttributeConstraint> AttributeConstraints
        {
            get
            {
                return this._attributeConstraints;
            }
        }
        
        /// <summary>
        /// The ReferenceConstraints property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public virtual ICollectionExpression<IReferenceConstraint> ReferenceConstraints
        {
            get
            {
                return this._referenceConstraints;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new ClassChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new ClassReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets fired when the IsInterface property changed its value
        /// </summary>
        public event EventHandler IsInterfaceChanged;
        
        /// <summary>
        /// Gets fired when the IsAbstract property changed its value
        /// </summary>
        public event EventHandler IsAbstractChanged;
        
        /// <summary>
        /// Gets fired when the InstanceOf property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> InstanceOfChanged;
        
        /// <summary>
        /// Gets fired when the Identifier property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> IdentifierChanged;
        
        /// <summary>
        /// Raises the IsInterfaceChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsInterfaceChanged(EventArgs eventArgs)
        {
            EventHandler handler = this.IsInterfaceChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsAbstractChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsAbstractChanged(EventArgs eventArgs)
        {
            EventHandler handler = this.IsAbstractChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Forwards change notifications for the BaseTypes property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void BaseTypesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("BaseTypes", e);
        }
        
        /// <summary>
        /// Raises the InstanceOfChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnInstanceOfChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.InstanceOfChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the InstanceOf property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetInstanceOf(object sender, EventArgs eventArgs)
        {
            this.InstanceOf = null;
        }
        
        /// <summary>
        /// Raises the IdentifierChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIdentifierChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.IdentifierChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Identifier property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetIdentifier(object sender, EventArgs eventArgs)
        {
            this.Identifier = null;
        }
        
        /// <summary>
        /// Forwards change notifications for the AttributeConstraints property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void AttributeConstraintsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("AttributeConstraints", e);
        }
        
        /// <summary>
        /// Forwards change notifications for the ReferenceConstraints property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ReferenceConstraintsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("ReferenceConstraints", e);
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of the current model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            return NMF.Models.Repository.MetaRepository.Instance.ResolveClass("http://nmf.codeplex.com/nmeta/#//Class/");
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Class class
        /// </summary>
        public class ClassChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Class _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public ClassChildrenCollection(Class parent)
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
                    count = (count + this._parent.AttributeConstraints.Count);
                    count = (count + this._parent.ReferenceConstraints.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.AttributeConstraints.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.ReferenceConstraints.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.AttributeConstraints.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.ReferenceConstraints.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IAttributeConstraint attributeConstraintsCasted = item.As<IAttributeConstraint>();
                if ((attributeConstraintsCasted != null))
                {
                    this._parent.AttributeConstraints.Add(attributeConstraintsCasted);
                }
                IReferenceConstraint referenceConstraintsCasted = item.As<IReferenceConstraint>();
                if ((referenceConstraintsCasted != null))
                {
                    this._parent.ReferenceConstraints.Add(referenceConstraintsCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.AttributeConstraints.Clear();
                this._parent.ReferenceConstraints.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.AttributeConstraints.Contains(item))
                {
                    return true;
                }
                if (this._parent.ReferenceConstraints.Contains(item))
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
                IEnumerator<IModelElement> attributeConstraintsEnumerator = this._parent.AttributeConstraints.GetEnumerator();
                try
                {
                    for (
                    ; attributeConstraintsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = attributeConstraintsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    attributeConstraintsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> referenceConstraintsEnumerator = this._parent.ReferenceConstraints.GetEnumerator();
                try
                {
                    for (
                    ; referenceConstraintsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = referenceConstraintsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    referenceConstraintsEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IAttributeConstraint attributeConstraintItem = item.As<IAttributeConstraint>();
                if (((attributeConstraintItem != null) 
                            && this._parent.AttributeConstraints.Remove(attributeConstraintItem)))
                {
                    return true;
                }
                IReferenceConstraint referenceConstraintItem = item.As<IReferenceConstraint>();
                if (((referenceConstraintItem != null) 
                            && this._parent.ReferenceConstraints.Remove(referenceConstraintItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.AttributeConstraints).Concat(this._parent.ReferenceConstraints).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Class class
        /// </summary>
        public class ClassReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Class _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public ClassReferencedElementsCollection(Class parent)
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
                    count = (count + this._parent.BaseTypes.Count);
                    if ((this._parent.InstanceOf != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Identifier != null))
                    {
                        count = (count + 1);
                    }
                    count = (count + this._parent.AttributeConstraints.Count);
                    count = (count + this._parent.ReferenceConstraints.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.BaseTypes.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.InstanceOfChanged += this.PropagateValueChanges;
                this._parent.IdentifierChanged += this.PropagateValueChanges;
                this._parent.AttributeConstraints.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.ReferenceConstraints.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.BaseTypes.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.InstanceOfChanged -= this.PropagateValueChanges;
                this._parent.IdentifierChanged -= this.PropagateValueChanges;
                this._parent.AttributeConstraints.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.ReferenceConstraints.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IClass baseTypesCasted = item.As<IClass>();
                if ((baseTypesCasted != null))
                {
                    this._parent.BaseTypes.Add(baseTypesCasted);
                }
                if ((this._parent.InstanceOf == null))
                {
                    IClass instanceOfCasted = item.As<IClass>();
                    if ((instanceOfCasted != null))
                    {
                        this._parent.InstanceOf = instanceOfCasted;
                        return;
                    }
                }
                if ((this._parent.Identifier == null))
                {
                    IAttribute identifierCasted = item.As<IAttribute>();
                    if ((identifierCasted != null))
                    {
                        this._parent.Identifier = identifierCasted;
                        return;
                    }
                }
                IAttributeConstraint attributeConstraintsCasted = item.As<IAttributeConstraint>();
                if ((attributeConstraintsCasted != null))
                {
                    this._parent.AttributeConstraints.Add(attributeConstraintsCasted);
                }
                IReferenceConstraint referenceConstraintsCasted = item.As<IReferenceConstraint>();
                if ((referenceConstraintsCasted != null))
                {
                    this._parent.ReferenceConstraints.Add(referenceConstraintsCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.BaseTypes.Clear();
                this._parent.InstanceOf = null;
                this._parent.Identifier = null;
                this._parent.AttributeConstraints.Clear();
                this._parent.ReferenceConstraints.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.BaseTypes.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.InstanceOf))
                {
                    return true;
                }
                if ((item == this._parent.Identifier))
                {
                    return true;
                }
                if (this._parent.AttributeConstraints.Contains(item))
                {
                    return true;
                }
                if (this._parent.ReferenceConstraints.Contains(item))
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
                IEnumerator<IModelElement> baseTypesEnumerator = this._parent.BaseTypes.GetEnumerator();
                try
                {
                    for (
                    ; baseTypesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = baseTypesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    baseTypesEnumerator.Dispose();
                }
                if ((this._parent.InstanceOf != null))
                {
                    array[arrayIndex] = this._parent.InstanceOf;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Identifier != null))
                {
                    array[arrayIndex] = this._parent.Identifier;
                    arrayIndex = (arrayIndex + 1);
                }
                IEnumerator<IModelElement> attributeConstraintsEnumerator = this._parent.AttributeConstraints.GetEnumerator();
                try
                {
                    for (
                    ; attributeConstraintsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = attributeConstraintsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    attributeConstraintsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> referenceConstraintsEnumerator = this._parent.ReferenceConstraints.GetEnumerator();
                try
                {
                    for (
                    ; referenceConstraintsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = referenceConstraintsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    referenceConstraintsEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IClass classItem = item.As<IClass>();
                if (((classItem != null) 
                            && this._parent.BaseTypes.Remove(classItem)))
                {
                    return true;
                }
                if ((this._parent.InstanceOf == item))
                {
                    this._parent.InstanceOf = null;
                    return true;
                }
                if ((this._parent.Identifier == item))
                {
                    this._parent.Identifier = null;
                    return true;
                }
                IAttributeConstraint attributeConstraintItem = item.As<IAttributeConstraint>();
                if (((attributeConstraintItem != null) 
                            && this._parent.AttributeConstraints.Remove(attributeConstraintItem)))
                {
                    return true;
                }
                IReferenceConstraint referenceConstraintItem = item.As<IReferenceConstraint>();
                if (((referenceConstraintItem != null) 
                            && this._parent.ReferenceConstraints.Remove(referenceConstraintItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.BaseTypes).Concat(this._parent.InstanceOf).Concat(this._parent.Identifier).Concat(this._parent.AttributeConstraints).Concat(this._parent.ReferenceConstraints).GetEnumerator();
            }
        }
    }
}
