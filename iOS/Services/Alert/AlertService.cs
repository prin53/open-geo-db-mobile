using System;
using Foundation;
using MvvmCross.Platform;
using OpenGeoDB.iOS.Extensions;
using OpenGeoDB.Services.Alert;
using UIKit;

namespace OpenGeoDB.iOS.Services.Alert
{
    public class AlertService : NSObject, IAlertService
    {
        private const string Tag = nameof(AlertService);

        public AlertService() : this(UIApplication.SharedApplication.GetTopViewController)
        {
            /* Required cosntructor */
        }

        public AlertService(Func<UIViewController> viewControllerFactory)
        {
            ViewControllerFactory = viewControllerFactory ?? throw new ArgumentNullException(nameof(viewControllerFactory));
        }

        protected Func<UIViewController> ViewControllerFactory { get; }

        protected virtual IDisposable Present(Func<UIAlertController> alertControllerFactory)
        {
            UIAlertController alertController = null;

            InvokeOnMainThread(() =>
            {
                alertController = alertControllerFactory();

                var viewController = ViewControllerFactory();
                if (viewController == null)
                {
                    throw new InvalidOperationException("'ViewControllerFactory' must produce not null view controller");
                }

                viewController.PresentViewController(alertController, true, null);
            });

            return new DisposableAction(() =>
            {
                try
                {
                    InvokeOnMainThread(() => alertController.DismissViewController(true, null));
                }
                catch (Exception exception)
                {
                    Mvx.TaggedError(Tag, exception.ToString());
                }
            });
        }

        public IDisposable Alert(string title, string message, string actionTitle, Action action)
        {
            return Present(() =>
            {
                UIAlertController alertController = null;

                InvokeOnMainThread(() =>
                {
                    alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                    alertController.AddAction(UIAlertAction.Create(actionTitle, UIAlertActionStyle.Default, a => action?.Invoke()));
                });

                return alertController;
            });
        }
    }
}
