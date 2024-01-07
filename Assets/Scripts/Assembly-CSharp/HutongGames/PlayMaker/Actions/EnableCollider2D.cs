using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Physics 2d")]
	[Tooltip("Enables/Disables a 2D Collider(or a Rigidbody) in a single GameObject.")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=11580.0")]
	public class EnableCollider2D : FsmStateAction
	{
		public enum Selection
		{
			None = 0,
			Box = 1,
			Circle = 2,
			Edge = 3,
			Polygon = 4,
			Rigidbody = 5
		}

		[ActionSection("Setup")]
		[Tooltip("The GameObject that owns the Collider.")]
		[CheckForComponent(typeof(Collider2D))]
		public FsmOwnerDefault gameObject;

		[Tooltip("Optionally drag a 2D Collider directly into this field (Script name will be ignored).")]
		[Title("Collider")]
		public Collider2D component;

		[Tooltip("The name of the Collider to enable/disable.")]
		[Title("or 2D Collider DropDown")]
		private FsmString script;

		[Title("or 2D Collider Type Select")]
		public Selection colliderSelect;

		[ActionSection("Options")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;

		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;

		[ActionSection("Collider Option")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable/disable all 2D Collider in gameobject.")]
		public FsmBool allCollider;

		private Collider2D componentTarget;

		public override void Reset()
		{
			gameObject = null;
			script = null;
			component = null;
			enable = true;
			allCollider = false;
			inclChildren = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				Debug.LogWarning("missing gameObject: " + ownerDefaultTarget.name);
				return;
			}
			if (!allCollider.Value & (component == null) & (colliderSelect == Selection.None))
			{
				Debug.LogWarning(ownerDefaultTarget.name + " !!! Check your setup ");
				return;
			}
			if (allCollider.Value)
			{
				colliderSelect = Selection.None;
				DisableAll(base.Fsm.GetOwnerDefaultTarget(gameObject));
			}
			switch (colliderSelect)
			{
			case Selection.Box:
				script = "BoxCollider2D";
				DisableBoxCollider(base.Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			case Selection.Edge:
				script = "EdgeCollider2D";
				DisableEdgeCollider(base.Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			case Selection.Circle:
				script = "SphereCollider2D";
				DisableCircleCollider(base.Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			case Selection.Rigidbody:
				script = "Rigidbody2D";
				DisableRigidbody(base.Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			case Selection.Polygon:
				script = "PolygonCollider2D";
				DisablePolygonCollider(base.Fsm.GetOwnerDefaultTarget(gameObject));
				break;
			}
			if ((colliderSelect == Selection.None || component != null) && !allCollider.Value)
			{
				DoEnableScript(base.Fsm.GetOwnerDefaultTarget(gameObject));
			}
			Finish();
		}

		private void DoEnableScript(GameObject go)
		{
			colliderSelect = Selection.None;
			if (go == null)
			{
				return;
			}
			componentTarget = component;
			componentTarget.enabled = enable.Value;
			if (!inclChildren.Value)
			{
				return;
			}
			for (int i = 0; i < go.transform.childCount; i++)
			{
				GameObject gameObject = go.transform.GetChild(i).gameObject;
				if (gameObject != null)
				{
					if (colliderSelect == Selection.None)
					{
						Debug.LogWarning("Please select type for child filter !!!");
						break;
					}
					(gameObject.gameObject.GetComponent(script.Value) as Collider2D).enabled = enable.Value;
				}
			}
		}

		private void DisableAll(GameObject go)
		{
			Collider2D[] components = go.gameObject.GetComponents<Collider2D>();
			Collider2D[] array = components;
			foreach (Collider2D collider2D in array)
			{
				collider2D.enabled = enable.Value;
			}
			if (inclChildren.Value)
			{
				Collider2D[] componentsInChildren = go.gameObject.GetComponentsInChildren<Collider2D>();
				Collider2D[] array2 = componentsInChildren;
				foreach (Collider2D collider2D2 in array2)
				{
					collider2D2.enabled = enable.Value;
				}
			}
		}

		private void DisableBoxCollider(GameObject go)
		{
			Collider2D[] components = go.gameObject.GetComponents<BoxCollider2D>();
			Collider2D[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				BoxCollider2D boxCollider2D = (BoxCollider2D)array[i];
				boxCollider2D.enabled = enable.Value;
			}
			if (inclChildren.Value)
			{
				Collider2D[] componentsInChildren = go.gameObject.GetComponentsInChildren<BoxCollider2D>();
				Collider2D[] array2 = componentsInChildren;
				for (int j = 0; j < array2.Length; j++)
				{
					BoxCollider2D boxCollider2D2 = (BoxCollider2D)array2[j];
					boxCollider2D2.enabled = enable.Value;
				}
			}
		}

		private void DisableEdgeCollider(GameObject go)
		{
			Collider2D[] components = go.gameObject.GetComponents<EdgeCollider2D>();
			Collider2D[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				EdgeCollider2D edgeCollider2D = (EdgeCollider2D)array[i];
				edgeCollider2D.enabled = enable.Value;
			}
			if (inclChildren.Value)
			{
				Collider2D[] componentsInChildren = go.gameObject.GetComponentsInChildren<EdgeCollider2D>();
				Collider2D[] array2 = componentsInChildren;
				for (int j = 0; j < array2.Length; j++)
				{
					EdgeCollider2D edgeCollider2D2 = (EdgeCollider2D)array2[j];
					edgeCollider2D2.enabled = enable.Value;
				}
			}
		}

		private void DisableRigidbody(GameObject go)
		{
			Rigidbody2D[] components = go.gameObject.GetComponents<Rigidbody2D>();
			Rigidbody2D[] array = components;
			foreach (Rigidbody2D rigidbody2D in array)
			{
				rigidbody2D.isKinematic = enable.Value;
			}
			if (inclChildren.Value)
			{
				Rigidbody2D[] componentsInChildren = go.gameObject.GetComponentsInChildren<Rigidbody2D>();
				Rigidbody2D[] array2 = componentsInChildren;
				foreach (Rigidbody2D rigidbody2D2 in array2)
				{
					rigidbody2D2.isKinematic = enable.Value;
				}
			}
		}

		private void DisablePolygonCollider(GameObject go)
		{
			Collider2D[] components = go.gameObject.GetComponents<PolygonCollider2D>();
			Collider2D[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				PolygonCollider2D polygonCollider2D = (PolygonCollider2D)array[i];
				polygonCollider2D.enabled = enable.Value;
			}
			if (inclChildren.Value)
			{
				Collider2D[] componentsInChildren = go.gameObject.GetComponentsInChildren<PolygonCollider2D>();
				Collider2D[] array2 = componentsInChildren;
				for (int j = 0; j < array2.Length; j++)
				{
					PolygonCollider2D polygonCollider2D2 = (PolygonCollider2D)array2[j];
					polygonCollider2D2.enabled = enable.Value;
				}
			}
		}

		private void DisableCircleCollider(GameObject go)
		{
			Collider2D[] components = go.gameObject.GetComponents<CircleCollider2D>();
			Collider2D[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				CircleCollider2D circleCollider2D = (CircleCollider2D)array[i];
				circleCollider2D.enabled = enable.Value;
			}
			if (inclChildren.Value)
			{
				Collider2D[] componentsInChildren = go.gameObject.GetComponentsInChildren<CircleCollider2D>();
				Collider2D[] array2 = componentsInChildren;
				for (int j = 0; j < array2.Length; j++)
				{
					CircleCollider2D circleCollider2D2 = (CircleCollider2D)array2[j];
					circleCollider2D2.enabled = enable.Value;
				}
			}
		}
	}
}
