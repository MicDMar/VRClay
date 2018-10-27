using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;

public class VRDrawSphere : MonoBehaviour
{
	public Transform GhostPosition;
	public GameObject GhostPrefab;
	private Renderer ghostRenderer;

	private GameObject Ghost;

	public SteamVR_TrackedController controller;
	private SteamVR_Controller.Device device;

	private bool triggerPressed;

	public Transform DrawParent = null;

	public bool setColor = true;

	public DrawSettings.DrawMode mode;

	void Start()
	{
		Ghost = Instantiate(GhostPrefab, GhostPosition);
		Ghost.transform.localPosition = Vector3.zero;
		if (setColor)
		{
			ghostRenderer = Ghost.GetComponent<Renderer>();
			if (ghostRenderer)
			{
				ghostRenderer.material.color =	DrawSettings.Color;
			}
		}
	}

	void Place(Vector3 position)
	{
		// Right now just make a new version of ghosts
		var newObject = Instantiate(GhostPrefab);
		newObject.transform.localPosition = Ghost.transform.position;
		newObject.transform.parent = DrawParent;
		newObject.transform.rotation = Ghost.transform.rotation;
		newObject.transform.localScale *= DrawSettings.ScaleFactor;

		if (setColor)
		{
			var renderer = newObject.GetComponent<Renderer>();
			if (renderer)
			{
				renderer.material.color = ghostRenderer.material.color;

				ghostRenderer.material.color = DrawSettings.Color;
			}
		}

	}

	void DrawGhost(Vector3 position)
	{

	}


	void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		triggerPressed = true;
		switch (DrawSettings.CurrentDrawMode)
		{
			case DrawSettings.DrawMode.Continuous:
				StartCoroutine(UpateInput(0.03f));
				break;
			case DrawSettings.DrawMode.Single:
				Place(Ghost.transform.position);
				break;
		}
	}

	void HandleTriggerUnclicked(object sender, ClickedEventArgs e)
	{
		triggerPressed = false;
	}

	IEnumerator UpateInput(float pollStep)
	{
		while (triggerPressed)
		{
			Place(Ghost.transform.position);
			yield return new WaitForSeconds(0.012f);
		}
	}

	private void OnDisable()
	{
		controller.TriggerClicked -= HandleTriggerClicked;
		controller.TriggerUnclicked -= HandleTriggerUnclicked;

		DrawSettings.OnScaleFactorChange.RemoveListener(ResizeGhost);
	}

	private void ResizeGhost(float newScaleFactor)
	{
		if (Ghost != null)
			Ghost.transform.localScale = GhostPrefab.transform.localScale * newScaleFactor;
		Debug.Log("Resize");
	}

	private void OnEnable()
	{
		controller = GetComponent<SteamVR_TrackedController>();
		if (controller == null)
			controller = GetComponentInParent<SteamVR_TrackedController>();

		device = SteamVR_Controller.Input((int)controller.controllerIndex);
		controller.TriggerClicked += HandleTriggerClicked;
		controller.TriggerUnclicked += HandleTriggerUnclicked;

		DrawSettings.OnScaleFactorChange.AddListener(ResizeGhost);

		Validate();
	}

	private void Validate()
	{
		ResizeGhost(DrawSettings.ScaleFactor);
		DrawSettings.CurrentDrawMode = mode;
		//Ghost.transform.sc = GhostPrefab.transform.localScale * DrawSettings.ScaleFactor;
	}

	void Update() 
	{
		Validate();
		//controller.TriggerClicked -= HandleTriggerClicked;
		//controller.TriggerUnclicked -= HandleTriggerUnclicked;

		// Assume whenever this script is enabled, assume it should do all of its jobs

		// Draw a sphere representing what will be created when the trigger is pressed

		// Upon pulling the trigger, place the items

	}
}
