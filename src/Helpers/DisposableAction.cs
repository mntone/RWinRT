using System;

namespace Mntone.RWinRT.Helpers
{
    public sealed class DisposableAction : IDisposable
    {
        private Action DiposableAction { get; }

        public DisposableAction(Action diposableAction)
        {
            DiposableAction = diposableAction;
        }

        public void Dispose() => DiposableAction();
    }
}
