using System;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.UI;

public class PlayMakerUGuiComponentProxy : MonoBehaviour
{
	public enum ActionType
	{
		SendFsmEvent = 0,
		SetFsmVariable = 1
	}

	public enum PlayMakerProxyEventTarget
	{
		Owner = 0,
		GameObject = 1,
		BroadCastAll = 2,
		FsmComponent = 3
	}

	public enum PlayMakerProxyVariableTarget
	{
		Owner = 0,
		GameObject = 1,
		GlobalVariable = 2,
		FsmComponent = 3
	}

	[Serializable]
	public struct FsmVariableSetup
	{
		public PlayMakerProxyVariableTarget target;

		public GameObject gameObject;

		public PlayMakerFSM fsmComponent;

		public int fsmIndex;

		public int variableIndex;

		public VariableType variableType;

		public string variableName;
	}

	[Serializable]
	public struct FsmEventSetup
	{
		public PlayMakerProxyEventTarget target;

		public GameObject gameObject;

		public PlayMakerFSM fsmComponent;

		public string customEventName;

		public string builtInEventName;

		public bool sendtoChildren;
	}

	public bool debug;

	private string error;

	public OwnerDefaultOption UiTargetOption;

	public GameObject UiTarget;

	public ActionType action;

	public FsmVariableSetup fsmVariableSetup;

	private FsmFloat fsmFloatTarget;

	private FsmBool fsmBoolTarget;

	private FsmVector2 fsmVector2Target;

	private FsmString fsmStringTarget;

	private FsmInt fsmIntTarget;

	public FsmEventSetup fsmEventSetup;

	private FsmEventTarget fsmEventTarget;

	private bool WatchInputField;

	private InputField inputField;

	private string lastInputFieldValue;

	private void Start()
	{
		if (action == ActionType.SetFsmVariable)
		{
			SetupVariableTarget();
		}
		else
		{
			SetupEventTarget();
		}
		SetupUiListeners();
	}

	private void Update()
	{
		if (WatchInputField && inputField != null && !inputField.text.Equals(lastInputFieldValue))
		{
			lastInputFieldValue = inputField.text;
			SetFsmVariable(lastInputFieldValue);
		}
	}

	private void SetupEventTarget()
	{
		if (fsmEventTarget == null)
		{
			fsmEventTarget = new FsmEventTarget();
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.BroadCastAll)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.BroadcastAll;
			fsmEventTarget.excludeSelf = false;
		}
		else if (fsmEventSetup.target == PlayMakerProxyEventTarget.FsmComponent)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.FSMComponent;
			fsmEventTarget.fsmComponent = fsmEventSetup.fsmComponent;
		}
		else if (fsmEventSetup.target == PlayMakerProxyEventTarget.GameObject)
		{
			fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
			fsmEventTarget.gameObject = new FsmOwnerDefault();
			fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			fsmEventTarget.gameObject.GameObject.Value = fsmEventSetup.gameObject;
		}
		else if (fsmEventSetup.target == PlayMakerProxyEventTarget.Owner)
		{
			fsmEventTarget.ResetParameters();
			fsmEventTarget.target = FsmEventTarget.EventTarget.GameObject;
			fsmEventTarget.gameObject = new FsmOwnerDefault();
			fsmEventTarget.gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			fsmEventTarget.gameObject.GameObject.Value = base.gameObject;
		}
	}

	private void SetupVariableTarget()
	{
		if (fsmVariableSetup.target == PlayMakerProxyVariableTarget.GlobalVariable)
		{
			if (fsmVariableSetup.variableType == VariableType.Bool)
			{
				fsmBoolTarget = FsmVariables.GlobalVariables.FindFsmBool(fsmVariableSetup.variableName);
			}
			else if (fsmVariableSetup.variableType == VariableType.Float)
			{
				fsmFloatTarget = FsmVariables.GlobalVariables.FindFsmFloat(fsmVariableSetup.variableName);
			}
			else if (fsmVariableSetup.variableType == VariableType.Vector2)
			{
				fsmVector2Target = FsmVariables.GlobalVariables.FindFsmVector2(fsmVariableSetup.variableName);
			}
			else if (fsmVariableSetup.variableType == VariableType.String)
			{
				fsmStringTarget = FsmVariables.GlobalVariables.FindFsmString(fsmVariableSetup.variableName);
			}
		}
		else if (fsmVariableSetup.target == PlayMakerProxyVariableTarget.FsmComponent)
		{
			if (fsmVariableSetup.fsmComponent != null)
			{
				if (fsmVariableSetup.variableType == VariableType.Bool)
				{
					fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
				}
				else if (fsmVariableSetup.variableType == VariableType.Float)
				{
					fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);
				}
				else if (fsmVariableSetup.variableType == VariableType.Vector2)
				{
					fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
				}
				else if (fsmVariableSetup.variableType == VariableType.String)
				{
					fsmStringTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
				}
			}
			else
			{
				Debug.LogError("set to target a FsmComponent but fsmEventTarget.target is null");
			}
		}
		else if (fsmVariableSetup.target == PlayMakerProxyVariableTarget.Owner)
		{
			if (fsmVariableSetup.gameObject != null)
			{
				if (fsmVariableSetup.fsmComponent != null)
				{
					if (fsmVariableSetup.variableType == VariableType.Bool)
					{
						fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Float)
					{
						fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Vector2)
					{
						fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.String)
					{
						fsmStringTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
					}
				}
			}
			else
			{
				Debug.LogError("set to target Owbner but fsmEventTarget.target is null");
			}
		}
		else
		{
			if (fsmVariableSetup.target != PlayMakerProxyVariableTarget.GameObject)
			{
				return;
			}
			if (fsmVariableSetup.gameObject != null)
			{
				if (fsmVariableSetup.fsmComponent != null)
				{
					if (fsmVariableSetup.variableType == VariableType.Bool)
					{
						fsmBoolTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmBool(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Float)
					{
						fsmFloatTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmFloat(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.Vector2)
					{
						fsmVector2Target = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmVector2(fsmVariableSetup.variableName);
					}
					else if (fsmVariableSetup.variableType == VariableType.String)
					{
						fsmStringTarget = fsmVariableSetup.fsmComponent.FsmVariables.FindFsmString(fsmVariableSetup.variableName);
					}
				}
			}
			else
			{
				Debug.LogError("set to target a Gameobject but fsmEventTarget.target is null");
			}
		}
	}

	private void SetupUiListeners()
	{
		if (UiTarget.GetComponent<Button>() != null)
		{
			UiTarget.GetComponent<Button>().onClick.AddListener(OnClick);
		}
		if (UiTarget.GetComponent<Toggle>() != null)
		{
			UiTarget.GetComponent<Toggle>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Toggle>().isOn);
			}
		}
		if (UiTarget.GetComponent<Slider>() != null)
		{
			UiTarget.GetComponent<Slider>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Slider>().value);
			}
		}
		if (UiTarget.GetComponent<Scrollbar>() != null)
		{
			UiTarget.GetComponent<Scrollbar>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Scrollbar>().value);
			}
		}
		if (UiTarget.GetComponent<ScrollRect>() != null)
		{
			UiTarget.GetComponent<ScrollRect>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<ScrollRect>().normalizedPosition);
			}
		}
		if (UiTarget.GetComponent<InputField>() != null)
		{
			inputField = UiTarget.GetComponent<InputField>();
			UiTarget.GetComponent<InputField>().onEndEdit.AddListener(onEndEdit);
			if (action == ActionType.SetFsmVariable)
			{
				WatchInputField = true;
				lastInputFieldValue = string.Empty;
			}
		}
		if (UiTarget.GetComponent<Dropdown>() != null)
		{
			UiTarget.GetComponent<Dropdown>().onValueChanged.AddListener(OnValueChanged);
			if (action == ActionType.SetFsmVariable)
			{
				SetFsmVariable(UiTarget.GetComponent<Dropdown>().value);
			}
		}
	}

	protected void OnClick()
	{
		if (debug)
		{
			Debug.Log("OnClick");
		}
		FsmEventData eventData = Fsm.EventData;
		FirePlayMakerEvent(eventData);
	}

	protected void OnValueChanged(bool value)
	{
		if (debug)
		{
			Debug.Log("OnValueChanged(bool): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData eventData = Fsm.EventData;
			eventData.BoolData = value;
			FirePlayMakerEvent(eventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(int value)
	{
		if (debug)
		{
			Debug.Log("OnValueChanged(int): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData eventData = Fsm.EventData;
			eventData.IntData = value;
			FirePlayMakerEvent(eventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(float value)
	{
		if (debug)
		{
			Debug.Log("OnValueChanged(float): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData eventData = Fsm.EventData;
			eventData.FloatData = value;
			FirePlayMakerEvent(eventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void OnValueChanged(Vector2 value)
	{
		if (debug)
		{
			Debug.Log("OnValueChanged(vector2): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData eventData = Fsm.EventData;
			eventData.Vector2Data = value;
			FirePlayMakerEvent(eventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	protected void onEndEdit(string value)
	{
		if (debug)
		{
			Debug.Log("onEndEdit(string): " + value);
		}
		if (action == ActionType.SendFsmEvent)
		{
			FsmEventData eventData = Fsm.EventData;
			eventData.StringData = value;
			eventData.BoolData = inputField.wasCanceled;
			FirePlayMakerEvent(eventData);
		}
		else
		{
			SetFsmVariable(value);
		}
	}

	private void SetFsmVariable(Vector2 value)
	{
		if (fsmVector2Target != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Vector2 set to " + value);
			}
			fsmVector2Target.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Vector2 MISSING !!", base.gameObject);
		}
	}

	private void SetFsmVariable(bool value)
	{
		if (fsmBoolTarget != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Bool set to " + value);
			}
			fsmBoolTarget.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Bool MISSING !!", base.gameObject);
		}
	}

	private void SetFsmVariable(float value)
	{
		if (fsmFloatTarget != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Float set to " + value);
			}
			fsmFloatTarget.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm Float MISSING !!", base.gameObject);
		}
	}

	private void SetFsmVariable(string value)
	{
		if (fsmStringTarget != null)
		{
			if (debug)
			{
				Debug.Log("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm String set to " + value);
			}
			fsmStringTarget.Value = value;
		}
		else
		{
			Debug.LogError("PlayMakerUGuiComponentProxy on " + base.name + ": Fsm String MISSING !!", base.gameObject);
		}
	}

	private void FirePlayMakerEvent(FsmEventData eventData)
	{
		fsmEventTarget.excludeSelf = false;
		fsmEventTarget.sendToChildren = fsmEventSetup.sendtoChildren;
		if (debug)
		{
			Debug.Log("Fire event: " + GetEventString());
		}
		PlayMakerUtils.SendEventToTarget(null, fsmEventTarget, GetEventString(), eventData);
	}

	public bool DoesTargetImplementsEvent()
	{
		string eventString = GetEventString();
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.BroadCastAll)
		{
			return FsmEvent.IsEventGlobal(eventString);
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.FsmComponent)
		{
			return PlayMakerUtils.DoesFsmImplementsEvent(fsmEventSetup.fsmComponent, eventString);
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.GameObject)
		{
			return PlayMakerUtils.DoesGameObjectImplementsEvent(fsmEventSetup.gameObject, eventString, fsmEventSetup.sendtoChildren);
		}
		if (fsmEventSetup.target == PlayMakerProxyEventTarget.Owner)
		{
			return PlayMakerUtils.DoesGameObjectImplementsEvent(base.gameObject, eventString, fsmEventSetup.sendtoChildren);
		}
		return false;
	}

	private string GetEventString()
	{
		return (!string.IsNullOrEmpty(fsmEventSetup.customEventName)) ? fsmEventSetup.customEventName : fsmEventSetup.builtInEventName;
	}
}
