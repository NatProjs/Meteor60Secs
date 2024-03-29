using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Returns the EventSystem's currently select GameObject.")]
	public class uGuiGetSelectedGameObject : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		[Tooltip("The currently selected GameObject")]
		public FsmGameObject StoreGameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event when the selected GameObject changes")]
		public FsmEvent ObjectChangedEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("If true, each frame will check the currently selected GameObject")]
		public bool everyFrame;

		private GameObject lastGameObject;

		public override void Reset()
		{
			StoreGameObject = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GetCurrentSelectedGameObject();
			lastGameObject = StoreGameObject.Value;
		}

		public override void OnUpdate()
		{
			GetCurrentSelectedGameObject();
			if (StoreGameObject.Value != lastGameObject && ObjectChangedEvent != null)
			{
				base.Fsm.Event(ObjectChangedEvent);
			}
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void GetCurrentSelectedGameObject()
		{
			StoreGameObject.Value = EventSystem.current.currentSelectedGameObject;
		}
	}
}
