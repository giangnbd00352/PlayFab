using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypePlayer { orange, vert, rouge};
public class CarController : MonoBehaviour {

	[SerializeField] float normalSpeed = 4f, currentSpeed;
	[SerializeField] bool canJump = false;
	[SerializeField] Texture orangeTex, vertTex, rougeTex;
	PlayManager playManager;

	public TypePlayer myTypePlayer;
	// Use this for initialization
	void Awake()
	{

		playManager = GameObject.Find("PlayFabManager").GetComponent<PlayManager>();

		//play Manager

		if (playManager.play_Car == "rouge")
		{
			myTypePlayer = TypePlayer.rouge;
		} else if (playManager.play_Car == "vert")
		{
			myTypePlayer = TypePlayer.vert;
		} else 
		{
			myTypePlayer = TypePlayer.orange;
		}

		switch (myTypePlayer)
		{
			case TypePlayer.orange:
				currentSpeed = normalSpeed;
				canJump = false;
				GetComponent<MeshRenderer>().material.mainTexture = orangeTex;
				break;
			case TypePlayer.vert:
				currentSpeed = normalSpeed * 2;
				canJump = false;
				GetComponent<MeshRenderer>().material.mainTexture = vertTex;
				break;
			case TypePlayer.rouge:
				currentSpeed = normalSpeed * 2;
				canJump = true;
				GetComponent<MeshRenderer>().material.mainTexture = rougeTex;
				break;	

		}
	}

	void Update() {
		transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * Input.GetAxis("Vertical"));
		transform.Rotate(Vector3.up * Time.deltaTime * normalSpeed * 30 * Input.GetAxis("Horizontal"));
	}

	void FixedUpdate()
	{
		if (canJump && Input.GetKeyDown(KeyCode.Space))
			GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
	}
}
