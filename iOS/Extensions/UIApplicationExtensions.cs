using System.Linq;
using UIKit;

namespace OpenGeoDB.iOS.Extensions
{
    public static class UIApplicationExtensions
    {
        public static UIViewController GetTopViewController(this UIApplication application)
        {
            var topViewController = application?.KeyWindow?.RootViewController;

            if (topViewController == null)
            {
                return null;
            }

            while (topViewController.PresentedViewController != null)
            {
                topViewController = topViewController.PresentedViewController;
            }

            var navigationController = topViewController as UINavigationController;
            if (navigationController != null)
            {
                topViewController = navigationController.ViewControllers.LastOrDefault();
            }

            if (topViewController == null)
            {
                return navigationController;
            }

            return topViewController;
        }
    }
}
