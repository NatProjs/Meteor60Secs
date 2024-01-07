using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_CreateBanner : FsmStateAction
	{
		public FsmInt bannerId;

		public bool ShowBannerOnLoad = true;

		public TextAnchor anchor;

		public GADBannerSize size;

		public bool CreateWithCoords;

		public FsmInt XCoord;

		public FsmInt YCoord;

		[Tooltip("Event fired when Ad is started")]
		public FsmEvent successEvent;

		[Tooltip("Event fired when Ad is failed to load")]
		public FsmEvent failEvent;

		private GoogleMobileAdBanner _banner;

		public override void OnEnter()
		{
			if (CreateWithCoords)
			{
				_banner = GoogleMobileAd.CreateAdBanner(XCoord.Value, YCoord.Value, size);
			}
			else
			{
				_banner = GoogleMobileAd.CreateAdBanner(anchor, size);
			}
			_banner.ShowOnLoad = ShowBannerOnLoad;
			bannerId.Value = _banner.id;
			GoogleMobileAdBanner banner = _banner;
			banner.OnLoadedAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner.OnLoadedAction, new Action<GoogleMobileAdBanner>(OnReady));
			GoogleMobileAdBanner banner2 = _banner;
			banner2.OnFailedLoadingAction = (Action<GoogleMobileAdBanner>)Delegate.Combine(banner2.OnFailedLoadingAction, new Action<GoogleMobileAdBanner>(OnFail));
		}

		private void OnReady(GoogleMobileAdBanner banner)
		{
			GoogleMobileAdBanner banner2 = _banner;
			banner2.OnLoadedAction = (Action<GoogleMobileAdBanner>)Delegate.Remove(banner2.OnLoadedAction, new Action<GoogleMobileAdBanner>(OnReady));
			GoogleMobileAdBanner banner3 = _banner;
			banner3.OnFailedLoadingAction = (Action<GoogleMobileAdBanner>)Delegate.Remove(banner3.OnFailedLoadingAction, new Action<GoogleMobileAdBanner>(OnFail));
			base.Fsm.Event(successEvent);
			Finish();
		}

		private void OnFail(GoogleMobileAdBanner banner)
		{
			GoogleMobileAdBanner banner2 = _banner;
			banner2.OnLoadedAction = (Action<GoogleMobileAdBanner>)Delegate.Remove(banner2.OnLoadedAction, new Action<GoogleMobileAdBanner>(OnReady));
			GoogleMobileAdBanner banner3 = _banner;
			banner3.OnFailedLoadingAction = (Action<GoogleMobileAdBanner>)Delegate.Remove(banner3.OnFailedLoadingAction, new Action<GoogleMobileAdBanner>(OnFail));
			base.Fsm.Event(failEvent);
			Finish();
		}
	}
}
