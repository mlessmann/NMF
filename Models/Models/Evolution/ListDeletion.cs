﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMF.Models.Repository;
using System.Collections;
using NMF.Serialization;
using System.ComponentModel;
using System.Reflection;

namespace NMF.Models.Evolution
{
    [XmlConstructor(4)]
    public class ListDeletion : IModelChange
    {
        [XmlConstructorParameter(0)]
        public Uri AbsoluteUri { get; set; }
        
        [XmlConstructorParameter(1)]
        public string CollectionPropertyName { get; set; }
        
        [XmlConstructorParameter(2)]
        public int StartingIndex { get; set; }

        [XmlConstructorParameter(3)]
        public int Count { get; set; }
        
        public ListDeletion(Uri absoluteUri, string collectionPropertyName, int startingIndex, int count)
        {
            if (absoluteUri == null)
                throw new ArgumentNullException(nameof(absoluteUri));
            if (string.IsNullOrEmpty(collectionPropertyName))
                throw new ArgumentNullException(nameof(collectionPropertyName));
            if (startingIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startingIndex));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            AbsoluteUri = absoluteUri;
            CollectionPropertyName = collectionPropertyName;
            StartingIndex = startingIndex;
            Count = count;
        }

        public void Apply(IModelRepository repository)
        {
            var parent = repository.Resolve(AbsoluteUri);
            var property = parent.GetType().GetRuntimeProperty(CollectionPropertyName);
            var list = property.GetValue(parent, null) as IList;
            
            for (int i = Math.Min(StartingIndex + Count - 1, list.Count - 1); i >= StartingIndex; i--)
            {
                list.RemoveAt(i);
            }
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as ListDeletion;
            if (other == null)
                return false;
            else
                return this.AbsoluteUri.Equals(other.AbsoluteUri)
                    && this.CollectionPropertyName.Equals(other.CollectionPropertyName)
                    && this.StartingIndex.Equals(other.StartingIndex)
                    && this.Count.Equals(other.Count);
        }

        public override int GetHashCode()
        {
            return AbsoluteUri?.GetHashCode() ?? 0
                ^ CollectionPropertyName?.GetHashCode() ?? 0
                ^ StartingIndex.GetHashCode()
                ^ Count.GetHashCode();
        }
    }
}
