using System.Reflection;

namespace PSI_Interface.IdentData
{
    public abstract class IdentDataInternalTypeAbstract
    {
        internal IdentDataInternalTypeAbstract()
        {
            this._identData = new IdentData(false);
        }

        internal IdentDataInternalTypeAbstract(IdentData parent)
        {
            this.IdentData = parent;
        }

        private IdentData _identData;

        internal IdentData IdentData
        {
            get { return this._identData; }
            set
            {
                if (!ReferenceEquals(this._identData, value))
                {
                    this._identData = value;
                    if (this._identData != null)
                    {
                        this.CascadeProperties();
                    }
                }
            }
        }

        private void CascadeProperties()
        {
            //foreach (var prop in this.GetType().GetProperties()) // Only will return public properties...
            // Cascade property setting on down the hierarchy. TODO: TEST THIS EXTENSIVELY!!!
            /*foreach (var prop in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
            {
                if (prop.GetValue(this) != null)
                {
                    if (prop.GetValue(this) is IdentDataInternalTypeAbstract)
                    {
                        ((IdentDataInternalTypeAbstract)(prop.GetValue(this))).IdentData = this._identData;
                    }
                    if (prop.GetValue(this) is IdentDataList<IdentDataInternalTypeAbstract>)
                    {
                        ((IdentDataList<IdentDataInternalTypeAbstract>) (prop.GetValue(this))).IdentData = this._identData;
                    }
                }
            }*/
        }
    }
}
