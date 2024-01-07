using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Move the transform to the end of the local transform list.")]
	public class SetTransformAsLastSibling : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to move as the last sibling.")]
		public FsmOwnerDefault gameObject;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				ownerDefaultTarget.transform.SetAsLastSibling();
			}
			Finish();
		}
	}
}
