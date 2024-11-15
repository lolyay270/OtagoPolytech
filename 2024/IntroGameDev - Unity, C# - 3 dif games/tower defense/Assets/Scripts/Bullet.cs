/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 22nd April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Bullet class controls:
/// - bullet movement
/// - enemy health from the bullet's damage
/// - and when the enemy has no health, despawning it and giving the player the gold reward
/// </summary>

using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region variables
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [HideInInspector] public GameObject targetEnemy; //set by EnemyShooter
    private bool hasHitEnemy;
    #endregion

    #region methods
    private void Update() //what the bullet should do every frame
    {
        if (targetEnemy != null)
        {
            Movement();
        }
        else //if enemy is killed by other bullet
        {
            Destroy(gameObject);
        }
    }

    private void Movement() //how the bullet moves to the target
    {
        Vector2 moveVector = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, moveSpeed * Time.deltaTime);
        transform.position = moveVector;
    }

    private void OnTriggerEnter2D(Collider2D other) //when the bullet hits the enemy
    {
        if (other.CompareTag("Enemy") && !hasHitEnemy) 
        {
            hasHitEnemy = true;
            EnemyHealth enemyHealth = targetEnemy.GetComponentInChildren<EnemyHealth>();
            enemyHealth.currentHealth -= damage;

            if (enemyHealth.currentHealth <= 0) //if enemy's health is none, destroy it and give reward
            {
                GameManager.Instance.Gold += targetEnemy.GetComponent<Enemy>().GoldRewardOnDeath;
                SFXManager.Instance.PlayEnemyKill();
                Destroy(targetEnemy.gameObject);
            }

            Destroy(gameObject);
        }
    }
    #endregion
}
