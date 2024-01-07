namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_InitGoogleAd : FsmStateAction
	{
		public override void OnEnter()
		{
			GoogleMobileAd.Init();
			Finish();
		}
	}
}
