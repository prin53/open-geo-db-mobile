using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using OpenGeoDB.Converters;
using OpenGeoDB.ViewModels.ZipCode;

namespace OpenGeoDB.iOS.Views.ZipCode
{
    [MvxChildPresentation]
    [MvxFromStoryboard("Main")]
    public partial class ZipCodeView : MvxViewController<ZipCodeViewModel>
    {
        public ZipCodeView(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        protected ZipCodeTableViewSource Source { get; private set; }

        protected void InitView()
        {
            Source = new ZipCodeTableViewSource(
                _tableView,
                ViewModel.LocalizedTextSource.GetText("SectionInformation"),
                ViewModel.LocalizedTextSource.GetText("SectionNearby")
            );

            _tableView.Source = Source;
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<ZipCodeView, ZipCodeViewModel>();
            set.Bind(this).For(v => v.Title).ToLocalizationId("Title");
            set.Bind(Source).To(vm => vm.Nearby);
            set.Bind(Source).For(v => v.SelectionChangedCommand).To(vm => vm.SelectCommand);
            set.Bind(Source).For(v => v.Location).To(vm => vm.Location);
            set.Bind(Source).For(v => v.LocationFriendly).To(vm => vm.Location).WithConversion(new LocationToStringConverter());
            set.Bind(Source).For(v => v.Nearby).To(vm => vm.Nearby);
            set.Bind(Source).For(v => v.Zip).To(vm => vm.Zip);
            set.Bind(Source).For(v => v.City).To(vm => vm.City);
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
