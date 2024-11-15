/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 17th June 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The SFXManager class controls:
/// - audio clip storage
/// - audio clip playing
/// </summary>

using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region variables
    [SerializeField] private AudioClip towerPlace;
    [SerializeField] private AudioClip bulletFire;
    [SerializeField] private AudioClip enemyKill;
    [SerializeField] private AudioClip lifeLost;

    [SerializeField] private AudioSource audioS;
    public static SFXManager Instance;
    #endregion

    #region methods
    //method to setup instance for other classes
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //-----play audio clip at set volume methods-----\\
    public void PlayTowerPlace()
    {
        audioS.PlayOneShot(towerPlace, audioS.volume);
    }

    public void PlayBulletFire()
    {
        audioS.PlayOneShot(bulletFire, audioS.volume);
    }

    public void PlayEnemyKill()
    {
        audioS.PlayOneShot(enemyKill, audioS.volume);
    }

    public void PlayLifeLost()
    {
        audioS.PlayOneShot(lifeLost, audioS.volume);
    }
    #endregion
}
