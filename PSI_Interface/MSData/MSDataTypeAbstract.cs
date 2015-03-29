namespace PSI_Interface.MSData
{
    /// <summary>
    /// Class specifically for usage with the MSData required types, for the data pass-through for CVParam translation
    /// </summary>
    public class MSDataInternalTypeAbstract
    {
        internal MSDataInternalTypeAbstract()
        {
            this.MsData = new MSData();
        }

        internal MSDataInternalTypeAbstract(MSData instance)
        {
            this.MsData = instance;
        }

        internal MSData MsData { get; set; }

        internal int BdaDefaultArrayLength { get; set; }
    }
}
