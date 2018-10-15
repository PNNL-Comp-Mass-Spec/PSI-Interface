# PSI_Interface

Objects and interfaces to support reading and writing of PSI/HUPO standard formats.

### Current Project Status:

mzIdentML can be read and written.

mzML can be read, writing is low priority and hasn't been implemented yet in a way that minimizes memory usage.

## Downloads
### Nuget

NuGet package [PSI_Interface](https://www.nuget.org/packages/PSI_Interface)

### Continuous Integration

[![Build status](https://ci.appveyor.com/api/projects/status/1cc3qxeg7p4603kg?svg=true)](https://ci.appveyor.com/project/PNNLCompMassSpec/psi-interface)

## General:
The readers support reading gzipped mzid and mzML files without needing to extract them

## mzIdentML reading:
### All of the identifications, with peptide and protein connections (easier to use when reading):
Use `var results = PSI_Interface.IdentData.SimpleMZIdentMLReader.Read(filePath);`

### In full (all information)
Use `PSI_Interface.IdentData.mzIdentML.MzIdentMlReaderWriter.Read(filePath)`

For easier interaction with the data, use the following example line:

`var identData = new PSI_Interface.IdentData.IdentDataObj(MzIdentMlReaderWriter.Read(filePath));`

## mzIdentML writing:
The hardest path: Populate `PSI_Interface.IdentData.mzIdentML.MzIdentMLType`, and write with `PSI_Interface.IdentData.mzIdentML.MzIdentMlReaderWriter.Write(mzIdentMLType, filePath)`

A slightly easier path: Populate `PSI_Interface.IdentData.IdentDataObj`, and write with `PSI_Interface.IdentData.mzIdentML.MzIdentMlReaderWriter.Write(new MzIdentMLType(identDataObj), filePath)`

Easiest path: Use `PSI_Interface.IdentData.IdentDataCreator`, calling in relative order the following functions, and providing extra data as available/specified:

```
var creator = new PSI_Interface.IdentData.IdentDataCreator("id", "name");
var software = creator.AddAnalysisSoftware("Software_ID", "Software_Name", "Software_Version", CV.CVID.MS_Software_Name /*if available, or CV.CVID.CVID_Unknown*/, "Software_Name_for_param");
var settings = creator.AddAnalysisSettings(software, "Settings_ID", CV.CVID.MS_ms_ms_search);
var searchDb = creator.AddSearchDatabase("Database_name", "Number_of_entries_in_database", "Database_location", CV.CVID.CVID_Unknown /*or published database if in CV*/, CV.CVID.MS_FASTA_format /*or other database format*/);
settings.AdditionalSearchParams.Items.Add(new CVParamObj(CV.CVID.MS_parent_mass_type_mono /*or other CV term*/); // Add all that are search parameters
settings.AdditionalSearchParams.Items.Add(new UserParamObj("name_of_parameter", "value_of_parameter"); // Add all other search parameters
var mod = new SearchModificationObj(); // set FixedMod, MassDelta, Residues, and add CVParamObjs to CVParams
settings.ModificationParams.Add(mod); // Repeat with a new SearchModificationObj() for each modification in the search
settings.Enzymes.Enzymes.Add(new EnzymeObj() /* populate data */): // Repeat if there are multiple enzymes, exclude if there are none
settings.ParentTolerances.AddRange(new CVParamObj[]
{
    new CVParamObj(CV.CVID.MS_search_tolerance_plus_value, "tolerance value") { UnitCvid = CV.CVID.UO_parts_per_million },
    new CVParamObj(CV.CVID.MS_search_tolerance_minus_value, "tolerance value") { UnitCvid = CV.CVID.UO_parts_per_million },
});
settings.FragmentTolerances.AddRange(new CVParamObj[]
{
    new CVParamObj(CV.CVID.MS_search_tolerance_plus_value, "tolerance value") { UnitCvid = CV.CVID.UO_parts_per_million },
    new CVParamObj(CV.CVID.MS_search_tolerance_minus_value, "tolerance value") { UnitCvid = CV.CVID.UO_parts_per_million },
});
settings.Threshold.Items.Add(new CVParamObj(CV.CVID.MS_no_threshold)); // Or choose an appropriate CV term and value, if a threshold is used

var specData = creator.AddSpectraData("path_to_spectrum_file", "Name_of_dataset", CV.CVID.MS_Thermo_nativeID_format /*or other approriate format type */, CV.CVID.MS_Thermo_RAW_format /*Whatever format the file actually is*/);

foreach (var result in searchResults)
{
    var creator.AddSpectrumIdentification(specData, "spectrum_native_id", "spectrum_elution_time", experimentalMz, charge);

    // Add all of the necessary information to the identification
    var pep = new PeptideObj(match.Sequence); /* add ModificationObj() to Modifications for each modification in the peptide
    specIdent.Peptide = pep;
    var dbSeq = new DbSequenceObj(searchDb, proteinLength, "proteinName", "proteinDescription");
    var pepEv = new PeptideEvidenceObj(dbSeq, pep, peptide_start_location, peptide_end_location, "prefix_residue", "suffix_residue", false /* use 'true' if is a decoy hit */);
    specIdent.AddPeptideEvidence(pepEv); // repeat with a new PeptideEvidenceObj() for every distinct peptide/protein/location match.

    specIdent.CVParams.Add(new CVParamObj() { Cvid = CV.CVID.MS_chemical_compound_formula, Value = match.Composition, }); // Repeat with different CV term and value for each score item
}

// Tie all of the information together
var identData = creator.GetIdentData();

// Write out to file
MzIdentMlReaderWriter.Write(new MzIdentMLType(identData), outputFilePath);
```

## mzML Reading:
Use `PSI_Interface.MSData.SimpleMzMLReader` to read an mzML file.

Several runtime options exist:
* Using random access reading (i.e., allowing non-sequential reading of spectra; default false; setting to true on a gzipped file means it will be extracted to a temp directory for reading)
* Using reduced memory (i.e., not reading the entire file into memory; default true)
* Reading spectra without reading the binary data (i.e., all the metadata, but not peak m/zs or intensities)
* Reading spectra with reading the binary data (i.e., all the metadata, with peak m/zs and intensities)

## Contacts

Written by Bryson Gibbons for the Department of Energy (PNNL, Richland, WA) \
Copyright 2017, Battelle Memorial Institute.  All Rights Reserved. \
E-mail: proteomics@pnnl.gov \
Website: https://omics.pnl.gov/ or https://panomics.pnnl.gov/

## License

The PSI Interface is licensed under the 2-Clause BSD License; 
you may not use this file except in compliance with the License.  You may obtain 
a copy of the License at https://opensource.org/licenses/BSD-2-Clause

Copyright 2018 Battelle Memorial Institute
