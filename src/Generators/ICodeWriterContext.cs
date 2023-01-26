using System;
using System.Text;

namespace Mntone.RWinRT.Generators
{
    public interface ICodeWriterContext
	{
		StringBuilder Builder { get; }

		Language Language { get; }

		string OutputDirectory { get; }

		string FileName { get; }

		string IntermediateFileName { get; }

		string RootNamespace { get; }

        string Indent { get; }

        string LineBreak { get; }

		Func<string, string> PreferredNameConverter { get; }

		bool SetReadOnly { get; }

		byte NestLevel { get; set; }

		string CurrentIndent();
	}
}
