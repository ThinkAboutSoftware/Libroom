using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void SceneChangeToGameMain()
    {
        SceneManager.LoadScene("AR");
    }

    public void LoadingScene()
    {
        LoadingSceneController.LoadScene("BeforeMainGame");
    }

    public void SceneChangeToGameExplain()
    {
        SceneManager.LoadScene("GameOption");
    }

    public void SceneChangeToGameIntro()
    {
        SceneManager.LoadScene("GameIntro");
    }

    public void SceneChangeToGameInformation()
    {
        SceneManager.LoadScene("GameInformation");
    }

    public void SceneChangeToGameCreator()
    {
        SceneManager.LoadScene("GameCreator");
    }

    public void SceneChangeToARDrawing()
    {
        SceneManager.LoadScene("ARDrawing");
    }

    public void SceneChangeToPainter()
    {
        SceneManager.LoadScene("Painter");
    }

    public void SceneChaneToAndroidCamera()
    {
        SceneManager.LoadScene("AndroidCamera");
    }

    public void SceneChangeToBookReport()
    {
        SceneManager.LoadScene("BookReportPage");
    }

    public void SceneChangeToBeforeMainGame()
    {
        SceneManager.LoadScene("BeforeMainGame");
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
