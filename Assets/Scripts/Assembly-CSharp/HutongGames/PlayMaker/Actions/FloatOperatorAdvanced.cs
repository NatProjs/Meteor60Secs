using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Performs math operations on 2 Floats: Add, Subtract, Multiply, Divide, Min, Max. This advanced version let you choose when to perform the action.")]
	public class FloatOperatorAdvanced : FsmStateActionAdvanced
	{
		public enum Operation
		{
			Add = 0,
			Subtract = 1,
			Multiply = 2,
			Divide = 3,
			Min = 4,
			Max = 5,
			Power = 6
		}

		[RequiredField]
		[Tooltip("The first float.")]
		public FsmFloat float1;

		[RequiredField]
		[Tooltip("The second float.")]
		public FsmFloat float2;

		[Tooltip("The math operation to perform on the floats.")]
		public Operation operation;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the operation in a float variable.")]
		public FsmFloat storeResult;

		public override void Reset()
		{
			base.Reset();
			float1 = null;
			float2 = null;
			operation = Operation.Add;
			storeResult = null;
		}

		public override void OnActionUpdate()
		{
			DoFloatOperator();
		}

		private void DoFloatOperator()
		{
			float value = float1.Value;
			float value2 = float2.Value;
			switch (operation)
			{
			case Operation.Add:
				storeResult.Value = value + value2;
				break;
			case Operation.Subtract:
				storeResult.Value = value - value2;
				break;
			case Operation.Multiply:
				storeResult.Value = value * value2;
				break;
			case Operation.Divide:
				storeResult.Value = value / value2;
				break;
			case Operation.Min:
				storeResult.Value = Mathf.Min(value, value2);
				break;
			case Operation.Max:
				storeResult.Value = Mathf.Max(value, value2);
				break;
			case Operation.Power:
				storeResult.Value = Mathf.Pow(value, value2);
				break;
			}
		}
	}
}
