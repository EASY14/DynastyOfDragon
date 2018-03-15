using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string SceneName = "";

    public void NextScene()
    {
        Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = SceneName;
        SceneManager.LoadScene("Loading");
    }
    public void NextScene(string name)
    {
        Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = name;
        SceneManager.LoadScene("Loading");
    }

    public void ResetScene()
    {
        Time.timeScale = 1.0f;
        AsyLoadScene.sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Loading");
    }

}
