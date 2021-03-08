using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Sets up the messages and sends them one by one to AutoTextWriter
public class AutoText : MonoBehaviour {
   
    private Text messageText;
    [SerializeField] private int i = 1;
    [SerializeField] private string[] messageArray = new string[] {
                "This is a test.",
                "Thank you for reading.",
                " ",
    };

    [SerializeField] private float textSpeed = 0.1f;

    // Initialize the messageText variable to hold the current message that will be outputted
    // Calls AddWriter for the first message so it plays automatically
    // For the rest of the messages an onClick listener is used
    private void Awake() {
        messageText = transform.Find("TextMessage").GetComponent<Text>();
        AutoTextWriter.AddWriter_Static(messageText, messageArray[0], textSpeed);
        transform.GetComponent<Button>().onClick.AddListener(MessageOnClick);
    }

    // The onClick function takes the array of messages that have been initialized in the Editor 
    // and sends them to the AddWriter function one at a time, one the number of messages has
    // ran out it will hide the TextBox game object.
    void MessageOnClick() {
        string message = messageArray[i];
        FindObjectOfType<AudioManager>().Play("ClickSound");
        AutoTextWriter.AddWriter_Static(messageText, message, textSpeed);
        if (i < messageArray.Length - 1) {
            i++;
        } else {
            this.gameObject.SetActive(false);
        }
    }
}
