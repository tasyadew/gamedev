using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    private float alpha = 0f;
    private float fadeSpeed = 1f;
    public Text text;
    Color textColor;

    void Start()
    {
        textColor = text.color;
    }

    void Update()
    {
        alpha += fadeSpeed * Time.deltaTime;
        text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

        if (alpha >= 1f || alpha <= 0f)
        {
            fadeSpeed = -fadeSpeed;
        }
    }
}
    