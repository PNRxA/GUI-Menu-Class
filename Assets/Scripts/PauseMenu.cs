using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MainMenu
{

    public float timer;
    public int secs, mins;
    public string clockTime;
    public RenderTexture miniMap;
    public float maxHealth, curHealth;

    //  public bool IsPaused = false;
    private bool myflag = false;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        mins = Mathf.FloorToInt(timer / 60);
        secs = Mathf.FloorToInt(timer - mins * 60);
        clockTime = string.Format("{0:0}:{1:00}", mins, secs);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (showOp)
            {
                showOp = false;
            }
            else
            {
                Pause();
            }

        }

        inGame = true;
    }

    void LateUpdate()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth < 0)
        {
            curHealth = 0;
        }
    }

    public void Pause()
    {
        Cursor.visible = !Cursor.visible;
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            Time.timeScale = 0;
            SaveOptions();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void OnGUI()
    {
        if (IsPaused && !showOp)
        {
            PMenu();
        }
        if (IsPaused && showOp)
        {
            Menu();
        }
        if (!IsPaused)
        {
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;

            // Timer
            GUI.Label(new Rect(7.5f * scrW, 0.25f * scrH, scrW, scrH), clockTime);
            // Map
            GUI.DrawTexture(new Rect(13.5f * scrW, 0.25f * scrH, 2f * scrW, 1.5f * scrH), miniMap);
            // Health
            GUI.Box(new Rect(6 * scrW, 0.25f * scrH, curHealth * (4f * scrW) / maxHealth, 0.25f * scrH), "");
        }
    }


    /* public bool TogglePauseShit()
     {
         if(IsPaused)
         {
             IsPaused = false;
             return false;
         }
         else
         {
             IsPaused = true;
             return true;

         }
     }*/

}
