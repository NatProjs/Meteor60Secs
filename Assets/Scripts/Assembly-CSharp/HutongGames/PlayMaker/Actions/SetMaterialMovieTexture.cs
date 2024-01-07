using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named texture in a game object's material to a movie texture.")]
	public class SetMaterialMovieTexture : ComponentAction<Renderer>
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[UIHint(UIHint.NamedTexture)]
		[Tooltip("A named texture in the shader.")]
		public FsmString namedTexture;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedTexture = "_MainTex";
		}

		public override void OnEnter()
		{
			DoSetMaterialTexture();
			Finish();
		}

		private void DoSetMaterialTexture()
		{
			// nun
		}
	}
}
