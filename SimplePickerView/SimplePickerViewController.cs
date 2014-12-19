//
//  Copyright (c) 2014 Douglas Media. All rights reserved.
//
//  This library is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This library is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using UIKit;
using CoreGraphics;

namespace SimplePickerView
{
	public delegate void SimplePickerViewDimissedEventHandler (object sender, EventArgs e);

	public class SimplePickerViewController : UIViewController
	{
		public event SimplePickerViewDimissedEventHandler OnSimplePickerViewDismissed;

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string HeaderText { get; set; }

		/// <summary>
		/// Gets or sets the color of the title text.
		/// </summary>
		/// <value>The color of the title text.</value>
		public UIColor HeaderTextColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the title background.
		/// </summary>
		/// <value>The color of the title background.</value>
		public UIColor HeaderBackgroundColor { get; set; }

		/// <summary>
		/// Gets or sets the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		public UIColor BackgroundColor { get; set; }

		/// <summary>
		/// Returns the date picker.
		/// </summary>
		/// <value>The date picker.</value>
		public UIDatePicker DatePicker { get; private set; }

		/// <summary>
		/// Returns the picker view.
		/// </summary>
		/// <value>The picker view.</value>
		public UIPickerView PickerView { get; private set; }

		/// <summary>
		/// Gets or sets the source field.
		/// </summary>
		/// <value>The source field.</value>
		public UITextField SourceField { get; set; }

		/// <summary>
		/// Gets or sets the source label.
		/// </summary>
		/// <value>The source label.</value>
		public UILabel SourceLabel { get; set; }

		/// <summary>
		/// Gets or sets the done button text.
		/// </summary>
		/// <value>The done button text.</value>
		public string DoneButtonText { get; set; }

		/// <summary>
		/// Gets or sets the height of the header bar.
		/// </summary>
		/// <value>The height of the header bar.</value>
		public int HeaderBarHeight { get; set; }

		SimplePickerViewType pickerType;

		/// <summary>
		/// Gets or sets the type of the picker.
		/// </summary>
		/// <value>The type of the picker.</value>
		public SimplePickerViewType PickerType {
			get {
				return pickerType;
			}
			private set {
				switch (value) {
				case SimplePickerViewType.Date:
					DatePicker = new UIDatePicker (CGRect.Empty);
					DatePicker.Mode = UIDatePickerMode.Date;
					PickerView = null;
					break;
				case SimplePickerViewType.Time:
					DatePicker = new UIDatePicker (CGRect.Empty);
					DatePicker.Mode = UIDatePickerMode.Time;
					PickerView = null;
					break;
				case SimplePickerViewType.DateTime:
					DatePicker = new UIDatePicker (CGRect.Empty);
					DatePicker.Mode = UIDatePickerMode.DateAndTime;
					PickerView = null;
					break;
				case SimplePickerViewType.List:
					PickerView = new UIPickerView (CGRect.Empty);
					DatePicker = null;
					break;
				}
				pickerType = value;
			}
		}

		SimplePickerViewModel source;

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		/// <value>The source.</value>
		public SimplePickerViewModel Source { 
			get { return source; }
			set {
				if (PickerView != null) {
					source = value;
					PickerView.Model = source;
				}
			} 
		}

		string[] data;
		UILabel headerLabel;
		UIButton doneButton;
		UIViewController parentViewController;
		UIView viewContainer;

		/// <summary>
		/// Constructs a new SimplePickerViewController with the given params.
		/// </summary>
		/// <param name="pickerType">The type of the picker.</param>
		/// <param name="parent">The parent view.</param>
		public SimplePickerViewController (SimplePickerViewType pickerType, UIViewController parent)
		{
			HeaderBackgroundColor = UIColor.White;
			HeaderTextColor = UIColor.Blue;
			BackgroundColor = UIColor.White;
			PickerType = pickerType;
			DoneButtonText = "Done";
			HeaderBarHeight = 40;
			parentViewController = parent;
		}

		/// <summary>
		/// Constructs a new SimplePickerViewController with the given params.
		/// </summary>
		/// <param name="parent">The parent view.</param>
		/// <param name="items">Items.</param>
		public SimplePickerViewController (UIViewController parent, string[] items) : this (SimplePickerViewType.List, parent)
		{
			data = items;
			Source = new SimplePickerViewModel (data);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetupComponents ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ShowViewController ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			if (InterfaceOrientation == UIInterfaceOrientation.Portrait || InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight) {
				ShowViewController (true);
				View.SetNeedsDisplay ();
			}
		}

		#region Private Methods

		void SetupComponents ()
		{
			// Sets the background color
			View.BackgroundColor = BackgroundColor;
			viewContainer = new UIView ();

			// Initialize the label for the header
			headerLabel = new UILabel (new CGRect (0, 0, 320 / 2, 44));
			headerLabel.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			headerLabel.BackgroundColor = HeaderBackgroundColor;
			headerLabel.TextColor = HeaderTextColor;
			headerLabel.Text = HeaderText;

			// Initialize the button
			doneButton = UIButton.FromType (UIButtonType.System);
			doneButton.SetTitleColor (HeaderTextColor, UIControlState.Normal);
			doneButton.BackgroundColor = UIColor.Clear;
			doneButton.SetTitle (DoneButtonText, UIControlState.Normal);
			doneButton.TouchUpInside += DoneButtonTapped;

			// Initialize the picker
			switch (PickerType) {
			case SimplePickerViewType.Date:
			case SimplePickerViewType.Time:
			case SimplePickerViewType.DateTime:
				DatePicker.BackgroundColor = BackgroundColor;
				viewContainer.AddSubview (DatePicker);
				break;
			case SimplePickerViewType.List:
				PickerView.BackgroundColor = BackgroundColor;
				viewContainer.AddSubview (PickerView);
				break;
			}

			viewContainer.BackgroundColor = HeaderBackgroundColor;
			viewContainer.AddSubview (headerLabel);
			viewContainer.AddSubview (doneButton);
			Add (viewContainer);
		}

		void ShowViewController (bool onRotate = false)
		{
			var doneButtonSize = new CGSize (71, 30);
			var width = UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.Portrait 
				? parentViewController.View.Frame.Width : parentViewController.View.Frame.Height;

			var containerViewSize = CGSize.Empty;
			switch (pickerType) {
			case SimplePickerViewType.Date:
			case SimplePickerViewType.Time:
			case SimplePickerViewType.DateTime:
				DatePicker.Frame = CGRect.Empty;
				containerViewSize = new CGSize (width, DatePicker.Frame.Height + HeaderBarHeight);
				break;
			case SimplePickerViewType.List:
				PickerView.Frame = CGRect.Empty;
				containerViewSize = new CGSize (width, PickerView.Frame.Height + HeaderBarHeight);
				break;
			}

			var containerViewFrame = CGRect.Empty;
			if (InterfaceOrientation == UIInterfaceOrientation.Portrait) {
				if (onRotate) {
					containerViewFrame = new CGRect (0, View.Frame.Height - containerViewSize.Height, containerViewSize.Width, containerViewSize.Height);
				} else {
					containerViewFrame = new CGRect (0, View.Bounds.Height - containerViewSize.Height, containerViewSize.Width, containerViewSize.Height);
				}
			} else {
				if (onRotate) {
					containerViewFrame = new CGRect (0, View.Frame.Width - containerViewSize.Height, containerViewSize.Width, containerViewSize.Height);
				} else {
					containerViewFrame = new CGRect (0, View.Bounds.Width - containerViewSize.Height, containerViewSize.Width, containerViewSize.Height);
				}
			}
			viewContainer.Frame = containerViewFrame;

			switch (pickerType) {
			case SimplePickerViewType.Date:
			case SimplePickerViewType.Time:
			case SimplePickerViewType.DateTime:
				DatePicker.Frame = new CGRect (DatePicker.Frame.X, HeaderBarHeight, DatePicker.Frame.Width, DatePicker.Frame.Height);
				break;
			case SimplePickerViewType.List:
				PickerView.Frame = new CGRect (PickerView.Frame.X, HeaderBarHeight, viewContainer.Frame.Width, viewContainer.Frame.Height);
				break;
			}

			headerLabel.Frame = new CGRect (10, 4, parentViewController.View.Frame.Width - 100, 35);
			doneButton.Frame = new CGRect (containerViewFrame.Width - doneButtonSize.Width - 10, 7, doneButtonSize.Width, doneButtonSize.Height);
		}

		void DoneButtonTapped (object sender, EventArgs e)
		{
			DismissViewController (true, null);
			if (OnSimplePickerViewDismissed != null) {
				OnSimplePickerViewDismissed (sender, e);
			}

			// Update the textfield, if available
			if (SourceField != null) {
				switch (pickerType) {
				case SimplePickerViewType.Date:
				case SimplePickerViewType.Time: 
				case SimplePickerViewType.DateTime:
					SourceField.Text = DatePicker.Date.ToString ();
					break;
				case SimplePickerViewType.List:
					if (source != null && data != null) {
						var index = PickerView.SelectedRowInComponent (0);
						SourceField.Text = data [index];
					}
					break;
				}
			}

			// Update the textfield, if available
			if (SourceLabel != null) {
				switch (pickerType) {
				case SimplePickerViewType.Date:
				case SimplePickerViewType.Time: 
				case SimplePickerViewType.DateTime:
					SourceLabel.Text = DatePicker.Date.ToString ();
					break;
				case SimplePickerViewType.List:
					if (source != null && data != null) {
						var index = PickerView.SelectedRowInComponent (0);
						SourceLabel.Text = data [index];
					}
					break;
				}
			}
		}

		#endregion;
	}
}

