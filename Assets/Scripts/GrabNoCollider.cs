using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrabNoCollider : MonoBehaviour
{
	private SteamVR_TrackedController controller;

	public GameObject MoveTarget;

	void Start()
	{
		if (controller == null)
			controller = GetComponent<SteamVR_TrackedController>();
		if (controller == null)
			controller = GetComponentInParent<SteamVR_TrackedController>();


		controller.Gripped += delegate(object sender, ClickedEventArgs args)
		{
			if (MoveTarget.transform.parent == null)
				MoveTarget.transform.parent = controller.transform;
		};
		controller.Ungripped += delegate(object sender, ClickedEventArgs args)
		{
			if (MoveTarget.transform.parent == controller.transform)
				MoveTarget.transform.parent = null;
		};
	}

	void Update()
	{
		
	}
}
