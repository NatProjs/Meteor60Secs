using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("Asynchronously Loads an asset stored at path in a Resources folder. The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
	public class ResourcesLoadAsynch : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
		public FsmString assetPath;

		[RequiredField]
		[Tooltip("The stored asset")]
		[UIHint(UIHint.Variable)]
		public FsmVar storeAsset;

		[ActionSection("Result")]
		[Tooltip("true if the loading succedded or not")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("The isDone property of the asynch request")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		[Tooltip("The progress of the asynch loading")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		[Tooltip("Event sent when loading is done")]
		public FsmEvent doneEvent;

		[Tooltip("Event sent when loading failed")]
		public FsmEvent failureEvent;

		private ResourceRequest _request;

		private FsmObject _objectVar;

		public override void Reset()
		{
			assetPath = null;
			storeAsset = new FsmVar();
			storeAsset.Type = VariableType.Texture;
			success = null;
			isDone = null;
			progress = null;
			doneEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			isDone.Value = false;
			bool flag = false;
			try
			{
				flag = loadResourceAsynch();
			}
			catch (UnityException ex)
			{
				Debug.LogWarning(ex.Message);
			}
			if (!flag)
			{
				success.Value = false;
				base.Fsm.Event(failureEvent);
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (_request == null)
			{
				return;
			}
			progress.Value = _request.progress;
			if (_request.isDone)
			{
				isDone.Value = _request.isDone;
				bool flag = StoreResource();
				success.Value = flag;
				if (flag)
				{
					base.Fsm.Event(doneEvent);
				}
				else
				{
					base.Fsm.Event(failureEvent);
				}
				Finish();
			}
		}

		public override string ErrorCheck()
		{
			switch (storeAsset.Type)
			{
			default:
				return "Only GameObject, Texture, Sprites, AudioClip and Material are supported";
			case VariableType.GameObject:
			case VariableType.String:
			case VariableType.Material:
			case VariableType.Texture:
			case VariableType.Object:
				return string.Empty;
			}
		}

		public bool loadResourceAsynch()
		{
			switch (storeAsset.Type)
			{
			case VariableType.GameObject:
				_request = Resources.LoadAsync(assetPath.Value, typeof(GameObject));
				break;
			case VariableType.Texture:
				_request = Resources.LoadAsync(assetPath.Value, typeof(Texture2D));
				break;
			case VariableType.Material:
				_request = Resources.LoadAsync(assetPath.Value, typeof(Material));
				break;
			case VariableType.String:
				_request = Resources.LoadAsync(assetPath.Value, typeof(TextAsset));
				break;
			case VariableType.Object:
				_objectVar = base.Fsm.Variables.GetFsmObject(storeAsset.variableName);
				_request = Resources.LoadAsync(assetPath.Value, _objectVar.ObjectType);
				break;
			default:
				return false;
			}
			return true;
		}

		public bool StoreResource()
		{
			if (_request == null)
			{
				return false;
			}
			if (_request.asset == null)
			{
				return false;
			}
			switch (storeAsset.Type)
			{
			case VariableType.GameObject:
			{
				GameObject gameObject = (GameObject)_request.asset;
				if (gameObject == null)
				{
					return false;
				}
				GameObject gameObject2 = Object.Instantiate(gameObject);
				if (gameObject2 == null)
				{
					return false;
				}
				FsmGameObject fsmGameObject = base.Fsm.Variables.GetFsmGameObject(storeAsset.variableName);
				fsmGameObject.Value = gameObject2;
				break;
			}
			case VariableType.Texture:
			{
				Texture2D texture2D = (Texture2D)_request.asset;
				if (texture2D == null)
				{
					return false;
				}
				FsmTexture fsmTexture = base.Fsm.Variables.GetFsmTexture(storeAsset.variableName);
				fsmTexture.Value = texture2D;
				break;
			}
			case VariableType.Material:
			{
				Material material = (Material)_request.asset;
				if (material == null)
				{
					return false;
				}
				FsmMaterial fsmMaterial = base.Fsm.Variables.GetFsmMaterial(storeAsset.variableName);
				fsmMaterial.Value = material;
				break;
			}
			case VariableType.String:
			{
				TextAsset textAsset = (TextAsset)_request.asset;
				if (textAsset == null)
				{
					return false;
				}
				FsmString fsmString = base.Fsm.Variables.GetFsmString(storeAsset.variableName);
				fsmString.Value = textAsset.text;
				break;
			}
			case VariableType.Object:
			{
				FsmObject fsmObject = base.Fsm.Variables.GetFsmObject(storeAsset.variableName);
				fsmObject.Value = _request.asset;
				if (fsmObject.Value != null && fsmObject.Value.GetType() == fsmObject.ObjectType)
				{
					return true;
				}
				fsmObject.Value = null;
				return false;
			}
			default:
				return false;
			}
			return true;
		}
	}
}
