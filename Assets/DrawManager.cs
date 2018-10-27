using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{


	static void DeleteClay()
	{
		var slayer = GameObject.FindGameObjectWithTag("Clay").GetComponent<KillChildren>();
		slayer.KillAllChildren();

	}

	void Start() 
	{
		
	}
	
	void Update() 
	{
		
	}
}
