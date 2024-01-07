namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	[Tooltip("Show Rewarded Video Ad (ANDROID PLATFORM ONLY)")]
	public class GAD_ShowRewardedVideoAd : FsmStateAction
	{
		[Tooltip("Called when a rewarded video ad opens a overlay that covers the screen.")]
		public FsmEvent adOpenedEvent;

		[Tooltip("Called when a rewarded video ad starts to play.")]
		public FsmEvent startedEvent;

		[Tooltip("Called when a rewarded video ad leaves the application (e.g., to go to the browser).")]
		public FsmEvent adLeftApplication;

		[Tooltip("Called when a rewarded video ad has triggered a reward. The app is responsible for crediting the user with the reward.")]
		public FsmEvent rewardedEvent;

		[Tooltip("Called when a rewarded video ad is closed.")]
		public FsmEvent adClosedEvent;

		public FsmString item;

		public FsmInt amount;

		public override void OnEnter()
		{
			item.Value = "item";
			amount.Value = 0;
		}

		private void OnReady()
		{
			base.Fsm.Event(adClosedEvent);
			Finish();
		}

		private void Rewarded(string i, int a)
		{
			item.Value = i;
			amount.Value = a;
			base.Fsm.Event(rewardedEvent);
		}

		private void RewardedVideoAdOpened()
		{
			base.Fsm.Event(adOpenedEvent);
		}

		private void RewardedVideoStarted()
		{
			base.Fsm.Event(startedEvent);
		}

		private void RewardedVideoAdLeftApplication()
		{
			base.Fsm.Event(adLeftApplication);
		}
	}
}
