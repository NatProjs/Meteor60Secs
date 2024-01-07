using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Translates a Game Object. Use a Vector3 and/or Vector2 variable and/or XYZ components. To leave any axis unchanged, set variable to 'None'.")]
	public class TranslateAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The game object to translate.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("A translation vector3. NOTE: You can override individual axis below.")]
		public FsmVector3 vector;

		[UIHint(UIHint.Variable)]
		[Tooltip("A translation vector2. NOTE: You can override individual axis below.")]
		public FsmVector2 vector2;

		[Tooltip("Translation along x axis.")]
		public FsmFloat x;

		[Tooltip("Translation along y axis.")]
		public FsmFloat y;

		[Tooltip("Translation along z axis.")]
		public FsmFloat z;

		[Tooltip("Translate in local or world space.")]
		public Space space;

		[Tooltip("Translate over one second")]
		public bool perSecond;

		private Transform _transform;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			vector = null;
			vector2 = new FsmVector2
			{
				UseVariable = true
			};
			x = new FsmFloat
			{
				UseVariable = true
			};
			y = new FsmFloat
			{
				UseVariable = true
			};
			z = new FsmFloat
			{
				UseVariable = true
			};
			space = Space.Self;
			perSecond = true;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_transform = ownerDefaultTarget.GetComponent<Transform>();
			}
			DoTranslate();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoTranslate();
		}

		private void DoTranslate()
		{
			if (!(_transform == null))
			{
				Vector3 vector = ((!this.vector.IsNone) ? this.vector.Value : new Vector3(x.Value, y.Value, z.Value));
				if (!vector2.IsNone)
				{
					vector.x = vector2.Value.x;
					vector.y = vector2.Value.y;
				}
				if (!x.IsNone)
				{
					vector.x = x.Value;
				}
				if (!y.IsNone)
				{
					vector.y = y.Value;
				}
				if (!z.IsNone)
				{
					vector.z = z.Value;
				}
				if (!perSecond)
				{
					_transform.Translate(vector, space);
				}
				else
				{
					_transform.Translate(vector * Time.deltaTime, space);
				}
			}
		}
	}
}
