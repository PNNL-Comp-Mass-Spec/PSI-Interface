using System;
using System.Collections.Generic;
using PSI_Interface.IdentData.mzIdentML;
using PSI_Interface.Utils;

namespace PSI_Interface.IdentData.IdentDataObjs
{
    /// <summary>
    /// MzIdentML DatabaseTranslationType
    /// </summary>
    /// <remarks>A specification of how a nucleic acid sequence database was translated for searching.</remarks>
    public class DatabaseTranslationObj : IdentDataInternalTypeAbstract, IEquatable<DatabaseTranslationObj>
    {
        // Ignore Spelling: MzIdentML

        private IdentDataList<TranslationTableObj> _translationTables;

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseTranslationObj()
        {
            TranslationTables = new IdentDataList<TranslationTableObj>(1);
            Frames = new List<int>();
        }

        /// <summary>
        /// Create an object using the contents of the corresponding MzIdentML object
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="idata"></param>
        public DatabaseTranslationObj(DatabaseTranslationType dt, IdentDataObj idata)
            : base(idata)
        {
            TranslationTables = new IdentDataList<TranslationTableObj>(1);
            Frames = new List<int>(1);

            if ((dt.TranslationTable?.Count > 0))
            {
                TranslationTables.AddRange(dt.TranslationTable, t => new TranslationTableObj(t, IdentData));
            }
            if (dt.frames != null)
                Frames = new List<int>(dt.frames);
        }


        /// <summary>
        /// Translation tables
        /// </summary>
        /// <remarks>min 1, max unbounded</remarks>
        public IdentDataList<TranslationTableObj> TranslationTables
        {
            get => _translationTables;
            set
            {
                _translationTables = value;
                if (_translationTables != null)
                    _translationTables.IdentData = IdentData;
            }
        }

        /// <summary>The frames in which the nucleic acid sequence has been translated as a space separated IdentDataList</summary>
        /// <remarks>Optional Attribute</remarks>
        /// <returns>List of allowed frames: -3, -2, -1, 1, 2, 3</returns>
        public List<int> Frames { get; set; }

        #region Object Equality

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public override bool Equals(object other)
        {
            return other is DatabaseTranslationObj o && Equals(o);
        }

        /// <summary>
        /// Object equality
        /// </summary>
        /// <param name="other"></param>
        public bool Equals(DatabaseTranslationObj other)
        {
            if (ReferenceEquals(this, other))
                return true;

            if (other == null)
                return false;

            return Equals(TranslationTables, other.TranslationTables) &&
                   ListUtils.ListEqualsUnOrdered(Frames, other.Frames);
        }

        /// <summary>
        /// Object hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TranslationTables != null ? TranslationTables.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Frames != null ? Frames.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
