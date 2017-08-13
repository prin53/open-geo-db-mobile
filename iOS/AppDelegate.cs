using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform;
using OpenGeoDB.iOS.Visual;
using UIKit;

namespace OpenGeoDB.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        private static void Main(string[] args)
        {
            UIApplication.Main(args, null, nameof(AppDelegate));
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            new Setup(this, new MvxIosViewPresenter(this, Window)).Initialize();

            Mvx.Resolve<IMvxAppStart>().Start();

            Window.MakeKeyAndVisible();

            Theme.Apply();

            return true;
        }
    }
}
