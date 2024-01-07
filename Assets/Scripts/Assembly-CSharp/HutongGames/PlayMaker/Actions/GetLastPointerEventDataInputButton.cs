using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Gets pointer data Input Button on the last System event.")]
	public class GetLastPointerEventDataInputButton : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(PointerEventData.InputButton))]
		public FsmEnum inputButton;

		public FsmEvent leftClick;

		public FsmEvent middleClick;

		public FsmEvent rightClick;

		public override void Reset()
		{
			inputButton = PointerEventData.InputButton.Left;
			leftClick = null;
			middleClick = null;
			rightClick = null;
		}

		public override void OnEnter()
		{
			ExecuteAction();
			Finish();
		}

		private void ExecuteAction()
		{
			if (GetLastPointerDataInfo.lastPointeEventData != null)
			{
				if (!inputButton.IsNone)
				{
					inputButton.Value = GetLastPointerDataInfo.lastPointeEventData.button;
				}
				if (!string.IsNullOrEmpty(leftClick.Name) && GetLastPointerDataInfo.lastPointeEventData.button == PointerEventData.InputButton.Left)
				{
					base.Fsm.Event(leftClick);
				}
				else if (!string.IsNullOrEmpty(middleClick.Name) && GetLastPointerDataInfo.lastPointeEventData.button == PointerEventData.InputButton.Middle)
				{
					base.Fsm.Event(middleClick);
				}
				else if (!string.IsNullOrEmpty(rightClick.Name) && GetLastPointerDataInfo.lastPointeEventData.button == PointerEventData.InputButton.Right)
				{
					base.Fsm.Event(rightClick);
				}
			}
		}
	}
}
