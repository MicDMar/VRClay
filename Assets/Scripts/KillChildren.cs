using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KillChildren : MonoBehaviour
{

	public Transform House;
	public void KillAllChildren()
	{
		foreach (GameObject child in House)
		{
			Destroy(child);
		}
	}

	public void KillAllChildren(String parentName)
	{
		var parent = GameObject.FindGameObjectWithTag(parentName);
		foreach (Transform child in parent.transform)
		{
			Destroy(child.gameObject);
		}
	}

	void Start()
	{
	}
	
	void Update() 
	{
		
	}
}
