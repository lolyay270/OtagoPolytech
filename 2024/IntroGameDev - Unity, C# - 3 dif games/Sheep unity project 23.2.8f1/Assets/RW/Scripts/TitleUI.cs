/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 16th April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The TitleUI class controls:
/// - the title menu button clicks
/// - game starting from menu
/// - exiting the game
/// </summary>

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    //listening for button clicks
    void Start()
    {
        startButton.onClick.AddListener(HandleStartClicked);
        quitButton.onClick.AddListener(HandleQuitClicked);
    }

    //start game when "start" button pressed
    private void HandleStartClicked()
    {
        SceneManager.LoadScene("Game");
    }

    //stop application and editor app running when "quit" clicked
    private void HandleQuitClicked()
    {
        Application.Quit();
    }
}
