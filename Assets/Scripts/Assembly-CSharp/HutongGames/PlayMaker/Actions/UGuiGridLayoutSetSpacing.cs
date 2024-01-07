using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("change the Spacing")]
	public class UGuiGridLayoutSetSpacing : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(GridLayoutGroup))]
		[Tooltip("The GameObject with the Grid Layout Group component.")]
		public FsmOwnerDefault gameObject;

		public FsmFloat spacingX;

		public FsmFloat spacingY;

		public FsmVector2 spacing;

		public bool everyFrame;

		private GridLayoutGroup _Grid;

		private Vector2 newSpacing;

		public override void Reset()
		{
			gameObject = null;
			spacingX = new FsmFloat
			{
				UseVariable = true
			};
			spacingY = new FsmFloat
			{
				UseVariable = true
			};
			spacing = new FsmVector2
			{
				UseVariable = true
			};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetSpacing();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetSpacing();
		}

		private void DoSetSpacing()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				if (ownerDefaultTarget != null)
				{
					_Grid = ownerDefaultTarget.GetComponent<GridLayoutGroup>();
				}
				if (!spacing.IsNone)
				{
					newSpacing = spacing.Value;
				}
				if (!spacingX.IsNone)
				{
					newSpacing.x = spacingX.Value;
				}
				if (!spacingY.IsNone)
				{
					newSpacing.y = spacingY.Value;
				}
				_Grid.spacing = newSpacing;
			}
		}
	}
}
