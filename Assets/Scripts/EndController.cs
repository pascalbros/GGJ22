using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndController : MonoBehaviour
{

    public static bool playerOneWins = true;

    public GameObject p1;
    public GameObject p2;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        if (playerOneWins) {
            p2.SetActive(false);
            text.text = "PLAYER 1 WINS";
        } else {
            p1.SetActive(false);
            text.text = "PLAYER 2 WINS";
        }
    }

    // Update is called once per frame
    void Update() {
        if (Keyboard.current.anyKey.wasPressedThisFrame) {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
