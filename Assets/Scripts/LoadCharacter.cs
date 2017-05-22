using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCharacter : MonoBehaviour
{

    public GameObject player;
    public Renderer character;
    public string charName = "Adventurer";

    public int skinIndex, hairIndex, mouthIndex, eyeIndex;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        character = player.GetComponentInChildren<SkinnedMeshRenderer>();
        LoadTexture();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadTexture()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
            SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
            SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
            SetTexture("Eyes", PlayerPrefs.GetInt("EyeIndex"));
            charName = PlayerPrefs.GetString("PlayerName");
            player.name = charName;
        }
    }

    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int matIndex = 0;

        switch (type)
        {
            case "Skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "Hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "Mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "Eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 4;
                break;
        }

        Material[] mat = character.materials;
        mat[matIndex].mainTexture = texture;
        character.materials = mat;

    }
}
