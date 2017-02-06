﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NMF.Models.Repository.Serialization;
using NMF.Models.Meta;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyModel;

namespace NMF.Models.Repository
{
    public sealed class MetaRepository : IModelRepository
    {
        private static MetaRepository instance = new MetaRepository();
        private ModelCollection entries;
        private ModelSerializer serializer = new ModelSerializer();
        private HashSet<string> touchedAssemblies = new HashSet<string>();

        event EventHandler<BubbledChangeEventArgs> IModelRepository.BubbledChange
        {
            add { }
            remove { }
        }

        public static MetaRepository Instance
        {
            get
            {
                return instance;
            }
        }

        public ModelSerializer Serializer
        {
            get
            {
                return serializer;
            }
        }

        private MetaRepository()
        {
            entries = new ModelCollection(this);
            serializer.KnownTypes.Add(typeof(INamespace));
            serializer.KnownTypes.Add(typeof(Model));
            
            FindNewAssemblies();
        }

        public IType ResolveType(string uriString)
        {
            return Resolve(new Uri(uriString, UriKind.Absolute)) as IType;
        }

        public IType ResolveClass(System.Type systemType)
        {
            if (systemType == null) throw new ArgumentNullException("systemType");
            var modelAtt = systemType.GetTypeInfo().GetCustomAttribute<ModelRepresentationClassAttribute>(false);
            
            if (modelAtt != null)
            {
                return ResolveType(modelAtt.UriString);
            }
            return null;
        }

        private void FindNewAssemblies()
        {
            var loadedAssemblies = DependencyContext.Default.GetDefaultAssemblyNames();

            foreach (var assemblyName in loadedAssemblies)
            {
                RegisterAssembly(assemblyName);
            }
        }

        private void RegisterAssembly(AssemblyName assemblyName)
        {
            if (assemblyName.Name.StartsWith("System.") || assemblyName.Name.StartsWith("Microsoft."))
                return;

            //AssemblyNames retrieved by DependencyContext only contain the Name,
            //whereas those retrieved by GetReferencesAssemblies() contain all info.
            //Therefore we can only compare on the Name
            if (!touchedAssemblies.Add(assemblyName.Name))
                return;

            var assembly = Assembly.Load(assemblyName);

            foreach (var dependency in assembly.GetReferencedAssemblies())
                RegisterAssembly(dependency);

            var attributes = assembly.GetCustomAttributes<ModelMetadataAttribute>();
            if (attributes != null && attributes.Any())
            {
                var saveMapping = new List<KeyValuePair<string, System.Type>>();
                foreach (var t in assembly.DefinedTypes)
                {
                    var modelRepresentation = t.GetCustomAttribute<ModelRepresentationClassAttribute>(false);
                    if (modelRepresentation != null)
                    {
                        serializer.KnownTypes.Add(t.AsType());
                        saveMapping.Add(new KeyValuePair<string, System.Type>(modelRepresentation.UriString, t.AsType()));
                    }
                }

                var names = assembly.GetManifestResourceNames();
                foreach (var metadata in attributes)
                {
                    Uri modelUri;
                    if (metadata != null && names.Contains(metadata.ResourceName) && Uri.TryCreate(metadata.ModelUri, UriKind.Absolute, out modelUri))
                    {
                        using (var stream = assembly.GetManifestResourceStream(metadata.ResourceName))
                            serializer.Deserialize(stream, modelUri, this, true);
                    }
                }
                for (int i = 0; i < saveMapping.Count; i++)
                {
                    var cls = ResolveType(saveMapping[i].Key);
                    if (cls != null)
                    {
                        var typeExtension = MappedType.FromType(cls);
                        typeExtension.SystemType = saveMapping[i].Value;
                    }
                    else
                    {
                        throw new InvalidOperationException(string.Format("The class {0} could not be resolved.", saveMapping[i].Key));
                    }
                }
            }
        }
        
        public IModelElement Resolve(Uri uri)
        {
            FindNewAssemblies();
            if (entries.TryGetValue(uri, out Model model))
            {
                return model.Resolve(uri.Fragment);
            }
            return null;
        }

        public IModelElement Resolve(string uriString)
        {
            return Resolve(new Uri(uriString, UriKind.Absolute));
        }

        IModelElement IModelRepository.Resolve(Uri uri, bool loadOnDemand)
        {
            return Resolve(uri);
        }

        public ModelCollection Models
        {
            get { return entries; }
        }

    }
}
