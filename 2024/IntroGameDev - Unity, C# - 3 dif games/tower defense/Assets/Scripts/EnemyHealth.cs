/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 22nd April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The EnemyHealth class controls:
/// - How much health an enemy has
/// - The fullness of the healthbar
/// </summary>

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region variables
    [SerializeField] private float maxHealth;
    [HideInInspector] public float currentHealth; //needed by Bullet
    private float originalXScale;
    #endregion

    #region methods
    private void Start()
    {
        currentHealth = maxHealth; //should be at max health when starting
        originalXScale = gameObject.transform.localScale.x; //initial scale so it can squish to accurate percentages
    }

    private void Update()
    {
        Vector3 newScale = gameObject.transform.localScale; //current scale size

        float fractionOfHealth = currentHealth / maxHealth; //fraction of how much health is remaining
        newScale.x = fractionOfHealth * originalXScale; //multiply the fraction to max health, to show accurate percentage

        gameObject.transform.localScale = newScale; //squish the healthbar to match
    }
    #endregion
}