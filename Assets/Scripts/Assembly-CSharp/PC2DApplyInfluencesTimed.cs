using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Apply the given influences to the camera during the corresponding durations")]
public class PC2DApplyInfluencesTimed : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("An array of the vectors representing the influences to be applied")]
	public FsmVector2[] Influences;

	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("An array of the vectors representing the influences to be applied")]
	public FsmFloat[] Durations;

	public override void Reset()
	{
		Influences = new FsmVector2[0];
		Durations = new FsmFloat[0];
	}

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null)
		{
			int length = Influences.GetLength(0);
			Vector2[] array = new Vector2[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (Influences.GetValue(i) as FsmVector2).Value;
			}
			float[] array2 = new float[length];
			for (int j = 0; j < length; j++)
			{
				array2[j] = (Durations.GetValue(j) as FsmFloat).Value;
			}
			ProCamera2D.Instance.ApplyInfluencesTimed(array, array2);
		}
		Finish();
	}
}
