/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Movement class controls:
/// - constant movement of any object it is attached to
/// </summary>

using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 move = new();

    //move object at constant speed
    void Update()
    {
        //times each value by delta time to fix frame specific issues
        float Xmove = move.x * Time.deltaTime;
        float Ymove = move.y * Time.deltaTime;
        float Zmove = move.z * Time.deltaTime;

        transform.Translate(Xmove, Ymove, Zmove);
    }
}
