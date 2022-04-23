using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlay : MonoBehaviour
{
    public AudioSource audioSource;
    private GameObject[] musics;
    // public string AR;

    public void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");
        audioSource = GetComponent<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();

        if (musics.Length > 1)
        {
            Destroy(transform.gameObject);
        }

        if (scene.name != "GameIntro")
        {
            Destroy(transform.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);

    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Stop();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayBtn()
    {
        // musics = GameObject.FindGameObjectsWithTag("Music");
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "AR")
        {
            Destroy(transform.gameObject);
        }
        if (scene.name != "GameIntro")
        {
            DontDestroyOnLoad(transform.gameObject);
        }
        // SceneManager.LoadScene("BeforeMainGame");
    }



    #region GameIntroScene & GameExplain
    // public void GameIntroScene()
    // {
    //     if (musics.Length == 1)
    //     {
    //         Destroy(this.gameObject);
    //     }
    //     SceneManager.LoadScene("GameIntro");
    // }

    // public void GameExplain()
    // {
    //     SceneManager.LoadScene("GameExplain");

    //     if (musics.Length == 1)
    //     {
    //         DontDestroyOnLoad(this.gameObject);
    //     }
    //     if (musics.Length >= 2)
    //     {
    //         Destroy(this.gameObject);
    //     }
    // }
    #endregion
}
