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

namespace SimplePickerView.Animation
{
	class SimplePickerViewTransitionDelegate : UIViewControllerTransitioningDelegate
	{
		private float _duration = 0.25f;

		public float Duration { 
			get {
				return _duration;
			} 
			set {
				_duration = value;
			}
		}

		public SimplePickerViewTransitionDelegate (float duration)
		{
			_duration = duration;
		}

		public override IUIViewControllerAnimatedTransitioning PresentingController (UIViewController presented, UIViewController presenting, UIViewController source)
		{
			var animation = new SimplePickerViewTransitionAnimation (_duration);
			animation.IsPresenting = true;
			return animation;
		}

		public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController (UIViewController dismissed)
		{
			var animation = new SimplePickerViewDismissAnimation (_duration);
			animation.IsPresenting = false;
			return animation;
		}

		public override IUIViewControllerInteractiveTransitioning GetInteractionControllerForDismissal (IUIViewControllerAnimatedTransitioning animator)
		{
			throw new System.NotImplementedException ();
		}

		public override IUIViewControllerInteractiveTransitioning GetInteractionControllerForPresentation (IUIViewControllerAnimatedTransitioning animator)
		{
			throw new System.NotImplementedException ();
		}
	
	}
}

