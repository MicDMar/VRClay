using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.WSA.WebCam;

public class ButtonPress : MonoBehaviour
{
	private Material onMaterial;
	private Material offMaterial;

	public UnityEvent onButtonPress;
	public UnityEvent onButtonRelease;

	private MeshRenderer renderer;

	private AudioSource _audioSource;

	private bool pressed = false;

	void Start()
	{
		renderer = GetComponent<MeshRenderer>();

		foreach (var material in renderer.materials)
		{
			if (material.name.Contains("button-off"))
			{
				offMaterial = material;
			}
			if (material.name.Contains("button-on"))
			{
				onMaterial = material;
			}
		}

		recolorOff();
		onButtonPress.AddListener(recolorOn);
		onButtonRelease.AddListener(recolorOff);

		_audioSource = GetComponent<AudioSource>();
	}

	void recolorOff()
	{
		renderer.material = offMaterial;
	}

	void recolorOn()
	{
		renderer.material = onMaterial;
	}

	void press()
	{
		onButtonPress.Invoke();
	}

	void release()
	{
		onButtonRelease.Invoke();
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("TriggerExit");
		if (_audioSource)
		{
			_audioSource.Play();
		}
		else
		{
			Debug.Log("No Audio source on button");
		}

		if (!pressed)
		{
			onButtonPress.Invoke();
		}

	}

	void OnTriggerExit(Collider other)
	{
		Debug.Log("TriggerExit");
		onButtonRelease.Invoke();
		pressed = false;
	}

	void Update() 
	{

	}
}
