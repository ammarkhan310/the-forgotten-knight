using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Calls the transition using the circleSlide animation
public class Transition : MonoBehaviour {
    public Animator circleSlide;
    [SerializeField] private float transitionTime = 1f;

    public void NextLevel() {
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LastLevel() {
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex - 1));
    }
    
    public void PlayGame() {
        StartCoroutine(LoadTransition(2));
    }

    public void Controls() {
        StartCoroutine(LoadTransition(1));
    }

    public void BackToMenu() {
       StartCoroutine(LoadTransition(0));
    }
    public void GameOver() {
        StartCoroutine(LoadTransition(4));
    }

    // A couritine that takes in a scene index and also waits for a set amount
    // of time so that the scene transition can play out. Then finally it loads
    // the new scene.
    IEnumerator LoadTransition(int sceneIndex){
        circleSlide.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
