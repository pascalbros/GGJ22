using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PathDirection {
    FORWARD, BACKWARD, UP, DOWN, LEFT, RIGHT
}

public static class PathMaker {

    private static PathDirection[] PathDirections = { PathDirection.FORWARD, PathDirection.UP, PathDirection.DOWN, PathDirection.LEFT, PathDirection.RIGHT };

    //Tracking the opposite direction, cause we can't go back
    private static Dictionary<PathDirection, PathDirection> OppositeDirections = new() {
        { PathDirection.FORWARD, PathDirection.BACKWARD },
        { PathDirection.BACKWARD, PathDirection.FORWARD },
        { PathDirection.UP, PathDirection.DOWN },
        { PathDirection.DOWN, PathDirection.UP },
        { PathDirection.LEFT, PathDirection.RIGHT },
        { PathDirection.RIGHT, PathDirection.LEFT },
    };

    public static Vector3[] GetPath(Vector3? from, float distance, int initialCount, int count) {

        //All the directions
        HashSet<PathDirection> directions = new(PathDirections);

        PathDirection lastDirection = PathDirection.FORWARD;
        List<Vector3> path = new(count);
        path.Add(from ?? Vector3.zero);

        //Adding first linear segment
        if (initialCount > 0) {
            for (int i = 0; i < initialCount - 1; i++) {
                path.Add(Forward(path[^1], distance));
            }
        }

        //Adding 2 or more pieces, in this way we should be safe with snake-path (eg: down, forward, up)
        var remainingCount = count - initialCount;

        while (remainingCount > 0) {
            directions.Remove(GetOppositeDirection(lastDirection));
            int attachPieces = Mathf.Min(remainingCount, Random.Range(2, 6));
            var randomDirection = directions.ToArray()[Random.Range(0, directions.Count-1)];
            for (int i = 0; i < attachPieces; i++) {
                path.Add(GetPosition(path[^1], distance, randomDirection));
            }
            directions.Add(GetOppositeDirection(lastDirection));
            lastDirection = randomDirection;
            remainingCount -= attachPieces;
        }
        return path.ToArray();
    }

    private static PathDirection GetOppositeDirection(PathDirection direction) {
        return OppositeDirections[direction];
    }

    private static Vector3 GetPosition(Vector3 from, float distance, PathDirection direction) {
        switch (direction) {
            case PathDirection.FORWARD: return Forward(from, distance);
            case PathDirection.BACKWARD: return Backward(from, distance);
            case PathDirection.UP: return Up(from, distance);
            case PathDirection.DOWN: return Down(from, distance);
            case PathDirection.LEFT: return Left(from, distance);
            case PathDirection.RIGHT: return Right(from, distance);
        }
        Debug.LogError("Invalid direction, it should never reach here");
        return Vector3.zero;
    }

    private static Vector3 Forward(Vector3 from, float distance) {
        return from + new Vector3(0, 0, distance);
    }

    private static Vector3 Backward(Vector3 from, float distance) {
        return from + new Vector3(0, 0, -distance);
    }

    private static Vector3 Up(Vector3 from, float distance) {
        return from + new Vector3(0, distance, 0);
    }

    private static Vector3 Down(Vector3 from, float distance) {
        return from + new Vector3(0, -distance, 0);
    }

    private static Vector3 Left(Vector3 from, float distance) {
        return from + new Vector3(distance, 0, 0);
    }

    private static Vector3 Right(Vector3 from, float distance) {
        return from + new Vector3(-distance, 0, 0);
    }
}
