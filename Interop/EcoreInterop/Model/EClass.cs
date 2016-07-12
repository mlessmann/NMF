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
    /// The default implementation of the EClass class
    /// </summary>
    [XmlNamespaceAttribute("http://www.eclipse.org/emf/2002/Ecore")]
    [XmlNamespacePrefixAttribute("ecore")]
    [ModelRepresentationClassAttribute("http://www.eclipse.org/emf/2002/Ecore#//EClass/")]
    [DebuggerDisplayAttribute("EClass {Name}")]
    public class EClass : EClassifier, IEClass, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Abstract property
        /// </summary>
        private Nullable<bool> _abstract;
        
        /// <summary>
        /// The backing field for the Interface property
        /// </summary>
        private Nullable<bool> _interface;
        
        /// <summary>
        /// The backing field for the ESuperTypes property
        /// </summary>
        private ObservableAssociationList<IEClass> _eSuperTypes;
        
        /// <summary>
        /// The backing field for the EOperations property
        /// </summary>
        private EClassEOperationsCollection _eOperations;
        
        /// <summary>
        /// The backing field for the EStructuralFeatures property
        /// </summary>
        private EClassEStructuralFeaturesCollection _eStructuralFeatures;
        
        /// <summary>
        /// The backing field for the EGenericSuperTypes property
        /// </summary>
        private ObservableCompositionList<IEGenericType> _eGenericSuperTypes;
        
        public EClass()
        {
            this._eSuperTypes = new ObservableAssociationList<IEClass>();
            this._eSuperTypes.CollectionChanged += this.ESuperTypesCollectionChanged;
            this._eOperations = new EClassEOperationsCollection(this);
            this._eOperations.CollectionChanged += this.EOperationsCollectionChanged;
            this._eStructuralFeatures = new EClassEStructuralFeaturesCollection(this);
            this._eStructuralFeatures.CollectionChanged += this.EStructuralFeaturesCollectionChanged;
            this._eGenericSuperTypes = new ObservableCompositionList<IEGenericType>(this);
            this._eGenericSuperTypes.CollectionChanged += this.EGenericSuperTypesCollectionChanged;
        }
        
        /// <summary>
        /// The abstract property
        /// </summary>
        [XmlElementNameAttribute("abstract")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Abstract
        {
            get
            {
                return this._abstract;
            }
            set
            {
                if ((this._abstract != value))
                {
                    Nullable<bool> old = this._abstract;
                    this._abstract = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnAbstractChanged(e);
                    this.OnPropertyChanged("Abstract", e);
                }
            }
        }
        
        /// <summary>
        /// The interface property
        /// </summary>
        [XmlElementNameAttribute("interface")]
        [XmlAttributeAttribute(true)]
        public virtual Nullable<bool> Interface
        {
            get
            {
                return this._interface;
            }
            set
            {
                if ((this._interface != value))
                {
                    Nullable<bool> old = this._interface;
                    this._interface = value;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnInterfaceChanged(e);
                    this.OnPropertyChanged("Interface", e);
                }
            }
        }
        
        /// <summary>
        /// The eSuperTypes property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("eSuperTypes")]
        [XmlAttributeAttribute(true)]
        [ConstantAttribute()]
        public virtual IListExpression<IEClass> ESuperTypes
        {
            get
            {
                return this._eSuperTypes;
            }
        }
        
        /// <summary>
        /// The eOperations property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("eOperations")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [XmlOppositeAttribute("eContainingClass")]
        [ConstantAttribute()]
        public virtual IListExpression<IEOperation> EOperations
        {
            get
            {
                return this._eOperations;
            }
        }
        
        /// <summary>
        /// The eStructuralFeatures property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("eStructuralFeatures")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [XmlOppositeAttribute("eContainingClass")]
        [ConstantAttribute()]
        public virtual IListExpression<IEStructuralFeature> EStructuralFeatures
        {
            get
            {
                return this._eStructuralFeatures;
            }
        }
        
        /// <summary>
        /// The eGenericSuperTypes property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [XmlElementNameAttribute("eGenericSuperTypes")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public virtual IListExpression<IEGenericType> EGenericSuperTypes
        {
            get
            {
                return this._eGenericSuperTypes;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new EClassChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new EClassReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class element that describes the structure of this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                return (IClass)NMF.Models.Repository.MetaRepository.Instance.ResolveType("http://www.eclipse.org/emf/2002/Ecore#//EClass/");
            }
        }
        
        /// <summary>
        /// Gets fired when the Abstract property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> AbstractChanged;
        
        /// <summary>
        /// Gets fired when the Interface property changed its value
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> InterfaceChanged;
        
        /// <summary>
        /// Raises the AbstractChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnAbstractChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.AbstractChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the InterfaceChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnInterfaceChanged(ValueChangedEventArgs eventArgs)
        {
            EventHandler<ValueChangedEventArgs> handler = this.InterfaceChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Forwards change notifications for the ESuperTypes property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void ESuperTypesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("ESuperTypes", e);
        }
        
        /// <summary>
        /// Forwards change notifications for the EOperations property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void EOperationsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("EOperations", e);
        }
        
        /// <summary>
        /// Forwards change notifications for the EStructuralFeatures property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void EStructuralFeaturesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("EStructuralFeatures", e);
        }
        
        /// <summary>
        /// Forwards change notifications for the EGenericSuperTypes property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void EGenericSuperTypesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("EGenericSuperTypes", e);
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            int eOperationsIndex = ModelHelper.IndexOfReference(this.EOperations, element);
            if ((eOperationsIndex != -1))
            {
                return ModelHelper.CreatePath("eOperations", eOperationsIndex);
            }
            int eStructuralFeaturesIndex = ModelHelper.IndexOfReference(this.EStructuralFeatures, element);
            if ((eStructuralFeaturesIndex != -1))
            {
                return ModelHelper.CreatePath("eStructuralFeatures", eStructuralFeaturesIndex);
            }
            int eGenericSuperTypesIndex = ModelHelper.IndexOfReference(this.EGenericSuperTypes, element);
            if ((eGenericSuperTypesIndex != -1))
            {
                return ModelHelper.CreatePath("eGenericSuperTypes", eGenericSuperTypesIndex);
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
            if ((reference == "EOPERATIONS"))
            {
                if ((index < this.EOperations.Count))
                {
                    return this.EOperations[index];
                }
                else
                {
                    return null;
                }
            }
            if ((reference == "ESTRUCTURALFEATURES"))
            {
                if ((index < this.EStructuralFeatures.Count))
                {
                    return this.EStructuralFeatures[index];
                }
                else
                {
                    return null;
                }
            }
            if ((reference == "EGENERICSUPERTYPES"))
            {
                if ((index < this.EGenericSuperTypes.Count))
                {
                    return this.EGenericSuperTypes[index];
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
            if ((attribute == "ABSTRACT"))
            {
                return this.Abstract;
            }
            if ((attribute == "INTERFACE"))
            {
                return this.Interface;
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
            if ((feature == "ESUPERTYPES"))
            {
                return this._eSuperTypes;
            }
            if ((feature == "EOPERATIONS"))
            {
                return this._eOperations;
            }
            if ((feature == "ESTRUCTURALFEATURES"))
            {
                return this._eStructuralFeatures;
            }
            if ((feature == "EGENERICSUPERTYPES"))
            {
                return this._eGenericSuperTypes;
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
            if ((feature == "ABSTRACT"))
            {
                this.Abstract = ((bool)(value));
                return;
            }
            if ((feature == "INTERFACE"))
            {
                this.Interface = ((bool)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            return ((IClass)(NMF.Models.Repository.MetaRepository.Instance.Resolve("http://www.eclipse.org/emf/2002/Ecore#//EClass/")));
        }
        
        /// <summary>
        /// The collection class to to represent the children of the EClass class
        /// </summary>
        public class EClassChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private EClass _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public EClassChildrenCollection(EClass parent)
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
                    count = (count + this._parent.EOperations.Count);
                    count = (count + this._parent.EStructuralFeatures.Count);
                    count = (count + this._parent.EGenericSuperTypes.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.EOperations.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.EStructuralFeatures.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.EGenericSuperTypes.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.EOperations.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.EStructuralFeatures.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.EGenericSuperTypes.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IEOperation eOperationsCasted = item.As<IEOperation>();
                if ((eOperationsCasted != null))
                {
                    this._parent.EOperations.Add(eOperationsCasted);
                }
                IEStructuralFeature eStructuralFeaturesCasted = item.As<IEStructuralFeature>();
                if ((eStructuralFeaturesCasted != null))
                {
                    this._parent.EStructuralFeatures.Add(eStructuralFeaturesCasted);
                }
                IEGenericType eGenericSuperTypesCasted = item.As<IEGenericType>();
                if ((eGenericSuperTypesCasted != null))
                {
                    this._parent.EGenericSuperTypes.Add(eGenericSuperTypesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.EOperations.Clear();
                this._parent.EStructuralFeatures.Clear();
                this._parent.EGenericSuperTypes.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.EOperations.Contains(item))
                {
                    return true;
                }
                if (this._parent.EStructuralFeatures.Contains(item))
                {
                    return true;
                }
                if (this._parent.EGenericSuperTypes.Contains(item))
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
                IEnumerator<IModelElement> eOperationsEnumerator = this._parent.EOperations.GetEnumerator();
                try
                {
                    for (
                    ; eOperationsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eOperationsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eOperationsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> eStructuralFeaturesEnumerator = this._parent.EStructuralFeatures.GetEnumerator();
                try
                {
                    for (
                    ; eStructuralFeaturesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eStructuralFeaturesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eStructuralFeaturesEnumerator.Dispose();
                }
                IEnumerator<IModelElement> eGenericSuperTypesEnumerator = this._parent.EGenericSuperTypes.GetEnumerator();
                try
                {
                    for (
                    ; eGenericSuperTypesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eGenericSuperTypesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eGenericSuperTypesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IEOperation eOperationItem = item.As<IEOperation>();
                if (((eOperationItem != null) 
                            && this._parent.EOperations.Remove(eOperationItem)))
                {
                    return true;
                }
                IEStructuralFeature eStructuralFeatureItem = item.As<IEStructuralFeature>();
                if (((eStructuralFeatureItem != null) 
                            && this._parent.EStructuralFeatures.Remove(eStructuralFeatureItem)))
                {
                    return true;
                }
                IEGenericType eGenericTypeItem = item.As<IEGenericType>();
                if (((eGenericTypeItem != null) 
                            && this._parent.EGenericSuperTypes.Remove(eGenericTypeItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.EOperations).Concat(this._parent.EStructuralFeatures).Concat(this._parent.EGenericSuperTypes).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the EClass class
        /// </summary>
        public class EClassReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private EClass _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public EClassReferencedElementsCollection(EClass parent)
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
                    count = (count + this._parent.ESuperTypes.Count);
                    count = (count + this._parent.EOperations.Count);
                    count = (count + this._parent.EStructuralFeatures.Count);
                    count = (count + this._parent.EGenericSuperTypes.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.ESuperTypes.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.EOperations.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.EStructuralFeatures.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.EGenericSuperTypes.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.ESuperTypes.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.EOperations.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.EStructuralFeatures.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.EGenericSuperTypes.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IEClass eSuperTypesCasted = item.As<IEClass>();
                if ((eSuperTypesCasted != null))
                {
                    this._parent.ESuperTypes.Add(eSuperTypesCasted);
                }
                IEOperation eOperationsCasted = item.As<IEOperation>();
                if ((eOperationsCasted != null))
                {
                    this._parent.EOperations.Add(eOperationsCasted);
                }
                IEStructuralFeature eStructuralFeaturesCasted = item.As<IEStructuralFeature>();
                if ((eStructuralFeaturesCasted != null))
                {
                    this._parent.EStructuralFeatures.Add(eStructuralFeaturesCasted);
                }
                IEGenericType eGenericSuperTypesCasted = item.As<IEGenericType>();
                if ((eGenericSuperTypesCasted != null))
                {
                    this._parent.EGenericSuperTypes.Add(eGenericSuperTypesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.ESuperTypes.Clear();
                this._parent.EOperations.Clear();
                this._parent.EStructuralFeatures.Clear();
                this._parent.EGenericSuperTypes.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.ESuperTypes.Contains(item))
                {
                    return true;
                }
                if (this._parent.EOperations.Contains(item))
                {
                    return true;
                }
                if (this._parent.EStructuralFeatures.Contains(item))
                {
                    return true;
                }
                if (this._parent.EGenericSuperTypes.Contains(item))
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
                IEnumerator<IModelElement> eSuperTypesEnumerator = this._parent.ESuperTypes.GetEnumerator();
                try
                {
                    for (
                    ; eSuperTypesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eSuperTypesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eSuperTypesEnumerator.Dispose();
                }
                IEnumerator<IModelElement> eOperationsEnumerator = this._parent.EOperations.GetEnumerator();
                try
                {
                    for (
                    ; eOperationsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eOperationsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eOperationsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> eStructuralFeaturesEnumerator = this._parent.EStructuralFeatures.GetEnumerator();
                try
                {
                    for (
                    ; eStructuralFeaturesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eStructuralFeaturesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eStructuralFeaturesEnumerator.Dispose();
                }
                IEnumerator<IModelElement> eGenericSuperTypesEnumerator = this._parent.EGenericSuperTypes.GetEnumerator();
                try
                {
                    for (
                    ; eGenericSuperTypesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = eGenericSuperTypesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    eGenericSuperTypesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IEClass eClassItem = item.As<IEClass>();
                if (((eClassItem != null) 
                            && this._parent.ESuperTypes.Remove(eClassItem)))
                {
                    return true;
                }
                IEOperation eOperationItem = item.As<IEOperation>();
                if (((eOperationItem != null) 
                            && this._parent.EOperations.Remove(eOperationItem)))
                {
                    return true;
                }
                IEStructuralFeature eStructuralFeatureItem = item.As<IEStructuralFeature>();
                if (((eStructuralFeatureItem != null) 
                            && this._parent.EStructuralFeatures.Remove(eStructuralFeatureItem)))
                {
                    return true;
                }
                IEGenericType eGenericTypeItem = item.As<IEGenericType>();
                if (((eGenericTypeItem != null) 
                            && this._parent.EGenericSuperTypes.Remove(eGenericTypeItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.ESuperTypes).Concat(this._parent.EOperations).Concat(this._parent.EStructuralFeatures).Concat(this._parent.EGenericSuperTypes).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the abstract property
        /// </summary>
        private sealed class AbstractProxy : ModelPropertyChange<IEClass, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public AbstractProxy(IEClass modelElement) : 
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
                    return this.ModelElement.Abstract;
                }
                set
                {
                    this.ModelElement.Abstract = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.AbstractChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.AbstractChanged -= handler;
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the interface property
        /// </summary>
        private sealed class InterfaceProxy : ModelPropertyChange<IEClass, Nullable<bool>>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public InterfaceProxy(IEClass modelElement) : 
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
                    return this.ModelElement.Interface;
                }
                set
                {
                    this.ModelElement.Interface = value;
                }
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be subscribed to the property change event</param>
            protected override void RegisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.InterfaceChanged += handler;
            }
            
            /// <summary>
            /// Registers an event handler to subscribe specifically on the changed event for this property
            /// </summary>
            /// <param name="handler">The handler that should be unsubscribed from the property change event</param>
            protected override void UnregisterChangeEventHandler(System.EventHandler<NMF.Expressions.ValueChangedEventArgs> handler)
            {
                this.ModelElement.InterfaceChanged -= handler;
            }
        }
    }
}

