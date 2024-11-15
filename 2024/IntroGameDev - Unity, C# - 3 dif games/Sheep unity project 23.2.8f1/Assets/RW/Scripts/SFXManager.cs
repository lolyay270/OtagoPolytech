/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 21st March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The SFXManager class controls:
/// - the audio clip storage
/// - audio clip playing at camera position
/// </summary>

using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region variables
    public static SFXManager Instance;

    [SerializeField] private Transform Camera;

    [SerializeField] private AudioClip ShootSFX;
    [SerializeField] private AudioClip SheepHitSFX;
    [SerializeField] private AudioClip SheepDropSFX;
    #endregion

    #region methods
    //setup instance for other classes to call
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //play haybale shoot sound
    public void Shoot()
    {
        AudioSource.PlayClipAtPoint(ShootSFX, Camera.position);
    }

    //play sheep eats haybale sound
    public void SheepHit()
    {
        AudioSource.PlayClipAtPoint(SheepHitSFX, Camera.position);
    }

    //play sheep falls from edge of map sound
    public void SheepDrop()
    {
        AudioSource.PlayClipAtPoint(SheepDropSFX, Camera.position);
    }
    #endregion
}
