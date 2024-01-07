using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the direction of a UGui Scrollbar component.")]
	public class uGuiScrollbarGetDirection : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Scrollbar))]
		[Tooltip("The GameObject with the Scrollbar UGui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The direction of the UGui Scrollbar component.")]
		[ObjectType(typeof(Scrollbar.Direction))]
		public FsmEnum direction;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private Scrollbar _scrollbar;

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
				_scrollbar = ownerDefaultTarget.GetComponent<Scrollbar>();
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
			if (_scrollbar != null)
			{
				direction.Value = _scrollbar.direction;
			}
		}
	}
}
