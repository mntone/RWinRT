using System.Linq;

namespace Mntone.RWinRT.Generators
{
	public abstract class Invokable<T> where T : class, ICodeUnitWriter, new()
	{
		public static T Instance { get; } = new T();

		public static void Write(ICodeWriterContext ctx, params string[] vals)
			=> Instance.WriteCore(ctx, vals);
	}
}

