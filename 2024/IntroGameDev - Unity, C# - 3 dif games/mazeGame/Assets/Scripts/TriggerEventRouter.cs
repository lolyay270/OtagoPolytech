/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 10th April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The TriggerEventRouter class controls:
/// - player's collision detection with monster and treasure
/// </summary>

using UnityEngine;
using UnityEngine.Events;

public class TriggerEventRouter : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnPlayerWin = new();
    [HideInInspector] public UnityEvent OnPlayerLose = new();

    private void OnTriggerEnter(Collider other)
    {
        //if player touches treasure, they win
        if (other.CompareTag("Treasure")) 
        {
            OnPlayerWin?.Invoke();
        }

        //if player touches monster, they lose
        if (other.CompareTag("Monster"))
        {
            OnPlayerLose?.Invoke();
        }
    }
}