/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 27th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The UserInterface class controls:
/// - updating UI elements to match stats
/// - show "game over" when 0 health
/// </summary>

using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    #region variables
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private Text goldLabel;
    [SerializeField] private Text waveLabel;
    [SerializeField] private Animator topHalfWaveStartLabel;
    [SerializeField] private Animator bottomHalfWaveStartLabel;
    [SerializeField] private Text healthLabel;
    [SerializeField] private Animator gameOverLabel;
    #endregion

    #region methods
    //utility method to setup listeners and update UI at start of game
    private void Awake()
    {
        //gold
        GameManager.Instance.OnGoldSet.AddListener(HandleGoldSet);
        HandleGoldSet(); 
        
        //wave
        enemySpawner.OnNewWave.AddListener(HandleNewWave);
        HandleNewWave();

        //health
        GameManager.Instance.OnHealthSet.AddListener(HandleHealthSet);
        GameManager.Instance.OnHealthZero.AddListener(HandleHealthZero);
        HandleHealthSet();
    }

    //set the gold in UI
    private void HandleGoldSet()
    {
        goldLabel.text = "GOLD: " + GameManager.Instance.Gold.ToString();
    }

    //set the wave number in UI
    private void HandleNewWave()
    {
        waveLabel.text = "WAVE: " + (enemySpawner.CurrentWaveIndex + 1).ToString();
        topHalfWaveStartLabel.SetTrigger("nextWave");
        bottomHalfWaveStartLabel.SetTrigger("nextWave");
    }

    //set health in UI
    private void HandleHealthSet()
    {
        healthLabel.text = "HEALTH: " + GameManager.Instance.Health.ToString();
    }

    //show "game over" when 0 health
    private void HandleHealthZero()
    {
        gameOverLabel.SetTrigger("gameOver");
    }
    #endregion
}
