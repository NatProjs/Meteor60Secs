using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;

[Tooltip("Add a target for the camera to follow.")]
public class PC2DAddCameraTarget : FsmStateActionProCamera2DBase
{
	[RequiredField]
	[Tooltip("The camera target to add")]
	public FsmGameObject target;

	[HasFloatSlider(0f, 1f)]
	[Tooltip("The influence this target horizontal position should have when calculating the average position of all the targets")]
	public FsmFloat targetInfluenceH = 1f;

	[HasFloatSlider(0f, 1f)]
	[Tooltip("The influence this target vertical position should have when calculating the average position of all the targets")]
	public FsmFloat targetInfluenceV = 1f;

	[Tooltip("The time it takes for this target to reach it's influence")]
	public FsmFloat duration = 0f;

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null && (bool)target.Value)
		{
			ProCamera2D.Instance.AddCameraTarget(target.Value.transform, targetInfluenceH.Value, targetInfluenceV.Value, duration.Value);
		}
		Finish();
	}
}
