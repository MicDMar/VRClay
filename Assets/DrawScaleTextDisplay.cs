using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class DrawScaleTextDisplay : MonoBehaviour
{
	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		DrawSettings.OnScaleFactorChange.AddListener(delegate(float newScale) { text.text = newScale.ToString("0.00"); });
	}
	
	void Update() 
	{

	}
}
