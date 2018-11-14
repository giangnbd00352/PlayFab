using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RememberScripts : MonoBehaviour {

	Toggle rememberToggle;
	// Use this for initialization
	void Awake()
	{
		rememberToggle = GameObject.Find("Remember").GetComponent<Toggle>();

		if (rememberToggle.isOn)
		{
			gameObject.GetComponent<InputField>().text = PlayerPrefs.GetString(gameObject.name);
		}
	}
	
	// Update is called once per frame
	public void SaveChange()
	{
		if (rememberToggle.isOn)
		{
			PlayerPrefs.SetString(gameObject.name, transform.Find("Text").GetComponent<Text>().text);
		}
	}
}
