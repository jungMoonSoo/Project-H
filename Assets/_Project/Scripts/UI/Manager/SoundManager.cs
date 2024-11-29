using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioClip[] effectSound;
    [SerializeField] GameObject baseObj;

    public void PlaySound(int index)  //효과음을 넣는 함수
    {
        AudioSource sound = null;

        for (int i = 0; i < transform.childCount; i++)  //하위 객체 중에 재생 중인 음악이 없는 경우는 그냥 사용
        {
            if(transform.GetChild(i).GetComponent<AudioSource>().isPlaying == false)
            {
                sound = transform.GetChild(i).GetComponent<AudioSource>();
                break;
            }
        }

        if (sound == null) //모두 재생 중인 음악이 있는 경우에는 새로운 객체를 만들어서 사용
        {
            GameObject soundObj = Instantiate(baseObj, transform);
            sound = soundObj.GetComponent<AudioSource>();
        }

        sound.clip = effectSound[index];
        sound.loop = false;
        sound.Play();
    }


    public void StopSound(int id) //재생 중인 음악을 종료하는 부분
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().clip == effectSound[id] &&
                transform.GetChild(i).GetComponent<AudioSource>().isPlaying)
            {
                transform.GetChild(i).GetComponent<AudioSource>().Stop();
                break;
            }
        }
    }
}
