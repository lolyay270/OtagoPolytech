/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 20th March 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The SheepManager class controls:
/// - the list of all alive sheep
/// - spawn locations for the sheep
/// - spawning sheep at a delay
/// - adding new sheep to alive list
/// - removing sheep from alive list when saved or fall
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    #region local variables           
    [SerializeField] private float spawnDelayFrames;
    [SerializeField] private List<Transform> locations = new();
    [SerializeField] private Sheep SheepPrefab;
    private List<Sheep> sheepList = new();
    public List<Sheep> SheepList
    {
        get { return sheepList; }
    }
    public bool isEnabled = true;
    #endregion

    #region methods
    //spawn sheep object after every time delay, when allowed
    private void Update()
    {
        if (Time.frameCount % spawnDelayFrames == 0 && isEnabled)
        {
            SpawnSheep();
        }
    }

    //spawn sheep at specific locations, add event listeners to sheep, add sheep to list of all alive sheep
    private void SpawnSheep()
    {
        int random = Random.Range(0, locations.Count); //randomly choose a spawn location
        Sheep newSheep = Instantiate(SheepPrefab, locations[random].position, new Quaternion(0, 0, 0, 0));         //using parent location isnt good for flexability later
        sheepList.Add(newSheep);
        newSheep.OnEatHay.AddListener(HandleSheepEatHay);
        newSheep.OnDrop.AddListener(HandleSheepDrop);
    }

    //remove sheep from alive list when saved with hay
    private void HandleSheepEatHay(Sheep sheep)
    {
        sheepList.Remove(sheep);
    }

    //remove sheep from alive list when fall off edge of map
    private void HandleSheepDrop(Sheep sheep)
    {
        sheepList.Remove(sheep);
    }
    #endregion
}
