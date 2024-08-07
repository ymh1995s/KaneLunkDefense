using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    AudioSource theAudio;
    public GameObject OverImage;
    [SerializeField] AudioClip Audio_GameOver;

    public void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    public void OverGame()
    {
        OverImage.SetActive(true);
        for (int i = 0; i < GameManager.instance.objectManager.enemy1.Length; i++) GameManager.instance.objectManager.enemy1[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy2.Length; i++) GameManager.instance.objectManager.enemy2[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy3.Length; i++) GameManager.instance.objectManager.enemy3[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy4.Length; i++) GameManager.instance.objectManager.enemy4[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy5.Length; i++) GameManager.instance.objectManager.enemy5[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy5.Length; i++) GameManager.instance.objectManager.enemy6[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy5.Length; i++) GameManager.instance.objectManager.enemy7[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy5.Length; i++) GameManager.instance.objectManager.enemy8[i].SetActive(false);
        /* 데모버전을 위한 주석
        theAudio.PlayOneShot(Audio_GameOver, 0.5f);
        */
        GameManager.instance.isGameClear = true;
    }


}
