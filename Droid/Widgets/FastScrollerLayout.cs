using System;
using Android.Animation;
using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace OpenGeoDB.Droid.Widgets
{
    [Register("openGeoDB.droid.widgets.FastScrollerLayout")]
    public class FastScrollerLayout : LinearLayout
    {
        private const int AnimationDuration = 200;
        private const int TrackSnapRange = 5;

        private int _height;
        private RecyclerView _recyclerView;

        public FastScrollerLayout(Context context) : base(context)
        {
            Initialize(context);
        }

        public FastScrollerLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context);
        }

        public FastScrollerLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Initialize(context);
        }

        public TextView BubbleView { get; private set; }

        public View HandleView { get; private set; }

        public RecyclerScrollListener ScrollListener { get; private set; }

        public ObjectAnimator CurrentBubbleAnimator { get; set; }

        public ObjectAnimator CurrentHandleAnimator { get; set; }

        public Func<int, string> TitleFactory { get; set; }

        public RecyclerView RecyclerView
        {
            get => _recyclerView;
            set
            {
                _recyclerView = value;
                _recyclerView?.AddOnScrollListener(ScrollListener);
            }
        }

        private void Initialize(Context context)
        {
            ScrollListener = new RecyclerScrollListener(this);

            Orientation = Orientation.Horizontal;

            SetClipChildren(false);

            LayoutInflater.FromContext(context).Inflate(Resource.Layout.LayoutFastScroller, this, true);

            BubbleView = FindViewById<TextView>(Resource.Id.Bubble);
            HandleView = FindViewById<View>(Resource.Id.Handle);

            BubbleView.Visibility = ViewStates.Invisible;
            HandleView.Visibility = ViewStates.Invisible;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            _height = h;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    if (e.GetX() < HandleView.GetX())
                    {
                        return false;
                    }

                    if (CurrentBubbleAnimator != null)
                    {
                        CurrentBubbleAnimator.Cancel();
                    }

                    if (BubbleView.Visibility == ViewStates.Invisible)
                    {
                        ShowBubble();
                    }

                    if (CurrentHandleAnimator != null)
                    {
                        CurrentHandleAnimator.Cancel();
                    }

                    if (HandleView.Visibility == ViewStates.Invisible)
                    {
                        ShowHandle(false);
                    }

                    HandleView.Selected = true;
                    SetPosition(e.GetY());
                    SetRecyclerViewPosition(e.GetY());
                    return true;
                case MotionEventActions.Move:
                    SetPosition(e.GetY());
                    SetRecyclerViewPosition(e.GetY());
                    return true;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    HandleView.Selected = false;
                    HideBubble();
                    HideHandle();
                    return true;
            }

            return base.OnTouchEvent(e);
        }

        protected void SetRecyclerViewPosition(float y)
        {
            if (RecyclerView == null)
            {
                return;
            }

            var itemCount = RecyclerView.GetAdapter().ItemCount;
            float proportion;

            if ((int)HandleView.GetY() == 0)
            {
                proportion = 0;
            }
            else if (HandleView.GetY() + HandleView.Height >= _height - TrackSnapRange)
            {
                proportion = 1;
            }
            else
            {
                proportion = y / _height;
            }

            var targetPosition = GetValueInRange(0, itemCount - 1, (int)(proportion * itemCount));

            RecyclerView.ScrollToPosition(targetPosition);

            BubbleView.Text = TitleFactory?.Invoke(targetPosition);
        }

        protected int GetValueInRange(int min, int max, int value)
        {
            return Math.Min(Math.Max(min, value), max);
        }

        public void SetPosition(float y)
        {
            var bubbleHeight = BubbleView.Height;
            var handleHeight = HandleView.Height;

            HandleView.SetY(GetValueInRange(0, _height - handleHeight, (int)(y - handleHeight / 2)));
            BubbleView.SetY(GetValueInRange(0, _height - bubbleHeight, (int)(y - bubbleHeight / 2)));
        }

        public void ShowHandle(bool animated = true)
        {
            HandleView.Visibility = ViewStates.Visible;

            if (animated)
            {
                if (CurrentHandleAnimator != null)
                {
                    CurrentHandleAnimator.Cancel();
                }

                CurrentHandleAnimator = (ObjectAnimator)ObjectAnimator.OfFloat(HandleView, "alpha", 0f, 1f)
                                                                      .SetDuration(AnimationDuration);
                CurrentHandleAnimator.Start();
            }
            else
            {
                HandleView.Alpha = 1f;
            }
        }

        public void HideHandle(bool animated = true)
        {
            if (animated)
            {
                if (CurrentHandleAnimator != null)
                {
                    CurrentHandleAnimator.Cancel();
                }

                CurrentHandleAnimator = (ObjectAnimator)ObjectAnimator.OfFloat(HandleView, "alpha", 1f, 0f)
                                                                      .SetDuration(AnimationDuration);
                CurrentHandleAnimator.AddListener(new HandleAnimationEndListener(this));
                CurrentHandleAnimator.Start();
            }
            else
            {
                HandleView.Alpha = 0f;
            }
        }

        public void ShowBubble()
        {
            BubbleView.Visibility = ViewStates.Visible;

            if (CurrentBubbleAnimator != null)
            {
                CurrentBubbleAnimator.Cancel();
            }

            CurrentBubbleAnimator = (ObjectAnimator)ObjectAnimator.OfFloat(BubbleView, "alpha", 0f, 1f).SetDuration(AnimationDuration);
            CurrentBubbleAnimator.Start();
        }

        public void HideBubble()
        {
            if (CurrentBubbleAnimator != null)
            {
                CurrentBubbleAnimator.Cancel();
            }

            CurrentBubbleAnimator = (ObjectAnimator)ObjectAnimator.OfFloat(BubbleView, "alpha", 1f, 0f).SetDuration(AnimationDuration);
            CurrentBubbleAnimator.AddListener(new BubbleAnimationEndListener(this));
            CurrentBubbleAnimator.Start();
        }
    }
}
