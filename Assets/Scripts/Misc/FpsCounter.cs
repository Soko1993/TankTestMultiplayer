using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    public Text text;
    private int fps;
    void Start()
    {
        InvokeRepeating(nameof(ShowFPS), 1, 1);
    }

    private void ShowFPS()
    {
        fps = (int)(1.0f / Time.deltaTime);
        text.text = "FPS:" + fps.ToString();
    }
}
