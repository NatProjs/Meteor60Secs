namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_InterstitialEvents : FsmStateAction
	{
		public FsmEvent OnLoadedEvent;

		public FsmEvent OnFailedToLoadEvent;

		public FsmEvent OnOpenEvent;

		public FsmEvent OnCloseEvent;

		public FsmEvent OnLeftApplicationEvent;

		public override void OnEnter()
		{
			GoogleMobileAd.OnInterstitialLoaded += OnLoaded;
			GoogleMobileAd.OnInterstitialFailedLoading += OnFailedToLoad;
			GoogleMobileAd.OnInterstitialOpened += OnOpen;
			GoogleMobileAd.OnInterstitialClosed += OnClose;
			GoogleMobileAd.OnInterstitialLeftApplication += OnLeftApplication;
		}

		private void OnLoaded()
		{
			base.Fsm.Event(OnLoadedEvent);
		}

		private void OnFailedToLoad()
		{
			base.Fsm.Event(OnFailedToLoadEvent);
		}

		private void OnOpen()
		{
			base.Fsm.Event(OnOpenEvent);
		}

		private void OnClose()
		{
			base.Fsm.Event(OnCloseEvent);
		}

		private void OnLeftApplication()
		{
			base.Fsm.Event(OnLeftApplicationEvent);
		}
	}
}
