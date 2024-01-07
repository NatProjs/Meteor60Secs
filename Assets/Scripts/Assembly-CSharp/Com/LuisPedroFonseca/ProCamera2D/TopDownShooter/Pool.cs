using System.Collections.Generic;
using UnityEngine;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class Pool : MonoBehaviour
	{
		public GameObject thing;

		private List<GameObject> things = new List<GameObject>();

		public GameObject nextThing
		{
			get
			{
				if (things.Count < 1)
				{
					GameObject gameObject = Object.Instantiate(thing);
					gameObject.transform.parent = base.transform;
					gameObject.SetActive(false);
					things.Add(gameObject);
					PoolMember poolMember = gameObject.AddComponent<PoolMember>();
					poolMember.pool = this;
				}
				GameObject gameObject2 = things[0];
				things.RemoveAt(0);
				gameObject2.SetActive(true);
				return gameObject2;
			}
			set
			{
				value.SetActive(false);
				things.Add(value);
			}
		}
	}
}
