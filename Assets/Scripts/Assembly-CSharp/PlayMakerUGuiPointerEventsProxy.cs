using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMakerUGuiPointerEventsProxy : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
{
	public bool debug;

	public PlayMakerEventTarget eventTarget;

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onClickEvent = new PlayMakerEvent("UGUI / ON POINTER CLICK");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDownEvent = new PlayMakerEvent("UGUI / ON POINTER DOWN");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onEnterEvent = new PlayMakerEvent("UGUI / ON POINTER ENTER");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onExitEvent = new PlayMakerEvent("UGUI / ON POINTER EXIT");

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onUpEvent = new PlayMakerEvent("UGUI / ON POINTER UP");

	public void OnPointerClick(PointerEventData data)
	{
		if (debug)
		{
			Debug.Log("OnPointerClick " + data.pointerId + " on " + base.gameObject.name);
		}
		GetLastPointerDataInfo.lastPointeEventData = data;
		onClickEvent.SendEvent(null, eventTarget);
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (debug)
		{
			Debug.Log("OnPointerDown " + data.pointerId + " on " + base.gameObject.name);
		}
		GetLastPointerDataInfo.lastPointeEventData = data;
		onDownEvent.SendEvent(null, eventTarget);
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (debug)
		{
			Debug.Log("OnPointerEnter " + data.pointerId + " on " + base.gameObject.name);
		}
		GetLastPointerDataInfo.lastPointeEventData = data;
		onEnterEvent.SendEvent(null, eventTarget);
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (debug)
		{
			Debug.Log("OnPointerExit " + data.pointerId + " on " + base.gameObject.name);
		}
		GetLastPointerDataInfo.lastPointeEventData = data;
		onExitEvent.SendEvent(null, eventTarget);
	}

	public void OnPointerUp(PointerEventData data)
	{
		if (debug)
		{
			Debug.Log("OnPointerUp " + data.pointerId + " on " + base.gameObject.name);
		}
		GetLastPointerDataInfo.lastPointeEventData = data;
		onUpEvent.SendEvent(null, eventTarget);
	}
}
