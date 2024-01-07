using DG.Tweening;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Plays forward all tweens (meaning tweens that were not already playing forward or complete)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsPlayForwardAll : FsmStateAction
	{
		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			int num = DOTween.PlayForwardAll();
			if (debugThis.Value)
			{
				Debug.Log("GameObject [" + base.State.Fsm.GameObjectName + "] FSM [" + base.State.Fsm.Name + "]  State [" + base.State.Name + "] - DOTween Control Methods Play Forward All - SUCCESS! - Played " + num + " tweens");
			}
			Finish();
		}
	}
}
