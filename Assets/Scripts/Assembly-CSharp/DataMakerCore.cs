using System.Collections.Generic;
using UnityEngine;

public class DataMakerCore
{
	public static DataMakerProxyBase GetDataMakerProxyPointer<T>(T type, GameObject aProxy, string nameReference, bool silent)
	{
		if (aProxy == null)
		{
			if (!silent)
			{
				Debug.LogError("Null Proxy");
			}
			return null;
		}
		DataMakerProxyBase[] components = aProxy.GetComponents<DataMakerProxyBase>();
		List<DataMakerProxyBase> list = new List<DataMakerProxyBase>();
		DataMakerProxyBase[] array = components;
		foreach (DataMakerProxyBase dataMakerProxyBase in array)
		{
			if (dataMakerProxyBase.GetType().Equals(type))
			{
				list.Add(dataMakerProxyBase);
			}
		}
		components = list.ToArray();
		if (components.Length > 1)
		{
			if (nameReference == string.Empty && !silent)
			{
				Debug.LogError(string.Concat("Several ", type, " coexists on the same GameObject and no reference is given to find the expected ", type));
			}
			DataMakerProxyBase[] array2 = components;
			foreach (DataMakerProxyBase dataMakerProxyBase2 in array2)
			{
				if (dataMakerProxyBase2.referenceName == nameReference)
				{
					return dataMakerProxyBase2;
				}
			}
			if (nameReference != string.Empty)
			{
				if (!silent)
				{
					Debug.LogError(string.Concat(type, " not found for reference <", nameReference, ">"));
				}
				return null;
			}
		}
		else if (components.Length > 0)
		{
			if (nameReference != string.Empty && nameReference != components[0].referenceName)
			{
				if (!silent)
				{
					Debug.LogError(string.Concat(type, " reference do not match"));
				}
				return null;
			}
			return components[0];
		}
		if (!silent)
		{
			Debug.LogError(string.Concat(type, " not found"));
		}
		return null;
	}
}
