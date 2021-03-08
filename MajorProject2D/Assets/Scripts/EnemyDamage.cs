using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Holds the values for the player health and takes damage from enemies
public class EnemyDamage : MonoBehaviour {
    [SerializeField] public Slider healthSlider;
    [SerializeField] public float health = 100f;

    private void Awake() {
        PlayerPrefs.SetFloat("health", health);
    }

    // Sets the health to the PlayerPref that's saved in memory
    private void Start() {
        health = PlayerPrefs.GetFloat("health");
        healthSlider.value = health;
    }

    private void Update() {
        PlayerPrefs.SetFloat("health", health);
    }

    // Decrements the healthSlider value and the health value
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            FindObjectOfType<AudioManager>().Play("PlayerDamageSound");
            healthSlider.value -= 10;
            health -= 10f;
        }
    }
}
