using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {

    public GameObject wayPointPrefab;
    private readonly List<Vector3> path = new();
    private readonly List<GameObject> waypoints = new();
    private int currentIndex = -1;
    private readonly int maxIndex = 8;
    private int passedWaypoints = 0;

    void Start()
    {
        AppendToPath(Constants.initialPosition);
    }

    public Vector3 GetNextPosition() {
        if (currentIndex >= maxIndex) {
            Destroy(waypoints[0]);
            waypoints.RemoveAt(0);
            path.RemoveAt(0);
        } else {
            currentIndex++;
        }
        //CheckIndex();
        passedWaypoints++;
        return path[currentIndex];
    }

    public int GetPassedWaypoints() {
        return passedWaypoints;
    }

    private void CheckIndex() {
        if (path.Count <= maxIndex * 2) {
            AppendToPath(path[^1] + Vector3.forward * Constants.wayPointsDistance);
        }
    }

    private void AppendToPath(Vector3 lastPosition) {
        path.AddRange(new List<Vector3>(PathMaker.GetPath(lastPosition, Constants.wayPointsDistance, 10, Constants.maxWaypoints)));
        var isFirst = true;
        foreach (var position in path) {
            waypoints.Add(Instantiate(wayPointPrefab, position, Quaternion.identity, transform));
            if (isFirst) {
                isFirst = false;
                waypoints[^1].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }
}