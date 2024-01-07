using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_BannerEvents : FsmStateAction
	{
		public FsmInt bannerId;

		public FsmEvent OnLoadedEvent;

		public FsmEvent OnFailedToLoadEvent;

		public FsmEvent OnOpenEvent;

		public FsmEvent OnCloseEvent;

		public FsmEvent OnLeftApplicationEvent;

		public override void OnEnter()
		{
			GoogleMobileAdBanner banner = GoogleMobileAd.GetBanner(bannerId.Value);
			if (banner != null)
			{
				banner.OnLoadedAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner.OnLoadedAction, new Action<GoogleMobileAdBanner>(OnLoaded));
				banner.OnFailedLoadingAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner.OnFailedLoadingAction, new Action<GoogleMobileAdBanner>(OnFailedToLoad));
				banner.OnOpenedAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner.OnOpenedAction, new Action<GoogleMobileAdBanner>(OnOpen));
				banner.OnClosedAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner.OnClosedAction, new Action<GoogleMobileAdBanner>(OnClose));
				banner.OnLeftApplicationAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner.OnLeftApplicationAction, new Action<GoogleMobileAdBanner>(OnLeftApplication));
			}
		}

		private void OnLoaded(GoogleMobileAdBanner banner)
		{
			base.Fsm.Event(OnLoadedEvent);
		}

		private void OnFailedToLoad(GoogleMobileAdBanner banner)
		{
			base.Fsm.Event(OnFailedToLoadEvent);
		}

		private void OnOpen(GoogleMobileAdBanner banner)
		{
			base.Fsm.Event(OnOpenEvent);
		}

		private void OnClose(GoogleMobileAdBanner banner)
		{
			base.Fsm.Event(OnCloseEvent);
		}

		private void OnLeftApplication(GoogleMobileAdBanner banner)
		{
			base.Fsm.Event(OnLeftApplicationEvent);
		}
	}
}
