using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScriptMultiplexer : MonoBehaviour
{
	private int selected = 1;
	private int lastSelected = 1;

	public int Selected
	{
		get { return selected; }
	}

	public List<GameObject> Scripts;
	void Start()
	{
		Select(selected);
	}
	
	void Update() 
	{
		
	}

	public void Select(int scriptNumber)
	{
		if (selected != scriptNumber)
		{
			disableAll();
			lastSelected = selected;
			selected = scriptNumber;
			//Scripts[scriptNumber].enabled = true;
			Scripts[selected].SetActive(true);
		}
	}

	public void Increment(int amount = 1)
	{
		int next = selected + amount;
		while (next >= Scripts.Count)
		{
			next -= Scripts.Count;
		}
	}

	public void Decrement(int amount = 1)
	{
		int next = selected - amount;
		while (next >= Scripts.Count)
		{
			next += Scripts.Count;
		}

	}

	public void Revert()
	{
		Select(lastSelected);
		// Find out why revert not happening properly
		/*
		Debug.Log(String.Format("Reverting selected: %d to lastSelected: %d", selected, lastSelected));
		int temp = selected;
		selected = lastSelected;
		lastSelected = temp;
		Debug.Log(String.Format("Now selected: %d to lastSelected: %d", selected, lastSelected));
		*/
	}

	void disableAll()
	{
		foreach (var script in Scripts)
		{
			// script.enabled = false;
			script.SetActive(false);
		}
	}
}
