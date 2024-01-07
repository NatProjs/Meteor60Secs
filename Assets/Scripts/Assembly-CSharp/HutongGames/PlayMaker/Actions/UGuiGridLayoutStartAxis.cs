using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set the Start Axis")]
	public class UGuiGridLayoutStartAxis : FsmStateAction
	{
		public enum StartAxis
		{
			Horizontal = 0,
			Vertical = 1
		}

		[RequiredField]
		[CheckForComponent(typeof(GridLayoutGroup))]
		[Tooltip("The GameObject with the Grid Layout Group component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Start Axis selection")]
		public StartAxis startAxis;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		public override void Reset()
		{
			gameObject = null;
			startAxis = StartAxis.Horizontal;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStartAxis();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoStartAxis();
		}

		private void DoStartAxis()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null) && ownerDefaultTarget != null)
			{
				_Grid = ownerDefaultTarget.GetComponent<GridLayoutGroup>();
				switch (startAxis)
				{
				case StartAxis.Horizontal:
					_Grid.startAxis = GridLayoutGroup.Axis.Horizontal;
					break;
				case StartAxis.Vertical:
					_Grid.startAxis = GridLayoutGroup.Axis.Vertical;
					break;
				}
			}
		}
	}
}
