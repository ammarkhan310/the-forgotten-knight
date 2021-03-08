using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Has the camera follow the transform of the player
public class followCamera : MonoBehaviour
{

    public Transform followTransform;
    
    // Checks for the scene, in the first level scene the camera only follows the x position
    // of the player but in the cave scene it also follows the y position.
    void FixedUpdate() {
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            this.transform.position = new Vector3(followTransform.position.x, -0.06f, -10f);
        }
        if (SceneManager.GetActiveScene().buildIndex == 3) {
            this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, -10f);
        }
    }
}
