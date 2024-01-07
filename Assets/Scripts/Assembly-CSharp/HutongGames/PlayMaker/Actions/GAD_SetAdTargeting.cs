namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Google Mobile Ad")]
	public class GAD_SetAdTargeting : FsmStateAction
	{
		public FsmString[] keywords;

		public bool tagForChildDirectedTreatment;

		public GoogleGender gender;

		public bool setBirthday;

		public FsmInt day;

		public AndroidMonth month;

		public FsmInt year;

		public override void OnEnter()
		{
			FsmString[] array = keywords;
			foreach (FsmString fsmString in array)
			{
				GoogleMobileAd.AddKeyword(fsmString.Value);
			}
			GoogleMobileAd.SetGender(gender);
			GoogleMobileAd.TagForChildDirectedTreatment(tagForChildDirectedTreatment);
			if (setBirthday)
			{
				GoogleMobileAd.SetBirthday(year.Value, month, day.Value);
			}
			Finish();
		}
	}
}
