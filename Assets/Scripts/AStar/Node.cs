using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Sebastian Lague @ https://www.youtube.com/watch?v=nhiFx28e7JY&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW&index=2
/// Modified by: Robin Chabouk
/// </summary>
public class Node : IHeapItem<Node> {

    /// <summary>
    /// Bool for whether a node can be walked on or not.
    /// </summary>
    public bool walkable;

    /// <summary>
    /// The vector3 for the position in the world that the node represents.
    /// </summary>
    public Vector3 worldPosition;

    /// <summary>
    /// The X position of the Node in the grid.
    /// </summary>
    public int gridX;

    /// <summary>
    /// The Y position of the Node in the grid.
    /// </summary>
    public int gridY;

    /// <summary>
    /// The gCost of the Node which is the distance of the node from the 
    /// starting node.
    /// </summary>
    public int gCost;

    /// <summary>
    /// The hCost of the node which is the distance of the node from the
    /// target node.
    /// </summary>
    public int hCost;

    /// <summary>
    /// The Node that is the parent (of the current node).
    /// </summary>
    public Node parent;

    /// <summary>
    /// The index of the node in the heap.
    /// </summary>
    int heapIndex;

    /// <summary>
    /// A Node constructor for creating Nodes that will make up the grid.
    /// </summary>
    /// <param name="_walkable">Whether the node can be walked on.</param>
    /// <param name="_worldPos">The position of the Node relative to the world.</param>
    /// <param name="_gridX">The X position of the Node.</param>
    /// <param name="_gridY">The Y position of the Node.</param>
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    /// <summary>
    /// The f cost,which is the sum of gCost an hCost.
    /// </summary>
    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    /// <summary>
    /// The index of the Node in the heap.
    /// </summary>
    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    /// <summary>
    /// Method for comparing nodes.
    /// </summary>
    /// <param name="nodeToCompare">The node to compare to</param>
    /// <returns></returns>
    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}