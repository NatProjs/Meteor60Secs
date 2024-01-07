using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Get the ScaleFactor of a CanvasScaler.")]
	public class uGuiCanvasScalerGetScaleFactor : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasScaler))]
		[Tooltip("The GameObject with an Unity UI CanvasScaler component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The scaleFactor of the a CanvasScaler component.")]
		public FsmFloat scaleFactor;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		private CanvasScaler _component;

		public override void Reset()
		{
			gameObject = null;
			scaleFactor = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_component = ownerDefaultTarget.GetComponent<CanvasScaler>();
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
			if (_component != null)
			{
				scaleFactor.Value = _component.scaleFactor;
			}
		}
	}
}
