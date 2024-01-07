using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("The eventType will be executed on all components on the GameObject that can handle it.")]
	public class EventSystemExecuteEvent : FsmStateAction
	{
		public enum EventHandlers
		{
			Submit = 0,
			beginDrag = 1,
			cancel = 2,
			deselectHandler = 3,
			dragHandler = 4,
			dropHandler = 5,
			endDragHandler = 6,
			initializePotentialDrag = 7,
			pointerClickHandler = 8,
			pointerDownHandler = 9,
			pointerEnterHandler = 10,
			pointerExitHandler = 11,
			pointerUpHandler = 12,
			scrollHandler = 13,
			submitHandler = 14,
			updateSelectedHandler = 15
		}

		[RequiredField]
		[Tooltip("The GameObject with  an IEventSystemHandler component ( a UI button for example).")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Type of handler to execute")]
		[ObjectType(typeof(EventHandlers))]
		public FsmEnum eventHandler;

		[Tooltip("Event Sent if execution was possible on GameObject")]
		public FsmEvent success;

		[Tooltip("Event Sent if execution was NOT possible on GameObject because it can not handle the eventHandler selected")]
		public FsmEvent canNotHandleEvent;

		private GameObject go;

		public override void Reset()
		{
			gameObject = null;
			eventHandler = EventHandlers.Submit;
			success = null;
			canNotHandleEvent = null;
		}

		public override void OnEnter()
		{
			if (ExecuteEvent())
			{
				base.Fsm.Event(success);
			}
			else
			{
				base.Fsm.Event(canNotHandleEvent);
			}
			Finish();
		}

		private bool ExecuteEvent()
		{
			go = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				LogError("Missing GameObject ");
				return false;
			}
			EventHandlers eventHandlers = (EventHandlers)(object)eventHandler.Value;
			switch (eventHandlers)
			{
			case EventHandlers.Submit:
				if (!ExecuteEvents.CanHandleEvent<ISubmitHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
				break;
			case EventHandlers.beginDrag:
				if (!ExecuteEvents.CanHandleEvent<IBeginDragHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.beginDragHandler);
				break;
			case EventHandlers.cancel:
				if (!ExecuteEvents.CanHandleEvent<ICancelHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.cancelHandler);
				break;
			case EventHandlers.deselectHandler:
				if (!ExecuteEvents.CanHandleEvent<IDeselectHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
				break;
			}
			if (eventHandlers == EventHandlers.dragHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IDragHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.dragHandler);
			}
			if (eventHandlers == EventHandlers.dropHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IDropHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.dropHandler);
			}
			if (eventHandlers == EventHandlers.endDragHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IEndDragHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.endDragHandler);
			}
			if (eventHandlers == EventHandlers.initializePotentialDrag)
			{
				if (!ExecuteEvents.CanHandleEvent<IInitializePotentialDragHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.initializePotentialDrag);
			}
			if (eventHandlers == EventHandlers.pointerClickHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerClickHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
			}
			if (eventHandlers == EventHandlers.pointerDownHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerDownHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
			}
			if (eventHandlers == EventHandlers.pointerUpHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerUpHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
			}
			if (eventHandlers == EventHandlers.pointerEnterHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerEnterHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
			}
			if (eventHandlers == EventHandlers.pointerExitHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IPointerExitHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
			}
			if (eventHandlers == EventHandlers.scrollHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IScrollHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.scrollHandler);
			}
			if (eventHandlers == EventHandlers.updateSelectedHandler)
			{
				if (!ExecuteEvents.CanHandleEvent<IUpdateSelectedHandler>(go))
				{
					return false;
				}
				ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.updateSelectedHandler);
			}
			return true;
		}
	}
}
