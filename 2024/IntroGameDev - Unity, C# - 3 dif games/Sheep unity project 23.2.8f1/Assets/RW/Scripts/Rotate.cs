/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Rotate class controls:
/// - constant rotation of any object it is attached to
/// </summary>

using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotate = new();

    // rotate object at a constant speed
    void Update()
    {
        //multiply by frame rate to fix stuttering
        float rotateX = rotate.x * Time.deltaTime;
        float rotateY = rotate.y * Time.deltaTime;
        float rotateZ = rotate.z * Time.deltaTime;

        transform.Rotate(rotateX, rotateY, rotateZ);
    }
}
