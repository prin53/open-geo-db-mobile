using System;
using Android.App;
using MvvmCross.Platform.Droid.Platform;
using OpenGeoDB.Services.Alert;

namespace OpenGeoDB.Droid.Services.Alert
{
    public class AlertService : IAlertService
    {
        public AlertService(IMvxAndroidCurrentTopActivity currentTopActivity)
        {
            CurrentTopActivity = currentTopActivity ?? throw new ArgumentNullException(nameof(currentTopActivity));
        }

        protected IMvxAndroidCurrentTopActivity CurrentTopActivity { get; }

        public IDisposable Alert(string title, string message, string actionTitle, Action action)
        {
            var activity = CurrentTopActivity.Activity;

            Dialog dialog = null;

            var builder = new AlertDialog.Builder(activity)
                .SetTitle(title)
                .SetMessage(message)
                .SetCancelable(false)
                .SetPositiveButton(actionTitle, (senderAlert, args) => action?.Invoke());

            activity.RunOnUiThread(() => dialog = builder.Show());

            return new DisposableAction(() => activity.RunOnUiThread(() => dialog?.Dismiss()));
        }
    }
}