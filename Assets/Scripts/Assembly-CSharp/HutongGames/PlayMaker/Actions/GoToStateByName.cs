using System.Reflection;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Immediately switch to a state with the selected name.")]
	public class GoToStateByName : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Name of FSM on Game Object. Leave to none to target this fsm")]
		public FsmString fsmName;

		[RequiredField]
		[Tooltip("The name of the state to go to")]
		public FsmString stateName;

		[Tooltip("Event Sent if the state was found")]
		public FsmEvent stateFoundEvent;

		[Tooltip("Event Sent if the state was not found")]
		public FsmEvent stateNotFoundEvent;

		public override void Reset()
		{
			gameObject = null;
			fsmName = new FsmString
			{
				UseVariable = true
			};
			stateName = null;
			stateFoundEvent = null;
			stateNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			DoGotoState();
			Finish();
		}

		private void DoGotoState()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				base.Fsm.Event(stateNotFoundEvent);
				return;
			}
			Fsm fsm = base.Fsm;
			if (!fsmName.IsNone)
			{
				PlayMakerFSM gameObjectFsm = ActionHelpers.GetGameObjectFsm(ownerDefaultTarget, fsmName.Value);
				if (gameObjectFsm != null)
				{
					fsm = gameObjectFsm.Fsm;
				}
			}
			FsmState fsmState = null;
			FsmState[] states = fsm.States;
			foreach (FsmState fsmState2 in states)
			{
				if (fsmState2.Name == stateName.Value)
				{
					fsmState = fsmState2;
					break;
				}
			}
			if (fsmState != null)
			{
				MethodInfo method = base.Fsm.GetType().GetMethod("SwitchState", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				method.Invoke(fsm, new object[1] { fsmState });
				base.Fsm.Event(stateFoundEvent);
			}
			else
			{
				base.Fsm.Event(stateNotFoundEvent);
			}
			Finish();
		}
	}
}
