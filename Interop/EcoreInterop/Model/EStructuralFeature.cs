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
using NMF.Models.Expressions;
using NMF.Models.Meta;
using NMF.Serialization;
using NMF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace NMF.Interop.Ecore
{
    
    
    /// <summary>
    /// The default implementation of the EStructuralFeature class
    /// </summary>
    [XmlNamespaceAttribute("http://www.eclipse.org/emf/2002/Ecore")]
    [XmlNamespacePrefixAttribute("ecore")]
    [ModelRepresentationClassAttribute("http://www.eclipse.org/emf/2002/Ecore#//EStructuralFeature/")]
    [DebuggerDisplayAttribute("EStructuralFeature {Name}")]
    public abstract class EStructuralFeature : ETypedElement, IEStructuralFeature, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Changeable property
        /// </summary>
        private Nullable<bool> _changeable;
        
        /// <summary>
        /// The backing field for the Volatile property
        /// </summary>
        private Nullable<bool> _volatile;
        
        /// <summary>
        /// The backing field for the Transient property
        /// </summary>
        private Nullable<bool> _transient;
        
        /// <summary>
        /// The backing field for the DefaultValueLiteral property
        /// </summary>
        private string _defaultValueLiteral;
        
        /// <summary>
        /// The backing field for the Unsettable property
        /// </summary>
        private Nullable<bool> _unsettable;
        
        /// <summary>
        /// The backing field for the Derived property
        /// </summary>
        private Nullable<bool> _derived;
        
        /// <summary>
        /// The changeable property
        /// </summary>
        [XmlElementNameAttribute("changeable")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Changeable
        {
            get
            {
                return this._changeable;
            }
            set
            {
                if ((this._changeable != value))
                {
                    Nullable<bool> old = this._changeable;
                    this._changeable = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnChangeableChanged(e);
                    this.OnPropertyChanged("Changeable", e);
                }
            }
        }
        
        /// <summary>
        /// The volatile property
        /// </summary>
        [XmlElementNameAttribute("volatile")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Volatile
        {
            get
            {
                return this._volatile;
            }
            set
            {
                if ((this._volatile != value))
                {
                    Nullable<bool> old = this._volatile;
                    this._volatile = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnVolatileChanged(e);
                    this.OnPropertyChanged("Volatile", e);
                }
            }
        }
        
        /// <summary>
        /// The transient property
        /// </summary>
        [XmlElementNameAttribute("transient")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Transient
        {
            get
            {
                return this._transient;
            }
            set
            {
                if ((this._transient != value))
                {
                    Nullable<bool> old = this._transient;
                    this._transient = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnTransientChanged(e);
                    this.OnPropertyChanged("Transient", e);
                }
            }
        }
        
        /// <summary>
        /// The defaultValueLiteral property
        /// </summary>
        [XmlElementNameAttribute("defaultValueLiteral")]
        [XmlAttributeAttribute(true)]
        public virtual string DefaultValueLiteral
        {
            get
            {
                return this._defaultValueLiteral;
            }
            set
            {
                if ((this._defaultValueLiteral != value))
                {
                    string old = this._defaultValueLiteral;
                    this._defaultValueLiteral = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnDefaultValueLiteralChanged(e);
                    this.OnPropertyChanged("DefaultValueLiteral", e);
                }
            }
        }
        
        /// <summary>
        /// The unsettable property
        /// </summary>
        [XmlElementNameAttribute("unsettable")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Unsettable
        {
            get
            {
                return this._unsettable;
            }
            set
            {
                if ((this._unsettable != value))
                {
                    Nullable<bool> old = this._unsettable;
                    this._unsettable = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnUnsettableChanged(e);
                    this.OnPropertyChanged("Unsettable", e);
                }
            }
        }
        
        /// <summary>
        /// The derived property
        /// </summary>
        [XmlElementNameAttribute("derived")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Derived
        {
            get
            {
                return this._derived;
            }
            set
            {
                if ((this._derived != value))
                {
                    Nullable<bool> old = this._derived;
                    this._derived = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnDerivedChanged(e);
                    this.OnPropertyChanged("Derived", e);
                }
            }
        }
        
        /// <summary>
        /// The eContainingClass property
        /// </summary>
        [XmlElementNameAttribute("eContainingClass")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("eStructuralFeatures")]
        public virtual IEClass EContainingClass
        {
            get
            {
                return ModelHelper.CastAs<IEClass>(this.Parent);
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
                return base.ReferencedElements.Concat(new EStructuralFeatureReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                return (IClass)NMF.Models.Repository.MetaRepository.Instance.ResolveType("http://www.eclipse.org/emf/2002/Ecore#//EStructuralFeature/");
            }
        }
        
        /// <summary>
        /// Gets fired when the Changeable property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ChangeableChanged;
        
        /// <summary>
        /// Gets fired when the Volatile property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> VolatileChanged;
        
        /// <summary>
        /// Gets fired when the Transient property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> TransientChanged;
        
        /// <summary>
        /// Gets fired when the DefaultValueLiteral property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> DefaultValueLiteralChanged;
        
        /// <summary>
        /// Gets fired when the Unsettable property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> UnsettableChanged;
        
        /// <summary>
        /// Gets fired when the Derived property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> DerivedChanged;
        
        /// <summary>
        /// Gets fired when the EContainingClass property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> EContainingClassChanged;
        
        /// <summary>
        /// Raises the ChangeableChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnChangeableChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.ChangeableChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the VolatileChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnVolatileChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.VolatileChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the TransientChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnTransientChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.TransientChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the DefaultValueLiteralChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnDefaultValueLiteralChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.DefaultValueLiteralChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the UnsettableChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnUnsettableChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.UnsettableChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the DerivedChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnDerivedChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.DerivedChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the EContainingClassChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnEContainingClassChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.EContainingClassChanged;
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
            IEClass oldEContainingClass = ModelHelper.CastAs<IEClass>(oldParent);
            IEClass newEContainingClass = ModelHelper.CastAs<IEClass>(newParent);
            if ((oldEContainingClass != null))
            {
                oldEContainingClass.EStructuralFeatures.Remove(this);
            }
            if ((newEContainingClass != null))
            {
                newEContainingClass.EStructuralFeatures.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldEContainingClass, newEContainingClass);
            this.OnEContainingClassChanged(e);
            this.OnPropertyChanged("EContainingClass", e);
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "CHANGEABLE"))
            {
                return this.Changeable;
            }
            if ((attribute == "VOLATILE"))
            {
                return this.Volatile;
            }
            if ((attribute == "TRANSIENT"))
            {
                return this.Transient;
            }
            if ((attribute == "DEFAULTVALUELITERAL"))
            {
                return this.DefaultValueLiteral;
            }
            if ((attribute == "UNSETTABLE"))
            {
                return this.Unsettable;
            }
            if ((attribute == "DERIVED"))
            {
                return this.Derived;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "ECONTAININGCLASS"))
            {
                this.EContainingClass = ((IEClass)(value));
                return;
            }
            if ((feature == "CHANGEABLE"))
            {
                this.Changeable = ((bool)(value));
                return;
            }
            if ((feature == "VOLATILE"))
            {
                this.Volatile = ((bool)(value));
                return;
            }
            if ((feature == "TRANSIENT"))
            {
                this.Transient = ((bool)(value));
                return;
            }
            if ((feature == "DEFAULTVALUELITERAL"))
            {
                this.DefaultValueLiteral = ((string)(value));
                return;
            }
            if ((feature == "UNSETTABLE"))
            {
                this.Unsettable = ((bool)(value));
                return;
            }
            if ((feature == "DERIVED"))
            {
                this.Derived = ((bool)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the property expression for the given attribute
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="attribute">The requested attribute in upper case</param>
        protected override NMF.Expressions.INotifyExpression<object> GetExpressionForAttribute(string attribute)
        {
            if ((attribute == "ECONTAININGCLASS"))
            {
                return new EContainingClassProxy(this);
            }
            return base.GetExpressionForAttribute(attribute);
        }
        
        /// <summary>
        /// Gets the property expression for the given reference
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="reference">The requested reference in upper case</param>
        protected override NMF.Expressions.INotifyExpression<NMF.Models.IModelElement> GetExpressionForReference(string reference)
        {
            if ((reference == "ECONTAININGCLASS"))
            {
                return new EContainingClassProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            return ((IClass)(NMF.Models.Repository.MetaRepository.Instance.Resolve("http://www.eclipse.org/emf/2002/Ecore#//EStructuralFeature/")));
        }
        
        /// <summary>
        /// The collection class to to represent the children of the EStructuralFeature class
        /// </summary>
        public class EStructuralFeatureReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private EStructuralFeature _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public EStructuralFeatureReferencedElementsCollection(EStructuralFeature parent)
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
                    if ((this._parent.EContainingClass != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.EContainingClassChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.EContainingClassChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.EContainingClass == null))
                {
                    IEClass eContainingClassCasted = item.As<IEClass>();
                    if ((eContainingClassCasted != null))
                    {
                        this._parent.EContainingClass = eContainingClassCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.EContainingClass = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.EContainingClass))
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
                if ((this._parent.EContainingClass != null))
                {
                    array[arrayIndex] = this._parent.EContainingClass;
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
                if ((this._parent.EContainingClass == item))
                {
                    this._parent.EContainingClass = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.EContainingClass).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the changeable property
        /// </summary>
        private sealed class ChangeableProxy : ModelPropertyChange<IEStructuralFeature, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public ChangeableProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.Changeable;
                }
                set
                {
                    this.ModelElement.Changeable = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.ChangeableChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.ChangeableChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the volatile property
        /// </summary>
        private sealed class VolatileProxy : ModelPropertyChange<IEStructuralFeature, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public VolatileProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.Volatile;
                }
                set
                {
                    this.ModelElement.Volatile = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.VolatileChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.VolatileChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the transient property
        /// </summary>
        private sealed class TransientProxy : ModelPropertyChange<IEStructuralFeature, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public TransientProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.Transient;
                }
                set
                {
                    this.ModelElement.Transient = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.TransientChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.TransientChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the defaultValueLiteral property
        /// </summary>
        private sealed class DefaultValueLiteralProxy : ModelPropertyChange<IEStructuralFeature, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public DefaultValueLiteralProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.DefaultValueLiteral;
                }
                set
                {
                    this.ModelElement.DefaultValueLiteral = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DefaultValueLiteralChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DefaultValueLiteralChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the unsettable property
        /// </summary>
        private sealed class UnsettableProxy : ModelPropertyChange<IEStructuralFeature, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public UnsettableProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.Unsettable;
                }
                set
                {
                    this.ModelElement.Unsettable = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.UnsettableChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.UnsettableChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the derived property
        /// </summary>
        private sealed class DerivedProxy : ModelPropertyChange<IEStructuralFeature, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public DerivedProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override Nullable<bool> Value
            {
                get
                {
                    return this.ModelElement.Derived;
                }
                set
                {
                    this.ModelElement.Derived = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DerivedChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DerivedChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the eContainingClass property
        /// </summary>
        private sealed class EContainingClassProxy : ModelPropertyChange<IEStructuralFeature, IEClass>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public EContainingClassProxy(IEStructuralFeature modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IEClass Value
            {
                get
                {
                    return this.ModelElement.EContainingClass;
                }
                set
                {
                    this.ModelElement.EContainingClass = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.EContainingClassChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.EContainingClassChanged -= handler;
            }
        }
    }
}

