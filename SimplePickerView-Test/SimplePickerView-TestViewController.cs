﻿//
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
using System.Drawing;

using Foundation;
using UIKit;
using SimplePickerView;

namespace SimplePickerViewTest
{
	public partial class SimplePickerView_TestViewController : UIViewController
	{
		public SimplePickerView_TestViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			dateButton.TouchUpInside += async (object sender, EventArgs e) => {
				var picker = new SimplePickerViewController (SimplePickerViewType.Date, this) {
					HeaderText = "Please choose a date",
					HeaderTextColor = UIColor.White,
					HeaderBackgroundColor = UIColor.Blue,
					SourceField = dateText
				};

				await PresentViewControllerAsync (picker, true);
			};

			customButton.TouchUpInside += async (object sender, EventArgs e) => {
				string[] data = new string[]{ "Meyer, Lisa", "Hitler, Adolf" };
				var picker = new SimplePickerViewController (this, data) {
					SourceField = dateText
				};
				await PresentViewControllerAsync (picker, true);
			};
		}
	
	}
}
