using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    public AudioSource ring;
    public AudioSource jump;
    public AudioSource dead;
    public AudioSource Sign;
    public AudioSource EndLevelCard;
    public AudioSource music;
    public AudioSource spike;
    public AudioSource spring;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void RingSound()
    {
        ring.Play();
    }

    public void JumpSound()
    {
        jump.Play();
    }
    
    public void DeadSound()
    {
        dead.Play();
    }
    public void SpringSound()
    {
        spring.Play();
    }
    public void SpikeSOund()
    {
        spike.Play();
    }

    public void SignSound()
    {
        Sign.Play();
    }

    public void EndLevelMusic()
    {
        music.Stop();
        EndLevelCard.Play();
    }


    public void musicTransition()
    {
        StartCoroutine(FadeoutMusic(music));
    }

    IEnumerator FadeoutMusic(AudioSource musica)
    {
        float startVolume = musica.volume;
        while(musica.volume > 0)
        {
            musica.volume -= startVolume * Time.deltaTime / 1.5f;
            yield return null;
                }
    }
}
