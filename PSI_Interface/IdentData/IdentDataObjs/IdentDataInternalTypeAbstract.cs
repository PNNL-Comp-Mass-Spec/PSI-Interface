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
            this._identData = null;
        }

        internal IdentDataInternalTypeAbstract(IdentDataObj parent)
        {
            this.IdentData = parent;
        }

        private IdentDataObj _identData;

        internal IdentDataObj IdentData
        {
            get { return this._identData; }
            set
            {
                if (!ReferenceEquals(this._identData, value))
                {
                    this._identData = value;
                    if (this._identData != null)
                    {
                        //this.CascadeProperties();
                    }
                }
            }
        }

        internal void CascadeProperties()
        {
            if (this._identData == null)
            {
                return;
            }
            //foreach (var prop in this.GetType().GetProperties()) // Only will return public properties...
            // Cascade property setting on down the hierarchy. TODO: TEST THIS EXTENSIVELY!!!
            foreach (var prop in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
            {
                var propValue = prop.GetValue(this);
                var propType = prop.PropertyType;
                if (propValue != null)
                {
                    if (prop.Name.Equals("IdentData"))
                    {
                        continue;
                    }
                    if (propValue is IdentDataInternalTypeAbstract)
                    {
                        var value = ((IdentDataInternalTypeAbstract)(prop.GetValue(this)));
                        value.IdentData = this._identData;
                        value.CascadeProperties();
                    }
                    if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(IdentDataList<>))
                    {
                        var identDataProp = propValue.GetType()
                            .GetProperty("IdentData",
                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.FlattenHierarchy);
                        identDataProp.SetValue(propValue, this._identData);
                        var cascadeMethod = propValue.GetType()
                            .GetMethod("CascadeProperties",
                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.FlattenHierarchy);
                        cascadeMethod.Invoke(propValue, null);
                        //if (propValue is IdentDataList<IdentDataInternalTypeAbstract>)
                        //{
                        //    ((IdentDataList<IdentDataInternalTypeAbstract>) (prop.GetValue(this))).IdentData =
                        //        this._identData;
                        //}
                    }
                }
            }
        }
    }
}
