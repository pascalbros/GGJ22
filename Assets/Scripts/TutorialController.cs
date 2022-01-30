using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    int playerOneCount = 0;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (playerOneCount < 2) {
            if (Keyboard.current.aKey.wasPressedThisFrame) {
                playerOneCount += 1;
                if (playerOneCount == 2) {
                    GetComponent<TextMeshProUGUI>().text = "Now Player 2, press L to Start";
                } else {
                    GetComponent<TextMeshProUGUI>().text = "Again, press A when the planet reaches the waypoint";
                }
            }
        } else {
            if (Keyboard.current.lKey.wasPressedThisFrame) {
                Destroy(gameObject);
            }
        }
    }
}
