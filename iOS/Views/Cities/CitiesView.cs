using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using OpenGeoDB.ViewModels.Cities;

namespace OpenGeoDB.iOS.Views.Cities
{
    [MvxRootPresentation(WrapInNavigationController = true)]
    [MvxFromStoryboard("Main")]
    public partial class CitiesView : MvxTableViewController
    {
        public CitiesView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        protected MvxTableViewSource Source { get; private set; }

        protected void InitView()
        {
            Source = new CitiesTableViewSource(TableView);

            TableView.Source = Source;
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<CitiesView, CitiesViewModel>();
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
