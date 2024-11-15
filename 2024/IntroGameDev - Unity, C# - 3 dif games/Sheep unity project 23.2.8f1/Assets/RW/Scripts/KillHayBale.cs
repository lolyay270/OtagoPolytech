/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The KillHayBale class controls:
/// - despawning haybale when hitting trigger
/// </summary>

using UnityEngine;

public class KillHayBale : MonoBehaviour
{
    //when a haybale collides with trigger delete that instance
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DestroyHay")
        {
            Destroy(other.gameObject);
        }
    }
}
