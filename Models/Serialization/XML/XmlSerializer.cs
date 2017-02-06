using System.Reflection;
using System.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml;
using System.Text;

namespace NMF.Serialization
{

    /// <summary>
    /// Class to serialize objects in a Xml-format.
    /// </summary>
    public class XmlSerializer
    {
        private static Dictionary<Type, TypeConverter> standardTypes = new Dictionary<Type, TypeConverter>();

        static XmlSerializer()
        {
            standardTypes.Add(typeof(string), new StringConverter());
            standardTypes.Add(typeof(Int32), new Int32Converter());
            standardTypes.Add(typeof(Int64), new Int64Converter());
            standardTypes.Add(typeof(Int16), new Int16Converter());
            standardTypes.Add(typeof(Boolean), new BooleanConverter());
            standardTypes.Add(typeof(UInt16), new UInt16Converter());
            standardTypes.Add(typeof(UInt32), new UInt32Converter());
            standardTypes.Add(typeof(UInt64), new UInt64Converter());
            standardTypes.Add(typeof(Byte), new ByteConverter());
            standardTypes.Add(typeof(SByte), new SByteConverter());
            standardTypes.Add(typeof(Double), new DoubleConverter());
            standardTypes.Add(typeof(Single), new SingleConverter());
            standardTypes.Add(typeof(Decimal), new DecimalConverter());
            standardTypes.Add(typeof(DateTime), new DateTimeConverter());
            standardTypes.Add(typeof(DateTimeOffset), new DateTimeOffsetConverter());
            standardTypes.Add(typeof(TimeSpan), new TimeSpanConverter());
        }

        private Dictionary<Type, ITypeSerializationInfo> types = new Dictionary<Type, ITypeSerializationInfo>();
        private XmlTypeCollection typesWrapper;
        private Dictionary<string, Dictionary<string, ITypeSerializationInfo>> typesByQualifier = new Dictionary<string, Dictionary<string, ITypeSerializationInfo>>();

        private static Type genericCollection = typeof(ICollection<>);

        private XmlSerializationSettings settings;

        private static object[] emptyObjects = {};

        /// <summary>
        /// Creates a new XmlSerializer with default settings and no preloaded types
        /// </summary>
        public XmlSerializer() : this(XmlSerializationSettings.Default) { }

        /// <summary>
        /// Creates a new XmlSerializer with default settings
        /// </summary>
        /// <param name="additionalTypes">Set of types to preload into the serializer</param>
        /// <remarks>Types will be loaded with default settings</remarks>
        public XmlSerializer(IEnumerable<Type> additionalTypes) : this(XmlSerializationSettings.Default, additionalTypes) { }

        /// <summary>
        /// Creates a new XmlSerializer with the specified settings
        /// </summary>
        /// <param name="settings">Serializer-settings for the serializer. Can be null or Nothing in Visual Basic. In this case, the default settings will be used.</param>
        public XmlSerializer(XmlSerializationSettings settings)
        {
            this.settings = settings;
            if (settings == null) settings = XmlSerializationSettings.Default;
            this.typesWrapper = new XmlTypeCollection(this);
        }

        /// <summary>
        /// Creates a new XmlSerializer with the specified settings and the given preloaded types
        /// </summary>
        /// <param name="additionalTypes">Set of types to load into the serializer</param>
        /// <param name="settings">The settings to use for the serializer</param>
        /// <remarks>The types will be loaded with the specified settings</remarks>
        public XmlSerializer(XmlSerializationSettings settings, IEnumerable<Type> additionalTypes)
        {
            this.settings = settings;
            if (settings == null) settings = XmlSerializationSettings.Default;
            this.typesWrapper = new XmlTypeCollection(this);
            if (additionalTypes != null)
            {
                foreach (Type t in additionalTypes)
                {
                    GetSerializationInfo(t, true);
                }
            }
        }

        /// <summary>
        /// The settings to be used in the serializer
        /// </summary>
        public XmlSerializationSettings Settings
        {
            get { return settings; }
        }

        /// <summary>
        /// The set of types that are known to the serializer
        /// </summary>
        public ICollection<Type> KnownTypes
        {
            get
            {
                return typesWrapper;
            }
        }

        internal Dictionary<Type, ITypeSerializationInfo> Types
        {
            get { return types; }
        }

        private Queue<Action> initializationQueue;

        protected void EnqueueInitialization(Action action)
        {
            if (initializationQueue != null)
            {
                initializationQueue.Enqueue(action);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private ITypeSerializationInfo AddType(Type type)
        {
            ITypeSerializationInfo info = CreateTypeSerializationInfoFor(type);
            types.Add(type, info);

            if (initializationQueue != null)
            {
                EnqueueBaseTypes(info);
            }
            else
            {
                initializationQueue = new Queue<Action>();
                EnqueueBaseTypes(info);

                while (initializationQueue.Count > 0)
                {
                    var initializationAction = initializationQueue.Dequeue();
                    initializationAction();
                }

                initializationQueue = null;
            }

            return info;
        }

        private void EnqueueBaseTypes(ITypeSerializationInfo info)
        {
            if (info.Type.GetTypeInfo().BaseType != null)
            {
                GetSerializationInfo(info.Type.GetTypeInfo().BaseType, true);
            }
            initializationQueue.Enqueue(() => InitializeTypeSerializationInfo(info));
        }

        protected void RegisterNamespace(ITypeSerializationInfo info)
        {
            Dictionary<string, ITypeSerializationInfo> typesOfNamespace;
            if (!typesByQualifier.TryGetValue(info.Namespace ?? "", out typesOfNamespace))
            {
                typesOfNamespace = new Dictionary<string, ITypeSerializationInfo>();
                if (info.Namespace != null)
                {
                    var ns = info.Namespace;
                    string alternate;
                    if (ns.EndsWith("/"))
                    {
                        alternate = ns.Substring(0, ns.Length - 1);
                    }
                    else
                    {
                        alternate = ns + "/";
                    }
                    typesByQualifier.Add(ns, typesOfNamespace);
                    typesByQualifier.Add(alternate, typesOfNamespace);
                }
                else
                {
                    typesByQualifier.Add("", typesOfNamespace);
                }
            }
            var elName = Settings.CaseSensitive ? info.ElementName : info.ElementName.ToUpperInvariant();
            if (!typesOfNamespace.ContainsKey(elName))
                typesOfNamespace.Add(elName, info);
        }

        protected ITypeSerializationInfo GetTypeInfo(string ns, string localName)
        {
            Dictionary<string, ITypeSerializationInfo> typesOfNs;
            if (typesByQualifier.TryGetValue(ns ?? "", out typesOfNs))
            {
                ITypeSerializationInfo info;
                if (!Settings.CaseSensitive) localName = localName.ToUpperInvariant();
                if (typesOfNs.TryGetValue(localName, out info))
                {
                    return info;
                }
            }
            return null;
        }

        protected virtual ITypeSerializationInfo CreateTypeSerializationInfoFor(Type type)
        {
            return new XmlTypeSerializationInfo(type);
        }

        protected virtual void InitializeTypeSerializationInfo(ITypeSerializationInfo serializationInfo)
        {
            var typeInfo = serializationInfo.Type.GetTypeInfo();
            XmlTypeSerializationInfo info = serializationInfo as XmlTypeSerializationInfo;

            if (info == null) throw new NotSupportedException("Cannot initialize other serialization info types");
            
            if (typeInfo.IsGenericType)
            {
                var genericTypes = typeInfo.GenericTypeArguments.Select(t => t.Name).Aggregate((a, b) => a + "-" + b);
                var sanitizedTypeName = typeInfo.Name.Substring(0, typeInfo.Name.IndexOf('`'));
                info.ElementName = sanitizedTypeName + "_" + genericTypes + "_";
            }
            else
                info.ElementName = Settings.GetPersistanceString(typeInfo.Name);
            info.Namespace = Settings.DefaultNamespace;
            
            info.ElementName = Settings.GetPersistanceString(typeInfo.GetCustomAttribute<XmlElementNameAttribute>(false)?.ElementName ?? info.ElementName);
            info.Namespace = typeInfo.GetCustomAttribute<XmlNamespaceAttribute>(false)?.Namespace ?? info.Namespace;
            info.NamespacePrefix = typeInfo.GetCustomAttribute<XmlNamespacePrefixAttribute>(false)?.NamespacePrefix;

            var constructorAttribute = typeInfo.GetCustomAttribute<XmlConstructorAttribute>(false);
            if (constructorAttribute != null)
                info.ConstructorProperties = new XmlPropertySerializationInfo[constructorAttribute.ParameterCount];

            var identifier = typeInfo.GetCustomAttribute<XmlIdentifierAttribute>(false)?.Identifier;
            var ignoredProperties = typeInfo.GetCustomAttributes<XmlIgnorePropertyAttribute>(false).Select(a => a.Property).ToList();
            
            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeInfo))
            {
                foreach (Type i in typeInfo.ImplementedInterfaces)
                {
                    if (i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == genericCollection)
                    {
                        Type collType = i.GetTypeInfo().GenericTypeArguments[0];
                        info.CollectionType = i;
                        var converter = GetTypeConverter(collType);
                        if (converter == null || !converter.CanConvertFrom(typeof(string)) || !converter.CanConvertTo(typeof(string)))
                            info.CollectionItemType = GetSerializationInfo(collType, true);
                        else
                            info.CollectionItemType = new StringConvertibleType(converter, collType);
                        break;
                    }
                }
                if (info.CollectionType == null && typeInfo.IsInterface && typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == genericCollection)
                {
                    info.CollectionType = typeInfo.AsType();
                    info.CollectionItemType = GetSerializationInfo(typeInfo.GenericTypeArguments[0], true);
                }

                if (info.CollectionType != null)
                {
                    info.CreateCollectionAddMethod();
                }
            }
            TypeInfo constructorType = typeInfo;
            if (typeInfo.BaseType != null)
            {
                var parentTsi = GetSerializationInfo(typeInfo.BaseType, true);
                info.BaseTypes.Add(parentTsi);
                if (!info.IsIdentified && parentTsi.IsIdentified)
                {
                    XmlPropertySerializationInfo identifierProperty = parentTsi.IdentifierProperty as XmlPropertySerializationInfo;
                    if (identifierProperty != null)
                    {
                        info.IdentifierProperty = identifierProperty;
                    }
                }
                if (parentTsi.ConstructorProperties != null && info.ConstructorProperties != null)
                {
                    Array.Copy(parentTsi.ConstructorProperties, info.ConstructorProperties, Math.Min(parentTsi.ConstructorProperties.Length, info.ConstructorProperties.Length));
                }
            }
            foreach (var pi in typeInfo.DeclaredProperties)
            {
                if (pi.GetMethod.IsStatic)
                    continue;
                var indexParams = pi.GetIndexParameters();
                if (indexParams == null || indexParams.Length == 0)
                {
                    if (!ignoredProperties.Contains(pi.Name))
                    {
                        CreatePropertySerializationInfo(info, identifier, info.ConstructorProperties, pi);
                    }
                }
            }
            if (info.ConstructorProperties != null)
            {
                Type[] ts = new Type[info.ConstructorProperties.GetLength(0)];
                for (int i = 0; i < info.ConstructorProperties.GetLength(0); i++)
                {
                    ts[i] = info.ConstructorProperties[i] == null ? typeof(object) : info.ConstructorProperties[i].PropertyType.Type;
                }
                info.Constructor = constructorType.DeclaredConstructors.FirstOrDefault(ci => ci.GetParameters().Select(p => p.ParameterType).SequenceEqual(ts));
                if (info.Constructor == null) throw new InvalidOperationException("No suitable constructor found for type " + typeInfo.FullName);
            }
            else
            {
                info.Constructor = constructorType.DeclaredConstructors.FirstOrDefault(ci => !ci.IsStatic && ci.GetParameters().Length == 0);
            }
            foreach (var att in typeInfo.GetCustomAttributes<XmlKnownTypeAttribute>(false))
            {
                var t = att.Type;
                if (t != null) GetSerializationInfo(t, true);
            }

            RegisterNamespace(info);
        }

        private void CreatePropertySerializationInfo(XmlTypeSerializationInfo typeSerializationInfo, string identifier, IPropertySerializationInfo[] constructorInfos, PropertyInfo pd)
        {
            var isId = Settings.TreatAsEqual(pd.Name, identifier);
            var cParam = pd.GetCustomAttribute<XmlConstructorParameterAttribute>(true);

            if (!typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(pd.PropertyType.GetTypeInfo()) && !pd.CanWrite && !isId &&
                cParam != null) return;

            XmlPropertySerializationInfo p;
            p = CreatePropertySerializationInfo(pd);

            DesignerSerializationVisibilityAttribute des = pd.GetCustomAttribute<DesignerSerializationVisibilityAttribute>(true);

            if ((des == null || des.Visibility == DesignerSerializationVisibility.Visible) && !p.IsReadOnly)
            {
                p.ShallCreateInstance = true;
            }
            else if (des != null && des.Visibility == DesignerSerializationVisibility.Content)
            {
                p.ShallCreateInstance = false;
            }
            else
            {
                if (cParam == null || des.Visibility == DesignerSerializationVisibility.Hidden)
                {
                    return;
                }
                else
                {
                    p.ShallCreateInstance = false;
                }
            }

            if (isId)
            {
                p.IsIdentifier = true;
                typeSerializationInfo.IdentifierProperty = p;
            }

            //property might be using its own type converter
            p.Converter = GetTypeConverter(pd);
            if (p.Converter == null || !p.Converter.CanConvertFrom(typeof(string)) || !p.Converter.CanConvertTo(typeof(string)))
            {
                p.PropertyType = GetSerializationInfo(pd.PropertyType, true);
            }
            else
            {
                p.PropertyType = new StringConvertibleType(p.Converter, pd.PropertyType);

                var defaultValue = Fetch(pd.GetCustomAttribute<DefaultValueAttribute>(true), dva => dva.Value);
                if (defaultValue != null)
                {
                    p.SetDefaultValue(defaultValue);
                }
            }

            //control serialization through an attribute
            if (cParam != null && constructorInfos != null)
            {
                if (cParam.Index >= 0 || cParam.Index < constructorInfos.GetLength(0))
                {
                    constructorInfos[cParam.Index] = p;
                }
                else
                {
                    var asAttribute = pd.GetCustomAttribute<XmlAttributeAttribute>(true);
                    if (asAttribute == null || !asAttribute.SerializeAsAttribute)
                    {
                        typeSerializationInfo.DeclaredElementProperties.Add(p);
                    }
                    else
                    {
                        typeSerializationInfo.DeclaredAttributeProperties.Add(p);
                    }
                }
            }
            else
            {
                var asAttribute = pd.GetCustomAttribute<XmlAttributeAttribute>(true);
                if (asAttribute == null || !asAttribute.SerializeAsAttribute)
                {
                    typeSerializationInfo.DeclaredElementProperties.Add(p);
                }
                else
                {
                    typeSerializationInfo.DeclaredAttributeProperties.Add(p);
                }
            }

            // default settings for element name and namespace
            p.ElementName = Settings.GetPersistanceString(pd.Name);
            p.Namespace = Settings.DefaultNamespace;
            // override element name settings
            var elementName = Fetch(pd.GetCustomAttribute<XmlElementNameAttribute>(true), att => att.ElementName);
            if (elementName != null) p.ElementName = Settings.GetPersistanceString(elementName);
            var ns = Fetch(pd.GetCustomAttribute<XmlNamespaceAttribute>(true), att => att.Namespace);
            if (ns != null) p.Namespace = Settings.GetPersistanceString(ns);
            var nsPrefix = Fetch(pd.GetCustomAttribute<XmlNamespacePrefixAttribute>(true), att => att.NamespacePrefix);
            if (nsPrefix != null) p.NamespacePrefix = Settings.GetPersistanceString(nsPrefix);
            p.IdentificationMode = Fetch(pd.GetCustomAttribute<XmlIdentificationModeAttribute>(true), att => att.Mode);

            // find opposite
            var oppositeAtt = pd.GetCustomAttribute<XmlOppositeAttribute>(true);
            if (oppositeAtt != null)
            {
                var oppositeType = p.PropertyType;
                if (oppositeAtt.OppositeType != null)
                {
                    oppositeType = GetSerializationInfo(oppositeAtt.OppositeType, true);
                }
                var oppositeProperty = oppositeType.AttributeProperties.OfType<XmlPropertySerializationInfo>().FirstOrDefault(prop => prop.ElementName == oppositeAtt.OppositeProperty);
                if (oppositeProperty == null && oppositeType.IsCollection && oppositeType.CollectionItemType != null)
                {
                    oppositeType = oppositeType.CollectionItemType;
                    oppositeProperty = oppositeType.AttributeProperties.OfType<XmlPropertySerializationInfo>().FirstOrDefault(prop => prop.ElementName == oppositeAtt.OppositeProperty);
                }
                if (oppositeProperty != null)
                {
                    p.Opposite = oppositeProperty;
                    oppositeProperty.Opposite = p;
                }
            }
        }

        private static TypeConverter GetTypeConverter(PropertyInfo pd)
        {
            var converterType = Fetch(pd.GetCustomAttribute<XmlTypeConverterAttribute>(true), att => att.Type);
            if (converterType != null)
            {
                try
                {
                    return Activator.CreateInstance(converterType) as TypeConverter;
                }
                catch (Exception)
                { }
            }
            var converterTypeString = Fetch(pd.GetCustomAttribute<TypeConverterAttribute>(true), att => att.ConverterTypeName);
            if (converterTypeString != null)
            {
                try
                {
                    return Activator.CreateInstance(Type.GetType(converterTypeString)) as TypeConverter;
                }
                catch (Exception) { }
            }

            return GetTypeConverter(pd.PropertyType);
        }

        private static TypeConverter GetTypeConverter(Type type)
        {
            TypeConverter converter;
            if (!standardTypes.TryGetValue(type, out converter))
            {
                if (type.GetTypeInfo().IsEnum)
                {
                    converter = new EnumConverter(type);
                }
                else
                {
                    var converterTypeString = Fetch(type.GetTypeInfo().GetCustomAttribute<TypeConverterAttribute>(true), att => att.ConverterTypeName);
                    if (converterTypeString != null)
                    {
                        try
                        {
                            converter = Activator.CreateInstance(Type.GetType(converterTypeString)) as TypeConverter;
                        }
                        catch (Exception) { }
                    }
                }
                standardTypes.Add(type, converter);
            }
            return converter;
        }

        private static TValue Fetch<T, TValue>(T obj, Func<T, TValue> func) where T : class
        {
            if (obj == null) return default(TValue);
            return func(obj);
        }

        protected virtual XmlPropertySerializationInfo CreatePropertySerializationInfo(PropertyInfo pd)
        {
            return Activator.CreateInstance(typeof(XmlPropertySerializationInfo<,>).MakeGenericType(pd.DeclaringType, pd.PropertyType), pd)
                as XmlPropertySerializationInfo;
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="path">The path for the resulting Xml-file</param>
        /// <param name="obj">The object to be serialized</param>
        /// <param name="fragment">A value that indicates whether the serializer should write a document definition</param>
        public void Serialize(object obj, string path, bool fragment = false)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                Serialize(obj, fs, fragment);
            }
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="stream">The stream for the resulting Xml-code</param>
        /// <param name="source">The object to be serialized</param>
        /// <param name="fragment">A value that indicates whether the serializer should write a document definition</param>
        public void Serialize(object source, Stream stream, bool fragment = false)
        {
            var sw = new StreamWriter(stream, Encoding.UTF8);
            Serialize(source, sw, fragment);
            sw.Flush();
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="writer">The TextWriter to write the Xml-code on</param>
        /// <param name="o">The object to be serialized</param>
        public void Serialize(object source, TextWriter writer)
        {
            Serialize(source, writer, false);
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="writer">The XmlWriter to write the Xml-code on</param>
        /// <param name="o">The object to be serialized</param>
        public void Serialize(object source, XmlWriter writer)
        {
            Serialize(source, writer, false);
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="target">The TextWriter to write the Xml-code on</param>
        /// <param name="fragment">A value that indicates whether the serializer should write a document definition</param>
        /// <param name="o">The object to be serialized</param>
        public void Serialize(object source, TextWriter target, bool fragment)
        {
            XmlWriter xml = XmlWriter.Create(target, Settings.GetXmlWriterSettings());
            Serialize(source, xml, fragment);
            xml.Flush();
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="target">The XmlWriter to write the Xml-code on</param>
        /// <param name="fragment">A value that indicates whether the serializer should write a document definition</param>
        /// <param name="o">The object to be serialized</param>
        public void Serialize(object source, XmlWriter target, bool fragment)
        {
            if (!fragment) target.WriteStartDocument();
            source = SelectRoot(source, fragment);
            var info = GetSerializationInfo(source.GetType(), true);
            WriteBeginRootElement(target, source, info);
            XmlSerializationContext context = CreateSerializationContext(source);
            Serialize(source, target, null, false, XmlIdentificationMode.FullObject, context);
            WriteEndRootElement(target, source, info);
            if (!fragment) target.WriteEndDocument();
        }

        /// <summary>
        /// Gets the serialization root element
        /// </summary>
        /// <param name="graph">The base element that should be serialized</param>
        /// <param name="fragment">A value indicating whether only a fragment should be written</param>
        /// <returns>The root element for serialization</returns>
        protected virtual object SelectRoot(object graph, bool fragment)
        {
            return graph;
        }

        protected virtual XmlSerializationContext CreateSerializationContext(object root)
        {
            return new XmlSerializationContext(root);
        }

        /// <summary>
        /// Serializes the given object
        /// </summary>
        /// <param name="writer">The XmlWriter to write the Xml-code on</param>
        /// <param name="converter">A TypeConverter that might convert the object straight to string. Can be left out.</param>
        /// <param name="writeInstance">A value that indicates whether the serializer should write the element definition</param>
        /// <param name="o">The object to be serialized</param>
        /// <param name="identificationMode">A value indicating whether it is allowed to the serializer to use identifier</param>
        /// <remarks>If a converter is provided that is able to convert the object to string and convert the string back to this object, just the string-conversion is printed out</remarks>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void Serialize(object obj, XmlWriter writer, IPropertySerializationInfo property, bool writeInstance, XmlIdentificationMode identificationMode, XmlSerializationContext context)
        {
            if (obj == null) return;
            if (property != null && property.IsStringConvertible)
            {
                writer.WriteString(property.ConvertToString(obj));
                return;
            }
            ITypeSerializationInfo info = GetSerializationInfo(obj.GetType(), false);
            if (WriteIdentifiedObject(writer, obj, identificationMode, info, context)) return;
            if (writeInstance) WriteBeginElement(writer, obj, info);
            if (info.ConstructorProperties != null)
            {
                WriteConstructorProperties(writer, obj, info, context);
            }
            WriteAttributeProperties(writer, obj, info, context);
            WriteElementProperties(writer, obj, info, context);
            WriteCollectionMembers(writer, obj, info, context);
            if (writeInstance) WriteEndElement(writer, obj, info);
        }

        protected virtual void WriteBeginRootElement(XmlWriter writer, object root, ITypeSerializationInfo info)
        {
            WriteBeginElement(writer, root, info);
        }

        protected virtual void WriteBeginElement(XmlWriter writer, object obj, ITypeSerializationInfo info)
        {
            writer.WriteStartElement(info.NamespacePrefix, info.ElementName, info.Namespace);
        }

        protected virtual void WriteConstructorProperties(XmlWriter writer, object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            for (int i = 0; i <= info.ConstructorProperties.GetUpperBound(0); i++)
            {
                IPropertySerializationInfo pi = info.ConstructorProperties[i];
                writer.WriteAttributeString(pi.NamespacePrefix, pi.ElementName, pi.Namespace, pi.ConvertToString(pi.GetValue(obj, context)));
            }
        }

        protected virtual void WriteAttributeProperties(XmlWriter writer, object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            foreach (IPropertySerializationInfo pi in info.AttributeProperties)
            {
                var value = pi.GetValue(obj, context);
                if (pi.ShouldSerializeValue(obj, value)) WriteAttributeValue(writer, obj, value, pi, context);
                if (pi.IsIdentifier)
                {
                    string id = CStr(pi.GetValue(obj, context));
                    context.RegisterId(id, obj);
                }
            }
        }

        protected virtual void WriteAttributeValue(XmlWriter writer, object obj, object value, IPropertySerializationInfo property, XmlSerializationContext context)
        {
            ITypeSerializationInfo info = property.PropertyType;

            if (value == null) return;

            string valueString = GetAttributeValue(value, property.PropertyType, context);

            if (valueString != null)
            {
                writer.WriteStartAttribute(property.NamespacePrefix, property.ElementName, property.Namespace);

                writer.WriteString(valueString);
                writer.WriteEndAttribute();
            }
            else if (info.IsCollection)
            {
                info = info.CollectionItemType;
                StringBuilder sb = new StringBuilder();
                var enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    foreach (object o in value as IEnumerable)
                    {
                        if (o != null)
                        {
                            string str = GetAttributeValue(o, info, context);
                            if (str != null)
                            {
                                sb.Append(str);
                                sb.Append(" ");
                            }
                            else
                            {
                                throw new InvalidOperationException(string.Format("Object {0} cannot be serialized as string", o));
                            }
                        }
                    }
                }
                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
                writer.WriteAttributeString(property.NamespacePrefix, property.ElementName, property.Namespace, sb.ToString());
            }
            else
            {
                throw new InvalidOperationException(string.Format("Property {0} cannot be serialized as string", property.ElementName));
            }
        }

        protected virtual string GetAttributeValue(object value, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            if (info.IsStringConvertible)
            {
                return info.ConvertToString(value);
            }
            else if (info.IsIdentified)
            {
                return CStr(info.IdentifierProperty.GetValue(value, context));
            }
            else
            {
                return null;
            }
        }

        protected virtual void WriteElementProperties(XmlWriter writer, object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            foreach (XmlPropertySerializationInfo pi in info.ElementProperties)
            {
                var value = pi.GetValue(obj, context);
                if (pi.ShouldSerializeValue(obj, value))
                {
                    writer.WriteStartElement(pi.NamespacePrefix, pi.ElementName, pi.Namespace);
                    Serialize(value, writer, pi, pi.ShallCreateInstance, pi.IdentificationMode, context);
                    writer.WriteEndElement();
                }
                if (pi.IsIdentifier)
                {
                    string id = CStr(value);
                    context.RegisterId(id, obj);
                }
            }
        }

        protected virtual void WriteCollectionMembers(XmlWriter writer, object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            if (info.IsCollection)
            {
                IEnumerable coll = obj as IEnumerable;
                foreach (object o in coll)
                {
                    Serialize(o, writer, null, true, XmlIdentificationMode.FullObject, context);
                }
            }
        }

        protected virtual void WriteEndElement(XmlWriter writer, object obj, ITypeSerializationInfo info)
        {
            writer.WriteEndElement();
        }

        protected virtual void WriteEndRootElement(XmlWriter writer, object root, ITypeSerializationInfo info)
        {
            writer.WriteEndElement();
        }

        protected virtual bool WriteIdentifiedObject(XmlWriter writer, object obj, XmlIdentificationMode identificationMode, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            if (!info.IsIdentified) return false;
            string id = CStr(info.IdentifierProperty.GetValue(obj, context));
            if (identificationMode == XmlIdentificationMode.Identifier || (identificationMode == XmlIdentificationMode.AsNeeded && context.ContainsId(id, info.Type)))
            {
                writer.WriteString(id);
                return true;
            }
            else if (identificationMode == XmlIdentificationMode.FullObject && context.ContainsId(id, info.Type))
            {
                writer.WriteStartElement(info.ElementName, info.Namespace);
                if (info.AttributeProperties.Contains(info.IdentifierProperty))
                {
                    writer.WriteAttributeString(info.IdentifierProperty.NamespacePrefix, info.IdentifierProperty.ElementName, info.IdentifierProperty.Namespace,
                        info.IdentifierProperty.ConvertToString(info.IdentifierProperty.GetValue(obj, context)));
                }
                else
                {
                    writer.WriteElementString(info.IdentifierProperty.NamespacePrefix, info.IdentifierProperty.ElementName, info.IdentifierProperty.Namespace,
                        info.IdentifierProperty.ConvertToString(info.IdentifierProperty.GetValue(obj, context)));
                }
                writer.WriteEndElement();
                return true;
            }
            return false;
        }

        private static string CStr(object obj)
        {
            return obj == null ? null : obj.ToString();
        }
         
        /// <summary>
        /// Deserializes an Xml-representation of an object back to the corresponding object
        /// </summary>
        /// <param name="path">The path to the Xml file containg the Xml code</param>
        /// <returns>The corresponding object</returns>
        public object Deserialize(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Deserialize(fs);
            }
        }

        /// <summary>
        /// Deserializes an Xml-representation of an object back to the corresponding object
        /// </summary>
        /// <param name="stream">The stream containg the Xml code</param>
        /// <returns>The corresponding object</returns>
        public object Deserialize(Stream stream)
        {
            return Deserialize(XmlReader.Create(stream));
        }

        /// <summary>
        /// Deserializes an Xml-representation of an object back to the corresponding object
        /// </summary>
        /// <param name="reader">A TextReader containg the Xml code</param>
        /// <returns>The corresponding object</returns>
        public object Deserialize(TextReader reader)
        {
            return Deserialize(XmlReader.Create(reader));
        }

        /// <summary>
        /// Deserializes an Xml-representation of an object back to the corresponding object
        /// </summary>
        /// <param name="reader">A XmlReader containing the Xml code</param>
        /// <returns>The corresponding object</returns>
        /// <remarks>The function will deserialize the object at the XmlReaders current position</remarks>
        public object Deserialize(XmlReader reader)
        {
            XmlSerializationContext context;
            object root = DeserializeRootInternal(reader, out context);
            context.Cleanup();
            return root;
        }
        
        protected object DeserializeInternal(XmlReader reader, IPropertySerializationInfo property, XmlSerializationContext context)
        {
            object d = null;
            while (reader.NodeType != XmlNodeType.Element) reader.Read();
            var propertyType = GetElementTypeInfo(reader, property);
            if (propertyType == null) throw new InvalidOperationException($"No information available what the type of {reader.LocalName} is.");
            d = CreateObject(reader, propertyType, context);
            Initialize(reader, d, context);
            return d;
        }

        protected object DeserializeRootInternal(XmlReader reader, out XmlSerializationContext context)
        {
            object root = CreateRoot(reader);

            context = CreateSerializationContext(root);

            Initialize(reader, root, context);
            return root;
        }

        protected object CreateRoot(XmlReader reader)
        {
            object root = null;
            while (reader.NodeType != XmlNodeType.Element) reader.Read();
            var rootInfo = GetRootElementTypeInfo(reader);
            root = CreateObject(reader, rootInfo, null);
            return root;
        }

        protected virtual ITypeSerializationInfo GetElementTypeInfo(XmlReader reader, IPropertySerializationInfo property)
        {
            return GetRootElementTypeInfo(reader);
        }

        protected virtual ITypeSerializationInfo GetRootElementTypeInfo(XmlReader reader)
        {
            var info = GetTypeInfo(reader.NamespaceURI, reader.LocalName);

            if (info != null)
            {
                return info;
            }
            else
            {
                throw new InvalidOperationException(string.Format("Could not identify element of type {0} in namespace {1}", reader.LocalName, reader.NamespaceURI));
            }
        }

        protected virtual object CreateObject(XmlReader reader, ITypeSerializationInfo tsi, XmlSerializationContext context)
        {
            if (tsi.ConstructorProperties == null)
            {
                return tsi.Constructor.Invoke(emptyObjects);
            }
            else
            {
                object[] objects = new object[tsi.ConstructorProperties.Length];
                for (int i = 0; i < tsi.ConstructorProperties.Length; i++)
                {
                    IPropertySerializationInfo pi = tsi.ConstructorProperties[i];
                    objects[i] = pi.ConvertFromString(reader.GetAttribute(pi.ElementName, pi.Namespace));
                }
                return tsi.Constructor.Invoke(objects);
            }
        }

        protected virtual bool InitializeProperty(XmlReader reader, IPropertySerializationInfo property, object obj, XmlSerializationContext context)
        {
            if (!GoToPropertyContent(reader)) return false;
            if (reader.NodeType == XmlNodeType.Text)
            {
                InitializePropertyFromText(property, obj, reader.Value, context);
            }
            else if (reader.NodeType != XmlNodeType.EndElement)
            {
                object target = DeserializeInternal(reader, property, context);
                if (!property.IsReadOnly && (target == null || property.PropertyType.Type.GetTypeInfo().IsAssignableFrom(target.GetType().GetTypeInfo())))
                {
                    property.SetValue(obj, target, context);
                }
                else if (property.PropertyType.IsCollection)
                {
                    object collection = property.GetValue(obj, context);
                    property.AddToCollection(collection, target, context);
                }
            }
            else
            {
                //do nothing
            }
            return true;
        }

        protected virtual bool GoToPropertyContent(XmlReader reader)
        {
            int currentDepth = reader.Depth;
            reader.Read();
            if (reader.Depth <= currentDepth) return false;
            return true;
        }

        protected virtual void InitializePropertyFromText(IPropertySerializationInfo property, object obj, string text, XmlSerializationContext context)
        {
            ITypeSerializationInfo info = property.PropertyType;
            if (property.IsStringConvertible)
            {
                property.SetValue(obj, property.ConvertFromString(text), context);
            }
            else if (info.IsCollection)
            {
                ITypeSerializationInfo itemInfo = info.CollectionItemType;
                object coll = property.GetValue(obj, context);
                foreach (var item in text.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (itemInfo.IsStringConvertible)
                    {
                        property.AddToCollection(obj, itemInfo.ConvertFromString(item), context);
                    }
                    else
                    {
                        EnqueueAddToPropertyDelay(property, obj, item, context);
                    }
                }
            }
            else
            {
                EnqueueSetPropertyDelay(property, obj, text, context);
            }
        }

        protected internal void EnqueueAddToPropertyDelay(IPropertySerializationInfo property, object obj, string text, XmlSerializationContext context)
        {
            context.LostProperties.Enqueue(new XmlAddToPropertyDelay(property) { Target = obj, Identifier = text });
        }

        protected internal void EnqueueSetPropertyDelay(IPropertySerializationInfo property, object obj, string text, XmlSerializationContext context)
        {
            context.LostProperties.Enqueue(new XmlSetPropertyDelay() { Identifier = text, Target = obj, Property = property });
        }

        /// <summary>
        /// Initializes the given object with the xml code at the current position of the XmlReader
        /// </summary>
        /// <param name="reader">The XmlReader with the Xml code</param>
        /// <param name="o">The object to initialize</param>
        /// <returns>The initialized object</returns>
        public void Initialize(XmlReader reader, object obj, XmlSerializationContext context)
        {
            if (obj == null) return;
            ITypeSerializationInfo info = GetSerializationInfo(obj.GetType(), false);
            if (reader.HasAttributes)
            {
                if (info.IsIdentified && info.AttributeProperties.Contains(info.IdentifierProperty))
                {
                    IPropertySerializationInfo p = info.IdentifierProperty;
                    var idValue = reader.GetAttribute(p.ElementName, p.Namespace);
                    if (idValue != null)
                    {
                        string id = CStr(p.ConvertFromString(idValue));
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (OverrideIdentifiedObject(obj, reader, context))
                            {
                                if (!context.ContainsId(id, info.Type))
                                {
                                    context.RegisterId(id, obj);
                                }
                                else
                                {
                                    obj = context.Resolve(id, info.Type);
                                }
                            }
                            else
                            {
                                context.RegisterId(id, obj);
                            }
                        }
                    }
                }
                InitializeAttributeProperties(reader, obj, info, context);
            }
            InitializeElementProperties(reader, ref obj, info, context);
        }

        protected virtual bool OverrideIdentifiedObject(object obj, XmlReader reader, XmlSerializationContext context)
        {
            return true;
        }

        protected virtual void InitializeElementProperties(XmlReader reader, ref object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            int currentDepth = reader.Depth;
            bool found;
            while (reader.Depth < currentDepth || reader.Read())
            {
                if (reader.Depth == currentDepth)
                {
                    break;
                }
                else if (reader.Depth < currentDepth)
                {
                    return;
                }
                if (reader.NodeType == XmlNodeType.Element)
                {
                    found = false;
                    foreach (XmlPropertySerializationInfo p in info.ElementProperties)
                    {
                        if (IsPropertyElement(reader, p))
                        {
                            if (p.ShallCreateInstance)
                            {
                                if (!InitializeProperty(reader, p, obj, context))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            else
                            {
                                Initialize(reader, p.GetValue(obj, context), context);
                            }
                            if (p.IsIdentifier)
                            {
                                string str = CStr(p.GetValue(obj, context));
                                if (!string.IsNullOrEmpty(str))
                                {
                                    if (context.ContainsId(str, info.Type))
                                    {
                                        obj = context.Resolve(str, info.Type);
                                    }
                                    else
                                    {
                                        context.RegisterId(str, obj);
                                    }
                                }
                            }
                            found = true;
                            break;
                        }
                    }
                    if (!found && info.IsCollection)
                    {
                        object o = DeserializeInternal(reader, null, context);
                        info.AddToCollection(obj, o);
                    }

                }
            }
        }

        protected virtual bool IsPropertyElement(XmlReader reader, IPropertySerializationInfo p)
        {
            return Settings.TreatAsEqual(reader.NamespaceURI, p.Namespace) && Settings.TreatAsEqual(reader.LocalName, p.ElementName);
        }

        protected virtual void InitializeAttributeProperties(XmlReader reader, object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
            var cont = reader.MoveToFirstAttribute();
            while (cont)
            {
                var foundAttribute = false;
                foreach (IPropertySerializationInfo p in info.AttributeProperties)
                {
                    if (IsPropertyElement(reader, p))
                    {
                        InitializePropertyFromText(p, obj, reader.Value, context);
                        foundAttribute = true;
                        break;
                    }
                }
                if (!foundAttribute)
                {
                    HandleUnknownAttribute(reader, obj, info, context);
                }
                cont = reader.MoveToNextAttribute();
            }
            reader.MoveToElement();
        }

        protected virtual void HandleUnknownAttribute(XmlReader reader, object obj, ITypeSerializationInfo info, XmlSerializationContext context)
        {
        }

        protected virtual bool HandleException(Exception ex)
        {
            return true;
        }


        public ITypeSerializationInfo GetSerializationInfo(Type type, bool createIfNecessary)
        {
            ITypeSerializationInfo info;
            if (type == null) throw new ArgumentNullException("type");

            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return GetSerializationInfo(typeInfo.GenericTypeArguments[0], createIfNecessary);
            }
            if (typeInfo.IsInterface)
            {
                foreach (var att in typeInfo.GetCustomAttributes<XmlDefaultImplementationTypeAttribute>(false))
                {
                    return GetSerializationInfo(att.DefaultImplementationType, createIfNecessary);
                }
            }
            if (!types.TryGetValue(type, out info))
            {
                if (createIfNecessary)
                {
                    info = AddType(type);
                }
                else
                {
                    foreach (XmlTypeSerializationInfo tmp in types.Values)
                    {
                        if (tmp.Type.GetTypeInfo().IsAssignableFrom(typeInfo))
                        {
                            if (info == null || info.Type.GetTypeInfo().IsAssignableFrom(tmp.Type.GetTypeInfo()))
                            {
                                info = tmp;
                            }
                        }
                    }
                    types.Add(type, info);
                }
            } 
            return info;
        }

        public object Deserialize(TextReader input, bool fragment)
        {
            return Deserialize(input);
        }
    }


}