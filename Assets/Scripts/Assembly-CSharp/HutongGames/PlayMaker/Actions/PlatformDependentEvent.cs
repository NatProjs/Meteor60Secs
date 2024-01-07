using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Sends Events based on platform dependent flag")]
	public class PlatformDependentEvent : FsmStateAction
	{
		public enum platformDependentFlags
		{
			UNITY_EDITOR = 0,
			UNITY_EDITOR_WIN = 1,
			UNITY_EDITOR_OSX = 2,
			UNITY_STANDALONE_OSX = 3,
			UNITY_DASHBOARD_WIDGET = 4,
			UNITY_STANDALONE_WIN = 5,
			UNITY_STANDALONE_LINUX = 6,
			UNITY_STANDALONE = 7,
			UNITY_WEBPLAYER = 8,
			UNITY_WII = 9,
			UNITY_IPHONE = 10,
			UNITY_ANDROID = 11,
			UNITY_PS3 = 12,
			UNITY_XBOX360 = 13,
			UNITY_NACL = 14,
			UNITY_FLASH = 15,
			UNITY_BLACKBERRY = 16,
			UNITY_WP8 = 17,
			UNITY_METRO = 18,
			UNITY_WINRT = 19,
			UNITY_IOS = 20,
			UNITY_PS4 = 21,
			UNITY_XBOXONE = 22,
			UNITY_TIZEN = 23,
			UNITY_WP8_1 = 24,
			UNITY_WSA = 25,
			UNITY_WSA_8_0 = 26,
			UNITY_WSA_8_1 = 27,
			UNITY_WINRT_8_0 = 28,
			UNITY_WINRT_8_1 = 29,
			UNITY_WEBGGL = 30
		}

		[Tooltip("The platform")]
		public platformDependentFlags platform;

		[Tooltip("The event to send for that platform")]
		public FsmEvent matchEvent;

		[Tooltip("The event to send for that platform")]
		public FsmEvent noMatchEvent;

		public override void Reset()
		{
			platform = platformDependentFlags.UNITY_WEBPLAYER;
			matchEvent = null;
			noMatchEvent = null;
		}

		private bool isMatch(platformDependentFlags valueA, platformDependentFlags valueB)
		{
			string text = Enum.GetName(typeof(platformDependentFlags), valueA);
			string text2 = Enum.GetName(typeof(platformDependentFlags), valueB);
			Debug.Log("is match?: " + text + " == " + text2 + " => " + string.Equals(text, text2));
			return string.Equals(text, text2);
		}

		public override void OnEnter()
		{
			platformDependentFlags platformDependentFlags = platform;
			Debug.Log("checking for " + platformDependentFlags);
			FsmEvent fsmEvent = matchEvent;
			if (platformDependentFlags == platformDependentFlags.UNITY_STANDALONE_WIN)
			{
				base.Fsm.Event(fsmEvent);
			}
			if (platformDependentFlags == platformDependentFlags.UNITY_STANDALONE)
			{
				base.Fsm.Event(fsmEvent);
			}
			base.Fsm.Event(noMatchEvent);
			Finish();
		}
	}
}
