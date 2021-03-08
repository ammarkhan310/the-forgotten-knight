using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the enemy functions
public class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public float speed = 2f;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] public bool isFacingRight;
    [SerializeField] private float pushBackForce;
    public bool movingRight = false;
    public bool isDead = false;
    private float freeze;
    [SerializeField] public float startFreeze;

    public GameObject spawnedCoin;


    private Rigidbody2D rb;
    private Animator controller;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
        isFacingRight = transform.localScale.x < 0;
    }


    // When the enemy takes damage, the freeze value is incremented and proceeds to freeze
    // the enemies movement for a set time.
    // Checks to see if the enemy is out of health and calls the DestroyEnemy coroutine.
    // If the enemy is still alive, it will move left and right from a set minimum X
    // and maximum X value set in the editor.
    void Update() {

        if (freeze <= 0) {
            speed = 2f;
        } else {
            speed = 0f;
            freeze -= Time.deltaTime;
        }

        if (health <= 0) {
            isDead = true;
            StartCoroutine(DestroyEnemy());
        }

        if (!isDead) {
            if (movingRight) {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (isFacingRight) {
                    TurnAround();
                }
            }

            if (rb.position.x >= maxX) {
                movingRight = false;
            }

            if (!movingRight) {
                transform.Translate(-Vector2.right * speed * Time.deltaTime);
                if (!isFacingRight) {
                    TurnAround();
                }
            }
            if (rb.position.x <= minX){
                movingRight = true;
            }
        }
    }

    // A couritine that starts the dying animation and the explode animation
    // Spawns in a coin right before the enemy is destroyed.
    IEnumerator DestroyEnemy(){
        controller.SetTrigger("Dying");
        yield return new WaitForSeconds(2);
        controller.SetTrigger("Explode");
        FindObjectOfType<AudioManager>().Play("EnemyExplodeSound");
        yield return new WaitForSeconds(1);
        Instantiate(spawnedCoin, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Referenced in the AttackManager, this function adds the damage from the player
    // to the enemy.
    // As the enemy is hit, a force is added to push it back a certain amount on top
    // of the freeze effect.
    public void TakeDamage(int damage) {
        FindObjectOfType<AudioManager>().Play("EnemyDamageSound");
        freeze = startFreeze;
        health -= damage;
        if (!(GameObject.Find("Player").GetComponent<PlayerMovement>().facingRight)) {
            rb.AddForce(Vector3.left * pushBackForce);
        } else {
            rb.AddForce(Vector3.right * pushBackForce);
        }
    }

    // Flips the sprite for the enemy
    public void TurnAround() {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = transform.localScale.x > 0;
    }
}
