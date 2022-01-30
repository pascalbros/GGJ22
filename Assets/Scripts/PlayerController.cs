using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float health = 1f;
    private float initialScale = 1f;

    public GameObject explosion;

    public bool letMeGrow = false;
    public bool finishedLastAnimation = false;

    void Start() {
        initialScale = transform.localScale.x;
    }

    private void Update() {
        if (letMeGrow && transform.localScale.x < 70f) {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale + Vector3.one, 30f * Time.deltaTime);
        }

        if (transform.localScale.x >= 70f) {
            finishedLastAnimation = true;
        }
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

    public void Explode() {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<ParticleSystem>().gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    public void Grow() {
        letMeGrow = true;
    }
}
