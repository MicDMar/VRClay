using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRUIInput : MonoBehaviour
{

	public SteamVR_LaserPointer laser;
	private SteamVR_TrackedController controller;


	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current),
			                      ExecuteEvents.submitHandler);
			Debug.Log("HandleTriggerClicked", EventSystem.current.currentSelectedGameObject);
		}

	}

	private void HandlePointerIn(object sender, PointerEventArgs e)
	{
		var button = e.target.GetComponent<Button>();
		if (button != null)
		{
			button.Select();
			Debug.Log("HandlePointerIn", e.target.gameObject);
		}
	}

	private void HandlePointerOut(object sender, PointerEventArgs e)
	{
		var button = e.target.GetComponent<Button>();
		if (button != null)
		{
			EventSystem.current.SetSelectedGameObject(null);
			Debug.Log("HandlePointerOut", e.target.gameObject);
		}

	}

	private void OnEnable()
	{
		if (laser == null)
			laser = GetComponent<SteamVR_LaserPointer>();
		laser.PointerIn -= HandlePointerIn;
		laser.PointerIn += HandlePointerIn;
		laser.PointerOut -= HandlePointerOut;
		laser.PointerOut += HandlePointerOut;

		controller = GetComponent<SteamVR_TrackedController>();
		if (controller == null)
			controller = GetComponentInParent<SteamVR_TrackedController>();

		controller.TriggerClicked -= HandleTriggerClicked;
		controller.TriggerClicked += HandleTriggerClicked;
	}

	void Start()
	{
		
	}
	
	void Update() 
	{
		
	}
}
