namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ExternalDataType
    /// </summary>
    /// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
    public interface IExternalDataType : IIdentifiableType
    {
        /// <remarks>A URI to access documentation and tools to interpret the external format of the ExternalData instance. 
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</remarks>
        /// <remarks>min 0, max 1</remarks>
        string ExternalFormatDocumentation { get; set; }

        /// <remarks>min 0, max 1</remarks>
        FileFormatInfo FileFormat { get; set; }

        /// <remarks>The location of the data file.</remarks>
        /// Required Attribute
        /// string
        string Location { get; set; }
    }
}