namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_SetAdTestDevices : FsmStateAction
	{
		public FsmString[] devicesIds;

		public override void OnEnter()
		{
			FsmString[] array = devicesIds;
			foreach (FsmString fsmString in array)
			{
				GoogleMobileAd.AddTestDevice(fsmString.Value);
			}
			Finish();
		}
	}
}
