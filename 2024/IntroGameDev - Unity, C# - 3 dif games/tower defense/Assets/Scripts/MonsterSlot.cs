/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The MonsterSlot class controls:
/// - clicking on the slot or monster
/// - spawning monster
/// - upgrading monster
/// </summary>

using UnityEngine;

public class MonsterSlot : MonoBehaviour
{
    #region variables
    private MonsterData spawnedMonster;
    [SerializeField] private MonsterData monsterPrefab;
    #endregion

    #region methods
    //method to manage clicking on slot
    private void OnMouseUp()
    {
        if (CanPlaceMonster())
        {
            SFXManager.Instance.PlayTowerPlace();
            SpawnMonster();
        }
        else if (CanUpgradeMonster())
        {
            UpgradeMonster();
        }
    }

    //utility method to check if a monster can be placed
    private bool CanPlaceMonster()
    {
        // if       empty slot     and        have enough gold for first level
        if (spawnedMonster == null && GameManager.Instance.Gold >= monsterPrefab.Levels[0].Cost)
        {
            return true;
        }
        return false;
    }

    //utility method to check if a monster can be upgraded
    private bool CanUpgradeMonster()
    {
        // if      is spawned      and       can level up                   and         have enough gold for next level
        if (spawnedMonster != null && spawnedMonster.GetNextLevel() != null && GameManager.Instance.Gold >= spawnedMonster.GetNextLevel().Cost)
        {
            return true;
        }
        return false;
    } 

    //method to spawn a monster on the slot, and remove cost from gold
    private void SpawnMonster() 
    {
        spawnedMonster = Instantiate(monsterPrefab, transform.position, transform.rotation);
        MonsterCostToGold(spawnedMonster);
    }

    // method to make monster the next level
    public void UpgradeMonster()
    {
        spawnedMonster.GetComponent<MonsterData>().IncreaseLevel();
        MonsterCostToGold(spawnedMonster);
    }

    //utility method to remove the cost of monster from player's gold
    private void MonsterCostToGold(MonsterData monster)
    {
        GameManager.Instance.Gold -= monster.CurrentLevel.Cost;
    }
    #endregion
}
