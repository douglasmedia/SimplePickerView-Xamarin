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

using UIKit;
using CoreGraphics;

namespace SimplePickerView.Animation
{
	/// <summary>
	/// Class which represents the animation, when the picker is dismissed.
	/// </summary>
	class SimplePickerViewDismissAnimation : UIViewControllerAnimatedTransitioning
	{
		/// <summary>
		/// Indicates whether the animation is presenting or not
		/// </summary>
		/// <value><c>true</c> if the animation is presenting; otherwise, <c>false</c>.</value>
		public bool IsPresenting { get; set; }

		public float Duration { get; set; }

		public SimplePickerViewDismissAnimation (float transitionDuration)
		{
			Duration = transitionDuration;
		}

		public override double TransitionDuration (IUIViewControllerContextTransitioning transitionContext)
		{
			return Duration;
		}

		public override void AnimateTransition (IUIViewControllerContextTransitioning transitionContext)
		{
			var fromViewController = transitionContext.GetViewControllerForKey (UITransitionContext.FromViewControllerKey);
			var toViewController = transitionContext.GetViewControllerForKey (UITransitionContext.ToViewControllerKey);

			var screenBounds = UIScreen.MainScreen.Bounds;
			var fromFrame = fromViewController.View.Frame;

			UIView.AnimateNotify (Duration, delegate {
				toViewController.View.Alpha = 1.0f;

				switch (fromViewController.InterfaceOrientation) {
				case UIInterfaceOrientation.Portrait:
					fromViewController.View.Frame = new CGRect (0, screenBounds.Height, fromFrame.Width, fromFrame.Height);
					break;
				case UIInterfaceOrientation.LandscapeLeft:
					fromViewController.View.Frame = new CGRect (screenBounds.Width, 0, fromFrame.Width, fromFrame.Height);
					break;
				case UIInterfaceOrientation.LandscapeRight:
					fromViewController.View.Frame = new CGRect (screenBounds.Width * -1, 0, fromFrame.Width, fromFrame.Height);
					break;
				}
			}, delegate {
				transitionContext.CompleteTransition (true);
			});
		}
	}
}
