using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets The scrollrect vertical flag")]
	public class uGuiScrollRectSetVertical : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(ScrollRect))]
		[Tooltip("The GameObject with the ScrollRect UGui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The vertical flag")]
		public FsmBool vertical;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private ScrollRect _scrollRect;

		private bool _originalValue;

		public override void Reset()
		{
			gameObject = null;
			vertical = null;
			resetOnExit = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_scrollRect = ownerDefaultTarget.GetComponent<ScrollRect>();
			}
			if (resetOnExit.Value)
			{
				_originalValue = _scrollRect.vertical;
			}
			DoSetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetValue();
		}

		private void DoSetValue()
		{
			if (_scrollRect != null)
			{
				_scrollRect.vertical = vertical.Value;
			}
		}

		public override void OnExit()
		{
			if (!(_scrollRect == null) && resetOnExit.Value)
			{
				_scrollRect.vertical = _originalValue;
			}
		}
	}
}
