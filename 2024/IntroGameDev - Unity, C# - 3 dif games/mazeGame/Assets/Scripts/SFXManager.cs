/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 13th June 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The SFXManager class controls:
/// - audio file storage
/// - playing audio files
/// </summary>

using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region variables
    //audio files
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip gameWin;
    [SerializeField] private AudioClip gameLose;

    //other
    public static SFXManager Instance;
    #endregion

    #region methods
    //setup instance, get audio source
    private void Awake()
    {
        if (Instance == null) Instance = this;
        audioS = GetComponent<AudioSource>();
    }

    //play win sound
    public void Win()
    {
        //AudioSource.PlayClipAtPoint(gameWin, position);
        audioS.PlayOneShot(gameWin, audioS.volume);
    }

    //play lose sound
    public void Lose()
    {
        //AudioSource.PlayClipAtPoint(gameLose, position);
        audioS.PlayOneShot(gameLose, audioS.volume);
    }
    #endregion
}
