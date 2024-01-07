using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class BoxCollider2dMatchRectTransform : MonoBehaviour
{
	[SerializeField]
	private bool _includeChildren;

	[SerializeField]
	private Vector2 _margin;

	private BoxCollider2D _bc2d;

	private RectTransform _rt;

	private Bounds bounds;

	private Vector2 _size;

	private Vector2 _lastMargin;

	private bool _isDirty;

	private Vector3[] s_Corners = new Vector3[4];

	public bool IncludeChildren
	{
		get
		{
			return _includeChildren;
		}
		set
		{
			if (value != _includeChildren)
			{
				_includeChildren = value;
				MatchSize();
			}
		}
	}

	public Vector2 Margin
	{
		get
		{
			return _margin;
		}
		set
		{
			if (value != _margin)
			{
				_margin = value;
				MatchSize();
			}
		}
	}

	private void Start()
	{
		MatchSize();
	}

	private void OnRectTransformDimensionsChange()
	{
		MatchSize();
	}

	private void Update()
	{
		if (IncludeChildren)
		{
			MatchSize();
		}
	}

	public void MatchSize()
	{
		if (_bc2d == null)
		{
			_bc2d = GetComponent<BoxCollider2D>();
		}
		if (_rt == null)
		{
			_rt = GetComponent<RectTransform>();
		}
		if (!(_rt == null) && !(_bc2d == null))
		{
			if (_includeChildren)
			{
				bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(base.transform);
			}
			else
			{
				bounds = CalculateRelativeRectTransformBounds(base.transform, base.transform);
			}
			_size = bounds.size;
			_bc2d.size = _size + _margin;
			_bc2d.offset = bounds.center;
		}
	}

	private Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
	{
		RectTransform[] array = new RectTransform[1] { child.GetComponent<RectTransform>() };
		if (array.Length > 0)
		{
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				array[i].GetWorldCorners(s_Corners);
				for (int j = 0; j < 4; j++)
				{
					Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(s_Corners[j]);
					vector = Vector3.Min(lhs, vector);
					vector2 = Vector3.Max(lhs, vector2);
				}
			}
			Bounds result = new Bounds(vector, Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}
		return new Bounds(Vector3.zero, Vector3.zero);
	}
}
