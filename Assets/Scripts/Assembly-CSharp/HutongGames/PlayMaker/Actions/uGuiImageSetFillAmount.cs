using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set The Fill Amount on a uGui image")]
	public class uGuiImageSetFillAmount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0f, 1f)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private Image _image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_image = ownerDefaultTarget.GetComponent<Image>();
			}
			DoSetFillAmount();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetFillAmount();
		}

		private void DoSetFillAmount()
		{
			if (_image != null)
			{
				_image.fillAmount = ImageFillAmount.Value;
			}
		}
	}
}
