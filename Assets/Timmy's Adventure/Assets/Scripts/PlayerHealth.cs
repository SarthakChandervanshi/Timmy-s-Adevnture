using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	private float fullHealth = 500;
	//public GameObject deathFX;
	float currentHealth;
	public AudioClip playerHurt;
	AudioSource playerAS;
	// public GameObject gameOverScreen;
    // public GameManager theGameManager;

	PlayerController playerControl;

	//Player Heart Bar
	public Slider heartBar;
	public Image damageScreen;
	bool damaged = false;
	Color damagedColour = new Color(5f,5f,0f,0.5f);
	float smoothColour = 5f;
	private Animator anim;

	// Use this for initialization
	void Start () {

		if(SceneManager.GetActiveScene().name == "level1")
        {
			currentHealth = fullHealth;
			PlayerPrefs.SetFloat("Health", currentHealth);
        }
		else if(SceneManager.GetActiveScene().name == "level4" || SceneManager.GetActiveScene().name == "level5" || SceneManager.GetActiveScene().name == "level6")
        {
			fullHealth = 650;
			currentHealth = PlayerPrefs.GetFloat("Health");
		}
		else
        {
			currentHealth = PlayerPrefs.GetFloat("Health");
        }
        
		//currentHealth = fullHealth;
       
		playerControl = GetComponent<PlayerController>();

		//Heart Bar
		heartBar.maxValue=fullHealth;
		Debug.Log(fullHealth);
		Debug.Log(currentHealth);
		heartBar.value=currentHealth;

		playerAS =GetComponent<AudioSource>();

		damaged = false;

		anim = GetComponent<Animator>();
		anim.SetBool("isDead", false);
	}

	// Update is called once per frame
	void Update () {
		if(damaged){
			damageScreen.color = damagedColour;
			addDamage(0f);
		}else{
			damageScreen.color = Color.Lerp(damageScreen.color, Color.clear,smoothColour*Time.deltaTime);
		}
		damaged = false;

	}

	public void addDamage(float damage){
		//if(damage<=0) return;
		Debug.Log(currentHealth);
		Debug.Log(damage);
		currentHealth = currentHealth - damage;
		PlayerPrefs.SetFloat("Health", currentHealth);
		//playerAS.clip =  playerHurt;
		//playerAS.Play(1);
		playerAS.PlayOneShot(playerHurt);
		heartBar.value = currentHealth;
		damaged = true;

		if(currentHealth<=0){
			anim.SetBool("isDead", true);
			StartCoroutine(Delay());
			makeDead();
		}
	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds(5);
	}
	public void addHealth(float health){
		currentHealth += health;
		if(currentHealth>fullHealth) currentHealth=fullHealth;
		PlayerPrefs.SetFloat("Health", currentHealth);
		heartBar.value =currentHealth;
	}
	public void makeDead(){
		//Instantiate(deathFX, transform.position, transform.rotation);

		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		PlayerPrefs.SetInt("levelAt", 2);
		PlayerPrefs.SetInt("Score", 0);
		SceneManager.LoadScene("GameOver");
		
	}
}
