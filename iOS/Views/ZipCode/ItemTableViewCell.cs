using System;
using UIKit;

namespace OpenGeoDB.iOS.Views.ZipCode
{
    public partial class ItemTableViewCell : UITableViewCell
    {
        public ItemTableViewCell(IntPtr handle) : base(handle)
        {
            /* Required constructor */
        }

        public UIImage Image
        {
            get => ImageView.Image;
            set => ImageView.Image = value;
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            SelectionStyle = UITableViewCellSelectionStyle.None;
        }
    }
}
