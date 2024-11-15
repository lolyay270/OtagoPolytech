/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 27th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The GameManager class controls:
/// - The score counter
/// - the lives counter
/// - telling the game UI to update
/// </summary>

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region variables/instances
    public static GameManager Instance;
    public SheepManager sheepSpawner;
    
    [HideInInspector] public int sheepSaved;
    [HideInInspector] public int sheepDropped;

    [SerializeField] private int numberOfLives;
    [HideInInspector] public int MaxNumberOfLives { get { return numberOfLives; } }

    [SerializeField] private float endGameDelaySecs;
    #endregion

    #region methods
    //setup instance for other classes to use
    private void Awake()
    {
        if (Instance == null) Instance = this;

        Application.targetFrameRate = 60;
    }

    //add to score counter, update UI accordingly
    public void SaveSheep()
    {
        sheepSaved++;
        UIManager.Instance.UpdateScore();
    }

    //add to sheep death counter, update UI accordingly
    public void DroppedSheep()
    {
        sheepDropped++;
        UIManager.Instance.UpdateLives();
        if (sheepDropped >= MaxNumberOfLives)
        {
            StartCoroutine(GameOver());
        }
    }

    //stop game, show "game over" then reset
    private IEnumerator GameOver()
    {
        //stop spawning, destroy all sheep
        sheepSpawner.isEnabled = false;
        foreach (Sheep sheep in sheepSpawner.SheepList)
        {
            Destroy(sheep.gameObject);
        }

        UIManager.Instance.GameOver(); //show players they lost

        yield return new WaitForSeconds(endGameDelaySecs); //wait some time before returning player to menu
        SceneManager.LoadScene("Title");
    }
    #endregion
}
