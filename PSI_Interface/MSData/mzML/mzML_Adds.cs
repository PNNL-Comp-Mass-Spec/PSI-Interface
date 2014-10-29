using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Interface.MSData.mzML
{
	class mzML_Adds
	{

		///// <summary>
		///// Handle a single binaryDataArray element and child nodes
		///// Called by ReadBinaryDataArrayList (xml hierarchy)
		///// </summary>
		///// <param name="reader">XmlReader that is only valid for the scope of the single binaryDataArray element</param>
		///// <param name="defaultLength">Default array length, coming from spectrum attribute</param>
		///// <returns></returns>
		//private BinaryDataArray ReadBinaryDataArray(XmlReader reader, int defaultLength)
		//{
		//	reader.MoveToContent();
		//	BinaryDataArray bda = new BinaryDataArray();
		//	bda.ArrayLength = defaultLength;
		//	int encLength = Convert.ToInt32(reader.GetAttribute("encodedLength"));
		//	int arrLength = Convert.ToInt32(reader.GetAttribute("arrayLength")); // Override the default; if non-existent, should get 0
		//	if (arrLength > 0)
		//	{
		//		bda.ArrayLength = arrLength;
		//	}
		//	bool compressed = false;
		//	reader.ReadStartElement("binaryDataArray"); // Throws exception if we are not at the "spectrum" tag.
		//	List<Param> paramList = new List<Param>();
		//	while (reader.ReadState == ReadState.Interactive)
		//	{
		//		// Handle exiting out properly at EndElement tags
		//		if (reader.NodeType != XmlNodeType.Element)
		//		{
		//			reader.Read();
		//			continue;
		//		}
		//
		//		switch (reader.Name)
		//		{
		//			case "referenceableParamGroupRef":
		//				// Schema requirements: zero to many instances of this element
		//				string rpgRef = reader.GetAttribute("ref");
		//				paramList.AddRange(_referenceableParamGroups[rpgRef]);
		//				reader.Read();
		//				break;
		//			case "cvParam":
		//				// Schema requirements: zero to many instances of this element
		//				paramList.Add(ReadCvParam(reader.ReadSubtree()));
		//				reader.Read(); // Consume the cvParam element (no child nodes)
		//				break;
		//			case "userParam":
		//				// Schema requirements: zero to many instances of this element
		//				paramList.Add(ReadUserParam(reader.ReadSubtree()));
		//				reader.Read();
		//				break;
		//			case "binary":
		//				// Schema requirements: zero to many instances of this element
		//				// Process the ParamList first.
		//				foreach (Param param in paramList)
		//				{
		//					/*
		//				 * MUST supply a *child* term of MS:1000572 (binary data compression type) only once
		//				 *   e.g.: MS:1000574 (zlib compression)
		//				 *   e.g.: MS:1000576 (no compression)
		//				 * MUST supply a *child* term of MS:1000513 (binary data array) only once
		//				 *   e.g.: MS:1000514 (m/z array)
		//				 *   e.g.: MS:1000515 (intensity array)
		//				 *   e.g.: MS:1000516 (charge array)
		//				 *   e.g.: MS:1000517 (signal to noise array)
		//				 *   e.g.: MS:1000595 (time array)
		//				 *   e.g.: MS:1000617 (wavelength array)
		//				 *   e.g.: MS:1000786 (non-standard data array)
		//				 *   e.g.: MS:1000820 (flow rate array)
		//				 *   e.g.: MS:1000821 (pressure array)
		//				 *   e.g.: MS:1000822 (temperature array)
		//				 * MUST supply a *child* term of MS:1000518 (binary data type) only once
		//				 *   e.g.: MS:1000521 (32-bit float)
		//				 *   e.g.: MS:1000523 (64-bit float)
		//				 */
		//					switch (param.Accession)
		//					{
		//						// MUST supply a *child* term of MS:1000572 (binary data compression type) only once
		//						case "MS:1000574":
		//							//   e.g.: MS:1000574 (zlib compression)
		//							compressed = true;
		//							break;
		//						case "MS:1000576":
		//							//   e.g.: MS:1000576 (no compression)
		//							compressed = false;
		//							break;
		//						// MUST supply a *child* term of MS:1000513 (binary data array) only once
		//						case "MS:1000514":
		//							//   e.g.: MS:1000514 (m/z array)
		//							bda.ArrayType = ArrayType.m_z_array;
		//							break;
		//						case "MS:1000515":
		//							//   e.g.: MS:1000515 (intensity array)
		//							bda.ArrayType = ArrayType.intensity_array;
		//							break;
		//						case "MS:1000516":
		//							//   e.g.: MS:1000516 (charge array)
		//							bda.ArrayType = ArrayType.charge_array;
		//							break;
		//						case "MS:1000517":
		//							//   e.g.: MS:1000517 (signal to noise array)
		//							bda.ArrayType = ArrayType.signal_to_noise_array;
		//							break;
		//						case "MS:1000595":
		//							//   e.g.: MS:1000595 (time array)
		//							bda.ArrayType = ArrayType.time_array;
		//							break;
		//						case "MS:1000617":
		//							//   e.g.: MS:1000617 (wavelength array)
		//							bda.ArrayType = ArrayType.wavelength_array;
		//							break;
		//						case "MS:1000786":
		//							//   e.g.: MS:1000786 (non-standard data array)
		//							bda.ArrayType = ArrayType.non_standard_data_array;
		//							break;
		//						case "MS:1000820":
		//							//   e.g.: MS:1000820 (flow rate array)
		//							bda.ArrayType = ArrayType.flow_rate_array;
		//							break;
		//						case "MS:1000821":
		//							//   e.g.: MS:1000821 (pressure array)
		//							bda.ArrayType = ArrayType.pressure_array;
		//							break;
		//						case "MS:1000822":
		//							//   e.g.: MS:1000822 (temperature array)
		//							bda.ArrayType = ArrayType.temperature_array;
		//							break;
		//						// MUST supply a *child* term of MS:1000518 (binary data type) only once
		//						case "MS:1000521":
		//							//   e.g.: MS:1000521 (32-bit float)
		//							bda.Precision = Precision.Precision32;
		//							break;
		//						case "MS:1000523":
		//							//   e.g.: MS:1000523 (64-bit float)
		//							bda.Precision = Precision.Precision64;
		//							break;
		//					}
		//				}
		//				byte[] bytes = Convert.FromBase64String(reader.ReadElementContentAsString()); // Consumes the start and end elements.
		//				int dataSize = 8;
		//				if (bda.Precision == Precision.Precision32)
		//				{
		//					dataSize = 4;
		//				}
		//				if (compressed)
		//				{
		//					bytes = DecompressZLib(bytes, bda.ArrayLength * dataSize);
		//				}
		//				if (bytes.Length % dataSize != 0 || bytes.Length / dataSize != bda.ArrayLength)
		//				{
		//					// We need to fail out.
		//				}
		//				//byte[] oneNumber = new byte[dataSize];
		//				//bool swapBytes = true;
		//				bda.Data = new double[bda.ArrayLength];
		//				for (int i = 0; i < bytes.Length; i += dataSize)
		//				{
		//					// mzML binary data should always be Little Endian. Some other data formats may use Big Endian, which would require a byte swap
		//					//Array.Copy(bytes, i, oneNumber, 0, dataSize);
		//					//if (swapBytes)
		//					//{
		//					//	Array.Reverse(oneNumber);
		//					//}
		//					if (dataSize == 4)
		//					{
		//						//bda.Data[i / dataSize] = BitConverter.ToSingle(oneNumber, 0);
		//						bda.Data[i / dataSize] = BitConverter.ToSingle(bytes, i);
		//					}
		//					else if (dataSize == 8)
		//					{
		//						//bda.Data[i / dataSize] = BitConverter.ToDouble(oneNumber, 0);
		//						bda.Data[i / dataSize] = BitConverter.ToDouble(bytes, i);
		//					}
		//				}
		//				break;
		//			default:
		//				reader.Skip();
		//				break;
		//		}
		//	}
		//	reader.Close();
		//	return bda;
		//}
		//
		///*********************************************************************************************************************************************
		// * TODO: Flesh out the algorithm/double check it, etc.
		// * Do some more work here.
		// * 
		// ********************************************************************************************************************************************/
		//private byte[] DecompressZLib(byte[] compressedBytes, int expectedBytes)
		//{
		//	var msCompressed = new MemoryStream(compressedBytes);
		//	// We must skip the first two bytes
		//	// See http://george.chiramattel.com/blog/2007/09/deflatestream-block-length-does-not-match.html
		//	// EAT the zlib headers, the rest is a normal 'deflate'd stream
		//	msCompressed.ReadByte();
		//	msCompressed.ReadByte();
		//	//var msInflated = new MemoryStream((int)(msCompressed.Length * 2));
		//	//var newBytes = new byte[msCompressed.Length * 2];
		//	byte[] newBytes = new byte[expectedBytes];
		//	// The last 32 bits (4 bytes) are supposed to be an Adler-32 checksum. Might need to remove them as well.
		//	using (var inflater = new DeflateStream(msCompressed, CompressionMode.Decompress))
		//	{
		//		var bytesRead = inflater.Read(newBytes, 0, expectedBytes);
		//		if (bytesRead != expectedBytes)
		//		{
		//			throw new XmlException("Fail decompressing data...");
		//		}
		//		//while (inflater.CanRead)
		//		//{
		//		//	var readBytes = new byte[4095];
		//		//	// Should be able to change to just this.
		//		//	var bytesRead = inflater.Read(readBytes, 0, readBytes.Length);
		//		//	if (bytesRead != 0)
		//		//	{
		//		//		msInflated.Write(readBytes, 0, bytesRead);
		//		//	}
		//		//}
		//	}
		//	//newBytes = new byte[msInflated.Length];
		//	//msInflated.Read(newBytes, 0, (int)msInflated.Length);
		//	return newBytes;
		//}
	}
}
