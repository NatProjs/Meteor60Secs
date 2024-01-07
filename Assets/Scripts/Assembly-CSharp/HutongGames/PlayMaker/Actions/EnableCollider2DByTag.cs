using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Enables/Disables a Collider2D(or a Rigidbody) by Tag (and Layer).")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10242")]
	public class EnableCollider2DByTag : FsmStateAction
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
		[Title("Collider2D Type Select")]
		public Selection ColliderSelect;

		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable/disable all Collider2D.")]
		[Title("or All Collider2D")]
		public FsmBool allCollider2D;

		[ActionSection("Options")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;

		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;

		[ActionSection("Tag and Layer Options")]
		[Tooltip("Activate this option?")]
		[UIHint(UIHint.Tag)]
		public FsmString tag;

		[Title("Incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;

		[Tooltip("Filter layer on child?")]
		public FsmBool inclLayerFilterOnChild;

		[UIHint(UIHint.Layer)]
		public int layer;

		private Collider2D componentTarget;

		public override void Reset()
		{
			enable = true;
			allCollider2D = false;
			inclChildren = false;
			layerFilterOn = false;
			layer = 0;
			inclLayerFilterOnChild = false;
		}

		public override void OnEnter()
		{
			if (!allCollider2D.Value & (ColliderSelect == Selection.None))
			{
				Debug.LogWarning(" !!! Check your setup - Collider2D Type Select = None");
				return;
			}
			if (allCollider2D.Value & !layerFilterOn.Value)
			{
				ColliderSelect = Selection.None;
				DisableAllTag();
			}
			if (allCollider2D.Value & layerFilterOn.Value)
			{
				ColliderSelect = Selection.None;
				DisableAllTagFilter();
			}
			switch (ColliderSelect)
			{
			case Selection.Box:
				DisableBoxCollider2D();
				break;
			case Selection.Circle:
				DisableCircleCollider2D();
				break;
			case Selection.Edge:
				DisableEdgeCollider2D();
				break;
			case Selection.Polygon:
				DisablePolygonCollider2D();
				break;
			case Selection.Rigidbody:
				DisableRigidbody2D();
				break;
			}
			Finish();
		}

		private void DisableAllTagFilter()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].layer != layer)
				{
					continue;
				}
				Collider2D[] components = array[i].gameObject.GetComponents<Collider2D>();
				Collider2D[] array2 = components;
				foreach (Collider2D collider2D in array2)
				{
					collider2D.enabled = enable.Value;
				}
				if (inclChildren.Value && !inclLayerFilterOnChild.Value)
				{
					Collider2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<Collider2D>();
					Collider2D[] array3 = componentsInChildren;
					foreach (Collider2D collider2D2 in array3)
					{
						collider2D2.enabled = enable.Value;
					}
				}
				if (!inclChildren.Value || !inclLayerFilterOnChild.Value)
				{
					continue;
				}
				Collider2D[] componentsInChildren2 = array[i].gameObject.GetComponentsInChildren<Collider2D>();
				foreach (Collider2D collider2D3 in componentsInChildren2)
				{
					if (collider2D3.gameObject.layer == layer)
					{
						collider2D3.enabled = enable.Value;
					}
				}
			}
		}

		private void DisableAllTag()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
				return;
			}
			for (int i = 0; i < array.Length; i++)
			{
				Collider2D[] components = array[i].gameObject.GetComponents<Collider2D>();
				Collider2D[] array2 = components;
				foreach (Collider2D collider2D in array2)
				{
					collider2D.enabled = enable.Value;
				}
				if (inclChildren.Value)
				{
					Collider2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<Collider2D>();
					Collider2D[] array3 = componentsInChildren;
					foreach (Collider2D collider2D2 in array3)
					{
						collider2D2.enabled = enable.Value;
					}
				}
			}
		}

		private void DisableBoxCollider2D()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
			}
			else if (!inclLayerFilterOnChild.Value)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Collider2D[] components = array[i].gameObject.GetComponents<BoxCollider2D>();
					Collider2D[] array2 = components;
					for (int j = 0; j < array2.Length; j++)
					{
						BoxCollider2D boxCollider2D = (BoxCollider2D)array2[j];
						boxCollider2D.enabled = enable.Value;
					}
					if (inclChildren.Value)
					{
						Collider2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<BoxCollider2D>();
						Collider2D[] array3 = componentsInChildren;
						for (int k = 0; k < array3.Length; k++)
						{
							BoxCollider2D boxCollider2D2 = (BoxCollider2D)array3[k];
							boxCollider2D2.enabled = enable.Value;
						}
					}
				}
			}
			else
			{
				if (!inclLayerFilterOnChild.Value)
				{
					return;
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].layer != layer)
					{
						continue;
					}
					Collider2D[] components2 = array[l].gameObject.GetComponents<BoxCollider2D>();
					Collider2D[] array4 = components2;
					for (int m = 0; m < array4.Length; m++)
					{
						BoxCollider2D boxCollider2D3 = (BoxCollider2D)array4[m];
						boxCollider2D3.enabled = enable.Value;
					}
					if (inclChildren.Value && !inclLayerFilterOnChild.Value)
					{
						Collider2D[] componentsInChildren2 = array[l].gameObject.GetComponentsInChildren<BoxCollider2D>();
						Collider2D[] array5 = componentsInChildren2;
						for (int n = 0; n < array5.Length; n++)
						{
							BoxCollider2D boxCollider2D4 = (BoxCollider2D)array5[n];
							boxCollider2D4.enabled = enable.Value;
						}
					}
					if (!inclChildren.Value || !inclLayerFilterOnChild.Value)
					{
						continue;
					}
					BoxCollider2D[] componentsInChildren3 = array[l].gameObject.GetComponentsInChildren<BoxCollider2D>();
					foreach (BoxCollider2D boxCollider2D5 in componentsInChildren3)
					{
						if (boxCollider2D5.gameObject.layer == layer)
						{
							boxCollider2D5.enabled = enable.Value;
						}
					}
				}
			}
		}

		private void DisableCircleCollider2D()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
			}
			else if (!inclLayerFilterOnChild.Value)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Collider2D[] components = array[i].gameObject.GetComponents<CircleCollider2D>();
					Collider2D[] array2 = components;
					for (int j = 0; j < array2.Length; j++)
					{
						CircleCollider2D circleCollider2D = (CircleCollider2D)array2[j];
						circleCollider2D.enabled = enable.Value;
					}
					if (inclChildren.Value)
					{
						Collider2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<CircleCollider2D>();
						Collider2D[] array3 = componentsInChildren;
						for (int k = 0; k < array3.Length; k++)
						{
							CircleCollider2D circleCollider2D2 = (CircleCollider2D)array3[k];
							circleCollider2D2.enabled = enable.Value;
						}
					}
				}
			}
			else
			{
				if (!inclLayerFilterOnChild.Value)
				{
					return;
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].layer != layer)
					{
						continue;
					}
					Collider2D[] components2 = array[l].gameObject.GetComponents<CircleCollider2D>();
					Collider2D[] array4 = components2;
					for (int m = 0; m < array4.Length; m++)
					{
						CircleCollider2D circleCollider2D3 = (CircleCollider2D)array4[m];
						circleCollider2D3.enabled = enable.Value;
					}
					if (inclChildren.Value && !inclLayerFilterOnChild.Value)
					{
						Collider2D[] componentsInChildren2 = array[l].gameObject.GetComponentsInChildren<CircleCollider2D>();
						Collider2D[] array5 = componentsInChildren2;
						for (int n = 0; n < array5.Length; n++)
						{
							CircleCollider2D circleCollider2D4 = (CircleCollider2D)array5[n];
							circleCollider2D4.enabled = enable.Value;
						}
					}
					if (!inclChildren.Value || !inclLayerFilterOnChild.Value)
					{
						continue;
					}
					CircleCollider2D[] componentsInChildren3 = array[l].gameObject.GetComponentsInChildren<CircleCollider2D>();
					foreach (CircleCollider2D circleCollider2D5 in componentsInChildren3)
					{
						if (circleCollider2D5.gameObject.layer == layer)
						{
							circleCollider2D5.enabled = enable.Value;
						}
					}
				}
			}
		}

		private void DisableEdgeCollider2D()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
			}
			else if (!inclLayerFilterOnChild.Value)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Collider2D[] components = array[i].gameObject.GetComponents<EdgeCollider2D>();
					Collider2D[] array2 = components;
					for (int j = 0; j < array2.Length; j++)
					{
						EdgeCollider2D edgeCollider2D = (EdgeCollider2D)array2[j];
						edgeCollider2D.enabled = enable.Value;
					}
					if (inclChildren.Value)
					{
						Collider2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<EdgeCollider2D>();
						Collider2D[] array3 = componentsInChildren;
						for (int k = 0; k < array3.Length; k++)
						{
							EdgeCollider2D edgeCollider2D2 = (EdgeCollider2D)array3[k];
							edgeCollider2D2.enabled = enable.Value;
						}
					}
				}
			}
			else
			{
				if (!inclLayerFilterOnChild.Value)
				{
					return;
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].layer != layer)
					{
						continue;
					}
					Collider2D[] components2 = array[l].gameObject.GetComponents<EdgeCollider2D>();
					Collider2D[] array4 = components2;
					for (int m = 0; m < array4.Length; m++)
					{
						EdgeCollider2D edgeCollider2D3 = (EdgeCollider2D)array4[m];
						edgeCollider2D3.enabled = enable.Value;
					}
					if (inclChildren.Value && !inclLayerFilterOnChild.Value)
					{
						Collider2D[] componentsInChildren2 = array[l].gameObject.GetComponentsInChildren<EdgeCollider2D>();
						Collider2D[] array5 = componentsInChildren2;
						for (int n = 0; n < array5.Length; n++)
						{
							EdgeCollider2D edgeCollider2D4 = (EdgeCollider2D)array5[n];
							edgeCollider2D4.enabled = enable.Value;
						}
					}
					if (!inclChildren.Value || !inclLayerFilterOnChild.Value)
					{
						continue;
					}
					EdgeCollider2D[] componentsInChildren3 = array[l].gameObject.GetComponentsInChildren<EdgeCollider2D>();
					foreach (EdgeCollider2D edgeCollider2D5 in componentsInChildren3)
					{
						if (edgeCollider2D5.gameObject.layer == layer)
						{
							edgeCollider2D5.enabled = enable.Value;
						}
					}
				}
			}
		}

		private void DisableRigidbody2D()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
			}
			else if (!inclLayerFilterOnChild.Value)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Rigidbody2D[] components = array[i].gameObject.GetComponents<Rigidbody2D>();
					Rigidbody2D[] array2 = components;
					foreach (Rigidbody2D rigidbody2D in array2)
					{
						rigidbody2D.isKinematic = !enable.Value;
					}
					if (inclChildren.Value)
					{
						Rigidbody2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<Rigidbody2D>();
						Rigidbody2D[] array3 = componentsInChildren;
						foreach (Rigidbody2D rigidbody2D2 in array3)
						{
							rigidbody2D2.isKinematic = !enable.Value;
						}
					}
				}
			}
			else
			{
				if (!inclLayerFilterOnChild.Value)
				{
					return;
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].layer != layer)
					{
						continue;
					}
					Rigidbody2D[] components2 = array[l].gameObject.GetComponents<Rigidbody2D>();
					Rigidbody2D[] array4 = components2;
					foreach (Rigidbody2D rigidbody2D3 in array4)
					{
						rigidbody2D3.isKinematic = !enable.Value;
					}
					if (inclChildren.Value && !inclLayerFilterOnChild.Value)
					{
						Rigidbody2D[] componentsInChildren2 = array[l].gameObject.GetComponentsInChildren<Rigidbody2D>();
						Rigidbody2D[] array5 = componentsInChildren2;
						foreach (Rigidbody2D rigidbody2D4 in array5)
						{
							rigidbody2D4.isKinematic = !enable.Value;
						}
					}
					if (!inclChildren.Value || !inclLayerFilterOnChild.Value)
					{
						continue;
					}
					Rigidbody2D[] componentsInChildren3 = array[l].gameObject.GetComponentsInChildren<Rigidbody2D>();
					foreach (Rigidbody2D rigidbody2D5 in componentsInChildren3)
					{
						if (rigidbody2D5.gameObject.layer == layer)
						{
							rigidbody2D5.isKinematic = !enable.Value;
						}
					}
				}
			}
		}

		private void DisablePolygonCollider2D()
		{
			GameObject[] array = GameObject.FindGameObjectsWithTag(tag.Value);
			if (array.Length == 0)
			{
				Debug.LogWarning("No object with tag:  " + tag.Value);
			}
			else if (!inclLayerFilterOnChild.Value)
			{
				for (int i = 0; i < array.Length; i++)
				{
					Collider2D[] components = array[i].gameObject.GetComponents<PolygonCollider2D>();
					Collider2D[] array2 = components;
					for (int j = 0; j < array2.Length; j++)
					{
						PolygonCollider2D polygonCollider2D = (PolygonCollider2D)array2[j];
						polygonCollider2D.enabled = enable.Value;
					}
					if (inclChildren.Value)
					{
						Collider2D[] componentsInChildren = array[i].gameObject.GetComponentsInChildren<PolygonCollider2D>();
						Collider2D[] array3 = componentsInChildren;
						for (int k = 0; k < array3.Length; k++)
						{
							PolygonCollider2D polygonCollider2D2 = (PolygonCollider2D)array3[k];
							polygonCollider2D2.enabled = enable.Value;
						}
					}
				}
			}
			else
			{
				if (!inclLayerFilterOnChild.Value)
				{
					return;
				}
				for (int l = 0; l < array.Length; l++)
				{
					if (array[l].layer != layer)
					{
						continue;
					}
					Collider2D[] components2 = array[l].gameObject.GetComponents<PolygonCollider2D>();
					Collider2D[] array4 = components2;
					for (int m = 0; m < array4.Length; m++)
					{
						PolygonCollider2D polygonCollider2D3 = (PolygonCollider2D)array4[m];
						polygonCollider2D3.enabled = enable.Value;
					}
					if (inclChildren.Value && !inclLayerFilterOnChild.Value)
					{
						Collider2D[] componentsInChildren2 = array[l].gameObject.GetComponentsInChildren<PolygonCollider2D>();
						Collider2D[] array5 = componentsInChildren2;
						for (int n = 0; n < array5.Length; n++)
						{
							PolygonCollider2D polygonCollider2D4 = (PolygonCollider2D)array5[n];
							polygonCollider2D4.enabled = enable.Value;
						}
					}
					if (!inclChildren.Value || !inclLayerFilterOnChild.Value)
					{
						continue;
					}
					PolygonCollider2D[] componentsInChildren3 = array[l].gameObject.GetComponentsInChildren<PolygonCollider2D>();
					foreach (PolygonCollider2D polygonCollider2D5 in componentsInChildren3)
					{
						if (polygonCollider2D5.gameObject.layer == layer)
						{
							polygonCollider2D5.enabled = enable.Value;
						}
					}
				}
			}
		}
	}
}
