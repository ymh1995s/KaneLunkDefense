using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    AudioSource theAudio;
    [SerializeField] AudioClip[] Audio_EnemyDeadSound;
    [SerializeField] AudioClip Audio_EnemyCollisionSound;

    void Start()
    {
        theAudio = GetComponent<AudioSource>();

    }

    public void DeadSound()
    {
        /* 데모버전을 위한 주석
        int SoundNo = Random.Range(0, 5);
        float soundVolumn = 0.16f;
        if (SoundNo == 4) soundVolumn = 0.12f; //아이고난5
        theAudio.PlayOneShot(Audio_EnemyDeadSound[SoundNo], soundVolumn);
        */
    }

    public void BorderHitSound()
    {
        /* 데모버전을 위한 주석
        theAudio.PlayOneShot(Audio_EnemyCollisionSound, 0.25f);
        */
    }
}
