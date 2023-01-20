using System.Xml;

namespace Mntone.RWinRT
{
#if NET6_0_OR_GREATER
	public record struct ResourceData
#else
	public struct ResourceData
#endif
	{
		public string Name;
		public string Value;
		public XmlSpace Space;
	}

#if NET6_0_OR_GREATER
	public record struct ResourcesData
#else
	public struct ResourcesData
#endif
	{
		public string ResourceType;
		public ResourceQualifiers Qualifiers;

		public ResourceData[] Resources;
		public bool IsDefault;
	}
}
