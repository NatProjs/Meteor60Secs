using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the direction of a UGui Slider component.")]
	public class uGuiSliderGetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Slider))]
		[Tooltip("The GameObject with the slider UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The value of the UGui slider component.")]
		[ObjectType(typeof(Slider.Direction))]
		public FsmEnum direction;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private Slider _slider;

		public override void Reset()
		{
			gameObject = null;
			direction = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_slider = ownerDefaultTarget.GetComponent<Slider>();
			}
			DoGetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetValue();
		}

		private void DoGetValue()
		{
			if (_slider != null)
			{
				direction.Value = _slider.direction;
			}
		}
	}
}
