using System;
using Android.Animation;
using Android.Views;

namespace OpenGeoDB.Droid.Widgets
{
    public class HandleAnimationEndListener : AnimatorListenerAdapter
    {
        public HandleAnimationEndListener(FastScrollerLayout fastScrollerLayout)
        {
            FastScrollerLayout = fastScrollerLayout ?? throw new ArgumentNullException(nameof(fastScrollerLayout));
        }

        protected FastScrollerLayout FastScrollerLayout { get; }

        public override void OnAnimationEnd(Animator animation)
        {
            base.OnAnimationEnd(animation);

            FastScrollerLayout.HandleView.Visibility = ViewStates.Invisible;
            FastScrollerLayout.CurrentHandleAnimator = null;
        }

        public override void OnAnimationCancel(Animator animation)
        {
            base.OnAnimationCancel(animation);

            FastScrollerLayout.HandleView.Visibility = ViewStates.Invisible;
            FastScrollerLayout.CurrentHandleAnimator = null;
        }
    }
}
