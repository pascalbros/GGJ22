using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersController : MonoBehaviour
{
    public GameObject firstPlayerPrefab;
    public GameObject secondPlayerPrefab;

    public PathController pathController;

    //Degrees/sec
    private float spinSpeed = 360;
    private GameObject currentPlayer;
    private GameObject otherPlayer;
    private GameObject firstPlayer;
    private GameObject secondPlayer;
    private Vector3 nextPosition;

    private float minAntiCheatDelay = 0.1f;
    private float currentAntiCheatTime = 0f;


    void Start() {
        secondPlayer = Instantiate(secondPlayerPrefab, pathController.GetNextPosition(), Quaternion.identity);
        firstPlayer = Instantiate(firstPlayerPrefab, secondPlayer.transform.position - Vector3.back * Constants.wayPointsDistance, Quaternion.identity);
        currentPlayer = firstPlayer;
        otherPlayer = secondPlayer;
        nextPosition = pathController.GetNextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentTime();
        UpdateAntiCheat();
        MoveCamera();
        RotatePlayer();
        CheckInput();
        Debug.Log(currentAntiCheatTime);
    }

    private void UpdateCurrentTime() {
        var scale = new Vector3(Time.deltaTime * 0.05f, Time.deltaTime * 0.05f, Time.deltaTime * 0.05f);
        otherPlayer.transform.localScale = otherPlayer.transform.localScale + scale;
        currentPlayer.transform.localScale = currentPlayer.transform.localScale - scale;
    }

    private void MoveCamera() {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, otherPlayer.transform.position + new Vector3(7, -2, 0), 0.1f);
    }

    private void RotatePlayer() {
        if (currentPlayer.transform.position.x != nextPosition.x) {
            currentPlayer.transform.RotateAround(otherPlayer.transform.position, Vector3.up, spinSpeed * Time.deltaTime);
        } else {
            currentPlayer.transform.RotateAround(otherPlayer.transform.position, Vector3.right, spinSpeed * Time.deltaTime);
        }
        currentPlayer.transform.rotation = Quaternion.identity;
    }

    private void CheckInput() {
        if (!Keyboard.current.anyKey.wasPressedThisFrame) { return; }
        if (currentAntiCheatTime > 0f) { currentAntiCheatTime = minAntiCheatDelay; return; }
        currentAntiCheatTime = minAntiCheatDelay;
        if (!IsCurrentPlayerAligned()) { return; }
        currentPlayer.transform.position = nextPosition;
        var temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
        nextPosition = pathController.GetNextPosition();
    }

    private void UpdateAntiCheat() {
        currentAntiCheatTime -= Time.deltaTime;
    }

    private bool IsCurrentPlayerAligned() {
        return Vector3.Distance(currentPlayer.transform.position, nextPosition) < 0.5f;
    }
}
