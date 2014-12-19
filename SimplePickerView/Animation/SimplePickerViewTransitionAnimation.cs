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
	class SimplePickerViewTransitionAnimation : UIViewControllerAnimatedTransitioning
	{
		/// <summary>
		/// Indicates whether the animation is presenting or not
		/// </summary>
		/// <value><c>true</c> if the animation is presenting; otherwise, <c>false</c>.</value>
		public bool IsPresenting { get; set; }

		public float Duration { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplePickerView.Animation.SimplePickerViewTransitionAnimation"/> class.
		/// </summary>
		/// <param name="duration">The duration</param>
		public SimplePickerViewTransitionAnimation (float duration)
		{
			Duration = duration;
		}

		public override double TransitionDuration (IUIViewControllerContextTransitioning transitionContext)
		{
			return Duration;
		}

		public override void AnimateTransition (IUIViewControllerContextTransitioning transitionContext)
		{
			var containerView = transitionContext.ContainerView;

			var toViewController = transitionContext.GetViewControllerForKey (UITransitionContext.ToViewControllerKey);
			var fromViewController = transitionContext.GetViewControllerForKey (UITransitionContext.FromViewControllerKey);

			containerView.AddSubview (toViewController.View);
			toViewController.View.Frame = CGRect.Empty;

			var startPoint = GetStartPoint (fromViewController.InterfaceOrientation);
			if (fromViewController.InterfaceOrientation == UIInterfaceOrientation.Portrait) {
				toViewController.View.Frame = new CGRect (startPoint.X, startPoint.Y, fromViewController.View.Frame.Width, fromViewController.View.Frame.Height);
			} else {
				toViewController.View.Frame = new CGRect (startPoint.X, startPoint.Y, fromViewController.View.Frame.Height, fromViewController.View.Frame.Width);
			}

			UIView.AnimateNotify (Duration, delegate {
				toViewController.View.Frame = new CGRect (0, 0, fromViewController.View.Frame.Width, fromViewController.View.Frame.Height);
				fromViewController.View.Alpha = 0.5f;
			}, delegate {
				transitionContext.CompleteTransition (true);
			});
		}

		CGPoint GetStartPoint (UIInterfaceOrientation orientation)
		{
			var screenBounds = UIScreen.MainScreen.Bounds;
			switch (orientation) {
			case UIInterfaceOrientation.Portrait:
				return new CGPoint (0, screenBounds.Height);
			case UIInterfaceOrientation.LandscapeLeft:
				return new CGPoint (screenBounds.Width, 0);
			case UIInterfaceOrientation.LandscapeRight:
				return new CGPoint (screenBounds.Width * -1, 0);
			default:
				return new CGPoint (0, screenBounds.Height);
			}
		}
	}
}

