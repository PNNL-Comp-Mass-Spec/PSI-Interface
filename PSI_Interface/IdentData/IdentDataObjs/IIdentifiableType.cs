namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML IdentifiableType
    /// </summary>
    /// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable.
    /// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
    public interface IIdentifiableType
    {
        /// <summary>An identifier is an unambiguous string that is unique within the scope
        /// (i.e. a document, a set of related documents, or a repository) of its use.</summary>
        /// <remarks>Required Attribute</remarks>
        string Id { get; set; }

        /// <summary>The potentially ambiguous common identifier, such as a human-readable name for the instance.</summary>
        /// <remarks>Required Attribute</remarks>
        string Name { get; set; }
    }
}