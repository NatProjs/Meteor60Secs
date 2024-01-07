using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Tweens the color of the CanvasRenderer color associated with this Graphic.")]
	public class uGuiGraphicCrossFadeColor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Color target of the UI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
		public FsmColor color;

		[Tooltip("The red channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat red;

		[Tooltip("The green channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat green;

		[Tooltip("The blue channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat blue;

		[Tooltip("The alpha channel Color target of the UI component. Leave to none for no effect, else it overrides the color property")]
		public FsmFloat alpha;

		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoredTimeScale;

		[Tooltip("Should also Tween the alpha channel?")]
		public FsmBool useAlpha;

		private Graphic _component;

		public override void Reset()
		{
			gameObject = null;
			color = null;
			red = new FsmFloat
			{
				UseVariable = true
			};
			green = new FsmFloat
			{
				UseVariable = true
			};
			blue = new FsmFloat
			{
				UseVariable = true
			};
			alpha = new FsmFloat
			{
				UseVariable = true
			};
			useAlpha = null;
			duration = 1f;
			ignoredTimeScale = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_component = ownerDefaultTarget.GetComponent<Graphic>();
			}
			Color value = _component.color;
			if (!color.IsNone)
			{
				value = color.Value;
			}
			if (!red.IsNone)
			{
				value.r = red.Value;
			}
			if (!green.IsNone)
			{
				value.g = green.Value;
			}
			if (!blue.IsNone)
			{
				value.b = blue.Value;
			}
			if (!alpha.IsNone)
			{
				value.a = alpha.Value;
			}
			_component.CrossFadeColor(value, duration.Value, ignoredTimeScale.Value, useAlpha.Value);
			Finish();
		}
	}
}
