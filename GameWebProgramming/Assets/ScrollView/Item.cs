using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item: MonoBehaviour {

	public Text txt;	
	void Start () {
	
	}
	public void SetInfo(int n)
	{
		txt.text = n.ToString();
	}		
	void Update () 
	{
	
	}
}
