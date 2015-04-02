using System.Reflection;

namespace PSI_Interface.MSData
{
    /// <summary>
    /// Class specifically for usage with the MSData required types, for the data pass-through for CVParam translation
    /// </summary>
    public abstract class MSDataInternalTypeAbstract
    {
        internal MSDataInternalTypeAbstract()
        {
            this.MsData = new MSData();
        }

        internal MSDataInternalTypeAbstract(MSData instance)
        {
            this.MsData = instance;
        }

        private MSData _msData;

        internal MSData MsData
        {
            get
            {
                return this._msData;
            }
            set
            {
                this._msData = value;
                if (this._msData != null)
                {
                    CascadeProperties();
                }
            }
        }

        internal int BdaDefaultArrayLength { get; set; } // Specifically needed for MSData -> mzML, for determining if ArrayLength should be specified.

        private void CascadeProperties()
        {
            //foreach (var prop in this.GetType().GetProperties()) // Only will return public properties...
            // Cascade property setting on down the hierarchy. TODO: TEST THIS EXTENSIVELY!!!
            foreach (var prop in this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
            {
                if (prop.GetValue(this) != null)
                {
                    if (prop.GetValue(this) is MSDataInternalTypeAbstract)
                    {
                        // TODO: how to handle the change of the BdaDefaultArrayLength, without wiping out the values of spectrum inappropriately? Probably best is to override the BdaDefaultArrayLength in spectrum?
                        ((MSDataInternalTypeAbstract)(prop.GetValue(this))).MsData = this._msData;
                    }
                    if (prop.GetValue(this) is MSDataList<MSDataInternalTypeAbstract>)
                    {
                        ((MSDataList<MSDataInternalTypeAbstract>)(prop.GetValue(this))).MsData = this._msData;
                    }
                }
            }
        }
    }
}
