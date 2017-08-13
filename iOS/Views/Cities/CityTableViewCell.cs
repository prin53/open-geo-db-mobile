using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using OpenGeoDB.ViewModels.Cities;

namespace OpenGeoDB.iOS.Views.Cities
{
    public partial class CityTableViewCell : MvxTableViewCell
    {
        public CityTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(InitBindings);
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<CityTableViewCell, CityItemViewModel>();
            set.Bind(TextLabel).To(vm => vm.Name);
            set.Apply();
        }
    }
}
