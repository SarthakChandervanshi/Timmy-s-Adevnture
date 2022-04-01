using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinscript : MonoBehaviour
{
    public AudioClip coin_picked;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coin_picked, transform.position);
            GUImanager.coins += 1;
            PlayerPrefs.SetInt("Coins", GUImanager.coins);
        }
    }
}
