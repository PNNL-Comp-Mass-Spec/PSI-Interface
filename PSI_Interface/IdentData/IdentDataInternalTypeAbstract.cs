using System.Reflection;

namespace PSI_Interface.IdentData
{
    public abstract class IdentDataInternalTypeAbstract
    {
        internal IdentDataInternalTypeAbstract()
        {
            this.IdentData = null;
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
                this._identData = value;
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
}
