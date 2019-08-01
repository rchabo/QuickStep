using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Sebastian Lague @ https://www.youtube.com/watch?v=nhiFx28e7JY&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=2
/// Modified by: Robin Chabouk
/// This script is responsible for creating the grid that is used by the
/// enemy for the A star algorithm.
/// </summary>
public class Grid : MonoBehaviour{

    /// <summary>
    /// Boolean for whether the grid squares should be displayed when
    /// the game runs.
    /// </summary>
    public bool displayGridGizmos;

    /// <summary>
    /// The areas of the grid which are un-walkablle.
    /// </summary>
    public LayerMask unwalkableMask;

    /// <summary>
    /// The size of the grid.
    /// </summary>
    public Vector2 gridWorldSize;

    /// <summary>
    /// The radius of each node.
    /// </summary>
    public float nodeRadius;

    /// <summary>
    /// The two dimensional array that holds all of the objects of type 'Node'
    /// that make up the grid.
    /// </summary>
    Node[,] grid;

    /// <summary>
    /// The diameter of each node.
    /// </summary>
    float nodeDiameter;

    /// <summary>
    /// The height and width of the grid.
    /// </summary>
    int gridSizeX, gridSizeY;

    /// <summary>
    /// Initialise the grid.
    /// </summary>
    void Awake(){
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    /// <summary>
    /// Get the size of the grid by multiplying height by width.
    /// </summary>
    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    /// <summary>
    /// Create the grid of size gridSizeX*gridSizeY. Loop through all of the positions of the grid where
    /// there should be a node, starting from the bottom left. Check if each node is walkable or not.
    /// </summary>
    void CreateGrid(){
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    /// <summary>
    /// Get the list of neighbours around a given Node.
    /// </summary>
    /// <param name="node">The Node to search around.</param>
    /// <returns>A list of all the neighbours for a given Node.</returns>
    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    /// <summary>
    /// Converts world positions into a position on the grid.
    /// </summary>
    /// <param name="worldPosition">The position in the world to be converted to a position on the grid.</param>
    /// <returns>the position on the grid representing the position in the world.</returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition){
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    /// <summary>
    /// Draws the gizmos such as the grid squares, used for debugging, only visible in editor mode.
    /// </summary>
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && displayGridGizmos) {
            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}