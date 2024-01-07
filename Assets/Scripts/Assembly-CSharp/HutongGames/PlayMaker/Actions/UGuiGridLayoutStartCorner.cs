using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set the Start Corner")]
	public class UGuiGridLayoutStartCorner : FsmStateAction
	{
		public enum StartCorner
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

		[Tooltip("Start Corner selection")]
		public StartCorner startCorner;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		public override void Reset()
		{
			gameObject = null;
			startCorner = StartCorner.UpperLeft;
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
				switch (startCorner)
				{
				case StartCorner.UpperLeft:
					_Grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
					break;
				case StartCorner.UpperRight:
					_Grid.startCorner = GridLayoutGroup.Corner.UpperRight;
					break;
				case StartCorner.LowerLeft:
					_Grid.startCorner = GridLayoutGroup.Corner.LowerLeft;
					break;
				case StartCorner.LowerRight:
					_Grid.startCorner = GridLayoutGroup.Corner.LowerRight;
					break;
				}
			}
		}
	}
}
