using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int startingLives;
    public Transform spawnPoint;
    //public SceneAsset nextScene;
    public TimeManager tm;

    [Header("Objectives")]
    public LightBulbObjective[] levelObjectives;
    int bulbsCollected = 0;

    public AudioSource levelMusic;
    // PlayerSounds ps;

    // Start is called before the first frame update
    void Start()
    {
        // ps = GetComponent<PlayerSounds>();

        GameManager.instance.lives = startingLives;
        GameManager.instance.SpawnPlayer(spawnPoint);
        GameManager.instance.currentLevel = this;

        //if (nextScene) GameManager.instance.nextScene = nextScene;

    }

    private void LateUpdate()
    {
        bool allObjectivesComplete = true;
        for(int i = 0; i < levelObjectives.Length; i ++)
        {
            if (levelObjectives[i])
            {
                if (levelObjectives[i].IsOn == false)
                {
                    allObjectivesComplete = false;
                    break;
                }
            }
        }
        GameManager.instance.IsWinConditionMet = allObjectivesComplete;
       // Debug.Log("has Won = " + GameManager.instance.IsWinConditionMet);
    }

    public void PauseLevelMusic()
    {
        if (levelMusic)
        {
            levelMusic.Pause();
        }
    }

    public void LevelHasBeenCompleted()
    {
        // Play sounds, animations, music and all other things that happen when the player wins
        CanvasManager canvas = GameManager.instance.currentCanvas;
        canvas.lightBulbImg.enabled = true;
        canvas.titleText.text = "All Bulbs Lit";
        canvas.lightBulbsCollectedText.text = bulbsCollected.ToString("00") + "/" + levelObjectives.Length;
        canvas.lightBulbsCollectedText.enabled = true;
        canvas.startButton.onClick.AddListener(() => GameManager.instance.ReloadScene());
        canvas.gameObject.SetActive(true);
        tm.StopTime();
    }

    public void LevelHasBeenLost()
    {
        CanvasManager canvas = GameManager.instance.currentCanvas;
        canvas.lightBulbImg.enabled = true;
        canvas.titleText.text = "Light Exstinguished";
        canvas.lightBulbsCollectedText.text = bulbsCollected.ToString("00") + "/" + levelObjectives.Length;
        canvas.lightBulbsCollectedText.enabled = true;
        canvas.startButton.onClick.AddListener(() => GameManager.instance.ReloadScene());
        canvas.gameObject.SetActive(true);
        tm.StopTime();
    }

    public void BulbCollected()
    {
        bulbsCollected += 1;
    }
}