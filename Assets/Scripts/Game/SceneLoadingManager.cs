using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoadingManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static SceneLoadingManager _instance; // SingleTon  private instance
    private string currentloadedScene { get; set; }
    #region Singleton
    public static SceneLoadingManager Instance
    {
        get
        {
            //create logic to create the instance 
            if(_instance == null)
            {
                GameObject go = new GameObject("SceneManager");
                go.AddComponent<SceneLoadingManager>();
                go.tag = "SceneManager";
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    #endregion
    void Awake()
    {
        _instance = this; // Assinging this object to the _instance 
    }

 

    public string GetCurrentLoadedScene()
    {
        return currentloadedScene;
    }
    #region Loading Different Scenes
    public void LoadTitleScene()  // loading title scene
    {
        SceneManager.LoadScene("TitleScene");
        currentloadedScene = "TitleScene";
    }

    public void LoadSplashScene() // loading splash scene
    {
        SceneManager.LoadScene("SplashScene");
        currentloadedScene = "SplashScene";
    }

    public void LoadMainScene() // loading main scene 
    {
        SceneManager.LoadScene("MainScene");
        currentloadedScene = "MainScene";
    }

    public void LoadCharacterSelectionScene() // loading main scene 
    {
        SceneManager.LoadScene("CharacterSelectionScene");
        currentloadedScene = "CharacterSelectionScene";
    }

    public void LoadMultiPlayerScene()
    {
        SceneManager.LoadScene("CoopScene");
    }
    #endregion

}
