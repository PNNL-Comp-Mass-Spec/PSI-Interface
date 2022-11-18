namespace PSI_Interface.SharedInterfaces
{
    internal interface ICVInfo
    {
        /// <summary>
        /// CV identifier
        /// </summary>
        string Id { get; }

        /// <summary>
        /// CV name
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// CV URI
        /// </summary>
        string URI { get; }

        /// <summary>
        /// CV Version
        /// </summary>
        string Version { get; }

    }
}
