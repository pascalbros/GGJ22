using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressBar : MonoBehaviour
{
    public static GameProgressBar shared;

    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        shared = this;
        SetPercentage(0);
    }

    private void OnDestroy() {
        shared = null;
    }

    public void SetPercentage(float value) {
        var width = Screen.width * 0.3f;
        rectTransform.sizeDelta = new Vector2(width - (value * width), 2);
    }
}
