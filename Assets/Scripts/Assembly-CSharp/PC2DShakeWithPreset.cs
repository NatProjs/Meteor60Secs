using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Shakes the camera position along its horizontal and vertical axes using a preset configured in the editor")]
public class PC2DShakeWithPreset : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The camera with the ProCamera2D component, most probably the MainCamera")]
	public FsmGameObject MainCamera;

	[HutongGames.PlayMaker.Tooltip("The name of the shake preset configured in the editor")]
	public FsmString PresetName;

	public override void OnEnter()
	{
		ProCamera2DShake component = MainCamera.Value.GetComponent<ProCamera2DShake>();
		if (component == null)
		{
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");
		}
		if (ProCamera2D.Instance != null && component != null)
		{
			component.Shake(PresetName.Value);
		}
		Finish();
	}
}
