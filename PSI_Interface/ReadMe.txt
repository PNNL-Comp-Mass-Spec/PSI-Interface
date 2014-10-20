Several files were created using xsd.exe, which is contained in the Windows SDK.

mzIdentML1_1_0.cs: xsd mzIdentML1_1_0.xsd /classes
mzML1_1_0.cs: xsd mzML1_1_0.xsd /classes
mzML1_1_1_idx.cs: xsd mzML1_1_1_idx.xsd /classes
For mzML1_1_1_idx.cs, many lines must be commented out to remove their conflicts with the contents of mzML1_1_0.cs