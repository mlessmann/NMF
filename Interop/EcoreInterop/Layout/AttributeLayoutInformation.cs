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
using NMF.Interop.Ecore;
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

namespace NMF.Interop.Layout
{
    
    
    /// <summary>
    /// The default implementation of the AttributeLayoutInformation class
    /// </summary>
    [XmlNamespaceAttribute("http://www.emftext.org/commons/layout")]
    [XmlNamespacePrefixAttribute("layout")]
    [ModelRepresentationClassAttribute("http://www.emftext.org/commons/layout#//AttributeLayoutInformation/")]
    public class AttributeLayoutInformation : LayoutInformation, IAttributeLayoutInformation, IModelElement
    {
        
        /// <summary>
        /// Gets the Class element that describes the structure of this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                return (IClass)NMF.Models.Repository.MetaRepository.Instance.ResolveType("http://www.emftext.org/commons/layout#//AttributeLayoutInformation/");
            }
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            return ((IClass)(NMF.Models.Repository.MetaRepository.Instance.Resolve("http://www.emftext.org/commons/layout#//AttributeLayoutInformation/")));
        }
    }
}

