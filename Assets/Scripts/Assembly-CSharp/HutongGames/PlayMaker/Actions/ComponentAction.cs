using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	public abstract class ComponentAction<T> : FsmStateAction where T : Component
	{
		protected GameObject cachedGameObject;

		protected T cachedComponent;

		protected Rigidbody rigidbody
		{
			get
			{
				return cachedComponent as Rigidbody;
			}
		}

		protected Rigidbody2D rigidbody2d
		{
			get
			{
				return cachedComponent as Rigidbody2D;
			}
		}

		protected Renderer renderer
		{
			get
			{
				return cachedComponent as Renderer;
			}
		}

		protected Animation animation
		{
			get
			{
				return cachedComponent as Animation;
			}
		}

		protected AudioSource audio
		{
			get
			{
				return cachedComponent as AudioSource;
			}
		}

		protected Camera camera
		{
			get
			{
				return cachedComponent as Camera;
			}
		}

		protected Text Text
		{
			get
			{
				return cachedComponent as Text;
			}
		}

		protected Image Image
		{
			get
			{
				return cachedComponent as Image;
			}
		}

		protected Light light
		{
			get
			{
				return cachedComponent as Light;
			}
		}

		protected bool UpdateCache(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			if ((Object)cachedComponent == (Object)null || cachedGameObject != go)
			{
				cachedComponent = go.GetComponent<T>();
				cachedGameObject = go;
				if ((Object)cachedComponent == (Object)null)
				{
					LogWarning("Missing component: " + typeof(T).FullName + " on: " + go.name);
				}
			}
			return (Object)cachedComponent != (Object)null;
		}
	}
}
