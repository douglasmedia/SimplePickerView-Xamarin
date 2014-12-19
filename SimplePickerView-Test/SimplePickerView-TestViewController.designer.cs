// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace SimplePickerViewTest
{
	[Register ("SimplePickerView_TestViewController")]
	partial class SimplePickerView_TestViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton customButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton dateButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel dateLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField dateText { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (customButton != null) {
				customButton.Dispose ();
				customButton = null;
			}
			if (dateButton != null) {
				dateButton.Dispose ();
				dateButton = null;
			}
			if (dateLabel != null) {
				dateLabel.Dispose ();
				dateLabel = null;
			}
			if (dateText != null) {
				dateText.Dispose ();
				dateText = null;
			}
		}
	}
}
