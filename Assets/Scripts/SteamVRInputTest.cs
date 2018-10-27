using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVRInputTest : MonoBehaviour
{
	private SteamVR_TrackedController controller;
	void Start()
	{
		controller = GetComponent<SteamVR_TrackedController>();
		controller.PadTouched += PadTouched;
		controller.PadClicked += PadPressed;
	}
	
	void Update() 
	{
		
	}

	void PadTouched(object sender, ClickedEventArgs e)
	{
		Debug.Log("Pad Touched");

	}

	void PadPressed(object sender, ClickedEventArgs e)
	{
		Debug.Log("Pad Pressed");
	}
}
