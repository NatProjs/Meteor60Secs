using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker.Ecosystem.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMakerUGuiDropEventsProxy : MonoBehaviour, IDropHandler, IEventSystemHandler
{
	public bool debug;

	public PlayMakerEventTarget eventTarget = new PlayMakerEventTarget(true);

	[EventTargetVariable("eventTarget")]
	[ShowOptions]
	public PlayMakerEvent onDropEvent = new PlayMakerEvent("UGUI / ON DROP");

	public void OnDrop(PointerEventData data)
	{
		if (debug)
		{
			Debug.Log("OnDrop " + data.pointerId + " on " + base.gameObject.name);
		}
		GetLastPointerDataInfo.lastPointeEventData = data;
		onDropEvent.SendEvent(null, eventTarget);
	}
}
