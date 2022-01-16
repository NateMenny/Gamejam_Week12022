using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool playGame = false;
    // Update is called once per frame
    void Update()
    {
        if (playGame)
        {
            if (GameManager.instance.playerInstance)
            {
                Rigidbody2D rb = GameManager.instance.playerInstance.GetComponent<Rigidbody2D>();
                float playerMaxVel = GameManager.instance.playerInstance.GetComponent<PlayerMovement2D>().maxVel;

                if (playerMaxVel >= 1f)
                {
                    if (rb.velocity.magnitude / playerMaxVel > 0.97f)
                    {

                        Time.timeScale = 0.5f;

                    }
                    else
                    {
                        Time.timeScale = 1f - (rb.velocity.magnitude / playerMaxVel) / 2;
                    }
                    Time.fixedDeltaTime = Time.timeScale * .02f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
        }
        else
        {
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

        if(GameManager.instance.IsWinConditionMet)
        {
            GameManager.instance.currentLevel.LevelHasBeenCompleted();
            StopTime();
        }
    }

    public void StartTime()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameManager.instance.currentCanvas.gameObject.SetActive(false);
        playGame = true;
    }

    public void StopTime()
    {
        Cursor.visible = true;
        GameManager.instance.currentCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

}
