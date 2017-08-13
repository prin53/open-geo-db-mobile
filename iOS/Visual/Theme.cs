using UIKit;

namespace OpenGeoDB.iOS.Visual
{
    public static class Theme
    {
        public static void Apply()
        {
            UIApplication.SharedApplication.KeyWindow.TintColor = Palette.Tint;

            UINavigationBar.Appearance.Translucent = true;
        }
    }
}
