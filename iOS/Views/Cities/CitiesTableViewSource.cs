using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Binding.iOS.Views;
using OpenGeoDB.Extensions;
using OpenGeoDB.ViewModels.Cities;
using UIKit;

namespace OpenGeoDB.iOS.Views.Cities
{
    public class CitiesTableViewSource : MvxTableViewSource
    {
        public CitiesTableViewSource(UITableView tableView) : base(tableView)
        {
            DeselectAutomatically = true;
        }

        public new IEnumerable<IGrouping<string, CityItemViewModel>> ItemsSource
        {
            get => base.ItemsSource as IEnumerable<IGrouping<string, CityItemViewModel>>;
            set => base.ItemsSource = value;
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return ItemsSource?.ElementAt(indexPath.Section)?.ElementAt(indexPath.Row);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return tableView.DequeueReusableCell(nameof(CityTableViewCell), indexPath);
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return ItemsSource?.ElementAt((int)section)?.Key;
        }

        public override string[] SectionIndexTitles(UITableView tableView)
        {
            return ItemsSource?.Select(item => item.Key).ToArray();
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return ItemsSource?.Count() ?? 0;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemsSource?.ElementAtOrDefault((int)section)?.Count() ?? 0;
        }

        public override nint SectionFor(UITableView tableView, string title, nint atIndex)
        {
            return ItemsSource.GetPositionByKey(title);
        }
    }
}
