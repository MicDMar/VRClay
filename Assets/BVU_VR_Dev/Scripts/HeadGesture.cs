using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadGesture : MonoBehaviour
{
	public float HeadAngle = 60.0f;

	public bool isFacingDown { get; private set; }

	void Start()
	{
		isFacingDown = false;
	}
	
	void Update()
	{
		isFacingDown = DetectFacingDown();
	}

	private bool DetectFacingDown()
	{
		return (CameraAngleFromGround() < HeadAngle);
	}

	private float CameraAngleFromGround()
	{
		return Vector3.Angle(Vector3.down, Camera.main.transform.rotation * Vector3.forward);
	}
}
