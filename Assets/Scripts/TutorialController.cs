using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (PathController.shared.GetPassedWaypoints() < 3) {
            if (Keyboard.current.aKey.wasPressedThisFrame) {
                GetComponent<TextMeshProUGUI>().text = "Again, press A when the planet reaches the waypoint";
            }
        } else {
            GetComponent<TextMeshProUGUI>().text = "Now Player 2, press L when the plane reaches the waypoint";
            if (PathController.shared.GetPassedWaypoints() == 4) {
                Destroy(gameObject);
            }
        }
    }
}
