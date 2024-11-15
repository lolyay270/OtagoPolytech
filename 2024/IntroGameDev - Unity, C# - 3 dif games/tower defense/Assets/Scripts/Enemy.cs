/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 27th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Enemy class controls:
/// - Enemy movement along the path
/// - Enemies rotating to the direction they're facing
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region variables
    private int currentTargetIndex;
    [HideInInspector] public List<Transform> movePath; //set by EnemySpawner
    [SerializeField] private float moveSpeed;
    private Vector3 lastPosition;
    [SerializeField] private GameObject body;
    [SerializeField] private int goldRewardOnDeath;
    public int GoldRewardOnDeath { get { return goldRewardOnDeath; } } //needed by Bullet
    #endregion

    #region methods
    private void Awake() //get the first position
    {
        lastPosition = transform.position;
    }

    private void Update() //move and rotate each frame
    {
        Movement();
        RotateIntoMoveDirection();
        lastPosition = transform.position;
    }

    private void Movement() //move the enemy at set speed along path, destroy at last location
    {
        if (transform.position != movePath[movePath.Count -1].position) //if not at last target
        {
            if (Vector2.Distance(transform.position, movePath[currentTargetIndex].position) == 0) //if moved onto to target
            {
                currentTargetIndex++; //changing target to next in list
            }
            else //if not at target, move to target
            {
                Vector2 moveVector = Vector2.MoveTowards(transform.position, movePath[currentTargetIndex].position, moveSpeed * Time.deltaTime);
                transform.position = moveVector;
            }
        }
        else //is at last target (cookie)
        {
            Destroy(gameObject);
        }
    }

    private void RotateIntoMoveDirection() //make enemy face where theyre going
    {
        Vector2 newDirection = (transform.position - lastPosition);
        body.transform.right = newDirection;
    }
    #endregion
}