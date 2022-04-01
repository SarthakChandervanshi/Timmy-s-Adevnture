using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour {
	public float enemyhealth;
	// public GameObject enemyDeathFX;
	public Slider enemyHealthBar;
	private float currhealth;
	public bool drops;
	public GameObject thedrop;
	public AudioClip deathKnell;
	//private int score;
	//public TextMeshProUGUI tmPro_score;

	// Use this for initialization
	void Start () {
		//GUImanager.enemyIsDead = false;
		//GUImanager.enemyBatIsDead = false;
		currhealth = enemyhealth;
		enemyHealthBar.maxValue = enemyhealth;
		enemyHealthBar.value = currhealth;
		/*if (PlayerPrefs.GetInt("Score") == null)
		{
			score = 0;
		}
		else
		{
			score = PlayerPrefs.GetInt("Score");
		}*/
		//Debug.Log("Initial SCore is 100");
		//score = 0;

	}

    // Update is called once per frame
    void Update()
    {
		enemyHealthBar.value = currhealth;
		//tmPro_score.text = "SCORE:" + score.ToString();	
		//Debug.Log("Update");
		
		//DiDor(200);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		/*if (coll.gameObject.name == "fireball(Clone)")
		{
			DiDor(200f);
			Debug.Log("200 Damage done");
		}*/

		if((coll.gameObject.tag == "Fireball") && (gameObject.tag == "EnemyBat"))
        {
			DiDor(100f);
        }
		else if(coll.gameObject.tag == "Fireball")
		{
			DiDor(200f);
			//score += 100;
			//Debug.Log("200 Damage done");
		}

		//Debug.Log("Score updated to +50");
		//score += 50;
	}
	public void DiDor(float damage){
		currhealth -= damage;
		//Debug.Log(currhealth);
		//enemyHealthBar.value = currhealth;
		if(currhealth==0){
			//score += 100;
       
			//Debug.Log("Cuurent health zero");
			//Debug.Log("Score updated to +100");
			if(damage == 200f)
            {
				GUImanager.score += 100;
				//GUImanager.enemyIsDead = true;
				//GUImanager.enemyIsDead = false;
				//Debug.Log("Helo");
			}
			else if(damage == 100f)
            {
				GUImanager.score += 25;
				//GUImanager.enemyBatIsDead = true;
				//GUImanager.enemyBatIsDead = false;
				//Debug.Log("100 damage");
			}
			PlayerPrefs.SetInt("Score", GUImanager.score);
			Invoke("MakeDead", 0.5f);	
		}

		//score += 20;
	}
	public void MakeDead(){
		AudioSource.PlayClipAtPoint(deathKnell, transform.position);
		Destroy(gameObject);
		//Debug.Log("MakeDead");
		// Instantiate (enemyDeathFX,transform.position, transform.rotation);
		if (drops)
			Instantiate(thedrop, transform.position, transform.rotation);
		//Debug.Log("Score updated to +500");
		//score += 500;
	}
}
