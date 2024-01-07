using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Color of the GUITexture attached to a Game Object.")]
	[Obsolete("GUITexture is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITextureColor : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmColor color;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			color = Color.white;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetImageColor();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetImageColor();
		}

		private void DoSetImageColor()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.Image.color = color.Value;
			}
		}
	}
}
