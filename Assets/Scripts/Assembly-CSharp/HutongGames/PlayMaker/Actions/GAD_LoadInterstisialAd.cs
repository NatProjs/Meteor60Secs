namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_LoadInterstisialAd : FsmStateAction
	{
		[Tooltip("Event fired when Ad is started")]
		public FsmEvent successEvent;

		[Tooltip("Event fired when Ad is failed to load")]
		public FsmEvent failEvent;

		public override void OnEnter()
		{
			GoogleMobileAd.LoadInterstitialAd();
			GoogleMobileAd.OnInterstitialLoaded += OnReady;
			GoogleMobileAd.OnInterstitialFailedLoading += OnFail;
		}

		private void OnReady()
		{
			GoogleMobileAd.OnInterstitialLoaded -= OnReady;
			GoogleMobileAd.OnInterstitialFailedLoading -= OnFail;
			base.Fsm.Event(successEvent);
			Finish();
		}

		private void OnFail()
		{
			GoogleMobileAd.OnInterstitialLoaded -= OnReady;
			GoogleMobileAd.OnInterstitialFailedLoading -= OnFail;
			base.Fsm.Event(failEvent);
			Finish();
		}
	}
}
