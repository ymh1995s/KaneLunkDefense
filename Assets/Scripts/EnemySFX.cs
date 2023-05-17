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
        int SoundNo = Random.Range(0, 5);
        float soundVolumn = 0.16f;
        if (SoundNo == 4) soundVolumn = 0.12f; //아이고난5
        print(SoundNo);
        theAudio.PlayOneShot(Audio_EnemyDeadSound[SoundNo], soundVolumn);
    }

    public void BorderHitSound()
    {
        theAudio.PlayOneShot(Audio_EnemyCollisionSound, 0.3f);
    }
}
