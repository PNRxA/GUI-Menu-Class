using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCustomisation : MonoBehaviour
{

    public Renderer character;
    public string charName = "Adventurer";

    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();

    public int skinIndex, hairIndex, mouthIndex, eyeIndex;
    public int skinMax, hairMax, mouthMax, eyeMax;

    // Use this for initialization
    void Start()
    {

        character = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();

        for (int i = 0; i < skinMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(textureTemp);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(textureTemp);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(textureTemp);
        }
        for (int i = 0; i < eyeMax; i++)
        {
            Texture2D textureTemp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(textureTemp);
        }

        SetTexture("Skin", 0);
        SetTexture("Hair", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);

    }

    void Save()
    {
        PlayerPrefs.SetInt("SkinIndex", skinIndex);
        PlayerPrefs.SetInt("HairIndex", hairIndex);
        PlayerPrefs.SetInt("MouthIndex", mouthIndex);
        PlayerPrefs.SetInt("EyeIndex", eyeIndex);
        PlayerPrefs.SetString("PlayerName", charName);
    }

    void SetTexture(string type, int dir)
    {
        int index = 0, max = 0;
        Texture2D[] textures = new Texture2D[0];
        int matIndex = 0;

        switch (type)
        {
            case "Skin":
                index = skinIndex;
                max = skinMax;
                textures = skin.ToArray();
                matIndex = 1;
                break;
            case "Hair":
                index = hairIndex;
                max = hairMax;
                textures = hair.ToArray();
                matIndex = 2;
                break;
            case "Mouth":
                index = mouthIndex;
                max = mouthMax;
                textures = mouth.ToArray();
                matIndex = 3;
                break;
            case "Eyes":
                index = eyeIndex;
                max = eyeMax;
                textures = eyes.ToArray();
                matIndex = 4;
                break;
            default:
                break;
        }
        index += dir;
        if (index < 0)
        {
            index = max - 1;
        }
        if (index > max - 1)
        {
            index = 0;
        }

        Material[] mat = character.materials;
        mat[matIndex].mainTexture = textures[index];
        character.materials = mat;

        switch (type)
        {
            case "Skin":
                skinIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyeIndex = index;
                break;
        }
    }

    void OnGUI()
    {

        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        int i = 0;

        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            SetTexture("Skin", -1);
        }

        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Skin");

        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Skin", 1);
        }
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            SetTexture("Hair", -1);
        }

        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Hair");

        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Hair", 1);
        }
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            SetTexture("Mouth", -1);
        }

        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Mouth");

        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Mouth", 1);
        }
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), "<"))
        {
            SetTexture("Eyes", -1);
        }

        GUI.Box(new Rect(0.75f * scrW, scrH + i * (0.5f * scrH), scrW, 0.5f * scrH), "Eyes");

        if (GUI.Button(new Rect(1.75f * scrW, scrH + i * (0.5f * scrH), 0.5f * scrW, 0.5f * scrH), ">"))
        {
            SetTexture("Eyes", 1);
        }
        i++;
        charName = GUI.TextField(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), charName, 12);
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Random"))
        {
            SetTexture("Skin", Random.Range(0, skinMax - 1));
            SetTexture("Hair", Random.Range(0, hairMax - 1));
            SetTexture("Mouth", Random.Range(0, mouthMax - 1));
            SetTexture("Eyes", Random.Range(0, eyeMax - 1));
        }
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Reset"))
        {

            skinIndex = 0;
            hairIndex = 0;
            mouthIndex = 0;
            eyeIndex = 0;
            SetTexture("Skin", 0);
            SetTexture("Hair", 0);
            SetTexture("Mouth", 0);
            SetTexture("Eyes", 0);
        }
        i++;
        if (GUI.Button(new Rect(0.25f * scrW, scrH + i * (0.5f * scrH), 2 * scrW, 0.5f * scrH), "Create Character"))
        {
            Save();
            SceneManager.LoadScene(1);
        }
    }
}
