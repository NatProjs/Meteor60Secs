using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Pauses a Movie Texture.")]
	public class PauseMovieTexture : FsmStateAction
	{
		public override void Reset()
		{
			// nuh
		}

		public override void OnEnter()
		{
			// nuhh
			Finish();
		}
	}
}
