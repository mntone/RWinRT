using System;
using System.Text;

namespace Mntone.RWinRT.Generators
{
    public interface ICodeWriterContext
	{
		StringBuilder Builder { get; }

		Language Language { get; }

		string FileName { get; }

		string RootNamespace { get; }

        string Indent { get; }

        string LineBreak { get; }

		Func<string, string> PreferredNameConverter { get; }

		byte NestLevel { get; set; }

		string CurrentIndent();
	}
}
