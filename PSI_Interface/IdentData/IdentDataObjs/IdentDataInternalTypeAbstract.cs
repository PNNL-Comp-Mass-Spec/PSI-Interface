using System.Reflection;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// Base abstraction for all IdentData types to tie them to the root object
    /// </summary>
    public abstract class IdentDataInternalTypeAbstract
    {
        internal IdentDataInternalTypeAbstract()
        {
            _identData = null;
        }

        internal IdentDataInternalTypeAbstract(IdentDataObj parent)
        {
            IdentData = parent;
        }

        private IdentDataObj _identData;

        internal IdentDataObj IdentData
        {
            get => _identData;
            set
            {
                if (!ReferenceEquals(_identData, value))
                {
                    _identData = value;
                    if (_identData != null)
                    {
                        CascadeProperties();
                    }
                }
            }
        }

        internal void CascadeProperties(bool force = false)
        {
            if (_identData == null)
            {
                return;
            }
            //foreach (var prop in this.GetType().GetProperties()) // Only will return public properties...
            // Cascade property setting on down the hierarchy. TODO: TEST THIS EXTENSIVELY!!!
            foreach (var prop in GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
            {
                var propValue = prop.GetValue(this);
                var propType = prop.PropertyType.GetTypeInfo();
                if (propValue != null)
                {
                    if (prop.Name.Equals("IdentData"))
                    {
                        continue;
                    }
                    if (propValue is IdentDataInternalTypeAbstract)
                    {
                        var value = ((IdentDataInternalTypeAbstract)(prop.GetValue(this)));
                        value.IdentData = _identData;
                        if (force)
                        {
                            value.CascadeProperties();
                        }
                    }
                    if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(IdentDataList<>))
                    {
                        var identDataProp = propValue.GetType()
                            .GetProperty("IdentData",
                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.FlattenHierarchy);
                        identDataProp.SetValue(propValue, _identData);
                        if (force)
                        {
                            var cascadeMethod = propValue.GetType()
                                .GetMethod("CascadeProperties",
                                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                    BindingFlags.FlattenHierarchy);
                            cascadeMethod.Invoke(propValue, new object[] { force });
                        }
                    }
                }
            }
        }
    }
}
