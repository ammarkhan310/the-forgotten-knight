using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Writes text letter by letter at a set speed
public class AutoTextWriter : MonoBehaviour {
    
    private Text uiText;
    private string textToWrite;
    private float timePerCharacter;
    private float timer;
    private int characterIndex;

    private static AutoTextWriter instance;

    private void Awake() {
        instance = this;
    }

    public static void AddWriter_Static(Text uiText, string textToWrite, float timePerCharacter) {
        instance.AddWriter(uiText, textToWrite, timePerCharacter);
    }

    private void AddWriter(Text uiText, string textToWrite, float timePerCharacter) {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }

    // Uses a timer to print out letters at a set time increment
    // Progresses through the string using characterIndex
    // Then writes to the textBox one at a time
    // Once the message is complete, set the uiText to null it won't run again and also return
    private void Update() {
        if (uiText != null) {
            timer -= Time.deltaTime;
            while (timer <= 0f) {
                //display next character
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);
                FindObjectOfType<AudioManager>().Play("TextSound");

                if(characterIndex >= textToWrite.Length) {
                    uiText = null;
                    return;
                }
            }
        }
    }

}
