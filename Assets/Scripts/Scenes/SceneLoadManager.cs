using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{

    public void ChangeToHuntingScene()
    {
        SceneManager.LoadSceneAsync("HuntingScene");
    }

    public void ChangeToStartingScene()
    {
        SceneManager.LoadSceneAsync("StartingScene");
    }

    public void ChangeToCampingScene()
    {
        SceneManager.LoadSceneAsync("CampingScene");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

}
