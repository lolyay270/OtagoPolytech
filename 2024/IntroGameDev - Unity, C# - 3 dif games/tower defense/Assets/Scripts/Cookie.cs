/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 19th April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Cookie class controls:
/// - The trigger between it and the enemies
/// - References to the green monster health indicators that sit on the cookie
/// </summary>

using UnityEngine;
using UnityEngine.Events;

public class Cookie : MonoBehaviour
{
    #region variables
    [HideInInspector] public UnityEvent OnEnemyEatCookie = new();
    public GameObject healthIndicator1, healthIndicator2, healthIndicator3, healthIndicator4, healthIndicator5;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D other) //trigger for enemies eating cookie
    {
        if (other.CompareTag("Enemy"))
        {
            OnEnemyEatCookie?.Invoke();
        }
    }
    #endregion
}
