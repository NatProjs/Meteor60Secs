using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Replaces a single Sprite on any GameObject. Object must have a Sprite Renderer.")]
	public class SwapSprite : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		public FsmOwnerDefault gameObject;

		public FsmBool resetOnExit;

		private SpriteRenderer spriteRenderer;

		private Sprite _orig;

		public override void Reset()
		{
			gameObject = null;
			resetOnExit = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				spriteRenderer = ownerDefaultTarget.GetComponent<SpriteRenderer>();
				if (spriteRenderer == null)
				{
					LogError("SwapSingleSprite: Missing SpriteRenderer!");
					return;
				}
				_orig = spriteRenderer.sprite;
				SwapSprites();
				Finish();
			}
		}

		public override void OnExit()
		{
			if (resetOnExit.Value)
			{
				spriteRenderer.sprite = _orig;
			}
		}

		private void SwapSprites()
		{
			// swap deez nuts
		}
	}
}
