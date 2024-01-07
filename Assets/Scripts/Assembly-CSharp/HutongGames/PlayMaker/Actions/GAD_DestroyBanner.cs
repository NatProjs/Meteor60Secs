namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_DestroyBanner : FsmStateAction
	{
		public FsmInt bannerId;

		public override void OnEnter()
		{
			GoogleMobileAd.DestroyBanner(bannerId.Value);
			Finish();
		}
	}
}
