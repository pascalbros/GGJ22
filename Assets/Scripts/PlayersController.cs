using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum GameState {
    NOT_STARTED, STARTED, ENDED
}

public class PlayersController : MonoBehaviour
{
    public GameObject firstPlayerPrefab;
    public GameObject secondPlayerPrefab;

    public PathController pathController;

    //Degrees/sec
    private float spinSpeed = 275;
    private PlayerController currentPlayer;
    private PlayerController otherPlayer;
    private PlayerController firstPlayer;
    private PlayerController secondPlayer;
    private Vector3 nextPosition;

    private float minAntiCheatDelay = 0.1f;
    private float currentAntiCheatTime = 0f;

    private GameState gameState = GameState.NOT_STARTED;

    void Start() {
        secondPlayer = Instantiate(secondPlayerPrefab, pathController.GetNextPosition(), Quaternion.identity).GetComponent<PlayerController>();
        firstPlayer = Instantiate(firstPlayerPrefab, secondPlayer.transform.position - Vector3.back * Constants.wayPointsDistance, Quaternion.identity).GetComponent<PlayerController>();
        currentPlayer = firstPlayer;
        otherPlayer = secondPlayer;
        nextPosition = pathController.GetNextPosition();
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, otherPlayer.transform.position + new Vector3(0, 0, -0.4f), 0.08f);
    }

    void Update() {
        if (gameState == GameState.NOT_STARTED) {
            CheckNotStarted();
            MoveCamera();
        } else if (gameState == GameState.STARTED) {
            UpdateCurrentTime();
            UpdateAntiCheat();
            MoveCamera();
            RotatePlayer();
            CheckInput();
            CheckPlayersHealth();
        }
    }

    private void CheckNotStarted() {
        if (!Keyboard.current.aKey.wasPressedThisFrame) { return; }
        gameState = GameState.STARTED;
    }

    private void UpdateCurrentTime() {
        otherPlayer.UpdateWithElapsedTime(Time.deltaTime);
        currentPlayer.UpdateWithElapsedTime(-Time.deltaTime);
        otherPlayer.transform.localScale = otherPlayer.HealthToScale();
        currentPlayer.transform.localScale = currentPlayer.HealthToScale();
    }

    private void MoveCamera() {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, otherPlayer.transform.position + new Vector3(0, 0, -0.4f), 0.08f);
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
        if (currentPlayer == firstPlayer) {
            if (!Keyboard.current.aKey.wasPressedThisFrame) { return; }
        } else {
            if (!Keyboard.current.lKey.wasPressedThisFrame) { return; }
        }
        if (currentAntiCheatTime > 0f) { currentAntiCheatTime = minAntiCheatDelay; return; }
        currentAntiCheatTime = minAntiCheatDelay;
        if (!IsCurrentPlayerAligned()) { return; }
        currentPlayer.transform.position = nextPosition;
        var temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
        nextPosition = pathController.GetNextPosition();
        var completion = pathController.GetPassedWaypoints() / (float)Constants.maxWaypoints;
        GameProgressBar.shared.SetPercentage(completion);
    }

    private void UpdateAntiCheat() {
        currentAntiCheatTime -= Time.deltaTime;
    }

    private bool IsCurrentPlayerAligned() {
        return Vector3.Distance(currentPlayer.transform.position, nextPosition) < 0.5f;
    }

    private void CheckPlayersHealth() {
        if (currentPlayer.GetHealth() <= 0f || pathController.GetPassedWaypoints() == Constants.maxWaypoints - 1) {
            gameState = GameState.ENDED;
            WinnerAnimation();
        }
    }

    private void WinnerAnimation() {
        if (firstPlayer.GetHealth() > secondPlayer.GetHealth()) {

        } else if (firstPlayer.GetHealth() < secondPlayer.GetHealth()) {

        } else {

        }
        Debug.Log("Cmon baby, do the animation");
    }

    static void RotateAround(Transform transform, Vector3 pivotPoint, Vector3 axis, float angle) {
        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        transform.position = rot * (transform.position - pivotPoint) + pivotPoint;
    }
}
