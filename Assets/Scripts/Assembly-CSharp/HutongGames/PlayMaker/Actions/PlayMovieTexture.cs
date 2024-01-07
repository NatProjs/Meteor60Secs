using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Movie)]
	[Tooltip("Plays a Movie Texture. Use the Movie Texture in a Material, or in the GUI.")]
	public class PlayMovieTexture : FsmStateAction
	{
		public FsmBool loop;

		public override void Reset()
		{
			loop = false;
		}

		public override void OnEnter()
		{
			Finish();
		}
	}
}
