using System.Collections;
using System.Collections.Generic;
using NewtonVR;
using UnityEngine;

public class DirectionalTeleporter : MonoBehaviour
{

	public NVRHand Hand;
	private NVRButtons stick = NVRButtons.Stick;


	void Start()
	{
		Hand = GetComponent<NVRHand>();
	}
	
	void Update()
	{
		var input = Hand.Inputs[stick];

		var pressed = "Pressed: " + input.IsPressed.ToString();
		var touched = "Touched: " + input.IsTouched.ToString();
		var nearTouched = "NTouched: " + input.IsNearTouched.ToString();
		var sAxis = "SingleAxis: " + input.SingleAxis;

		Debug.Log(input.Axis);
		Debug.Log(pressed);
		Debug.Log(touched);
		Debug.Log(nearTouched);
		Debug.Log(sAxis);
	}
}
