using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Author: Sebastian Lague @ https://www.youtube.com/watch?v=mZfyt03LDH4&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=3
/// Modified by: Robin Chabouk
/// </summary>
public class Pathfinding : MonoBehaviour {

    /// <summary>
    /// The manager for requesting paths.
    /// </summary>
    PathRequestManager requestManager;

    /// <summary>
    /// The grid that pathfinding will occur on.
    /// </summary>
    Grid grid;

    /// <summary>
    /// initialse the PathRequestManager and the Grid.
    /// </summary>
    void Awake() {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    /// <summary>
    /// Method that starts the coroutine for finding a path.
    /// </summary>
    /// <param name="startPos">The starting position of the seeker.</param>
    /// <param name="targetPos">The target position of the seeker.</param>
    public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    /// <summary>
    /// Heap of nodes contains the set of nodes that are yet to be evaluated.
    /// Hashset contains the nodes that have already been evaluated. While
    /// there are still nodes to evaluate, a path will continue to be found.
    /// When the target node has been found, retrace path is called to find all
    /// the nodes that were traversed to get to the target node from the start node.
    /// </summary>
    /// <param name="startPos">The starting position of the seeker</param>
    /// <param name="targetPos">The target position of the seeker</param>
    /// <returns>Null, so the method continues to be executed in the next frame.</returns>
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos){
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.walkable && targetNode.walkable) {
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node node = openSet.RemoveFirst();

                closedSet.Add(node);

                if (node == targetNode) {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in grid.GetNeighbours(node)) {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        } else {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess) {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    /// <summary>
    /// Retrace steps back to the start Node from the target node.
    /// </summary>
    /// <param name="startNode">node to retrace path from</param>
    /// <param name="endNode">target Node to be retraced to</param>
    /// <returns>The array of waypoints needed to retrace to endNode</returns>
    Vector3[] RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path, endNode);
        Array.Reverse(waypoints);
        return waypoints;
    }

    /// <summary>
    /// Simplify the given path.
    /// </summary>
    /// <param name="path">List of nodes that make up the path to be simplified.</param>
    /// <param name="endNode">The ending node of the path.</param>
    /// <returns>The simplified path.</returns>
    Vector3[] SimplifyPath(List<Node> path, Node endNode) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++) {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        if (waypoints.Count < 1) {
            waypoints.Add(endNode.worldPosition);
        }
        return waypoints.ToArray();
    }

    /// <summary>
    /// Get the distance between nodeA and nodeB.
    /// </summary>
    /// <param name="nodeA">nodeA</param>
    /// <param name="nodeB">nodeB</param>
    /// <returns>distance between nodeA and nodeB</returns>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
