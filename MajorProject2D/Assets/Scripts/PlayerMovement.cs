using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float slideForce = 500f;
    [SerializeField] public bool facingRight = true;
    [SerializeField] public float maxSpeed = 100f;

    [SerializeField] private Transform footPosition;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float closeEnough = 0.1f;
    [SerializeField] Transition transition;
    private bool isDead = false;
    private bool isMenuOpen;

    private Rigidbody2D rb;
    private Animator controller;

    // Controls the players movement
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
    }

    // If the players health is at 0, call the GameOver coroutine
    // Also set isDead to true and use that as a check to make sure the player can't move
    // during the few seconds before the Game Over scene transitions.
    // Using a raycast to get the foot position and set isGrounded to check if the player
    // can jump or not. Use that same raycast to check if there's an angle present, so
    // basically just checking if the player is standing on a slope, and using that to 
    // start the rolling animation. The slide action adds a forward force to the player
    // (or backward) depending on which way the player is facing.
    private void Update() {
        if (gameObject.GetComponent<EnemyDamage>().health <= 0) {
            isDead = true;
            StartCoroutine(GameOver());
        }

        RaycastHit2D hit = Physics2D.Raycast(footPosition.position, Vector2.down, closeEnough, groundLayers);

        float angle = ((Vector3.Angle( hit.normal, transform.right)));
        if (angle < 55 && angle > 40) {
            controller.SetBool("Slope", true);
        } else if (angle > 55) {
            controller.SetBool("Slope", false);
        }
        
        if (hit.collider) {
            isGrounded = true;
            controller.SetBool("Grounded", true);
        } else {
            isGrounded = false;
            controller.SetBool("Grounded", false);
        }

        if(!isGrounded) {
            controller.SetTrigger("Landed");
        }

        if(!isDead){
            if(!isMenuOpen) {
                if (Input.GetButtonDown("Jump") && isGrounded) {
                    rb.AddForce(Vector3.up * jumpForce);
                    controller.SetTrigger("Jump");
                }

                if (Input.GetButtonDown("Slide") && isGrounded) {
                    if (!facingRight) {
                        rb.AddForce(Vector3.left * slideForce);
                    } else {
                        rb.AddForce(Vector3.right * slideForce);
                    }
                    controller.SetTrigger("Slide");
                }
            }
        }

        isMenuOpen = gameObject.GetComponent<OpenMenu>().menuOpen;
    }

    // Called the movement in FixedUpdate since it seemed to run smoother this way.
    // Set a transform to flip the player sprite depending on which way they are facing.
    private void FixedUpdate() {
        if(!isDead) {
            if(!isMenuOpen) {
                float movementX = Input.GetAxis("Horizontal") * movementSpeed;
                Vector3 movement = Vector3.right * movementX * Time.deltaTime;
                transform.Translate(movement);

                controller.SetFloat("Speed", Mathf.Abs(movementX));

                if(Input.GetAxis("Horizontal") != 0){
                    if (movementX < 0f) {
                        facingRight = false;
                    } else {
                        facingRight = true;
                    }
                }

                if (!facingRight) {
                    transform.localScale = new Vector3(-6f, 6f, 6f);
                } else {
                    transform.localScale = new Vector3(6f, 6f, 6f);
                }

                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
        }
    }

    // A couritine to play the player dying animation and call the game over scene.
    IEnumerator GameOver(){
        controller.SetTrigger("Dying");
        yield return new WaitForSeconds(3);
        transition.GameOver();
    }
}
