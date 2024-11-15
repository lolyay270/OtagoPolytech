/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 10th April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The MazeConstructor class controls:
/// - generating the maze data
/// - generating the node array
/// - holding the data for both 2d arrays
/// </summary>

using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    #region variables/accessors/instances
    [SerializeField] private bool showDebug;
    private Vector2Int gridSize = new();
    [SerializeField] private float positionClosedChance = 0.1f; // How likely we are to fill a space (0.1 == a 90% chance)

    private int[,] data;
    public int[,] Data
    {
        get { return data; }
    }
    private Node[,] graph;
    public Node[,] Graph
    {
        get { return graph; }
    }

    public static MazeConstructor Instance;
    #endregion

    #region methods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //method to show the grid of maze in the game view
    private void OnGUI()
    {
        if (!showDebug)
            return;
        
        string msg = "";

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                msg += data[x, y] == 0 ? "\u25a1" : "\u25a0";
            }
            msg += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    //method to make the 2d array of open and closed cells for maze
    public void GenerateMazeDataFromDimensions(int xMax, int yMax)
    {
        int[,] maze = new int[xMax, yMax]; //new empty maze

        //close some cells
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)  //iterate over maze 2d array
            {
                if (x == 0 || x == xMax - 1 || y == 0 || y == yMax - 1) //if on outer edge of maze
                {
                    maze[x, y] = 1;
                }
                else if (x % 2 == 0 && y % 2 == 0) // if both axis are even
                {
                    float randNum = Random.value;
                    if (randNum >= positionClosedChance) // 90% chance
                    {
                        maze[x, y] = 1; // close this cell

                        randNum = Random.value; // 25% chance for each vertical or horizontal connected space to be closed
                        if (randNum < 0.25f)
                        {
                            maze[x - 1, y] = 1;
                        }
                        else if (randNum < 0.5f)
                        {
                            maze[x + 1, y] = 1;
                        }
                        else if (randNum < 0.75f)
                        {
                            maze[x, y - 1] = 1;
                        }
                        else
                        {
                            maze[x, y + 1] = 1;
                        }
                    }
                } //dont need "else { maze[x, y] = 0 }" cause int array objects are default 0
            }
        }

        //give data to UI view 
        gridSize = new Vector2Int(xMax, yMax);

        //save the data in this class so it cannot be changed
        data = maze;

        GenerateNodeGraph(xMax, yMax);
    }

    //method to make the node 2d array for pathfinder
    private void GenerateNodeGraph(int xMax, int yMax)
    {
        graph = new Node[xMax,yMax];
        for (int x = 0; x < xMax; x++)
        {
            for (int y = 0; y < yMax; y++)
            {
                graph[x, y] = new Node(x, y, true);
                if (Data[x,y] == 1)
                {
                    graph[x, y].walkable = false;
                }
            }
        }
    }
    #endregion
}