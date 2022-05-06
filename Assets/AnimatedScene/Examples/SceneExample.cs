using FM;
using UnityEngine;

public class SceneExample : MonoBehaviour {
    // Functions in Script
    AsyncOperation async;

    public void ChangeSceneAsync(string sceneName) {
        async = SceneLoader.LoadSceneAsync(sceneName);
    }

    public void ChangeScene(string sceneName) {
        SceneLoader.LoadScene(sceneName);
    }

    public void ChangeSceneWithIndex(int index) {
        SceneLoader.LoadScene(index);
    }

    public void ChangeSceneAsynWithIndex(int index) {
        async = SceneLoader.LoadSceneAsync(index);
    }

    void Start() {
        Debug.Log(SceneLoader.GetActiveScene().name);
        SceneLoader.AddOnAnimDoneListener(() => {
            Debug.Log("Animation Complete");
        });
    }

    void Update() {
        if (async != null) {
            Debug.Log(async.progress);
        }
    }
    // Functions in Script

}