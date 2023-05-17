using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    AudioSource theAudio;
    public GameObject ClearImage;
    [SerializeField] AudioClip Audio_GameClear;

    public void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    public void PushEndButton()
    {
        Application.Quit();
    }

    public void ClearGame()
    {
        ClearImage.SetActive(true);
        for (int i = 0; i < GameManager.instance.objectManager.enemy1.Length; i++) GameManager.instance.objectManager.enemy1[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy2.Length; i++) GameManager.instance.objectManager.enemy2[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy3.Length; i++) GameManager.instance.objectManager.enemy3[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy4.Length; i++) GameManager.instance.objectManager.enemy4[i].SetActive(false);
        for (int i = 0; i < GameManager.instance.objectManager.enemy5.Length; i++) GameManager.instance.objectManager.enemy5[i].SetActive(false);
        theAudio.PlayOneShot(Audio_GameClear, 0.5f);
        GameManager.instance.isGameClear = true;
    }
}
