using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenGeoDB.Services.Alert
{
    /// <summary>
    /// Presents alerts to user.
    /// </summary>
    public interface IAlertService
    {
        /// <summary>
        /// Alerts with the specified title, message, title for action button and action.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="actionTitle">Action title.</param>
        /// <param name="action">Action.</param>
        IDisposable Alert(string title, string message, string actionTitle, Action action);
    }

    public static class IAlertServiceExtensions
    {
        private static void Cancel<TResult>(IDisposable disposable, TaskCompletionSource<TResult> taskCompletionSource)
        {
            disposable.Dispose();
            taskCompletionSource.TrySetCanceled();
        }

        /// <summary>
        /// Alerts with the specified title, message, title for action button and action.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="alertService">Alert service.</param>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="actionTitle">Action title.</param>
        public static Task AlertAsync(this IAlertService alertService, string title, string message, string actionTitle)
        {
            return alertService.AlertAsync(title, message, actionTitle, CancellationToken.None);
        }

        /// <summary>
        /// Alerts with the specified title, message, title for action button and action.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="alertService">Alert service.</param>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="actionTitle">Action title.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task AlertAsync(this IAlertService alertService, string title, string message, string actionTitle, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            Action action = () => taskCompletionSource.TrySetResult(null);

            var disposable = alertService.Alert(title, message, actionTitle, action);

            using (cancellationToken.Register(() => Cancel(disposable, taskCompletionSource)))
            {
                await taskCompletionSource.Task;
            }
        }

        /// <summary>
        /// Shows the error.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="alertService">Alert service.</param>
        /// <param name="message">Message.</param>
        public static Task ErrorAsync(this IAlertService alertService, string message)
        {
            return alertService.AlertAsync("Error", message, "Ok", CancellationToken.None);
        }
    }
}
