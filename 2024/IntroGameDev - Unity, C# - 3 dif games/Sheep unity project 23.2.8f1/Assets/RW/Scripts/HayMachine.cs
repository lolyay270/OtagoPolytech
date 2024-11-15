/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The HayMachine class controls:
/// - movement with player input inside bounds
/// - spawning hay with player input and delay
/// </summary>

using UnityEngine;

public class HayMachine : MonoBehaviour
{
    #region local variables
    [SerializeField] private float speed;
    private float xMove;
    [SerializeField] private GameObject spawingObject;
    private float timeSinceLastSpawn;
    [SerializeField] private float spawnDelay;
    [SerializeField] private Transform haySpawnLocation;
    #endregion

    #region methods
    //the methods that need to run every frame
    private void Update()
    {
        Movement();
        SpawnHay();
    }

    //moving the haymachine with player input within bounds
    private void Movement()
    {
        //update location
        xMove = Input.GetAxis("Horizontal");

        //movement a,d for left,right     bounds are +-22 
        if (xMove > 0 && transform.position.x < 22)
        {
            MoveXAxis();
        }
        if (xMove < 0 && transform.position.x > -22)
        {
            MoveXAxis();
        }
    }

    //utility method to move machine
    private void MoveXAxis()
    {
        transform.Translate(speed * xMove * Time.deltaTime, 0, 0);
    }

    //method to spawn hay on player input with delay
    private void SpawnHay()
    {
        if (timeSinceLastSpawn < spawnDelay) //only listen for spacebar after spawn delay time
        {
            timeSinceLastSpawn += Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space)) //spawn haybale inside hay machine on space press 
            {
                timeSinceLastSpawn = 0;
                Instantiate(spawingObject, haySpawnLocation.position, haySpawnLocation.rotation);
                SFXManager.Instance.Shoot();
            }
        }
    }
    #endregion
}
