using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("change the grid layout cell size")]
	public class UGuiGridLayoutSetCellSize : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(GridLayoutGroup))]
		[Tooltip("The GameObject with the Grid Layout Group component.")]
		public FsmOwnerDefault gameObject;

		public FsmFloat cellSizeX;

		public FsmFloat cellSizeY;

		public FsmVector2 cellSize;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		private Vector2 newCellSize;

		public override void Reset()
		{
			gameObject = null;
			cellSizeX = new FsmFloat
			{
				UseVariable = true
			};
			cellSizeY = new FsmFloat
			{
				UseVariable = true
			};
			cellSize = new FsmVector2
			{
				UseVariable = true
			};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetCellSize();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetCellSize();
		}

		private void DoSetCellSize()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				if (ownerDefaultTarget != null)
				{
					_Grid = ownerDefaultTarget.GetComponent<GridLayoutGroup>();
				}
				if (!cellSize.IsNone)
				{
					newCellSize = cellSize.Value;
				}
				if (!cellSizeX.IsNone)
				{
					newCellSize.x = cellSizeX.Value;
				}
				if (!cellSizeY.IsNone)
				{
					newCellSize.y = cellSizeY.Value;
				}
				_Grid.cellSize = newCellSize;
			}
		}
	}
}
