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

using System.Collections.Generic;
using UIKit;
using CoreGraphics;

namespace SimplePickerView
{
	/// <summary>
	/// Generic class which represents the data of the PickerView. 
	/// </summary>
	public class SimplePickerViewModel : UIPickerViewModel
	{
		public string[] Items { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplePickerView.SimplePickerViewModel"/> class.
		/// </summary>
		/// <param name="data">The data to display </param>
		public SimplePickerViewModel (List<string> data)
		{
			Items = data.ToArray ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplePickerView.SimplePickerViewModel"/> class.
		/// </summary>
		/// <param name="data">The data to display</param>
		public SimplePickerViewModel (string[] data)
		{
			Items = data;
		}

		public override int GetComponentCount (UIPickerView picker)
		{
			return 1;
		}

		public override int GetRowsInComponent (UIPickerView picker, int component)
		{
			return Items.Length;
		}

		public override UIView GetView (UIPickerView picker, int row, int component, UIView view)
		{
			var label = new UILabel (new CGRect (0, 0, 300, 37)) {
				BackgroundColor = UIColor.Clear,
				Text = Items [row],
				TextAlignment = UITextAlignment.Center
			};

			return label;
		}
	}
}
