namespace PSI_Interface.MSData
{
    /// <summary>
    /// Class specifically for usage with the MSData required types, for the data pass-through for CVParam translation
    /// </summary>
    public class MSDataInternalTypeAbstract
    {
        internal MSData MsData { get; set; }

        internal int BdaDefaultArrayLength { get; set; }
    }
}
