using System;
using Android.Animation;
using Android.Views;

namespace OpenGeoDB.Droid.Widgets
{
    public class BubbleAnimationEndListener : AnimatorListenerAdapter
    {
        public BubbleAnimationEndListener(FastScrollerLayout fastScrollerLayout)
        {
            FastScrollerLayout = fastScrollerLayout ?? throw new ArgumentNullException(nameof(fastScrollerLayout));
        }

        protected FastScrollerLayout FastScrollerLayout { get; }

        public override void OnAnimationEnd(Animator animation)
        {
            base.OnAnimationEnd(animation);

            FastScrollerLayout.BubbleView.Visibility = ViewStates.Invisible;
            FastScrollerLayout.CurrentBubbleAnimator = null;
        }
        public override void OnAnimationCancel(Animator animation)
        {
            base.OnAnimationCancel(animation);

            FastScrollerLayout.BubbleView.Visibility = ViewStates.Invisible;
            FastScrollerLayout.CurrentBubbleAnimator = null;
        }
    }
}
