/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 15th May 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The GameManager class controls:
/// - starting of each method/object required to run
/// - random maze size
/// - spawning all objects inside the maze
/// - win and lose endings
/// </summary>

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region variables/instances
    private int[,] data;
    public int xMax, yMax;
    public float mazeScale = 3.75f, mazeHeight = 3.5f;

    //spawned objects
    [SerializeField] private FpsMovement playerPrefab;
    private FpsMovement player;
    public FpsMovement Player { get { return player; }}

    [SerializeField] private Enemy monsterPrefab;
    private Enemy monster;
    public Enemy Monster { get { return monster; }}

    [SerializeField] private GameObject treasurePrefab;
    private GameObject treasure;
    [SerializeField] private int treasureToPlayerMin; // 5 means will spawn at closest to player of 5 spaces

    //other
    private TriggerEventRouter trigEventRoute;
    public static GameManager Instance;
    [SerializeField] private float EndScreenShowSecs;
    private Coroutine pathfind, monsterMove;
    [SerializeField] private TextMeshProUGUI endGameText;
    #endregion

    #region methods
    //starting most classes when they need to, setup instance
    private void Start()
    {
        if (Instance == null) Instance = this;

        //make maze
        RandomMazeSize();
        MazeConstructor.Instance.GenerateMazeDataFromDimensions(xMax, yMax);
        data = MazeConstructor.Instance.Data;
        MeshGenerator.Instance.MakeMazeMesh(mazeScale, mazeHeight);

        //spawning things we need generated after maze
        SpawnPlayer();
        SpawnMonster();
        SpawnTreasure();

        //monster move
        pathfind = StartCoroutine(Pathfinder.Instance.Run());
        monsterMove = StartCoroutine(monster.Run());

        //end of game listeners
        trigEventRoute.OnPlayerWin.AddListener(HandleGameWin);
        trigEventRoute.OnPlayerLose.AddListener(HandleGameLose);
    }

    private void RandomMazeSize()
    {
        for (int i = 0; i < 2; i++)
        {
            //random min is 8 so size of 9 has the same chance as all other numbers
            int random = Random.Range(8, 26); //8 to 25 incl
            if (random % 2 == 0) random++; //increase 1 to make odd 
            if (i == 0) xMax = random;
            if (i == 1) yMax = random;
        }
    }

    #region spawn inside of maze objects
    private void SpawnPlayer()
    {
        Vector2 spawnLocation;

        //set location as the first open slot
        spawnLocation = GetFirstOpenSpace();

        //get player height for height of spawn
        float playerHeight = playerPrefab.GetComponent<CharacterController>().height;

        //rotate player to face an open space next to them
        int yRotate = RotatePlayerToOpenSlot(spawnLocation);

        //multiply location based on scale of maze
        spawnLocation = new Vector2(spawnLocation.x * mazeScale, spawnLocation.y * mazeScale);

        player = Instantiate(playerPrefab, new Vector3(spawnLocation.x, playerHeight/2, spawnLocation.y), Quaternion.Euler(0, yRotate, 0));
        trigEventRoute = player.GetComponent<TriggerEventRouter>();
    }

    //utility method to find the first walkable space in the maze
    private Vector2 GetFirstOpenSpace() 
    {
        for (int x = 1; x < xMax - 1; x++)         //making starting and ending 1 from max/min since surrounding walls (x & y = 0 && x & y == max) are always filled,
        {                                           //it makes no sense to waste resources checking something thats never going to be an open space
            for (int y = 1; y < yMax - 1; y++)
            {
                if (data[x, y] == 0)
                {
                    return new Vector2(x, y);
                }
            }
        }
        return Vector2.zero;
    }

    private int RotatePlayerToOpenSlot(Vector2 spawnLocation)
    {
        int rotation;

        if (xMax <= yMax) //prefer to look down the longest axis if both front and right are open spaces
        {
            if (data[(int)spawnLocation.x, (int)spawnLocation.y + 1] == 0) //if space in front is open
            {
                rotation = 0;
            }
            else
            {
                rotation = 90;
            }
        }
        else
        {
            if (data[(int)spawnLocation.x + 1, (int)spawnLocation.y] == 0) //if space to right is open
            {
                rotation = 90;
            }
            else
            {
                rotation = 0;
            }
        }
        return rotation;
    }

    private void SpawnMonster()
    {
        //get last open space in maze
        Vector2 spawnLocation = GetLastOpenSpace();

        //multiply location based on scale of maze
        spawnLocation = new Vector2(spawnLocation.x * mazeScale, spawnLocation.y * mazeScale);

        //height of monster
        float monsterHeight = monsterPrefab.transform.position.y;

        monster = Instantiate(monsterPrefab, new Vector3(spawnLocation.x, monsterHeight, spawnLocation.y), Quaternion.Euler(0, 0, 0));
    }

    //utility method to find the last walkable space in the maze
    private Vector2 GetLastOpenSpace()
    {
        for (int x = xMax - 1; x >= 1; x--)
        {
            for (int y = yMax - 1; y >= 1; y--)
            {
                if (data[x, y] == 0)
                {
                    return new Vector2(x, y);
                }
            }
        }
        return Vector2.zero;
    }

    private void SpawnTreasure()
    {
        Vector2 location2D = GetRandomOpenSpace();
        Vector3 spawnLocation = new Vector3(location2D.x * mazeScale, 0, location2D.y * mazeScale);
        treasure = Instantiate(treasurePrefab, spawnLocation, Quaternion.Euler(0, 0, 0));
    }

    //utility method to find a random walkable space away from the player
    private Vector2 GetRandomOpenSpace()
    {
        Vector2Int location = Vector2Int.zero;
        int smallestAxis = Mathf.Min(xMax, yMax);
        int xMin, yMin;
        if (treasureToPlayerMin >= smallestAxis)
        {
            xMin = smallestAxis / 2;
            yMin = smallestAxis / 2;
        }
        else
        {
            xMin = treasureToPlayerMin;
            yMin = treasureToPlayerMin;
        }

        for (int i = 0; i < 50; i++) 
        {
            location.x = Random.Range(xMin, xMax - 1);
            location.y = Random.Range(yMin, yMax - 1);
            if (data[location.x, location.y] == 0)
            {
                return location;
            } 
        } 
        return Vector2Int.zero;
    }
    #endregion

    #region end game
    private void HandleGameWin()
    {
        endGameText.text = "Ayyyy, you got the treasure"; //display "You Escaped!" thru Canvas

        SFXManager.Instance.Win();

        StartCoroutine(GameEnd());
        treasure.SetActive(false); //disable treasure object

        StopCoroutine(monsterMove);
        StopCoroutine(pathfind); //disable monster pathfinding
    }

    private void HandleGameLose()
    {
        endGameText.text = "Shame, you got eaten"; //display "You Were Caught!" thru Canvas

        SFXManager.Instance.Lose();

        StartCoroutine(GameEnd());
    }
    private IEnumerator GameEnd() //end of any game win OR lose
    {
        player.GetComponent<FpsMovement>().enabled = false; //disable player controls

        yield return new WaitForSeconds(EndScreenShowSecs); //wait time to allow win/lose message to show

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//restart the game
    }
    #endregion
    #endregion
}
