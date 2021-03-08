using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Calls the next scene depending on where the player is standing
public class CallScene : MonoBehaviour {
    [SerializeField] Transition transition;
    void Start() {

    }

    // Checks to see if the player is in Scene 2 (The first area) or 
    // Scene 3 (The cave) and calls the correct transition function accordingly.
    void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            if (this.gameObject.transform.position.x > 103f) {
                transition.NextLevel();
            }
        }
        if(SceneManager.GetActiveScene().buildIndex == 3) {
            if (this.gameObject.transform.position.x < 5f) {
                transition.LastLevel();
            }
        }
    }
}
