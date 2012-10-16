RangeSliderMonoTouch
====================

A MonoTouch port of iOS RangeSlider : https://github.com/buildmobile/iosrangeslider

You can use it like this:

```csharp
	RangeSlider rs = new RangeSlider(new RectangleF(10,220,300,30));
	rs.MinValue = 1;
	rs.SelectedMinValue = 1;

	rs.MaxValue = 100;
	rs.SelectedMaxValue = 100;

	rs.MinRange = 20;
	rs.ThumbChanged = delegate {
		Console.WriteLine ("Range: {0} -> {1}", rs.SelectedMinValue, rs.SelectedMaxValue);
	};


	viewController.View.AddSubview(rs);
```

Images come from the original project.