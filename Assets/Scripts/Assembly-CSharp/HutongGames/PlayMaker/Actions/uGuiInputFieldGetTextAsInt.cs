using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets the text value as an int of a UGui InputField component.")]
	public class uGuiInputFieldGetTextAsInt : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(InputField))]
		[Tooltip("The GameObject with the InputField ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text value as an int of the UGui InputField component.")]
		public FsmInt value;

		[UIHint(UIHint.Variable)]
		[Tooltip("true if text resolves to an int")]
		public FsmBool isInt;

		[Tooltip("true if text resolves to an int")]
		public FsmEvent isIntEvent;

		[Tooltip("Event sent if text does not resolves to an int")]
		public FsmEvent isNotIntEvent;

		public bool everyFrame;

		private InputField _inputField;

		private int _value;

		private bool _success;

		public override void Reset()
		{
			value = null;
			isInt = null;
			isIntEvent = null;
			isNotIntEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_inputField = ownerDefaultTarget.GetComponent<InputField>();
			}
			DoGetTextValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetTextValue();
		}

		private void DoGetTextValue()
		{
			if (_inputField != null)
			{
				_success = int.TryParse(_inputField.text, out _value);
				value.Value = _value;
				isInt.Value = _success;
				base.Fsm.Event((!_success) ? isNotIntEvent : isIntEvent);
			}
		}
	}
}
