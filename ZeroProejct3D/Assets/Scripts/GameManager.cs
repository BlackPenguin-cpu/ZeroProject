using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshPro timerText;
    public float curDuration = startDuration;

    [SerializeField]
    private const float startDuration = 30f;

    private void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        curDuration -= Time.fixedDeltaTime;

        timerText.text = String.Format("{0:0#.0#}", curDuration);
    }
}