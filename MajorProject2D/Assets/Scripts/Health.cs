using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float health;
    
    void Start() {
        health = PlayerPrefs.GetFloat("health");
    }
}
