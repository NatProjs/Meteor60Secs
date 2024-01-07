using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("get the value (zero based index) sprite and text  the Dropdown uGui Component")]
	public class uGuiDropDownGetSelectedData : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Dropdown))]
		[Tooltip("The GameObject with the DropDown ui component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The selected index of the dropdown (zero based index).")]
		[UIHint(UIHint.Variable)]
		public FsmInt value;

		[Tooltip("The selected text.")]
		[UIHint(UIHint.Variable)]
		public FsmString text;

		[ObjectType(typeof(Sprite))]
		[UIHint(UIHint.Variable)]
		public FsmObject image;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private Dropdown _dropDown;

		private List<Dropdown.OptionData> _options;

		public override void Reset()
		{
			gameObject = null;
			value = null;
			text = null;
			image = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_dropDown = ownerDefaultTarget.GetComponent<Dropdown>();
			}
			GetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			GetValue();
		}

		private void GetValue()
		{
			if (!(_dropDown == null))
			{
				if (!value.IsNone)
				{
					value.Value = _dropDown.value;
				}
				if (!text.IsNone)
				{
					text.Value = _dropDown.options[_dropDown.value].text;
				}
				if (!image.IsNone)
				{
					image.Value = _dropDown.options[_dropDown.value].image;
				}
			}
		}
	}
}
