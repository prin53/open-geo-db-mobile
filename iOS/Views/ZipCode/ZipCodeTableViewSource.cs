using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Binding.iOS.Views;
using OpenGeoDB.iOS.Visual;
using OpenGeoDB.Models;
using OpenGeoDB.ViewModels.ZipCodes;
using UIKit;

namespace OpenGeoDB.iOS.Views.ZipCode
{
    public class ZipCodeTableViewSource : MvxTableViewSource
    {
        private const int LocationSectionIndex = 0;
        private const int InformationSectionIndex = 1;
        private const int NearbySectionIndex = 2;
        private const int RowsInLcationSection = 1;
        private const int RowsInInformationSectionIndex = 3;

        public ZipCodeTableViewSource(
            UITableView tableView,
            string informationSectionTitle,
            string nearbySectionTitle
        ) : base(tableView)
        {
            InformationSectionTitle = informationSectionTitle;
            NearbySectionTitle = nearbySectionTitle;

            MapTableViewCell = (MapTableViewCell)tableView.DequeueReusableCell(nameof(MapTableViewCell));
            ZipTableViewCell = (ItemTableViewCell)tableView.DequeueReusableCell(nameof(ItemTableViewCell));
            CityTableViewCell = (ItemTableViewCell)tableView.DequeueReusableCell(nameof(ItemTableViewCell));
            LocationTableViewCell = (ItemTableViewCell)tableView.DequeueReusableCell(nameof(ItemTableViewCell));

            ZipTableViewCell.Image = Images.Zip;
            CityTableViewCell.Image = Images.City;
            LocationTableViewCell.Image = Images.Location;

            DeselectAutomatically = true;
        }

        protected MapTableViewCell MapTableViewCell { get; }

        protected ItemTableViewCell ZipTableViewCell { get; }

        protected ItemTableViewCell CityTableViewCell { get; }

        protected ItemTableViewCell LocationTableViewCell { get; }

        public string InformationSectionTitle { get; }

        public string NearbySectionTitle { get; }

        public LocationModel Location
        {
            get => MapTableViewCell.Location;
            set => MapTableViewCell.Location = value;
        }

        public IEnumerable<ZipCodeItemViewModel> Nearby
        {
            get => MapTableViewCell.Nearby;
            set => MapTableViewCell.Nearby = value;
        }

        public string Zip
        {
            get => ZipTableViewCell.TextLabel.Text;
            set => ZipTableViewCell.TextLabel.Text = value;
        }

        public string City
        {
            get => CityTableViewCell.TextLabel.Text;
            set => CityTableViewCell.TextLabel.Text = value;
        }

        public string LocationFriendly
        {
            get => LocationTableViewCell.TextLabel.Text;
            set => LocationTableViewCell.TextLabel.Text = value;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            switch (section)
            {
                case LocationSectionIndex:
                    return Dimensions.HeaderHeightMin;
                default:
                    return Dimensions.HeaderHeight;
            }
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }

        public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            switch (section)
            {
                case InformationSectionIndex:
                    return InformationSectionTitle;
                case NearbySectionIndex:
                    return NearbySectionTitle;
                default:
                    return null;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            switch (section)
            {
                case LocationSectionIndex:
                    return RowsInLcationSection;
                case InformationSectionIndex:
                    return RowsInInformationSectionIndex;
                default:
                    return base.RowsInSection(tableview, section);
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return ItemsSource?.Count() == 0 ? 2 : 3;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            switch (indexPath.Section)
            {
                case LocationSectionIndex:
                    return MapTableViewCell;
                case InformationSectionIndex:
                    switch (indexPath.Row)
                    {
                        case 0:
                            return ZipTableViewCell;
                        case 1:
                            return CityTableViewCell;
                        default:
                            return LocationTableViewCell;
                    }
                default:
                    return tableView.DequeueReusableCell(nameof(NearestZipCodeTableViewCell), indexPath);
            }
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if (indexPath.Section != NearbySectionIndex)
            {
                return;
            }

            base.RowSelected(tableView, indexPath);
        }
    }
}
