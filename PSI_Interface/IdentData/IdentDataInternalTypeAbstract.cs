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

        internal IdentData IdentData { get; set; }
    }
}
