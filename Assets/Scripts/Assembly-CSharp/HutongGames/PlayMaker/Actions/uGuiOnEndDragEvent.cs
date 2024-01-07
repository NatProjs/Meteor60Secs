using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Sends event Called by the EventSystem once dragging ends.\n Use GetLastPointerDataInfo action to get info from the event")]
	public class uGuiOnEndDragEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the UGui button component.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Event sent when OnEndDrag is called")]
		public FsmEvent onEndDragEvent;

		private GameObject _go;

		private EventTrigger _trigger;

		private EventTrigger.Entry entry;

		public override void Reset()
		{
			gameObject = null;
			onEndDragEvent = null;
		}

		public override void OnEnter()
		{
			_go = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(_go == null))
			{
				_trigger = _go.GetComponent<EventTrigger>();
				if (_trigger == null)
				{
					_trigger = _go.AddComponent<EventTrigger>();
				}
				if (entry == null)
				{
					entry = new EventTrigger.Entry();
				}
				entry.eventID = EventTriggerType.EndDrag;
				entry.callback.AddListener(delegate(BaseEventData data)
				{
					OnEndDragDelegate((PointerEventData)data);
				});
				_trigger.triggers.Add(entry);
			}
		}

		public override void OnExit()
		{
			entry.callback.RemoveAllListeners();
			_trigger.triggers.Remove(entry);
		}

		private void OnEndDragDelegate(PointerEventData data)
		{
			GetLastPointerDataInfo.lastPointeEventData = data;
			base.Fsm.Event(onEndDragEvent);
		}
	}
}
