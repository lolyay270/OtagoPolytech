/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 7th June 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Enemy class controls:
/// - monster/enemy movement
/// - monster/enemy sounds
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region variables
    //movement
    private List<Node> movePath = new();
    [SerializeField] private float moveSpeed;
    private Vector3 lastPosition;

    //sound
    [SerializeField] private AudioClip zombieGrowl;
    [SerializeField] private float soundDelaySecs;
    [SerializeField] private float soundDelayRandomness;
    #endregion

    #region methods
    //method to run the script's other methods in a set order, called by GameManager
    public IEnumerator Run()
    {
        while (movePath.Count == 0) //while path doesnt exist
        {
            movePath = Pathfinder.Instance.Path;
            lastPosition = transform.position;
            yield return null;
        }

        StartCoroutine(MakeNoise()); //start coroutine once, after path is made

        while (movePath.Count > 0) //path is made, get moving
        {
            movePath = Pathfinder.Instance.Path; //get updated path

            if (movePath.Count == 1) //monster and player are in the same node
            {
                SmallMovement();
            }
            else //monster is not in players node
            {
                LargeMovement();
            }

            RotateToMovement();
            lastPosition = transform.position; //update current pos for next frame

            yield return null;
        }
    }

    //--------MOVEMENT--------\\
    //method to move the monster towards the next node in path
    private void LargeMovement()
    {
        if (transform.position != ConvertNodeToPos(movePath[movePath.Count - 1])) //if not at last target
        {
            Vector3 moveVector = Vector3.MoveTowards(transform.position, ConvertNodeToPos(movePath[1]), moveSpeed * Time.deltaTime);
            transform.position = moveVector;
        }
    }

    //method to move monster to the player, when in same node
    private void SmallMovement()
    {
        //keeping the clown on the ground by only taking x and z from player
        Vector3 playerPos = GameManager.Instance.Player.transform.position; 
        Vector3 moveVector = Vector3.MoveTowards(transform.position, new Vector3(playerPos.x, transform.position.y, playerPos.z), moveSpeed * Time.deltaTime); 
        transform.position = moveVector;
    }

    //method to rotate the monster to face the direction it is moving
    private void RotateToMovement()
    {
        if (transform.position - lastPosition != Vector3.zero) //stop dumb message telling me its zero
        {
            transform.forward = (transform.position - lastPosition);
        }
    }

    //utility method to convert node coords into maze scaled positions
    private Vector3 ConvertNodeToPos(Node node)
    {
        Vector3 pos = new(node.x, transform.position.y, node.y); //making top-down into 3d
        pos.x *= GameManager.Instance.mazeScale; //multiply to get position coords
        pos.z *= GameManager.Instance.mazeScale;
        return pos;
    }

    //--------SOUND--------\\
    //make noise randomly in a set range
    private IEnumerator MakeNoise()
    {
        while (true)
        {
            float waitRandomness = Random.Range(-soundDelayRandomness, soundDelayRandomness);
            float waitTime = soundDelaySecs + waitRandomness;
            AudioSource.PlayClipAtPoint(zombieGrowl, transform.position);
            yield return new WaitForSeconds(waitTime);
        }
    }
    #endregion
}
