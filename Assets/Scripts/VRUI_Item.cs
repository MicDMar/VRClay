using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class VRUI_Item : MonoBehaviour
{
	private BoxCollider _collider;
	private RectTransform rectTransform;

	private void OnEnable()
	{
		ValidateCollider();
	}

	// Ensure we have a collider to interact with for the VR LaserPointer
	private void ValidateCollider()
	{
		rectTransform = GetComponent<RectTransform>();

		_collider = GetComponent<BoxCollider>();
		if (_collider == null)
		{
			_collider = gameObject.AddComponent<BoxCollider>();
		}

		_collider.size = rectTransform.sizeDelta;
	}

	// Called when script loaded/value changed in inspector
	private void OnValidate()
	{
		ValidateCollider();
	}
}
