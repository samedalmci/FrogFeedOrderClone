using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("General Stuff")]
    public static GameManager instance;
    public string levelName;

    [Header("Scenes")]
    public GameObject LostScene;
    public GameObject WonScene;
  

    [Header("UI Elements")]
   
    public Image black;
    public Animator anim;
    public TMP_Text TextCurrentMoves;

    [Header("Game Stats")]
    public int minimumMoveLimit;
    public int currentMoves;
    public int totalMoves;
    public int frogCount;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentMoves = totalMoves;
        Time.timeScale = 1f;
    }

   

   
    void Update()
    {
        // Check if current moves are less than minimum move limit to trigger game over
        if (currentMoves < minimumMoveLimit)
        {
            LostScene.SetActive(true);
            Sound_Grape.instance.LevelLostSFX();
        }

        // Check if all frogs are collected to trigger level completion
        if (frogCount == 0)
        {
            WonScene.SetActive(true);
            Sound_Grape.instance.LevelWonSFX();
        }

        TextCurrentMoves.text = (currentMoves.ToString() + "Move");

    }

    IEnumerator FadeOutAndLoadScene(bool loadNextScene)
    {
        // Fade screen to black for scene transitions
        anim.SetBool("FadeBool", true);
        yield return new WaitUntil(() => black.color.a == 1);

        // Load next scene if specified, otherwise reload current scene
        if (loadNextScene)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        Time.timeScale = 0f;
    }
     

    public void FadeGameOver() 
    {
        StartCoroutine(FadeOutAndLoadScene(false));
    }
    public void FadeLevelCompleted()
    {

        StartCoroutine(FadeOutAndLoadScene(true));
    }


   


}
