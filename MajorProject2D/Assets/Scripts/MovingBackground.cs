using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Creates a parallax effect for the background
public class MovingBackground : MonoBehaviour {
    private float length;
    private float startPosition;
    public GameObject backgroundCam;
    public float parallax;

    void Start() {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // For the first level scene the parallax effect follows the camera but
    // for the cave scene the camera is following the players Y position so 
    // the Y position in the transform here has to be locked otherwise the
    // background will move up and down.
    void FixedUpdate() {
        float temp = (backgroundCam.transform.position.x * (1 - parallax));
        float distance = (backgroundCam.transform.position.x * parallax);

        if (SceneManager.GetActiveScene().buildIndex == 2) {
            transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        } else if (SceneManager.GetActiveScene().buildIndex == 3) {
            transform.position = new Vector3(startPosition + distance, 2.5f, transform.position.z);
        }

        if (temp > startPosition + length) {
            startPosition += length;
        } else if (temp < startPosition - length) {
            startPosition -= length;
        }
    }
}