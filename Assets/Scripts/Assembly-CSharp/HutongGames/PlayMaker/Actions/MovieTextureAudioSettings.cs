using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Sets the Game Object as the Audio Source associated with the Movie Texture. The Game Object must have an AudioSource Component.")]
	public class MovieTextureAudioSettings : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject gameObject;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			Finish();
		}
	}
}
