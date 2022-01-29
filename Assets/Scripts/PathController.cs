using UnityEngine;

public class PathController : MonoBehaviour {

    public GameObject wayPointPrefab;

    void Start()
    {
        var path = PathMaker.GetPath(Vector3.zero, 1, 10, 500);
        foreach (var position in path) {
            Instantiate(wayPointPrefab, position, Quaternion.identity);
        }
    }

    void Update()  {
        
    }
}