using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Stops all current shakes on the camera")]
public class PC2DShakeStop : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The camera with the ProCamera2D component, most probably the MainCamera")]
	public FsmGameObject MainCamera;

	public override void OnEnter()
	{
		ProCamera2DShake component = MainCamera.Value.GetComponent<ProCamera2DShake>();
		if (component == null)
		{
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");
		}
		if (ProCamera2D.Instance != null && component != null)
		{
			component.StopShaking();
		}
		Finish();
	}
}
