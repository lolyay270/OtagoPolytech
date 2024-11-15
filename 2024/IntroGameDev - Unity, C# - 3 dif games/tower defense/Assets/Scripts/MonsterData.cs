/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 22nd March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The MonsterData class controls:
/// - The MonsterLevel class
/// - The levels a monster can have
/// - Increasing a monster's level
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    #region variables/class as variable/accessors
    [System.Serializable] public class MonsterLevel
    {
        [SerializeField] private int cost;
        [HideInInspector] public int Cost { get { return cost; } }

        [SerializeField] private GameObject visualization;
        [HideInInspector] public GameObject Visualization { get { return visualization; } }

        [SerializeField] private Bullet bullet;
        [HideInInspector] public Bullet Bullet { get {  return bullet; } }

        [SerializeField] private float shotCooldown;
        [HideInInspector] public float ShotCooldown { get { return shotCooldown; } }
    }

    private MonsterLevel currentLevel;
    public MonsterLevel CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            currentLevelIndex = levels.IndexOf(currentLevel);

            //show only current level on screen
            foreach (MonsterLevel level in levels)
            {
                if (level == currentLevel) 
                {
                    level.Visualization.SetActive(true);
                }
                else
                {
                    level.Visualization.SetActive(false);
                }
            }
        }
    }

    [SerializeField] private List<MonsterLevel> levels;
    public List<MonsterLevel> Levels { get { return levels; } }
    private int currentLevelIndex;
    #endregion

    #region methods
    // Sets our level to the first level when this GameObject is enabled.
    private void OnEnable()
    {
        CurrentLevel = levels[0];
    }

    //utility method to get the next level of a monster
    public MonsterLevel GetNextLevel()
    {
        //check if at max level already
        if (currentLevelIndex < levels.Count - 1)
        {
            return levels[currentLevelIndex + 1];
        }
        return null;
    }

    //utility method to make the monster the next level
    public void IncreaseLevel()
    {
        CurrentLevel = levels[currentLevelIndex + 1];
    }
    #endregion
}
