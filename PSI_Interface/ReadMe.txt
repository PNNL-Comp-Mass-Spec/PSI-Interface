Several files were created using xsd.exe, which is contained in the Windows SDK.

mzIdentML1_1_0.cs: xsd mzIdentML1.1.0.xsd /classes /namespace:PSI_Interface.mzIdentML
mzML1_1_0.cs:      xsd mzML1.1.0.xsd /classes /namespace:PSI_Interface.mzML
mzML1_1_1_idx.cs:  xsd mzML1.1.1_idx.xsd /classes /namespace:PSI_Interface.mzML /element:IndexListType /element:IndexType /element:OffsetType /element:indexedmzML
mzML1_1_0.cs:      xsd mzML1.1.0.xsd /dataset /namespace:PSI_Interface.mzML
mzML1_1_1_idx.cs:  xsd mzML1.1.1_idx.xsd /dataset /namespace:PSI_Interface.mzML /element:IndexListType /element:IndexType /element:OffsetType /element:indexedmzML

For mzML1_1_1_idx.cs, many lines must be commented out to remove their conflicts with the contents of mzML1_1_0.cs
mzIdentML schema uses xsd:union and xsd:list, so it cannot be transformed into a Dataset Subclass.
mzML can be transformed into a Dataset Subclass, which might be useful (in-memory cache)