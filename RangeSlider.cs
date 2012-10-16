using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace RangesliderMT
{
	public class RangeSlider : UIControl
	{

		public float MinValue;
		public float MaxValue;
		public float MinRange;
		public float SelectedMinValue;
		public float SelectedMaxValue;
		public Action<RangeSlider> ThumbChanged;


		private UIImageView minThumb, maxThumb, track, trackBackground;
		float padding = 20f;
		bool maxThumbOn = false, minThumbOn = false;
		float distanceFromCenter;


		public RangeSlider (RectangleF frame) : base(frame)
		{
			trackBackground = new UIImageView(UIImage.FromFile("bar-background.png"));
			trackBackground.Center = Center;
			AddSubview(trackBackground);

			track = new UIImageView(UIImage.FromFile("bar-highlight.png"));
			track.Center = Center;
			AddSubview (track);

			minThumb = new UIImageView(UIImage.FromFile("handle.png"),
			                           UIImage.FromFile ("handle-hover.png"));
			minThumb.Frame = new RectangleF(0,0,Frame.Size.Height, Frame.Size.Height);
			minThumb.ContentMode = UIViewContentMode.Center;
			AddSubview (minThumb);

			maxThumb = new UIImageView(UIImage.FromFile("handle.png"),
			                           UIImage.FromFile ("handle-hover.png"));
			maxThumb.Frame = new RectangleF(0,0,Frame.Size.Height, Frame.Size.Height);
			maxThumb.ContentMode = UIViewContentMode.Center;
			AddSubview (maxThumb);



		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			minThumb.Center = new PointF(XForValue(SelectedMinValue), Center.Y - Frame.Top);
			maxThumb.Center = new PointF(XForValue(SelectedMaxValue), Center.Y - Frame.Top);

			UpdateTrackHighlight();
		}

		private float XForValue(float val)
		{
			return (Frame.Size.Width - (padding * 2))*((val - MinValue) / (MaxValue - MinValue)) + padding;
		}

		private float ValueForX(float x)
		{
			return MinValue + (x-padding) / (Frame.Size.Width - (padding * 2)) * (MaxValue - MinValue);
		}

		public override bool ContinueTracking (UITouch uitouch, UIEvent uievent)
		{

			if (!minThumbOn && !maxThumbOn)
				return true;

			var touchPoint = uitouch.LocationInView (this);
			if (minThumbOn) 
			{
				minThumb.Center = new PointF(Math.Max (
					XForValue(MinValue), Math.Min(touchPoint.X - distanceFromCenter, XForValue(SelectedMaxValue - MinRange))), minThumb.Center.Y - Frame.Top);
				SelectedMinValue = ValueForX (minThumb.Center.X);
			}

			if (maxThumbOn) 
			{

				maxThumb.Center = new PointF(Math.Min (
					XForValue(MaxValue), 
				    Math.Max(
						touchPoint.X - distanceFromCenter, 
						XForValue(SelectedMinValue + MinRange)
					  )
					), maxThumb.Center.Y - Frame.Top);


				SelectedMaxValue = ValueForX (maxThumb.Center.X);
			}

			UpdateTrackHighlight();
			this.SetNeedsLayout();


			if (ThumbChanged != null) ThumbChanged(this);

			return true;
		}

		public override bool BeginTracking (UITouch uitouch, UIEvent uievent)
		{

			var touchPoint = uitouch.LocationInView (this);

			if (minThumb.Frame.Contains (touchPoint)) {
				minThumbOn = true;
				distanceFromCenter = touchPoint.X - minThumb.Center.X;
			} else if (maxThumb.Frame.Contains (touchPoint)) {
				maxThumbOn = true;
				distanceFromCenter = touchPoint.X - maxThumb.Center.X;
			}



			return true;
		}

		public override void EndTracking (UITouch uitouch, UIEvent uievent)
		{
			minThumbOn = false;
			maxThumbOn = false;
		}

		private void UpdateTrackHighlight ()
		{
			track.Frame = new RectangleF(minThumb.Center.X, 
			                             maxThumb.Center.Y - (track.Frame.Height /2),
			                             maxThumb.Center.X - minThumb.Center.X,
			                             track.Frame.Size.Height);
			trackBackground.Frame = new RectangleF(0 + (minThumb.Frame.Size.Width / 2), 
			                                       maxThumb.Center.Y- (trackBackground.Frame.Height /2),
			                                       Frame.Width - (minThumb.Frame.Size.Width), trackBackground.Frame.Size.Height);


		}

	}

}


