using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inv = new List<Item>();
    public bool showInv;
    public Item selectedItem;
    public Vector2 scrollpos = Vector2.zero;
    public int money;
    public bool showConsoleWindow;
    public float scrW = Screen.width / 16;
    public float scrH = Screen.height / 10;
    public Rect consoleWindowRect = new Rect(0, 0, 0, 0);
    public Rect inventoryWindowRect = new Rect(0, 0, 0, 0);
    public string idNumber;

    private int _selectedIndex; // for drage and drop stuff later

    void Start()
    {
        inv.Add(ItemDatabase.createItem(000));
        inv.Add(ItemDatabase.createItem(300));
        inv.Add(ItemDatabase.createItem(400));
    }

    public bool ToggleInv()
    {
        if (!showInv)
        {
            scrW = Screen.width / 16;
            scrH = Screen.height / 10;
            inventoryWindowRect = new Rect(.5f * scrW, .5f * scrH, 9 * scrW, 8 * scrH);
            consoleWindowRect = new Rect(15 * scrW, 0 * scrH, scrW, scrH);
            showInv = true;
            Cursor.visible = true;
            Time.timeScale = 0;
            return true;
        }
        else
        {
            showConsoleWindow = false;
            showInv = false;
            Cursor.visible = false;
            Time.timeScale = 1;
            return false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInv();
        }
        if (Input.GetKeyDown(KeyCode.I) && showInv)
        {
            showConsoleWindow = !showConsoleWindow;
        }
    }

    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 10;

        if (showInv)
        {
            GUI.Window(0, inventoryWindowRect, InventoryWindow, "Inventory");
        }

        if (showConsoleWindow)
        {
            consoleWindowRect = ClampToScreen(GUI.Window(1, consoleWindowRect, ConsoleWindow, "Console"));
        }
    }

    void ConsoleWindow(int windowID)
    {
        idNumber = GUI.TextField(new Rect(0, .25f * scrH, scrW, .25f * scrH), idNumber, 3);
        idNumber = Regex.Replace(idNumber, @"[^0-9]", "");
        if (GUI.Button(new Rect(0, .5f * scrH, scrW, .25f * scrH), "Add"))
        {
            inv.Add(ItemDatabase.createItem(System.Int32.Parse(idNumber)));
        }
        GUI.DragWindow();
    }

    void InventoryWindow(int windowID)
    {
        //background
        //GUI.Box(new Rect(scrW * 0.5f, scrH * 0.5f, scrW * 8, scrH * 8), "Inventory");
        //for loop list
        if (inv.Count <= 14)
        {
            for (int i = 0; i < inv.Count; i++)
            {
                //buttons for each inventory item
                if (GUI.Button(new Rect(.5f * scrW, .75f * scrH + i * (scrH * 0.5f), 3 * scrW, 0.5f * scrH), inv[i].Name))
                {
                    selectedItem = inv[i];
                }
            }
        }
        else
        {
            scrollpos = GUI.BeginScrollView(new Rect(.5f * scrW, .75f * scrH, 4 * scrW, 7 * scrH), scrollpos, new Rect(0, 0, 0, 7 * scrH + ((inv.Count - 14) * .5f * scrH)), false, true);
            for (int i = 0; i < inv.Count; i++)
            {
                //buttons for each inventory item
                if (GUI.Button(new Rect(0 * scrW, 0 * scrH + i * (scrH * 0.5f), 3 * scrW, 0.5f * scrH), inv[i].Name))
                {
                    selectedItem = inv[i];
                }
            }
            GUI.EndScrollView();
        }
        if (selectedItem != null)
        {
            if (selectedItem.Type == ItemType.Armour)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Equip"))
                {

                }
            }
            else if (selectedItem.Type == ItemType.Coins)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Drop"))
                {

                }
            }
            else if (selectedItem.Type == ItemType.Consumable)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Eat"))
                {

                }
            }
            else if (selectedItem.Type == ItemType.Craftable)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Craft"))
                {

                }
            }
            else if (selectedItem.Type == ItemType.Potion)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Drink"))
                {

                }
            }
            else if (selectedItem.Type == ItemType.Quest)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Use"))
                {

                }
            }
            else if (selectedItem.Type == ItemType.Weapon)
            {
                GUI.DrawTexture(new Rect(scrW * 4.5f, scrH, scrW * 2.5f, scrH * 2.5f), selectedItem.Icon);
                GUI.Box(new Rect(scrW * 4.5f, 3 * scrH, scrW * 2.5f, scrH * 4f), selectedItem.Name + "\n" + selectedItem.Description + "\nValue: " + selectedItem.Value + "\nHeal: " + selectedItem.Heal);
                if (GUI.Button(new Rect(scrW * 5.75f, 7.5f * scrH, scrW * 1.25f, scrH * 0.5f), "Equip"))
                {

                }
            }
        }
    }

    Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width-r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height-r.height);
        return (r);
    }
}
