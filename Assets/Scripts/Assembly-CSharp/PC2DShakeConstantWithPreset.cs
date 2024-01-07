using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Enables a constant shake on the camera using a preset configured in the editor")]
public class PC2DShakeConstantWithPreset : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The camera with the ProCamera2D component, most probably the MainCamera")]
	public FsmGameObject MainCamera;

	[HutongGames.PlayMaker.Tooltip("The name of the constant shake preset configured in the editor")]
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
			component.ConstantShake(PresetName.Value);
		}
		Finish();
	}
}
