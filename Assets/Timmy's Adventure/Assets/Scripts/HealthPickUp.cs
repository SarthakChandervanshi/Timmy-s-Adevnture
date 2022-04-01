using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {

	private float healthAmount = 50;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag == "Player"){
			PlayerHealth theHealth  = coll.gameObject.GetComponent<PlayerHealth>();
			if (healthAmount * PlayerPrefs.GetInt("levelAt") <= 200)
				theHealth.addHealth(healthAmount * PlayerPrefs.GetInt("levelAt"));
			else
				theHealth.addHealth(250);
			Destroy(gameObject);
		}
	}
}
