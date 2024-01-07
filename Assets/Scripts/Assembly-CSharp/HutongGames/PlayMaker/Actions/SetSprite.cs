using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("sprite")]
	[Tooltip("Sets a Sprite on any GameObject. Object must have a Sprite Renderer.")]
	public class SetSprite : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(SpriteRenderer))]
		[Tooltip("The GameObject with a Sprite Renderer.")]
		public FsmOwnerDefault gameObject;

		[ObjectType(typeof(Sprite))]
		[Tooltip("The Sprite to set")]
		public FsmObject sprite;

		private SpriteRenderer spriteRenderer;

		public override void Reset()
		{
			gameObject = null;
			sprite = null;
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
				DoSetSprite();
				Finish();
			}
		}

		private void DoSetSprite()
		{
			spriteRenderer.sprite = sprite.Value as Sprite;
		}
	}
}
