using System;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace OpenGeoDB.iOS.Views.Update
{
    [MvxRootPresentation]
    [MvxFromStoryboard("Main")]
    public partial class UpdateView : MvxViewController
    {
        public UpdateView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }
    }
}
