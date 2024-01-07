using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("change the grid layout cell size")]
	public class UGuiGridLayoutSetConstraint : FsmStateAction
	{
		public enum Constraint
		{
			Flexible = 0,
			FixedColumnCount = 1,
			FixedRowCount = 2
		}

		[RequiredField]
		[CheckForComponent(typeof(GridLayoutGroup))]
		[Tooltip("The GameObject with the Grid Layout Group component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Contstraint selection")]
		public Constraint constraint;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		public override void Reset()
		{
			gameObject = null;
			constraint = Constraint.Flexible;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoConstraint();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoConstraint();
		}

		private void DoConstraint()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null) && ownerDefaultTarget != null)
			{
				_Grid = ownerDefaultTarget.GetComponent<GridLayoutGroup>();
				switch (constraint)
				{
				case Constraint.Flexible:
					_Grid.constraint = GridLayoutGroup.Constraint.Flexible;
					break;
				case Constraint.FixedRowCount:
					_Grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
					break;
				case Constraint.FixedColumnCount:
					_Grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
					break;
				}
			}
		}
	}
}
