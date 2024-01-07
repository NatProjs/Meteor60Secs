using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("uGui")]
	[Tooltip("Set The Fill Amount on a uGui image")]
	public class uGuiImageGetFillAmount : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the Image ui component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The fill amount.")]
		public FsmFloat ImageFillAmount;

		[Tooltip("Repeats every frame")]
		public bool everyFrame;

		private Image _image;

		public override void Reset()
		{
			gameObject = null;
			ImageFillAmount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				_image = ownerDefaultTarget.GetComponent<Image>();
			}
			DoGetFillAmount();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetFillAmount();
		}

		private void DoGetFillAmount()
		{
			if (_image != null)
			{
				ImageFillAmount.Value = _image.fillAmount;
			}
		}
	}
}
