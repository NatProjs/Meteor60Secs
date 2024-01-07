using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputNavigator : MonoBehaviour
{
	private EventSystem system;

	public KeyCode[] NavigationKeys = new KeyCode[1] { KeyCode.Tab };

	private bool _keyDown;

	private void Start()
	{
		system = EventSystem.current;
	}

	private void Update()
	{
		_keyDown = false;
		if (Input.anyKeyDown)
		{
			KeyCode[] navigationKeys = NavigationKeys;
			foreach (KeyCode key in navigationKeys)
			{
				if (Input.GetKeyDown(key))
				{
					_keyDown = true;
					break;
				}
			}
		}
		if (!_keyDown)
		{
			return;
		}
		Selectable selectable = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
		if (selectable != null)
		{
			InputField component = selectable.GetComponent<InputField>();
			if (component != null)
			{
				component.OnPointerClick(new PointerEventData(system));
			}
			system.SetSelectedGameObject(selectable.gameObject, new BaseEventData(system));
		}
		else
		{
			system.SetSelectedGameObject(system.firstSelectedGameObject, new BaseEventData(system));
		}
	}
}
