using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using OpenGeoDB.ViewModels.ZipCodes;
using UIKit;

namespace OpenGeoDB.iOS.Views.ZipCode
{
    public partial class NearestZipCodeTableViewCell : MvxTableViewCell
    {
        public NearestZipCodeTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(InitBindings);
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Accessory = UITableViewCellAccessory.DisclosureIndicator;
            SelectionStyle = UITableViewCellSelectionStyle.Default;
        }

        protected void InitBindings()
        {
            var set = this.CreateBindingSet<NearestZipCodeTableViewCell, ZipCodeItemViewModel>();
            set.Bind(TextLabel).To(vm => vm.Zip);
            set.Apply();
        }
    }
}
