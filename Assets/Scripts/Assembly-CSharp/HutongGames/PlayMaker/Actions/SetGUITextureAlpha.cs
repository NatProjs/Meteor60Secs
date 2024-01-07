using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Alpha of the Image attached to a Game Object. Useful for fading GUI elements in/out.")]
	[Obsolete("Image is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITextureAlpha : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmFloat alpha;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			alpha = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoImageAlpha();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoImageAlpha();
		}

		private void DoImageAlpha()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				Color color = base.Image.color;
				base.Image.color = new Color(color.r, color.g, color.b, alpha.Value);
			}
		}
	}
}
