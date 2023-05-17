using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public GameObject MainImage;

    public void PushStartButton()
    {
        MainImage.SetActive(false);
        GameManager.instance.isGameStart = true;
        GameManager.instance.time_start = Time.time; //�޲ٴٽÿ�
    }

    public void PushEndButton()
    {
        Application.Quit();
    }
}
