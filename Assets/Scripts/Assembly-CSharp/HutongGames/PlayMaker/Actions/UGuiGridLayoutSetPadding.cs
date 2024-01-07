using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("change the Padding")]
	public class UGuiGridLayoutSetPadding : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(GridLayoutGroup))]
		[Tooltip("The GameObject with the Grid Layout Group component.")]
		public FsmOwnerDefault gameObject;

		public FsmInt left;

		public FsmInt right;

		public FsmInt top;

		public FsmInt bottom;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		public override void Reset()
		{
			gameObject = null;
			left = new FsmInt
			{
				UseVariable = true
			};
			right = new FsmInt
			{
				UseVariable = true
			};
			top = new FsmInt
			{
				UseVariable = true
			};
			bottom = new FsmInt
			{
				UseVariable = true
			};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetPadding();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetPadding();
		}

		private void DoSetPadding()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				if (ownerDefaultTarget != null)
				{
					_Grid = ownerDefaultTarget.GetComponent<GridLayoutGroup>();
				}
				if (!left.IsNone)
				{
					_Grid.padding.left = left.Value;
				}
				if (!right.IsNone)
				{
					_Grid.padding.right = right.Value;
				}
				if (!top.IsNone)
				{
					_Grid.padding.top = top.Value;
				}
				if (!bottom.IsNone)
				{
					_Grid.padding.bottom = bottom.Value;
				}
			}
		}
	}
}
