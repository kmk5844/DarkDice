using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fixed : MonoBehaviour
{
    private void Awake()
    {
        SetResoultion();   
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetUpResolution(int setWidth, int setHeight)
    {
        CanvasScaler canvasScaler = FindObjectOfType<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2 (setWidth, setHeight);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }

    public void SetResoultion(int setWidth = 1920, int setHeight = 1080)
    {
        SetUpResolution(setWidth, setHeight);
        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
}
