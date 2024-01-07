using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Tweens the alpha of the CanvasRenderer color associated with this Graphic.")]
	public class uGuiGraphicCrossFadeAlpha : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Graphic))]
		[Tooltip("The GameObject with an Unity UI component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The alpha target")]
		public FsmFloat alpha;

		[Tooltip("The duration of the tween")]
		public FsmFloat duration;

		[Tooltip("Should ignore Time.scale?")]
		public FsmBool ignoredTimeScale;

		private Graphic _component;

		public override void Reset()
		{
			gameObject = null;
			alpha = null;
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
			_component.CrossFadeAlpha(alpha.Value, duration.Value, ignoredTimeScale.Value);
			Finish();
		}
	}
}
