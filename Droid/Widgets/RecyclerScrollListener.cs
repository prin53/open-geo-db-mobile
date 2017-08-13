using System;
using Android.Support.V7.Widget;

namespace OpenGeoDB.Droid.Widgets
{
    public class RecyclerScrollListener : RecyclerView.OnScrollListener
	{
		public RecyclerScrollListener(FastScrollerLayout fastScrollerLayout)
		{
			FastScrollerLayout = fastScrollerLayout ?? throw new ArgumentNullException(nameof(fastScrollerLayout));
		}

		protected FastScrollerLayout FastScrollerLayout { get; }

		public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
		{
			if (!FastScrollerLayout.HandleView.Selected)
			{
				if (newState == RecyclerView.ScrollStateIdle)
				{
					FastScrollerLayout.HideHandle();
				}
				else if (newState == RecyclerView.ScrollStateDragging)
				{
					FastScrollerLayout.ShowHandle();
				}
			}
		}

		public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
		{
			var firstVisibleView = recyclerView.GetChildAt(0);
			var firstVisiblePosition = recyclerView.GetChildPosition(firstVisibleView);
			var visibleRange = recyclerView.ChildCount;
			var lastVisiblePosition = firstVisiblePosition + visibleRange;
			var itemCount = recyclerView.GetAdapter().ItemCount;
			var position = default(int);

			if (firstVisiblePosition == 0)
			{
				position = 0;
			}
			else if (lastVisiblePosition == itemCount - 1)
			{
				position = itemCount - 1;
			}
			else
			{
				position = firstVisiblePosition;
			}

			var proportion = position / (float)itemCount;

			FastScrollerLayout.SetPosition(FastScrollerLayout.Height * proportion);
		}
	}
}
