namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML ExternalDataType
    /// </summary>
    /// <remarks>Data external to the XML instance document. The location of the data file is given in the location attribute.</remarks>
    public interface IExternalDataType : IIdentifiableType
    {
        /// <summary>A URI to access documentation and tools to interpret the external format of the ExternalData instance.
        /// For example, XML Schema or static libraries (APIs) to access binary formats.</summary>
        /// <remarks>min 0, max 1</remarks>
        string ExternalFormatDocumentation { get; set; }

        /// <summary>min 0, max 1</summary>
        FileFormatInfo FileFormat { get; set; }

        /// <summary>The location of the data file.</summary>
        /// <remarks>Required Attribute</remarks>
        string Location { get; set; }
    }
}