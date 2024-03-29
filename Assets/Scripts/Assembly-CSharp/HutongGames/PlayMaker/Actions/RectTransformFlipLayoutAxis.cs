using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Flips the horizontal and vertical axes of the RectTransform size and alignment, and optionally its children as well.")]
	public class RectTransformFlipLayoutAxis : FsmStateAction
	{
		public enum RectTransformFlipOptions
		{
			Horizontal = 0,
			Vertical = 1,
			Both = 2
		}

		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The axis to flip")]
		public RectTransformFlipOptions axis;

		[Tooltip("Flips around the pivot if true. Flips within the parent rect if false.")]
		public FsmBool keepPositioning;

		[Tooltip("Flip the children as well?")]
		public FsmBool recursive;

		public override void Reset()
		{
			gameObject = null;
			axis = RectTransformFlipOptions.Both;
			keepPositioning = null;
			recursive = null;
		}

		public override void OnEnter()
		{
			DoFlip();
			Finish();
		}

		private void DoFlip()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget != null))
			{
				return;
			}
			RectTransform component = ownerDefaultTarget.GetComponent<RectTransform>();
			if (component != null)
			{
				if (axis == RectTransformFlipOptions.Both)
				{
					RectTransformUtility.FlipLayoutAxes(component, keepPositioning.Value, recursive.Value);
				}
				else if (axis == RectTransformFlipOptions.Horizontal)
				{
					RectTransformUtility.FlipLayoutOnAxis(component, 0, keepPositioning.Value, recursive.Value);
				}
				else if (axis == RectTransformFlipOptions.Vertical)
				{
					RectTransformUtility.FlipLayoutOnAxis(component, 1, keepPositioning.Value, recursive.Value);
				}
			}
		}
	}
}
