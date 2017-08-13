using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using OpenGeoDB.ViewModels.ZipCodes;

namespace OpenGeoDB.iOS.Views.ZipCodes
{
    [MvxChildPresentation]
    [MvxFromStoryboard("Main")]
    public partial class ZipCodesView : MvxTableViewController
    {
        public ZipCodesView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        protected MvxTableViewSource Source { get; private set; }

        protected void InitView()
        {
            Source = new MvxSimpleTableViewSource(TableView, typeof(ZipCodeTableViewCell));

            TableView.Source = Source;
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<ZipCodesView, ZipCodesViewModel>();
            set.Bind(this).For(v => v.Title).ToLocalizationId("Title");
            set.Bind(Source).To(vm => vm.Items);
            set.Bind(Source).For(v => v.SelectionChangedCommand).To(vm => vm.SelectCommand);
            set.Apply();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitView();

            InitBindings();
        }
    }
}
