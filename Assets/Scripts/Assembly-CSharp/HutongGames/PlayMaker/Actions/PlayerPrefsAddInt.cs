using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Adds a value to a playerprefs int identified by key. WARNING!! use PlayerPrefs only at key moments")]
	public class PlayerPrefsAddInt : FsmStateAction
	{
		[Tooltip("Case sensitive key.")]
		public FsmString key;

		public FsmInt add;

		private int variables;

		public override void Reset()
		{
			key = string.Empty;
			add = null;
		}

		public override void OnEnter()
		{
			if (!key.IsNone || !key.Value.Equals(string.Empty))
			{
				variables = PlayerPrefs.GetInt(key.Value, 0);
			}
			variables += add.Value;
			PlayerPrefs.SetInt(key.Value, variables);
			Finish();
		}
	}
}
