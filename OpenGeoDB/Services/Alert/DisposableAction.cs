using System;

namespace OpenGeoDB.Services.Alert
{
    public class DisposableAction : IDisposable
    {
        protected Action Action
        {
            get;
            private set;
        }

        public DisposableAction(Action action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Dispose()
        {
            Action();

            GC.SuppressFinalize(this);
        }
    }
}
