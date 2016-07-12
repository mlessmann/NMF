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
    /// The default implementation of the EAnnotation class
    /// </summary>
    [XmlNamespaceAttribute("http://www.eclipse.org/emf/2002/Ecore")]
    [XmlNamespacePrefixAttribute("ecore")]
    [ModelRepresentationClassAttribute("http://www.eclipse.org/emf/2002/Ecore#//EAnnotation/")]
    public class EAnnotation : EModelElement, IEAnnotation, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Source property
        /// </summary>
        private string _source;
        
        /// <summary>
        /// The backing field for the Details property
        /// </summary>
        private ObservableCompositionList<IEStringToStringMapEntry> _details;
        
        /// <summary>
        /// The backing field for the Contents property
        /// </summary>
        private ObservableCompositionList<IEObject> _contents;
        
        /// <summary>
        /// The backing field for the References property
        /// </summary>
        private ObservableAssociationList<IEObject> _references;
        
        public EAnnotation()
        {
            this._details = new ObservableCompositionList<IEStringToStringMapEntry>(this);
            this._details.CollectionChanged += this.DetailsCollectionChanged;
            this._contents = new ObservableCompositionList<IEObject>(this);
            this._contents.CollectionChanged += this.ContentsCollectionChanged;
            this._references = new ObservableAssociationList<IEObject>();
            this._references.CollectionChanged += this.ReferencesCollectionChanged;
        }
        
        /// <summary>
        /// The source property
        /// </summary>
        [XmlElementNameAttribute("source")]
        [XmlAttributeAttribute(true)]
        public virtual string Source
        {
            get
            {
                return this._source;
            }
            set
            {
                if ((this._source != value))
                {
                    string old = this._source;
                    this._source = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnSourceChanged(e);
                    this.OnPropertyChanged("Source", e);
                }
            }
        }
        
        /// <summary>
        /// The details property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("details")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public virtual IListExpression<IEStringToStringMapEntry> Details
        {
            get
            {
                return this._details;
            }
        }
        
        /// <summary>
        /// The eModelElement property
        /// </summary>
        [XmlElementNameAttribute("eModelElement")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("eAnnotations")]
        public virtual IEModelElement EModelElement
        {
            get
            {
                return ModelHelper.CastAs<IEModelElement>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// The contents property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("contents")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public virtual IListExpression<IEObject> Contents
        {
            get
            {
                return this._contents;
            }
        }
        
        /// <summary>
        /// The references property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("references")]
        [XmlAttributeAttribute(true)]
        [ConstantAttribute()]
        public virtual IListExpression<IEObject> References
        {
            get
            {
                return this._references;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new EAnnotationChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new EAnnotationReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                return (IClass)NMF.Models.Repository.MetaRepository.Instance.ResolveType("http://www.eclipse.org/emf/2002/Ecore#//EAnnotation/");
            }
        }
        
        /// <summary>
        /// Gets fired when the Source property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> SourceChanged;
        
        /// <summary>
        /// Gets fired when the EModelElement property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> EModelElementChanged;
        
        /// <summary>
        /// Raises the SourceChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnSourceChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.SourceChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Forwards change notifications for the Details property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void DetailsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Details", e);
        }
        
        /// <summary>
        /// Raises the EModelElementChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnEModelElementChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.EModelElementChanged;
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
            IEModelElement oldEModelElement = ModelHelper.CastAs<IEModelElement>(oldParent);
            IEModelElement newEModelElement = ModelHelper.CastAs<IEModelElement>(newParent);
            if ((oldEModelElement != null))
            {
                oldEModelElement.EAnnotations.Remove(this);
            }
            if ((newEModelElement != null))
            {
                newEModelElement.EAnnotations.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldEModelElement, newEModelElement);
            this.OnEModelElementChanged(e);
            this.OnPropertyChanged("EModelElement", e);
        }
        
        /// <summary>
        /// Forwards change notifications for the Contents property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ContentsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Contents", e);
        }
        
        /// <summary>
        /// Forwards change notifications for the References property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ReferencesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("References", e);
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            int detailsIndex = ModelHelper.IndexOfReference(this.Details, element);
            if ((detailsIndex != -1))
            {
                return ModelHelper.CreatePath("details", detailsIndex);
            }
            int contentsIndex = ModelHelper.IndexOfReference(this.Contents, element);
            if ((contentsIndex != -1))
            {
                return ModelHelper.CreatePath("contents", contentsIndex);
            }
            return base.GetRelativePathForNonIdentifiedChild(element);
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "DETAILS"))
            {
                if ((index < this.Details.Count))
                {
                    return this.Details[index];
                }
                else
                {
                    return null;
                }
            }
            if ((reference == "CONTENTS"))
            {
                if ((index < this.Contents.Count))
                {
                    return this.Contents[index];
                }
                else
                {
                    return null;
                }
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "SOURCE"))
            {
                return this.Source;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "DETAILS"))
            {
                return this._details;
            }
            if ((feature == "CONTENTS"))
            {
                return this._contents;
            }
            if ((feature == "REFERENCES"))
            {
                return this._references;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "EMODELELEMENT"))
            {
                this.EModelElement = ((IEModelElement)(value));
                return;
            }
            if ((feature == "SOURCE"))
            {
                this.Source = ((string)(value));
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
            if ((attribute == "EMODELELEMENT"))
            {
                return new EModelElementProxy(this);
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
            if ((reference == "EMODELELEMENT"))
            {
                return new EModelElementProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            return ((IClass)(NMF.Models.Repository.MetaRepository.Instance.Resolve("http://www.eclipse.org/emf/2002/Ecore#//EAnnotation/")));
        }
        
        /// <summary>
        /// The collection class to to represent the children of the EAnnotation class
        /// </summary>
        public class EAnnotationChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private EAnnotation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public EAnnotationChildrenCollection(EAnnotation parent)
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
                    count = (count + this._parent.Details.Count);
                    count = (count + this._parent.Contents.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Details.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.Contents.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Details.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.Contents.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IEStringToStringMapEntry detailsCasted = item.As<IEStringToStringMapEntry>();
                if ((detailsCasted != null))
                {
                    this._parent.Details.Add(detailsCasted);
                }
                IEObject contentsCasted = item.As<IEObject>();
                if ((contentsCasted != null))
                {
                    this._parent.Contents.Add(contentsCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Details.Clear();
                this._parent.Contents.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Details.Contains(item))
                {
                    return true;
                }
                if (this._parent.Contents.Contains(item))
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
                IEnumerator<IModelElement> detailsEnumerator = this._parent.Details.GetEnumerator();
                try
                {
                    for (
                    ; detailsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = detailsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    detailsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> contentsEnumerator = this._parent.Contents.GetEnumerator();
                try
                {
                    for (
                    ; contentsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = contentsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    contentsEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IEStringToStringMapEntry eStringToStringMapEntryItem = item.As<IEStringToStringMapEntry>();
                if (((eStringToStringMapEntryItem != null) 
                            && this._parent.Details.Remove(eStringToStringMapEntryItem)))
                {
                    return true;
                }
                IEObject eObjectItem = item.As<IEObject>();
                if (((eObjectItem != null) 
                            && this._parent.Contents.Remove(eObjectItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Details).Concat(this._parent.Contents).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the EAnnotation class
        /// </summary>
        public class EAnnotationReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private EAnnotation _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public EAnnotationReferencedElementsCollection(EAnnotation parent)
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
                    count = (count + this._parent.Details.Count);
                    if ((this._parent.EModelElement != null))
                    {
                        count = (count + 1);
                    }
                    count = (count + this._parent.Contents.Count);
                    count = (count + this._parent.References.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Details.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.EModelElementChanged += this.PropagateValueChanges;
                this._parent.Contents.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.References.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Details.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.EModelElementChanged -= this.PropagateValueChanges;
                this._parent.Contents.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.References.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IEStringToStringMapEntry detailsCasted = item.As<IEStringToStringMapEntry>();
                if ((detailsCasted != null))
                {
                    this._parent.Details.Add(detailsCasted);
                }
                if ((this._parent.EModelElement == null))
                {
                    IEModelElement eModelElementCasted = item.As<IEModelElement>();
                    if ((eModelElementCasted != null))
                    {
                        this._parent.EModelElement = eModelElementCasted;
                        return;
                    }
                }
                IEObject contentsCasted = item.As<IEObject>();
                if ((contentsCasted != null))
                {
                    this._parent.Contents.Add(contentsCasted);
                }
                IEObject referencesCasted = item.As<IEObject>();
                if ((referencesCasted != null))
                {
                    this._parent.References.Add(referencesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Details.Clear();
                this._parent.EModelElement = null;
                this._parent.Contents.Clear();
                this._parent.References.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Details.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.EModelElement))
                {
                    return true;
                }
                if (this._parent.Contents.Contains(item))
                {
                    return true;
                }
                if (this._parent.References.Contains(item))
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
                IEnumerator<IModelElement> detailsEnumerator = this._parent.Details.GetEnumerator();
                try
                {
                    for (
                    ; detailsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = detailsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    detailsEnumerator.Dispose();
                }
                if ((this._parent.EModelElement != null))
                {
                    array[arrayIndex] = this._parent.EModelElement;
                    arrayIndex = (arrayIndex + 1);
                }
                IEnumerator<IModelElement> contentsEnumerator = this._parent.Contents.GetEnumerator();
                try
                {
                    for (
                    ; contentsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = contentsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    contentsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> referencesEnumerator = this._parent.References.GetEnumerator();
                try
                {
                    for (
                    ; referencesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = referencesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    referencesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IEStringToStringMapEntry eStringToStringMapEntryItem = item.As<IEStringToStringMapEntry>();
                if (((eStringToStringMapEntryItem != null) 
                            && this._parent.Details.Remove(eStringToStringMapEntryItem)))
                {
                    return true;
                }
                if ((this._parent.EModelElement == item))
                {
                    this._parent.EModelElement = null;
                    return true;
                }
                IEObject eObjectItem = item.As<IEObject>();
                if (((eObjectItem != null) 
                            && this._parent.Contents.Remove(eObjectItem)))
                {
                    return true;
                }
                if (((eObjectItem != null) 
                            && this._parent.References.Remove(eObjectItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Details).Concat(this._parent.EModelElement).Concat(this._parent.Contents).Concat(this._parent.References).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the source property
        /// </summary>
        private sealed class SourceProxy : ModelPropertyChange<IEAnnotation, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public SourceProxy(IEAnnotation modelElement) : 
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
                    return this.ModelElement.Source;
                }
                set
                {
                    this.ModelElement.Source = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.SourceChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.SourceChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the eModelElement property
        /// </summary>
        private sealed class EModelElementProxy : ModelPropertyChange<IEAnnotation, IEModelElement>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public EModelElementProxy(IEAnnotation modelElement) : 
                    base(modelElement)
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IEModelElement Value
            {
                get
                {
                    return this.ModelElement.EModelElement;
                }
                set
                {
                    this.ModelElement.EModelElement = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.EModelElementChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.EModelElementChanged -= handler;
            }
        }
    }
}

