/// <remarks>
/// Author: Jenna Boyes
/// Date Created: 29th April 2024
/// Bugs: None known at this time
/// </remarks>

/// <summary>
/// The MeshGenerator class controls:
/// - the game object called "mazeMesh"
/// - the filling that object with mesh triangles
/// </summary>

using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    #region variables and accessors
    private float scale, height;
    public Material wallMaterial, floorMaterial;
    public static MeshGenerator Instance;
    #endregion

    #region methods
    //setup for instance
    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    //make a new object and fill with finished mesh
    public void MakeMazeMesh(float MazeScale, float MazeHeight)
    {
        scale = MazeScale;
        height = MazeHeight;

        if (wallMaterial && floorMaterial)
        {
            GameObject mazeObject = new();
            mazeObject.transform.position = Vector3.zero;
            mazeObject.name = "mazeMesh";
            mazeObject.AddComponent<MeshFilter>().mesh = GenerateMazeMeshFromData(MazeConstructor.Instance.Data);
            mazeObject.AddComponent<MeshRenderer>().materials = new Material[2] { floorMaterial, wallMaterial };
            mazeObject.AddComponent<MeshCollider>().sharedMesh = mazeObject.GetComponent<MeshFilter>().mesh; //fixing the player falling thru floor
        }
    }

    //make each individual square of maze walls
    public Mesh GenerateMazeMeshFromData(int[,] data)
    {
        // TODO: Declare your mesh, new vertices list and new UVs list here. The lists will initially be empty.
        Mesh maze = new();
        List<Vector3> newVertices = new();
        List<Vector2> newUVs = new();

        // TODO: Set the subMeshCount of your mesh to 2. This will allow us to display different materials on the floor and walls.
        maze.subMeshCount = 2;

        // TODO: Create 2 lists for your triangles, one for the floor triangles and 1 for the wall triangles.
        List<int> floorTriangles = new();
        List<int> wallTriangles = new();

        // The rows and columns of our data we are going to span.
        int xMax = data.GetUpperBound(0); //xMax
        int yMax = data.GetUpperBound(1); //yMax

        // Gives us half of the cell height. This is used to position our wall quads.
        float halfH = height * .5f;

        // Iterate over our data.
        for (int x = 0; x <= xMax; x++)
        {
            for (int y = 0; y <= yMax; y++)
            {
                if (data[x, y] != 1)
                {
                    // -- FLOOR AND CEILING --
                    // Generate a quad for the floor
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(x * scale, 0, y * scale),
                        Quaternion.LookRotation(Vector3.up),
                        new Vector3(scale, scale, 1)
                    ), ref newVertices, ref newUVs, ref floorTriangles);

                    // Generate a quad for the ceiling
                    AddQuad(Matrix4x4.TRS(
                        new Vector3(x * scale, height, y * scale),
                        Quaternion.LookRotation(Vector3.down),
                        new Vector3(scale, scale, 1)
                        ), ref newVertices, ref newUVs, ref floorTriangles);
                    
                    // ------ WALLS --------
                    // left
                    if (x - 1 < 0 || data[x - 1, y] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((x - .5f) * scale, halfH, y * scale),
                            Quaternion.LookRotation(Vector3.right),
                            new Vector3(scale, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    // front
                    if (y + 1 > yMax || data[x, y + 1] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(x * scale, halfH, (y + 0.5f) * scale),
                            Quaternion.LookRotation(Vector3.back),
                            new Vector3(scale, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    // right
                    if (x + 1 > xMax || data[x + 1, y] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3((x + .5f) * scale, halfH, y * scale),
                            Quaternion.LookRotation(Vector3.left),
                            new Vector3(scale, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }

                    // back
                    if (y - 1 < 0 || data[x, y - 1] == 1)
                    {
                        AddQuad(Matrix4x4.TRS(
                            new Vector3(x * scale, halfH, (y - 0.5f) * scale),
                            Quaternion.LookRotation(Vector3.forward),
                            new Vector3(scale, height, 1)
                        ), ref newVertices, ref newUVs, ref wallTriangles);
                    }
                }
            }
        }

        //Set the vertices and UVs of the maze mesh.
        maze.SetVertices(newVertices);
        maze.SetUVs(0, newUVs);

        // Sets triangles for the 2 submeshes (one for the floor, and another for the walls.)
        maze.SetTriangles(floorTriangles.ToArray(), 0);
        maze.SetTriangles(wallTriangles.ToArray(), 1);

        // Ensures the maze renders properly with lighting etc.
        maze.RecalculateNormals();

        return maze;
    }

    // Adds a quad like we did in our examples.
    // We have to provide a matrix that will perform a transformation on the vertices.
    // newVertices, newUVs and newTriangles are all passed in using the "ref" keyword.
    // This allows us to cumultaively build up a massive list of vertices, UVs and tris
    // as we loop through the data arrays, which will ultimately get assigned to the mesh.
    private void AddQuad(Matrix4x4 matrix, ref List<Vector3> newVertices,
        ref List<Vector2> newUVs, ref List<int> newTriangles)
    {
        int index = newVertices.Count;

        Vector3 vert1 = new(-.5f, -.5f, 0);
        Vector3 vert2 = new(-.5f, .5f, 0);
        Vector3 vert3 = new(.5f, .5f, 0);
        Vector3 vert4 = new(.5f, -.5f, 0);

        newVertices.Add(matrix.MultiplyPoint3x4(vert1));
        newVertices.Add(matrix.MultiplyPoint3x4(vert2));
        newVertices.Add(matrix.MultiplyPoint3x4(vert3));
        newVertices.Add(matrix.MultiplyPoint3x4(vert4));

        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));

        newTriangles.Add(index + 2);
        newTriangles.Add(index + 1);
        newTriangles.Add(index);

        newTriangles.Add(index + 3);
        newTriangles.Add(index + 2);
        newTriangles.Add(index);
    }
    #endregion
}
