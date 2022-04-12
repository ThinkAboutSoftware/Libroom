using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManagement : MonoBehaviour
{
    public GameObject BackgroundMusic;
    public AudioSource backmusic;

    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        // BackgroundMusic = GameObject.FindGameObjectWithTag("Music");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        // if (backmusic.isPlaying) return; //배경음악이 재생되고 있다면 패스
        if (obj.Length > 1)
        {
            Destroy(this.BackgroundMusic);
        }
        DontDestroyOnLoad(this.BackgroundMusic);
    }

    public void BackGroundMusicOffButton() //배경음악 키고 끄는 버튼
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        // backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        if (backmusic.isPlaying)
        {
            backmusic.Pause();
            SceneManager.LoadScene("AR");
        }
        else backmusic.Play();
    }

    public void ExplainScene()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        BackgroundMusic = GameObject.FindGameObjectWithTag("Music");
        // backmusic = BackgroundMusic.GetComponent<AudioSource>(); //배경음악 저장해둠
        SceneManager.LoadScene("GameExplain");
        if (obj.Length > 1)
        {
            Destroy(BackgroundMusic);
        }
        DontDestroyOnLoad(BackgroundMusic);
    }
}