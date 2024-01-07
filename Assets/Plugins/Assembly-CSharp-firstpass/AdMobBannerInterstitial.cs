using UnityEngine;
using UnityEngine.SceneManagement;

public class AdMobBannerInterstitial : MonoBehaviour
{
	public string sceneBannerId
	{
		get
		{
			return SceneManager.GetActiveScene().name + "_" + base.gameObject.name;
		}
	}

	private void Awake()
	{
		if (!GoogleMobileAd.IsInited)
		{
			GoogleMobileAd.Init();
		}
	}

	private void Start()
	{
		ShowBanner();
	}

	public void ShowBanner()
	{
		GoogleMobileAd.StartInterstitialAd();
	}
}
