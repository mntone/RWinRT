using CommandLine;
using Mntone.RWinRT.Generators;
using System.Collections.Generic;

namespace Mntone.RWinRT
{
	public sealed class Options
	{
		[Option('i', "input", Required = true, Separator = ',', HelpText = "Input filename.")]
		public IEnumerable<string> InputFilenames { get; set; }

		[Option('e', "exclude", Default = new string[] { }, Separator = ',', HelpText = "Exclude resource name.")]
		public IEnumerable<string> ExcludeResources { get; set; }

		[Option('o', "output", Required = true, HelpText = "Output directory.")]
		public string OutputDirectory { get; set; }

		[Option('f', "filename", Default = null, HelpText = "Output filename without an extension.")]
		public string FileName { get; set; }

		[Option('n', "namespace", Required = true, HelpText = "Root namespace.")]
		public string RootNamespace { get; set; }

		[Option("impl-namespace", Default = "__impl", HelpText = "Impl namespace.")]
		public string ImplNamespace { get; set; }

		[Option('d', "default", Default = "Resources", HelpText = "Default filename.")]
		public string DefaultResourceFilename { get; set; }

		[Option("indent", Default = "Space4", HelpText = "Indent (Tab, Space1, Space2, Space3, ...).")]
		public string Indent { get; set; }

		[Option('l', "language", Default = "en-US", HelpText = "Default language.")]
		public string DefaultLanguage { get; set; }

		[Option("langver", Default = Language.CSharp10, HelpText = "Output langver (CSharp9, CSharp10, CSharp11, Cpp14 and Cpp17).")]
		public Language LanguageVersion { get; set; }

		[Option("linebreak", Default = LineBreak.LF, HelpText = "Line break (LF, CR, CRLF).")]
		public LineBreak LineBreak { get; set; }

		[Option("mode", Default = int.MaxValue, HelpText = "Output mode (Generator version).")]
		public int Mode { get; set; }

		[Option('p', "public", Default = false, HelpText = "Accessor level is public (C# only).")]
		public bool IsPublic { get; set; }
	}
}
