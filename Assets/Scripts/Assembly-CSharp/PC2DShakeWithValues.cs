using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using UnityEngine;

[HutongGames.PlayMaker.Tooltip("Shakes the camera position along its horizontal and vertical axes with the given values")]
public class PC2DShakeWithValues : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[HutongGames.PlayMaker.Tooltip("The camera with the ProCamera2D component, most probably the MainCamera")]
	public FsmGameObject MainCamera;

	[HutongGames.PlayMaker.Tooltip("The shake strength on each axis")]
	public FsmVector2 Strength;

	[HutongGames.PlayMaker.Tooltip("The duration of the shake")]
	public FsmFloat Duration = 1f;

	[HutongGames.PlayMaker.Tooltip("Indicates how much will the shake vibrate. Don't use values lower than 1 or higher than 100 for better results")]
	public FsmInt Vibrato = 10;

	[HutongGames.PlayMaker.Tooltip("Indicates how much random the shake will be")]
	[HasFloatSlider(0f, 1f)]
	public FsmFloat Randomness = 0.1f;

	[HutongGames.PlayMaker.Tooltip("The initial angle of the shake. Use -1 if you want it to be random.")]
	[HasFloatSlider(-1f, 360f)]
	public FsmInt InitialAngle = 10;

	[HutongGames.PlayMaker.Tooltip("The maximum rotation the camera can reach during shake")]
	public FsmVector3 Rotation;

	[HutongGames.PlayMaker.Tooltip("How smooth the shake should be, 0 being instant")]
	[HasFloatSlider(0f, 0.5f)]
	public FsmFloat Smoothness;

	public override void OnEnter()
	{
		ProCamera2DShake component = MainCamera.Value.GetComponent<ProCamera2DShake>();
		if (component == null)
		{
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");
		}
		if (ProCamera2D.Instance != null && component != null)
		{
			component.Shake(Duration.Value, Strength.Value, Vibrato.Value, Randomness.Value, InitialAngle.Value, Rotation.Value, Smoothness.Value);
		}
		Finish();
	}
}
