namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_ShowInterstisialAd : FsmStateAction
	{
		[Tooltip("Event fired when Ad is started")]
		public FsmEvent successEvent;

		[Tooltip("Event fired when Ad is failed to load")]
		public FsmEvent failEvent;

		public override void OnEnter()
		{
			GoogleMobileAd.ShowInterstitialAd();
			Finish();
		}
	}
}
