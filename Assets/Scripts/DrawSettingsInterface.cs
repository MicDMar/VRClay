using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

// Used to allow UnityEvents to reference our DrawSettings, which is static
// Unfortunately this requires this to use setter methods
public class DrawSettingsInterface : MonoBehaviour
{

	public static float multiplier;
	public GameObject indicator;

	public void setRandom(bool newValue)
	{
		DrawSettings.UseRandomColor = newValue;
	}

	public void toggleRandom()
	{
		DrawSettings.UseRandomColor = (!DrawSettings.UseRandomColor);
	}

	public void ChangeColor(int newValue)
	{
			DrawSettings.ColorCode = newValue;
	}

	public void IncreaseScaleFactor(float amount = 0.1f)
	{
		DrawSettings.ScaleFactor += amount;
	}

	public void DecreaseScaleFactor(float amount = 0.1f)
	{
		DrawSettings.ScaleFactor -= amount;
	}

	public void SetScaleFactor(float newScaleFactor = 1.0f)
	{
		DrawSettings.ScaleFactor = newScaleFactor;
	}

	void Start()
	{
	}
	
	void Update() 
	{
	}

}
