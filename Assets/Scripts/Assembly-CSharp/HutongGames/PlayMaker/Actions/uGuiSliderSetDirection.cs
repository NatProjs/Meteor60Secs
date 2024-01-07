using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the direction of a UGui Slider component.")]
	public class uGuiSliderSetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The direction of the UGui slider component.")]
		[ObjectType(typeof(Slider.Direction))]
		public FsmEnum direction;

		[Tooltip("Include the  RectLayouts. Leave to none for no effect")]
		public FsmBool includeRectLayouts;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private Slider _slider;

		private Slider.Direction _originalValue;

		public override void Reset()
		{
			gameObject = null;
			direction = Slider.Direction.LeftToRight;
			includeRectLayouts = new FsmBool
			{
				UseVariable = true
			};
			resetOnExit = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_slider = ownerDefaultTarget.GetComponent<Slider>();
			}
			if (resetOnExit.Value)
			{
				_originalValue = _slider.direction;
			}
			DoSetValue();
		}

		private void DoSetValue()
		{
			if (_slider != null)
			{
				if (includeRectLayouts.IsNone)
				{
					_slider.direction = (Slider.Direction)(object)direction.Value;
				}
				else
				{
					_slider.SetDirection((Slider.Direction)(object)direction.Value, includeRectLayouts.Value);
				}
			}
		}

		public override void OnExit()
		{
			if (!(_slider == null) && resetOnExit.Value)
			{
				if (includeRectLayouts.IsNone)
				{
					_slider.direction = _originalValue;
				}
				else
				{
					_slider.SetDirection(_originalValue, includeRectLayouts.Value);
				}
			}
		}
	}
}
