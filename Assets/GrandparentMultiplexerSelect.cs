using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrandparentMultiplexerSelect : MonoBehaviour
{
	private UIDashBoard dashboard;
	void Start()
	{
		dashboard = transform.parent.gameObject.GetComponent<UIDashBoard>();
		if (dashboard == null)
		{
			dashboard = transform.parent.parent.gameObject.GetComponent<UIDashBoard>();
		}

		if (dashboard == null)
		{
			throw new Exception("You dun gufed");
		}

	}
	
	void Update() 
	{
		
	}

	public void Select(int option)
	{
		dashboard.InputMultiplexer.Select(option);
	}
}
