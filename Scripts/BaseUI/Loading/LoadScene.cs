using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class AsyLoadScene
{
    private static float loadTime = 0;
    private const float LastLoadTime = 3.0f;
    public static string sceneName; //1
    private static AsyncOperation oper;

    public static void LoadScene() //2
    {
        oper = SceneManager.LoadSceneAsync(sceneName);
        if (oper == null)
            return;
        oper.allowSceneActivation = false;
        loadTime = 0;
    }

    public static void EnterScene()//4
    {
        if (oper == null)
            return;

        oper.allowSceneActivation = true;
    }

    //[0.0,1.0]
    public static float GetProgress()  //3
    {
        loadTime += Time.deltaTime;
        float a = loadTime / LastLoadTime;
        if (a < 0.98f)
        {
            return loadTime / LastLoadTime;
        }
        else
        {
            if(oper.progress < 0.89f)
            {
                return 0.98f;
            }
            else
            {
                return 1.0f;
            }
        } 

    }
}
