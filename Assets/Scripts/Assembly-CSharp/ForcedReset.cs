using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ForcedReset : MonoBehaviour
{
	private void Update()
	{
		if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
		{
			Application.LoadLevelAsync(Application.loadedLevelName);
		}
	}
}
