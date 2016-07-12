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
    /// Represents a simple-valued attribute
    /// </summary>
    [XmlNamespaceAttribute("http://nmf.codeplex.com/nmeta/")]
    [XmlNamespacePrefixAttribute("nmeta")]
    [ModelRepresentationClassAttribute("http://nmf.codeplex.com/nmeta/#//Attribute/")]
    [DebuggerDisplayAttribute("Attribute {Name}")]
    public class Attribute : TypedElement, IAttribute, IModelElement
    {
        
        /// <summary>
        /// The backing field for the DefaultValue property
        /// </summary>
        private string _defaultValue;
        
        /// <summary>
        /// The backing field for the Refines property
        /// </summary>
        private IAttribute _refines;
        
        /// <summary>
        /// The default value for this attribute
        /// </summary>
        [XmlAttributeAttribute(true)]
        public virtual string DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                if ((this._defaultValue != value))
                {
                    string old = this._defaultValue;
                    this._defaultValue = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnDefaultValueChanged(e);
                    this.OnPropertyChanged("DefaultValue", e);
                }
            }
        }
        
        /// <summary>
        /// The type that declared this attribute
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("Attributes")]
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
        /// Gets or sets the attribute that is implemented by the current attribute
        /// </summary>
        [XmlAttributeAttribute(true)]
        public virtual IAttribute Refines
        {
            get
            {
                return this._refines;
            }
            set
            {
                if ((this._refines != value))
                {
                    IAttribute old = this._refines;
                    this._refines = value;
                    if ((old != null))
                    {
                        old.Deleted -= this.OnResetRefines;
                    }
                    if ((value != null))
                    {
                        value.Deleted += this.OnResetRefines;
                    }
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnRefinesChanged(e);
                    this.OnPropertyChanged("Refines", e);
                }
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new AttributeReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                return (IClass)NMF.Models.Repository.MetaRepository.Instance.ResolveType("http://nmf.codeplex.com/nmeta/#//Attribute/");
            }
        }
        
        /// <summary>
        /// Gets fired when the DefaultValue property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> DefaultValueChanged;
        
        /// <summary>
        /// Gets fired when the DeclaringType property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> DeclaringTypeChanged;
        
        /// <summary>
        /// Gets fired when the Refines property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> RefinesChanged;
        
        /// <summary>
        /// Raises the DefaultValueChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnDefaultValueChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.DefaultValueChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
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
                oldDeclaringType.Attributes.Remove(this);
            }
            if ((newDeclaringType != null))
            {
                newDeclaringType.Attributes.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldDeclaringType, newDeclaringType);
            this.OnDeclaringTypeChanged(e);
            this.OnPropertyChanged("DeclaringType", e);
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
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "DEFAULTVALUE"))
            {
                return this.DefaultValue;
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
            if ((feature == "DECLARINGTYPE"))
            {
                this.DeclaringType = ((IStructuredType)(value));
                return;
            }
            if ((feature == "REFINES"))
            {
                this.Refines = ((IAttribute)(value));
                return;
            }
            if ((feature == "DEFAULTVALUE"))
            {
                this.DefaultValue = ((string)(value));
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
            if ((attribute == "DECLARINGTYPE"))
            {
                return new DeclaringTypeProxy(this);
            }
            if ((attribute == "REFINES"))
            {
                return new RefinesProxy(this);
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
            if ((reference == "DECLARINGTYPE"))
            {
                return new DeclaringTypeProxy(this);
            }
            if ((reference == "REFINES"))
            {
                return new RefinesProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            return ((IClass)(NMF.Models.Repository.MetaRepository.Instance.Resolve("http://nmf.codeplex.com/nmeta/#//Attribute/")));
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Attribute class
        /// </summary>
        public class AttributeReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Attribute _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public AttributeReferencedElementsCollection(Attribute parent)
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
                this._parent.DeclaringTypeChanged += this.PropagateValueChanges;
                this._parent.RefinesChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.DeclaringTypeChanged -= this.PropagateValueChanges;
                this._parent.RefinesChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
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
                    IAttribute refinesCasted = item.As<IAttribute>();
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.DeclaringType).Concat(this._parent.Refines).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the DefaultValue property
        /// </summary>
        private sealed class DefaultValueProxy : ModelPropertyChange<IAttribute, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public DefaultValueProxy(IAttribute modelElement) : 
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
                    return this.ModelElement.DefaultValue;
                }
                set
                {
                    this.ModelElement.DefaultValue = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DefaultValueChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DefaultValueChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the DeclaringType property
        /// </summary>
        private sealed class DeclaringTypeProxy : ModelPropertyChange<IAttribute, IStructuredType>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public DeclaringTypeProxy(IAttribute modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IStructuredType Value
            {
                get
                {
                    return this.ModelElement.DeclaringType;
                }
                set
                {
                    this.ModelElement.DeclaringType = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DeclaringTypeChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.DeclaringTypeChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the Refines property
        /// </summary>
        private sealed class RefinesProxy : ModelPropertyChange<IAttribute, IAttribute>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public RefinesProxy(IAttribute modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IAttribute Value
            {
                get
                {
                    return this.ModelElement.Refines;
                }
                set
                {
                    this.ModelElement.Refines = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.RefinesChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.RefinesChanged -= handler;
            }
        }
    }
}

