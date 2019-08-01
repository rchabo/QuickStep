using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Author: Sebastian Lague @ https://www.youtube.com/watch?v=dn1XRIaROM4&index=5&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
/// Modified by: Robin Chabouk
/// When multiple units want to request paths, this script manages those requests.
/// </summary>
public class PathRequestManager : MonoBehaviour {

    /// <summary>
    /// The queue of requests.
    /// </summary>
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();

    /// <summary>
    /// The current pathrequest.
    /// </summary>
    PathRequest currentPathRequest;

    /// <summary>
    /// The instance of path that is being processed.
    /// </summary>
    static PathRequestManager instance;

    /// <summary>
    /// Reference to the pathfinding class.
    /// </summary>
    Pathfinding pathfinding;

    /// <summary>
    /// Boolean for whether a path is currently being processed.
    /// </summary>
    bool isProcessingPath;

    void Awake() {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    /// <summary>
    /// Method for requesting a path.
    /// </summary>
    /// <param name="pathStart">The location from which the path is being requested.</param>
    /// <param name="pathEnd">The target location to which the path should be made.</param>
    /// <param name="callback">Store the path in an Action and a boolean to tell whether the request was successful.</param>
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    /// <summary>
    /// If a path is not being processed, try to process the next one.
    /// </summary>
    void TryProcessNext() {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    /// <summary>
    /// Called once pathfinding has finished finding a path.
    /// </summary>
    /// <param name="path">The path that was found if one was found.</param>
    /// <param name="success">If path was found successfully or not</param>
    public void FinishedProcessingPath(Vector3[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    /// <summary>
    /// Struct to hold all of the information for a path request.
    /// </summary>
    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }

    }
}
