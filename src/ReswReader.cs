using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Mntone.RWinRT
{
	public static class ReswReader
	{
		public static class Namespaces
		{
			public const string XML = "xml";
			public const string XSD = "http://www.w3.org/2001/XMLSchema";
			public const string MsData = "urn:schemas-microsoft-com:xml-msdata";
		}

		private static string LoadValue(XmlReader reader)
		{
			reader.ReadToNextSibling("value");
			bool isEmpty = reader.IsEmptyElement;
			reader.ReadStartElement("value");
			var retValue = reader.ReadContentAsString();
			if (!isEmpty) reader.ReadEndElement(); // </value>
			return retValue;
		}

		private static ResourceData? LoadData(XmlReader reader)
		{
			if (!reader.IsStartElement("data"))
			{
				reader.ReadToNextSibling("data");
			}

			bool isEmpty = reader.IsEmptyElement;
			var name = reader.GetAttribute("name") ?? throw new ArgumentException("text");
			var space = reader.XmlSpace;

			reader.ReadStartElement("data");
			var retValue = new ResourceData
			{
				Name = name,
				Space = space,
				Value = LoadValue(reader),
			};
			if (!isEmpty) reader.ReadEndElement(); // </data>
			return retValue;
		}


		private static ResourceData[] LoadDataArray(XmlReader reader)
		{
			var retValue = new List<ResourceData>();
			while (reader.IsStartElement("data"))
			{
				var resourceData = LoadData(reader);
				if (resourceData.HasValue)
				{
					retValue.Add(resourceData.Value);
				}
			}
			return retValue.ToArray();
		}

		private static ResourceData[] LoadRoot(XmlReader reader)
		{
			if (!reader.IsStartElement()) return new ResourceData[] { };

			reader.ReadStartElement("root");
			reader.ReadToFollowing("data");
			var retValue = LoadDataArray(reader);
			reader.ReadEndElement(); // </root>
			return retValue;
		}

		public static ResourceData[] Load(Stream stream)
		{
			var settings = new XmlReaderSettings()
			{
				IgnoreComments = true,
				IgnoreWhitespace = true,
			};

			using (var reader = XmlReader.Create(stream, settings))
			{
				reader.Read();
				return LoadRoot(reader);
			}
		}

		public static ResourceData[] LoadFromText(string text)
		{
			var content = new UTF8Encoding(false).GetBytes(text);
			using (var stream = new MemoryStream(content))
			{
				return Load(stream);
			}
		}
	}
}
