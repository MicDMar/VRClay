using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


// Should be the home for all 'global' settings used for drawing
public class DrawSettings : MonoBehaviour
{
	[System.Serializable]
	public class ColorEvent : UnityEvent<Color> {}

	[System.Serializable]
	public class FloatEvent : UnityEvent<float> {}

	public enum DrawMode
	{
		Continuous,
		Single,
	}
	public static DrawMode CurrentDrawMode { get;  set; }

	public static readonly ColorEvent OnColorChange = new ColorEvent();
	public static bool UseRandomColor = false;
	private static Color color;
	public static int ColorCode = 0;
	public static Color Color
	{
		get
		{
			if (UseRandomColor)
			{
				return Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f);
			}

			switch (ColorCode)
			{
				case 0:
					return color = Color.black;
				case 1:
					return color = Color.red;
				case 2:
					return color = Color.yellow;
				case 3:
					return color = Color.green;
				case 4:
					return color = Color.blue;
				case 5:
					return color = Color.cyan;
				case 6:
					return color = Color.magenta;
				case 7:
					return color = Color.white;
				default:
					return color = Color.gray;
			}
		}
		set
		{
			color = value;
			OnColorChange.Invoke(color);
		}
	}

	public static readonly FloatEvent OnScaleFactorChange = new FloatEvent();
	private static float scaleFactor = 0.05f;
	public static float ScaleFactor
	{
		get { return scaleFactor; }
		set
		{
			if (value > maxScaleFactor)
			{
				scaleFactor = maxScaleFactor;
			}
			else if (value < minScaleFactor)
			{
				scaleFactor = minScaleFactor;
			}
			else
			{
				scaleFactor = value;
			}
			OnScaleFactorChange.Invoke(scaleFactor);
		}
	}

	private static float minScaleFactor = 0.05f;
	private static float maxScaleFactor = 3.0f;

	void Start()
	{
	}
	
	void Update() 
	{
	}
}
