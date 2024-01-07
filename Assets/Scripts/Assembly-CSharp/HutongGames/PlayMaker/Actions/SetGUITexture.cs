using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Texture used by the Image attached to a Game Object.")]
	[Obsolete("Image is part of the legacy UI system and will be removed in a future release")]
	public class SetGUITexture : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject that owns the Image.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Texture to apply.")]
		public FsmTexture texture;

		public override void Reset()
		{
			gameObject = null;
			texture = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				// ???
			}
			Finish();
		}
	}
}
