using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Valve.VR;

public class FlipDashboard : MonoBehaviour
{
	private HeadGesture gesture;
	private GameObject dashboard;

	public Transform AttachPoint;
	public GameObject dashboardPrefab;
	public bool HologramStyle = true;

	public bool isOpen { get; private set; }
	private Vector3 startRotation;

	public Vector3 StartingGrowScale = Vector3.zero;
	public Vector3 EndingGrowScale;
	public float GrowTime = 0.5f;

	public bool manageMultiplexer = true;
	public ScriptMultiplexer Multiplexer;
	public int MultiplexerSetting = 0;

	void Start()
	{
		isOpen = true;
		gesture = GetComponent<HeadGesture>();
		dashboard = GameObject.Instantiate(dashboardPrefab, AttachPoint);

		UIDashBoard dashboardOptions = dashboard.GetComponent<UIDashBoard>();
		dashboardOptions.InputMultiplexer = Multiplexer;

		startRotation = dashboard.transform.localEulerAngles;
		EndingGrowScale = dashboard.transform.localScale;

		CloseDashBoard();
	}

	void Update()
	{
		if (gesture.isFacingDown && (Vector3.Angle(Vector3.left, Camera.main.transform.rotation * Vector3.forward) <= Vector3.Angle(Vector3.left, AttachPoint.position)))
		{
			OpenDashBoard();
			// is open
			// Align to face

		}
		else
		{
			CloseDashBoard();
		}

	}

	private void CloseDashBoard()
	{
		if (isOpen)
		{
			dashboard.SetActive(false);
			isOpen = false;
			dashboard.transform.localScale = StartingGrowScale;

			if (Multiplexer.Selected == MultiplexerSetting)
				Multiplexer.Revert();
		}
	}

	private void OpenDashBoard()
	{
		if (!isOpen)
		{
			dashboard.SetActive(true);
			isOpen = true;
			StartCoroutine(growDashBoard());

			Multiplexer.Select(MultiplexerSetting);
		}

		if (HologramStyle)
		{
			dashboard.transform.forward = (dashboard.transform.position - Camera.main.transform.position).normalized;
		}
		else
		{
			// Update position and rotation accordingly for normal mode
		}
	}

	private IEnumerator growDashBoard()
	{
		Vector3 originalScale = StartingGrowScale;

		float currentTime = 0.0f;

		do
		{
			dashboard.transform.localScale = Vector3.Lerp(originalScale, EndingGrowScale, currentTime / GrowTime);
			currentTime += Time.deltaTime;
			yield return null;

		} while (currentTime < GrowTime);

	}
}
