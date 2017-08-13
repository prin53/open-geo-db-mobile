using System;
using System.Threading.Tasks;
using OpenGeoDB.Services.Alert;

namespace OpenGeoDB.Test.Unit.Mocks
{
    public class DisposableActionMock : DisposableAction
    {
        public DisposableActionMock(Action acion, TimeSpan delay) : base(acion)
        {
            Task.Run(() => InvokeAction(delay));
        }

        private async void InvokeAction(TimeSpan delay)
        {
            await Task.Delay((int)delay.TotalMilliseconds);

            Action?.Invoke();
        }
    }
}
