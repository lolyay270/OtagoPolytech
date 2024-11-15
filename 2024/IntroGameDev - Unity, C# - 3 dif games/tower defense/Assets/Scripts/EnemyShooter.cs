/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 22nd April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The EnemyShooter class controls:
/// - What enemy the monster shoots at
/// - Rotating the monster to the target
/// - Spawning the bullet
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    #region variables
    private float lastShotTime;
    private List<GameObject> enemiesInRange = new();
    [SerializeField] private GameObject body;
    private GameObject targetEnemy;
    private List<Transform> movePath;
    #endregion

    #region methods
    //utility method to get enemy movement path for FindFirstEnemy
    private void Awake()
    {
        movePath = EnemySpawner.Instance.MovePath;
    }

    //method that runs finds and shoots enemy if cooldown is done
    private void Update()
    {
        if (Time.time - lastShotTime >= GetComponent<MonsterData>().CurrentLevel.ShotCooldown) //shot cooldown is done
        {
            if (enemiesInRange.Count > 0) //if there are enemies able to be shot
            {
                targetEnemy = FindFirstEnemy();
                RotateToEnemy(targetEnemy);
                SFXManager.Instance.PlayBulletFire();
                Shoot(targetEnemy);
            }
        }
    }

    //method to get the first enemy in attack range to set as the target
    private GameObject FindFirstEnemy()
    {
        GameObject firstEnemy = enemiesInRange[0];
        List<float> pathYs = new();
        foreach (Transform t in movePath)
        {
            pathYs.Add(t.position.y);
        }

        foreach (GameObject enemy in enemiesInRange) //finding lowest y value
        {
            if (enemy.transform.position.y < firstEnemy.transform.position.y)
            {
                firstEnemy = enemy;
            }
        }

        //finding if firstEnemy is on a horizontal section of the path
        if (firstEnemy.transform.position.y == movePath[0].position.y) //if at spawn level
        {
            FirstEnemyOnHorizontal(movePath[0].position.y, false, firstEnemy);  //find right most enemy
        }
        else if (firstEnemy.transform.position.y == movePath[1].position.y || firstEnemy.transform.position.y == movePath[2].position.y) //if at middle horizontal
        {
            FirstEnemyOnHorizontal(movePath[1].position.y, true, firstEnemy);  //find left most enemy
        }
        else if (firstEnemy.transform.position.y == movePath[3].position.y) //if at cookie level
        {
            FirstEnemyOnHorizontal(movePath[3].position.y, false, firstEnemy); //find right most enemy
        }
        //if its not on a horizontal section, it must the first already

        return firstEnemy;
    }

    //utility method to find which enemy is furthest forward at a certain height
    private GameObject FirstEnemyOnHorizontal(float y, bool left, GameObject furthestEnemy)
    {
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy.transform.position.y == y) //correct horizontal layer
            {
                if (left && enemy.transform.position.x < furthestEnemy.transform.position.x) //enemies moving left, and the checking enemy is further left
                {
                    furthestEnemy = enemy;
                }
                else if (!left && enemy.transform.position.x > furthestEnemy.transform.position.x) //enemies moving right, and the checking enemy is further right
                {
                    furthestEnemy = enemy;
                }
            }
        }
        return furthestEnemy;
    }

    //method to spawn a bullet that moves to target
    private void Shoot(GameObject target)
    {
        Bullet bullet = Instantiate(GetComponent<MonsterData>().CurrentLevel.Bullet, transform);
        bullet.targetEnemy = target;
        lastShotTime = Time.time; //reset shot cooldown timer to start now
    }

    //mathod to rotate the monster to face the target enemy
    private void RotateToEnemy(GameObject target)
    {
        body.transform.right = transform.position - target.transform.position;
    }

    //utility method to add enemies in shooting range to a list
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    //utility method to remove enemies from the list when they leave shooting range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
    #endregion
}
