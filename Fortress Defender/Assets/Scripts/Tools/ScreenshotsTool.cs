using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScreenshotsTool : MonoBehaviour
{
    [Header("Screenshot key")]
    public KeyCode screenShotButton;
    public int screenshotCounter;

    private void ScreenshotCapture()
    {
        if (Input.GetKeyDown(screenShotButton))
        {
            Time.timeScale = 0;

            ScreenCapture.CaptureScreenshot(
            $"screenshot{screenshotCounter}.png");
            Debug.Log("A screenshot was taken!");
            screenshotCounter++;
        }
    }

    void Update()
    {
        ScreenshotCapture();
    }
}
