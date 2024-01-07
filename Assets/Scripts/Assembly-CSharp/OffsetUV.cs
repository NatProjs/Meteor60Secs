using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Meshes")]
[HutongGames.PlayMaker.Tooltip("Offset Mesh UV")]
public class OffsetUV : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(MeshFilter))]
	public FsmOwnerDefault gameObject;

	public FsmBool affectSharedMesh;

	public FsmVector2 offset;

	public FsmFloat offsetX;

	public FsmFloat offsetY;

	public bool everyFrame;

	private Mesh _mesh;

	private Vector2[] _uvs;

	private float _xOffset;

	private float _yOffset;

	public override void Reset()
	{
		gameObject = null;
		offset = null;
		offsetX = null;
		offsetY = null;
		everyFrame = false;
	}

	public override void OnEnter()
	{
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
		if (ownerDefaultTarget == null)
		{
			Finish();
			return;
		}
		_mesh = ownerDefaultTarget.GetComponent<MeshFilter>().mesh;
		if (_mesh == null)
		{
			LogError("Missing Mesh!");
			Finish();
			return;
		}
		_uvs = new Vector2[_mesh.vertices.Length];
		_uvs = _mesh.uv;
		DoOffsetUV();
		if (!everyFrame)
		{
			Finish();
		}
	}

	public override void OnUpdate()
	{
		DoOffsetUV();
	}

	private void DoOffsetUV()
	{
		if (_mesh == null)
		{
			return;
		}
		Vector2[] array = new Vector2[_uvs.Length];
		float num = offsetX.Value;
		float num2 = offsetY.Value;
		if (!offset.IsNone)
		{
			num += offset.Value.x;
			num2 += offset.Value.y;
		}
		if (!num.Equals(_xOffset) || !num2.Equals(_yOffset))
		{
			_xOffset = num;
			_yOffset = num2;
			for (int i = 0; i < _uvs.Length; i++)
			{
				array[i] = new Vector2(_uvs[i].x + _xOffset, _uvs[i].y + _yOffset);
			}
			_mesh.uv = array;
		}
	}
}
