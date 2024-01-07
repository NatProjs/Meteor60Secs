using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Starts or stops a cinematic")]
public class PC2DCinematicsToggle : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The gameObject that contains the ProCamera2DCinematics component")]
	public FsmGameObject Cinematics;

	public override void OnEnter()
	{
		ProCamera2DCinematics component = Cinematics.Value.GetComponent<ProCamera2DCinematics>();
		if (component == null)
		{
			Debug.LogError("No Cinematics component found in the gameObject: " + Cinematics.Value.name);
		}
		if (ProCamera2D.Instance != null && component != null)
		{
			component.Toggle();
		}
		Finish();
	}
}
