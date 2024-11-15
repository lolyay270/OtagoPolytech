/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 27th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The UIManager class controls:
/// - score in game UI
/// - lives in game UI
/// - game over message
/// </summary>

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region variables
    [SerializeField] private Text savedCounter;
    [SerializeField] private List<Text> nonChangingText;
    [SerializeField] private List<GameObject> healthIndicators = new();
    [SerializeField] private Text gameOver;

    public static UIManager Instance;
    #endregion

    #region methods
    //utility mnethod to setup instance for other classes to call
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //method to update the lives heart images in UI
    public void UpdateLives()
    {
        int currentLives = GameManager.Instance.MaxNumberOfLives - GameManager.Instance.sheepDropped; //max lives - sheep dropped
        for (int i = 0; i < healthIndicators.Count; i++)
        {
            if (i < currentLives) healthIndicators[i].GetComponent<Image>().enabled = true; //if is less than current lives, enable
            else healthIndicators[i].GetComponent<Image>().enabled = false; //if is equal or above health count, disable
        }
    }

    //method to update score number in UI
    public void UpdateScore()
    {
        savedCounter.text = (GameManager.Instance.sheepSaved).ToString();
    }

    //method to stop all game UI and show "Game Over" with final score
    public void GameOver()
    {
        //hide normal UI
        foreach (Text text in nonChangingText)
        {
            text.enabled = false;
        }
        savedCounter.enabled = false;

        //show "game over" to user
        gameOver.text = $"Game Over \n Your score was {GameManager.Instance.sheepSaved}";
    }
    #endregion
}
