using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Picks up and counts the number of gold the player has
public class CoinCollector : MonoBehaviour {

    [SerializeField] private float goldCounter = 0;
    public TMPro.TextMeshProUGUI GoldValue;

    // Gets the value of gold from memory
    void Start() {
        goldCounter = PlayerPrefs.GetInt("gold");
        GoldValue.text = goldCounter.ToString();
    }

    void Update() {
        GoldValue.text = PlayerPrefs.GetInt("gold").ToString();
    }

    // When the player collides with the coin collider, check
    // which coin it is and set the goldVal accordingly
    // Then save that value to memory and destroy the coin
    void OnTriggerEnter2D(Collider2D col) {
        int goldVal = 0;
        if (col.gameObject.CompareTag("Coin_Silver")) {
            FindObjectOfType<AudioManager>().Play("CoinSound");
            goldVal = PlayerPrefs.GetInt("gold") + 10;
        }
        if (col.gameObject.CompareTag("Coin_Gold")) {
            FindObjectOfType<AudioManager>().Play("CoinSound");
            goldVal = PlayerPrefs.GetInt("gold") + 50;
        }
        PlayerPrefs.SetInt("gold", goldVal);
        Destroy(col.gameObject);
    }
}
