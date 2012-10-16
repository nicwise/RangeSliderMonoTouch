using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace RangesliderMT
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			window.BackgroundColor = UIColor.White;

			var viewController = new UIViewController();
			viewController.View.Frame = window.Frame;



			RangeSlider rs = new RangeSlider(new RectangleF(10,220,300,30));
			rs.MinValue = 1;
			rs.SelectedMinValue = 1;

			rs.MaxValue = 100;
			rs.SelectedMaxValue = 100;

			rs.MinRange = 20;
			rs.ThumbChanged = delegate {
				Console.WriteLine ("Range: {0} -> {1}", rs.SelectedMinValue, rs.SelectedMaxValue);
			};
			rs.BackgroundColor = UIColor.Red;

			viewController.View.AddSubview(rs);

			
			// If you have defined a view, add it here:
			// window.AddSubview (navigationController.View);
			
			// make the window visible

			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

