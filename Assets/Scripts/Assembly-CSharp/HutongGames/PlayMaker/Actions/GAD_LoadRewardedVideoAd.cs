namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	[Tooltip("Start Load Rewarded Video Ad request (ANDROID PLATFORM ONLY)")]
	public class GAD_LoadRewardedVideoAd : FsmStateAction
	{
		[Tooltip("Called when a rewarded video ad is loaded.")]
		public FsmEvent successEvent;

		[Tooltip("Called when a rewarded video ad request failed.")]
		public FsmEvent failEvent;

		public override void OnEnter()
		{
			GoogleMobileAd.controller.OnRewardedVideoLoaded += OnReady;
			GoogleMobileAd.controller.OnRewardedVideoAdFailedToLoad += RewardedVideoAdFailedToLoad;
			GoogleMobileAd.controller.LoadRewardedVideo();
		}

		private void OnReady()
		{
			GoogleMobileAd.controller.OnRewardedVideoLoaded -= OnReady;
			GoogleMobileAd.controller.OnRewardedVideoAdFailedToLoad -= RewardedVideoAdFailedToLoad;
			base.Fsm.Event(successEvent);
			Finish();
		}

		private void RewardedVideoAdFailedToLoad(int statusCode)
		{
			GoogleMobileAd.controller.OnRewardedVideoLoaded -= OnReady;
			GoogleMobileAd.controller.OnRewardedVideoAdFailedToLoad -= RewardedVideoAdFailedToLoad;
			base.Fsm.Event(failEvent);
			Finish();
		}
	}
}
