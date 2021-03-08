using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the attack of the player
public class AttackManager : MonoBehaviour {

    public static AttackManager instance;
    [SerializeField] public bool canReceiveInput;
    [SerializeField] public bool inputReceived;

    [SerializeField] public Transform attackPos;
    [SerializeField] public float attackRange;
    [SerializeField] public int damage;

    [SerializeField] public LayerMask whatisEnemies;

    private void Awake() {
        instance = this;
    }

    void Update() {
        Attack();
    }

    // Checks if the input for attack is registered and then checks if the system is ready for an attack input.
    // Three attacks are chained together in the animation using behaviour scripts.
    // Gets an array of enemies using their tag, and then calls the TakeDamage function in the enemy script.
    public void Attack () {
        if (Input.GetButtonDown("Fire1")) {
            if (canReceiveInput) {
                inputReceived = true;
                canReceiveInput = false;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++) {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            } else {
                return;
            }
        }
    }

    // Used to visualize the hitbox radius of the sword attack and setup attackPos.
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    // Swaps between being able to take an input and not.
    public void InputManager() {
        if (!canReceiveInput) {
            canReceiveInput = true;
        } else {
            canReceiveInput = false;
        }
    }
}
