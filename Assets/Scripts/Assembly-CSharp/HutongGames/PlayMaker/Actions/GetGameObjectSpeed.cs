using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Get the speed of a gameObject. No need to have it set up with a physic component.")]
	public class GetGameObjectSpeed : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("The current speed")]
		[UIHint(UIHint.Variable)]
		public FsmFloat speed;

		[Tooltip("The current speed vector")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 speedVector;

		[Tooltip("Use local or world space for the Speed vector.")]
		public Space space;

		private GameObject go;

		private Vector3 lastPosition;

		public override void Reset()
		{
			gameObject = null;
			speed = null;
			speedVector = null;
			space = Space.World;
		}

		public override void OnEnter()
		{
			go = base.Fsm.GetOwnerDefaultTarget(gameObject);
			lastPosition = go.transform.position;
		}

		public override void OnUpdate()
		{
			doComputeSpeed();
		}

		private void doComputeSpeed()
		{
			if (!(go == null))
			{
				Vector3 position = go.transform.position;
				Vector3 vector = position - lastPosition;
				if (!speed.IsNone)
				{
					speed.Value = vector.magnitude / Time.deltaTime;
				}
				if (space == Space.Self)
				{
					vector = go.transform.InverseTransformPoint(go.transform.position + vector);
				}
				speedVector.Value = vector;
				lastPosition = position;
			}
		}
	}
}
