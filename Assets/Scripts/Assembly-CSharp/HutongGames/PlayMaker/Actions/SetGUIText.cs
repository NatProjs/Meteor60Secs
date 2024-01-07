using System;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUIElement)]
	[Tooltip("Sets the Text used by the Text Component attached to a Game Object.")]
	[Obsolete("Text is part of the legacy UI system and will be removed in a future release")]
	public class SetGUIText : ComponentAction<Text>
	{
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.TextArea)]
		public FsmString text;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			text = string.Empty;
		}

		public override void OnEnter()
		{
			DoSetText();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetText();
		}

		private void DoSetText()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.Text.text = text.Value;
			}
		}
	}
}
