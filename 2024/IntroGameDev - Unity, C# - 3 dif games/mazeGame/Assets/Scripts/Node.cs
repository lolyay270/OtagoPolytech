/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 24th May 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The Node class controls:
/// - what properties a pathfinding node has
/// </summary>

public class Node
{
    //  varibles
    public int x, y;
    public int gCost, hCost;
    public int fCost { get { return gCost + hCost; } } //auto math
    public Node prevNode;
    public bool walkable;

    // constructor
    public Node(int x, int y, bool walkable)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
    }
}
