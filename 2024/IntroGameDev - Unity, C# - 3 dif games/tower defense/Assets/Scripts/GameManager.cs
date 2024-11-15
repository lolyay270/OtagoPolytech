/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 27th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The GameManager class controls:
/// - Health and gold amounts
/// - Health visuals of monsters on cookie
/// </summary>

using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region variables/accessors and object instances
    [SerializeField] private int startingGold;
    private int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            OnGoldSet?.Invoke(); //event to run UI update
        }
    }
    private int health = 5; //health is always 5 max due to number of monsters on cookie
    public int Health
    {
        get { return health; }
        set 
        {
            health = value;  
            OnHealthSet?.Invoke(); //event to run UI update
            if (health == 0) OnHealthZero?.Invoke(); 
        }
    }

    public static GameManager Instance;
    [SerializeField] private Cookie cookie;
    #endregion

    #region events
    [HideInInspector] public UnityEvent OnGoldSet = new();
    [HideInInspector] public UnityEvent OnHealthSet = new();
    [HideInInspector] public UnityEvent OnHealthZero = new();
    #endregion

    #region methods
    //utility method to setup static instance and event listener
    private void Awake()
    {
        if (Instance == null) Instance = this;

        cookie.OnEnemyEatCookie.AddListener(HandleCookieEat);
        Gold = startingGold;
    }

    //method to change health, show on cookie and play sound
    private void HandleCookieEat()
    {
        SFXManager.Instance.PlayLifeLost();
        Health -= 1;

        //disable all indicators, then enable the ones that should be on
        cookie.healthIndicator1.SetActive(false);
        cookie.healthIndicator2.SetActive(false);
        cookie.healthIndicator3.SetActive(false);
        cookie.healthIndicator4.SetActive(false);
        cookie.healthIndicator5.SetActive(false);
        if (Health >= 1) cookie.healthIndicator1.SetActive(true);
        if (Health >= 2) cookie.healthIndicator2.SetActive(true);
        if (Health >= 3) cookie.healthIndicator3.SetActive(true);
        if (Health >= 4) cookie.healthIndicator4.SetActive(true);
        if (Health >= 5) cookie.healthIndicator5.SetActive(true);
    }
    #endregion
}
