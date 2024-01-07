using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Enter a room when using the Rooms extension")]
public class PC2DRoomsEnter : FsmStateActionProCamera2DBase
{
	[HutongGames.PlayMaker.Tooltip("Set the current room by index")]
	[RequiredField]
	public FsmInt RoomIndex;

	[HutongGames.PlayMaker.Tooltip("Set the current room by ID. Note that using ID will override index")]
	public FsmString RoomId;

	[HutongGames.PlayMaker.Tooltip("If false, the camera instantly transitions to the room. If true, the camera uses the transition configured in the Rooms extension editor.")]
	public bool UseTransition = true;

	private ProCamera2DRooms _rooms;

	public override void Reset()
	{
		RoomIndex = 0;
		RoomId = null;
	}

	public override void OnEnter()
	{
		ProCamera2D instance = ProCamera2D.Instance;
		if (instance == null)
		{
			Debug.LogError("No ProCamera2D found! Please add the core component to your Main Camera.");
			Finish();
			return;
		}
		_rooms = instance.GetComponent<ProCamera2DRooms>();
		if (_rooms == null)
		{
			Debug.LogError("No Rooms extension found in ProCamera2D!");
			Finish();
		}
		else
		{
			SetRoom();
			Finish();
		}
	}

	private void SetRoom()
	{
		if (!RoomId.IsNone && !string.IsNullOrEmpty(RoomId.Value))
		{
			_rooms.EnterRoom(RoomId.Value, UseTransition);
		}
		else
		{
			_rooms.EnterRoom(RoomIndex.Value, UseTransition);
		}
	}
}
