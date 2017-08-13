using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using OpenGeoDB.ViewModels.ZipCodes;
using UIKit;

namespace OpenGeoDB.iOS.Views.ZipCodes
{
    public partial class ZipCodeTableViewCell : MvxTableViewCell
    {
        public ZipCodeTableViewCell(IntPtr handle) : base(handle)
        {
            Accessory = UITableViewCellAccessory.DisclosureIndicator;

            this.DelayBind(InitBindings);
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<ZipCodeTableViewCell, ZipCodeItemViewModel>();
            set.Bind(TextLabel).To(vm => vm.Zip);
            set.Apply();
        }
    }
}
