using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float health = 1f;
    private float initialScale = 1f;
    void Start() {
        initialScale = transform.localScale.x;
    }

    public Vector3 HealthToScale() {
        return Vector3.one * initialScale * health;
    }

    public float GetHealth() {
        return health;
    }

    public void UpdateWithElapsedTime(float deltaTime) {
        health = health + (deltaTime * 0.15f); // 6 sec to die
    }
}
