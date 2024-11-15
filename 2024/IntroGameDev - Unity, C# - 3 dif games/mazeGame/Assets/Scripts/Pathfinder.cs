/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 24th May 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Pathfinder class controls:
/// - the storage of the path
/// - the generation of the path
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    #region variables
    public Node[,] Graph 
    { 
        get { return MazeConstructor.Instance.Graph; } 
    }
    private const int MOVE_DIAGONAL_COST = 14;
    private const int MOVE_STRAIGHT_COST = 10;

    [SerializeField] private LineRenderer LR;

    private Vector2Int playerNode, monsterNode;
    private List<Node> path = new();
    public List<Node> Path { get { return path; } }

    public static Pathfinder Instance;
    #endregion

    #region methods
    //setup instance
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //waits for graph to be generated, then updates path only when monster/player current nodes change
    public IEnumerator Run()
    {
        // wait 1 frame to ensure that our graph has been generated, then generate the initial path
        yield return null; 
        UpdatePath();

        while (true)
        {
            //check both end nodes if they have changed since last frame
            if (playerNode != ConvertTransToNode(GameManager.Instance.Player.transform) || monsterNode != ConvertTransToNode(GameManager.Instance.Monster.transform))
            {
                UpdatePath();
            }
            yield return null;
        }
    }

    private void UpdatePath()
    {
        //get nodes of player and monster
        playerNode = ConvertTransToNode(GameManager.Instance.Player.transform);
        monsterNode = ConvertTransToNode(GameManager.Instance.Monster.transform);

        //find the path 
        path = FindPath(monsterNode.x, monsterNode.y, playerNode.x, playerNode.y);

        // ---show debug line--- \\
        List<Vector3> points = new();
        float mazeScale = GameManager.Instance.mazeScale;
        if (path != null)
        {
            foreach (var node in path)
            {
                points.Add(new Vector3(node.x * mazeScale, 1f, node.y * mazeScale));
            }
            LR.positionCount = points.Count;
            LR.SetPositions(points.ToArray());
        }
    }

    //method to find and return the cheapest path
    private List<Node> FindPath(int startX, int startY, int endX, int endY)
    {
        Node startNode = Graph[startX, startY];
        Node endNode = Graph[endX, endY];

        // openList contains all the nodes we are currently considering.
        List<Node> openList = new() { startNode };
        // closedList is all the nodes we have already explored.
        List<Node> closedList = new();

        for (int x = 0; x < Graph.GetLength(0); x++)
            for (int y = 0; y < Graph.GetLength(1); y++)
            {
                Node pathNode = Graph[x, y];
                pathNode.gCost = int.MaxValue; // Until we've considered a node, make it's gCost massive. Since gCost will be overwritten on a node if a cheaper path is found to it.
                pathNode.prevNode = null; // This will be set once we find the best node to get to this node from.
            }

        // Initialize the start node.
        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);

        // This is the heart of the algorithm:
        while (openList.Count > 0)
        {
            // Consider the lowest fCost node.
            Node currentNode = GetLowestFCostNode(openList);

            // if the current node is the end node, return the path
            if (currentNode == endNode)
            {
                return CalculatePath(currentNode);
            }

            // Move current into closedList as we dont want to double check it
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // Iterate over all of the current node's neighbours.
            List<Node> neighbours = GetNeighbourList(currentNode);
            foreach (Node neighbour in neighbours)
            {
                int potentialgCost = currentNode.gCost + CalculateDistance(currentNode, neighbour);
                if (closedList.Contains(neighbour) || !neighbour.walkable) { } //already checked or not walkable, do nothing 
                else if (potentialgCost < neighbour.gCost) //if actual distance is less than recorded (is int.MaxValue, or neighbour to a node checked already)
                {
                    neighbour.prevNode = currentNode; //set the trail backwards for the full path
                    neighbour.gCost = potentialgCost; //update to less distance number
                    neighbour.hCost = CalculateDistance(neighbour, endNode); //update

                    if (!openList.Contains(neighbour)) //if not in list to check, add it
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        // If we get to the end of the while loop and our base condition hasn't been met, we couldn't find a path. Return null.
        if (openList.Count == 0 && GetLowestFCostNode(closedList) != endNode)
        {
            return null;
        }
        else
        {
            return closedList;
        }
    }

    //Utility method to calculate the distance between any 2 nodes
    private int CalculateDistance(Node from, Node to)
    {
        int xDist = Mathf.Abs(from.x - to.x); 
        int yDist = Mathf.Abs(from.y - to.y); //getting absolute values cause {to} can be larger than {from} sometimes

        int diagonalDist = Mathf.Min(xDist, yDist); //you have to move BOTH x and y with diag, so it is just the smaller of them
        int straightDist = Mathf.Max(xDist, yDist) - diagonalDist; //what ever is left over after diag must be straight

        return (diagonalDist * MOVE_DIAGONAL_COST) + (straightDist * MOVE_STRAIGHT_COST);
    }

    //Utility method to find which connected nodes can actually be moved onto
    private List<Node> GetNeighbourList(Node node)
    {
        List<Node> neighbours = new();
        int x = node.x, y = node.y;

        //----------- NESW Directions -----------\\
        if (x > 1) //if not beside left wall
        {
            neighbours.Add(Graph[x - 1, y]);
        }

        if (x < Graph.GetUpperBound(0)) //if not beside right wall
        {
            neighbours.Add(Graph[x + 1, y]);
        }

        if (y > 1) //if not beside bottom wall
        {
            neighbours.Add(Graph[x, y - 1]);
        }
        
        if (y < Graph.GetUpperBound(1)) //if not beside top wall
        {
            neighbours.Add(Graph[x, y + 1]);
        }

        //----------- Diagonal Directions -----------\\
        //i want to check all these nodes are open, so when the monster makes diagonal moves, they do not cut thru the corner of a wall

        if (y > 1 && x > 1)//if not beside both bottom left walls
        {
            if (Graph[x - 1, y].walkable && Graph[x, y - 1].walkable && Graph[x - 1, y - 1].walkable) //if bot, left and bot left all open
            {
                neighbours.Add(Graph[x - 1, y - 1]);
            }
        }

        if (x > 1 && y < Graph.GetUpperBound(1)) //if not beside both top left walls
        {
            if (Graph[x - 1, y].walkable && Graph[x, y + 1].walkable && Graph[x - 1, y + 1].walkable) //if top, top left and left all open
            {
                neighbours.Add(Graph[x - 1, y + 1]);
            }
        }
        
        if (x < Graph.GetUpperBound(0) &&  y > 1) //if not bot right walls
        {
            if (Graph[x + 1, y].walkable && Graph[x, y - 1].walkable && Graph[x + 1, y - 1].walkable) //if bot, bot right and right all open
            {
                neighbours.Add(Graph[x + 1, y - 1]);
            }

        }

        if (x < Graph.GetUpperBound(0) && y < Graph.GetUpperBound(1)) //if not top right walls
        {
            if (Graph[x + 1, y].walkable && Graph[x, y + 1].walkable && Graph[x + 1, y + 1].walkable) //if top, top right and right all open
            {
                neighbours.Add(Graph[x + 1, y + 1]);
            }
        }

        return neighbours;
    }

    // Utility function that returns the node with the lowest fCost from the provided list.
    private Node GetLowestFCostNode(List<Node> nodeList){
        Node lowestFCostNode = nodeList[0];
        for(int i = 1; i < nodeList.Count; i++)
            if(nodeList[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = nodeList[i];

        return lowestFCostNode;
    }

    // Utility function that will be called once we have found the end node to trace back the path that was taken to get there.
    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new();
        path.Add(endNode);
        Node currentNode = endNode;
        while (currentNode.prevNode != null)
        {
            path.Add(currentNode.prevNode);
            currentNode = currentNode.prevNode;
        }
        path.Reverse();
        return path;
    }

    //Utility method to convert an objects transform into a Node position
    private Vector2Int ConvertTransToNode(Transform trans)
    {
        Vector2 node = new( trans.position.x, trans.position.z ); //getting top down coords
        node.x /= GameManager.Instance.mazeScale;
        node.y /= GameManager.Instance.mazeScale;
        return new Vector2Int (Mathf.RoundToInt(node.x), Mathf.RoundToInt(node.y)); //rounding to closest int 
    }
    #endregion
}