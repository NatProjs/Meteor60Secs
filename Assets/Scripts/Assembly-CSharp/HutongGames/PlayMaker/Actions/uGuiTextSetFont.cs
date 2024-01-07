using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sets the text value of a UGui Text component.")]
	public class uGuiTextSetFont : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		[Tooltip("The GameObject with the text ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Font))]
		[Tooltip("The text of the UGui Text component.")]
		public FsmObject font;

		[Tooltip("Reset when exiting this state.")]
		public FsmBool resetOnExit;

		private Text _text;

		private Font _originalFont;

		public override void Reset()
		{
			gameObject = null;
			resetOnExit = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_text = ownerDefaultTarget.GetComponent<Text>();
				if (resetOnExit.Value)
				{
					_originalFont = _text.font;
				}
				_text.font = font.Value as Font;
			}
			Finish();
		}

		public override void OnExit()
		{
			if (!(_text == null) && resetOnExit.Value)
			{
				_text.font = _originalFont;
			}
		}
	}
}
