using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMakerUGuiDragEventsExecutionProxy : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IEventSystemHandler
{
	public bool AutoDetect;

	public Component[] _beginDragTargets;

	public Component[] _dragTargets;

	public Component[] _endDragTargets;

	private void Start()
	{
		if (AutoDetect)
		{
			_beginDragTargets = base.transform.parent.GetComponentsInParent(typeof(IBeginDragHandler));
			_dragTargets = base.transform.parent.GetComponentsInParent(typeof(IDragHandler));
			_endDragTargets = base.transform.parent.GetComponentsInParent(typeof(IEndDragHandler));
		}
	}

	public void OnBeginDrag(PointerEventData data)
	{
		Component[] beginDragTargets = _beginDragTargets;
		for (int i = 0; i < beginDragTargets.Length; i++)
		{
			IBeginDragHandler handler = (IBeginDragHandler)beginDragTargets[i];
			ExecuteEvents.beginDragHandler(handler, data);
		}
	}

	public void OnDrag(PointerEventData data)
	{
		Component[] dragTargets = _dragTargets;
		for (int i = 0; i < dragTargets.Length; i++)
		{
			IDragHandler handler = (IDragHandler)dragTargets[i];
			ExecuteEvents.dragHandler(handler, data);
		}
	}

	public void OnEndDrag(PointerEventData data)
	{
		Component[] endDragTargets = _endDragTargets;
		for (int i = 0; i < endDragTargets.Length; i++)
		{
			IEndDragHandler handler = (IEndDragHandler)endDragTargets[i];
			ExecuteEvents.endDragHandler(handler, data);
		}
	}
}
