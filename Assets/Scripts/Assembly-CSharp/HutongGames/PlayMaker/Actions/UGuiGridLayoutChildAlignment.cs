using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("set the Child Alignment")]
	public class UGuiGridLayoutChildAlignment : FsmStateAction
	{
		public enum ChildAlignment
		{
			UpperLeft = 0,
			UpperRight = 1,
			LowerLeft = 2,
			LowerRight = 3
		}

		[RequiredField]
		[CheckForComponent(typeof(GridLayoutGroup))]
		[Tooltip("The GameObject with the Grid Layout Group component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Child Alignment selection")]
		public ChildAlignment childAlignment;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		public override void Reset()
		{
			gameObject = null;
			childAlignment = ChildAlignment.UpperLeft;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStartCorner();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoStartCorner();
		}

		private void DoStartCorner()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null) && ownerDefaultTarget != null)
			{
				_Grid = ownerDefaultTarget.GetComponent<GridLayoutGroup>();
				switch (childAlignment)
				{
				case ChildAlignment.UpperLeft:
					_Grid.childAlignment = TextAnchor.UpperLeft;
					break;
				case ChildAlignment.UpperRight:
					_Grid.childAlignment = TextAnchor.UpperRight;
					break;
				case ChildAlignment.LowerLeft:
					_Grid.childAlignment = TextAnchor.LowerLeft;
					break;
				case ChildAlignment.LowerRight:
					_Grid.childAlignment = TextAnchor.LowerRight;
					break;
				}
			}
		}
	}
}
