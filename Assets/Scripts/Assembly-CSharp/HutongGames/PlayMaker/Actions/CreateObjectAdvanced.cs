using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Creates a Game Object, usually from a Prefab. Let you define parent and name")]
	public class CreateObjectAdvanced : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to create. Usually a Prefab.")]
		public FsmGameObject gameObject;

		[Tooltip("GameObject  parent. Leave to null or none for no parenting")]
		public FsmOwnerDefault parent;

		[Tooltip("GameObject name. Leave to null or none for default")]
		public FsmString name;

		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;

		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;

		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;

		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;

		public override void Reset()
		{
			gameObject = null;
			parent = new FsmOwnerDefault();
			parent.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			name = new FsmString
			{
				UseVariable = true
			};
			spawnPoint = null;
			position = new FsmVector3
			{
				UseVariable = true
			};
			rotation = new FsmVector3
			{
				UseVariable = true
			};
			storeObject = null;
		}

		public override void OnEnter()
		{
			GameObject value = this.gameObject.Value;
			if (value != null)
			{
				Vector3 vector = Vector3.zero;
				Vector3 euler = Vector3.up;
				if (spawnPoint.Value != null)
				{
					vector = spawnPoint.Value.transform.position;
					if (!position.IsNone)
					{
						vector += position.Value;
					}
					euler = (rotation.IsNone ? spawnPoint.Value.transform.eulerAngles : rotation.Value);
				}
				else
				{
					if (!position.IsNone)
					{
						vector = position.Value;
					}
					if (!rotation.IsNone)
					{
						euler = rotation.Value;
					}
				}
			}
			Finish();
		}
	}
}
