using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUIStyle muteButton;
    public GUISkin menuSkin;
    public Texture2D muteTex, unmuteTex;
    protected bool showOp, IsPaused;
    public bool mute;
    public float audioSlider, amSlider, dirSlider, volMute;
    public AudioSource audi;
    public Light dirLight;


    protected bool inGame = false;

    // Use this for initializations
    void Start()
    {
        audi = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        if (PlayerPrefs.HasKey("mute"))
        {
            RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("amLight");
            dirLight.intensity = PlayerPrefs.GetFloat("dirLight");
            audi.volume = PlayerPrefs.GetFloat("volume");

            if (PlayerPrefs.GetInt("mute") == 0)
            {
                mute = false;
                audi.volume = PlayerPrefs.GetFloat("volume");
                muteButton.normal.background = unmuteTex;
            }
            else
            {
                mute = true;
                audi.volume = 0;
                volMute = PlayerPrefs.GetFloat("volume");
                muteButton.normal.background = muteTex;
            }
        }
        else
        {
            muteButton.normal.background = unmuteTex;
        }

        audioSlider = audi.volume;
        dirSlider = dirLight.intensity;
        amSlider = RenderSettings.ambientIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSlider != audi.volume)
        {
            audi.volume = audioSlider;
        }
        if (dirSlider != dirLight.intensity)
        {
            dirLight.intensity = dirSlider;
        }
        if (amSlider != RenderSettings.ambientIntensity)
        {
            RenderSettings.ambientIntensity = amSlider;
        }
    }

    protected void OnGUI()
    {
        Menu();
    }

    protected void Menu()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "I am a box");
        GUI.skin = menuSkin;
        if (!showOp)
        {
            GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "I box");

            if (GUI.Button(new Rect(6 * scrW, 3 * scrH, 4 * scrW, scrH), "Play"))
            {
                SceneManager.LoadScene(2);
                inGame = true;
                Time.timeScale = 1;
            }
            if (GUI.Button(new Rect(6 * scrW, 5 * scrH, 4 * scrW, scrH), "Options"))
            {
                showOp = true;
            }
            if (GUI.Button(new Rect(6 * scrW, 7 * scrH, 4 * scrW, scrH), "Exit"))
            {
                Application.Quit();
            }
        }
        else
        {
            GUI.Box(new Rect(7 * scrW, 3 * scrH, 2 * scrW, scrH), "Volume");
            if (GUI.Button(new Rect(7 * scrW, 1.25f * scrH, 2 * scrW, scrH), "Gooey"))
            {
                ;
            }
            GUI.Box(new Rect(2 * scrW, 1.25f * scrH, 2 * scrW, scrH), "Volume");
            GUI.Box(new Rect(2 * scrW, 2.25f * scrH, 2 * scrW, scrH), "Brightness");
            GUI.Box(new Rect(2 * scrW, 3.25f * scrH, 2 * scrW, scrH), "DirLight");

            if (!mute)
            {
                audioSlider = GUI.HorizontalSlider(new Rect(2 * scrW, 2 * scrH, 2 * scrW, .5f * scrH), audioSlider, 0f, 1f);
            }
            else
            {
                GUI.HorizontalSlider(new Rect(2 * scrW, 2 * scrH, 2 * scrW, .5f * scrH), audioSlider, 0f, 1f);
            }

            amSlider = GUI.HorizontalSlider(new Rect(2 * scrW, 3 * scrH, 2 * scrW, .5f * scrH), amSlider, 0f, 8f);
            dirSlider = GUI.HorizontalSlider(new Rect(2 * scrW, 4 * scrH, 2 * scrW, .5f * scrH), dirSlider, 0f, 8f);
            GUI.skin = null;
            if (GUI.Button(new Rect(7 * scrW, 5 * scrH, 2 * scrW, scrH), "", muteButton))
            {
                ToggleMute();
            }
            GUI.skin = menuSkin;
            if (GUI.Button(new Rect(7 * scrW, 7 * scrH, 2 * scrW, scrH), "Back"))
            {
                SaveOptions();
                if (inGame)
                {
                    showOp = false;

                }
                else
                {
                    showOp = false;
                }
            }
        }


    }
    protected void PMenu()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.Box(new Rect(4 * scrW, 0.25f * scrH, 8 * scrW, 2 * scrH), "Paused");

        if (GUI.Button(new Rect(6 * scrW, 3 * scrH, 4 * scrW, scrH), "Resume"))
        {
            PauseMenu pause = GetComponent<PauseMenu>();
            pause.Pause();
        }
        if (GUI.Button(new Rect(6 * scrW, 5 * scrH, 4 * scrW, scrH), "Options"))
        {
            showOp = true;
        }
        if (GUI.Button(new Rect(6 * scrW, 7 * scrH, 4 * scrW, scrH), "Exit To Menu"))
        {
            SceneManager.LoadScene(0);
        }
    }

    protected void SaveOptions()
    {
        //PlayerPrefs.SetFloat("volume", audioSlider);
        PlayerPrefs.SetFloat("dirLight", dirSlider);
        PlayerPrefs.SetFloat("amLight", amSlider);

        if (!mute)
        {
            PlayerPrefs.SetInt("mute", 0);
            PlayerPrefs.SetFloat("volume", audioSlider);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 1);
            PlayerPrefs.SetFloat("volume", volMute);
        }
    }

    bool ToggleMute()
    {
        if (mute)
        {
            audioSlider = volMute;
            mute = false;
            muteButton.normal.background = unmuteTex;
            return false;
        }
        else
        {
            volMute = audioSlider;
            audioSlider = 0;
            mute = true;
            muteButton.normal.background = muteTex;
            return true;
        }
    }

}
