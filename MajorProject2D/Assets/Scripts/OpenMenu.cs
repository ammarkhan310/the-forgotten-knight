using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Opens the menu
public class OpenMenu : MonoBehaviour {
    public GameObject MenuCanvas;
    public bool menuOpen;

    public void OpenPanel() {
        if(MenuCanvas != null) {
            bool isActive = MenuCanvas.activeSelf;
            if(isActive) {
                FindObjectOfType<AudioManager>().Play("MenuOpenSound");
            } else if (!isActive) {
                FindObjectOfType<AudioManager>().Play("MenuCloseSound");
            }
            MenuCanvas.SetActive(!isActive);
            menuOpen = !isActive;
            Debug.Log(menuOpen);
        }
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            OpenPanel();
        }
    }
}
