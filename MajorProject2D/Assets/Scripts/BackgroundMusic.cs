using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour {

    void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            FindObjectOfType<AudioManager>().Play("MenuTheme");
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            FindObjectOfType<AudioManager>().Play("ControlTheme");
        }
        if (SceneManager.GetActiveScene().buildIndex == 2) {
            FindObjectOfType<AudioManager>().Play("LevelTheme");
        }
        if (SceneManager.GetActiveScene().buildIndex == 3) {
            FindObjectOfType<AudioManager>().Play("CaveTheme");
        }
    }
}
