using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextBoxInstatiator : MonoBehaviour {

    [SerializeField] private GameObject TextBox1;
    [SerializeField] private GameObject TextBox2;
    [SerializeField] private GameObject TextBox3;
    private int[] onlyOnce = {1, 1, 1};
    private int[] onlyOnce2 = {1, 1, 1};

    // Kind of a weird way to do it but I created an onlyOnce array of values,
    // (could've been bools), and used that to make sure the textBoxes were
    // made visible only one time. This was to prevent the player from having
    // to see the textBox everytime they walked by that same x coordinate.
    // Also I could have made invisible gameObjects as anchors on the field and
    // used their position to check if the player was at a certain point but
    // hard checking the x position of the player seemed to work okay.
    // Since the player is in multiple scenes, I check which scene I'm in and
    // then use that to show a certain textBox.
    void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            if (this.gameObject.transform.position.x > -6f && onlyOnce[0] == 1) {
                TextBox1.SetActive(true);
                onlyOnce[0] = 0;
            }
            if (this.gameObject.transform.position.x > 17.4f && onlyOnce[1] == 1) {
                TextBox2.SetActive(true);
                onlyOnce[1] = 0;
            }
            if (this.gameObject.transform.position.x > 67.0f && onlyOnce[2] == 1) {
                TextBox3.SetActive(true);
                onlyOnce[2] = 0;
            }
        }

        if(SceneManager.GetActiveScene().buildIndex == 3) {
            if (this.gameObject.transform.position.x > 11.55f && onlyOnce2[0] == 1) {
                TextBox1.SetActive(true);
                onlyOnce2[0] = 0;
            }
            if (this.gameObject.transform.position.x > 123f && onlyOnce2[1] == 1) {
                TextBox2.SetActive(true);
                onlyOnce2[1] = 0;
            }
            if (this.gameObject.transform.position.x > 150f && onlyOnce2[2] == 1) {
                TextBox3.SetActive(true);
                onlyOnce2[2] = 0;
            }
        }
    }
}
