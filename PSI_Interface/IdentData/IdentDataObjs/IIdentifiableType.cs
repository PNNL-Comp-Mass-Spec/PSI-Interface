namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML IdentifiableType
    /// </summary>
    /// <remarks>Other classes in the model can be specified as sub-classes, inheriting from Identifiable. 
    /// Identifiable gives classes a unique identifier within the scope and a name that need not be unique.</remarks>
    public interface IIdentifiableType
    {
        /// <remarks>An identifier is an unambiguous string that is unique within the scope 
        /// (i.e. a document, a set of related documents, or a repository) of its use.</remarks>
        /// Required Attribute
        /// string
        string Id { get; set; }

        /// <remarks>The potentially ambiguous common identifier, such as a human-readable name for the instance.</remarks>
        /// Required Attribute
        /// string
        string Name { get; set; }
    }
}