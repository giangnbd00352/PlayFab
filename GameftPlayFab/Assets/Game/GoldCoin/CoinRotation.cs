using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour {
    [SerializeField]
    float speed=300f;
	[SerializeField]
    UIGameScripts uIGameScripts;
	void Update () {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}

    void OnTriggerStay(Collider other)
    {
        uIGameScripts.Coins += 1;        
    }
    
}
