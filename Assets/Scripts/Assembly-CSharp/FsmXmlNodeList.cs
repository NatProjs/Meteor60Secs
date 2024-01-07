using System.Xml;
using UnityEngine;

public class FsmXmlNodeList : Object
{
	private XmlNodeList _xmlNodeList;

	public XmlNodeList Value
	{
		get
		{
			return _xmlNodeList;
		}
		set
		{
			Debug.Log(DataMakerXmlUtils.XmlNodeListToString(value));
			_xmlNodeList = value;
		}
	}

	public override string ToString()
	{
		return "FsmXmlNodeList";
	}
}
