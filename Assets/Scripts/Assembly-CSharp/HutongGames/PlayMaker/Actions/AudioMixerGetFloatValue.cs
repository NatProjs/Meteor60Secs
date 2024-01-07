using UnityEngine.Audio;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[ActionTarget(typeof(AudioMixer), "theMixer", false)]
	[Tooltip("Gets the float value of an exposed parameter for a Unity Audio Mixer. Prior to calling SetFloat and after ClearFloat has been called on this parameter the value returned will be that of the current snapshot or snapshot transition.")]
	public class AudioMixerGetFloatValue : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AudioMixer))]
		[Tooltip("The Audio Mixer with the exposed parameter.")]
		public FsmObject theMixer;

		[RequiredField]
		[Tooltip("The name of the exposed parameter.")]
		[Title("Name of Parameter")]
		public FsmString exposedFloatName;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Float value in a variable")]
		public FsmFloat storeFloatValue;

		public bool everyFrame;

		public override void Reset()
		{
			theMixer = null;
			exposedFloatName = null;
			storeFloatValue = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetMixerFloatValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetMixerFloatValue();
		}

		public void DoGetMixerFloatValue()
		{
			AudioMixer audioMixer = theMixer.Value as AudioMixer;
			string value = exposedFloatName.Value;
			if (audioMixer != null && !string.IsNullOrEmpty(value))
			{
				float value2 = 0f;
				audioMixer.GetFloat(value, out value2);
				storeFloatValue.Value = value2;
			}
		}
	}
}
