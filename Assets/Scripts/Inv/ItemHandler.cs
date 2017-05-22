using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public int _id = 000;
    public string _name = "";
    public string _description = "";
    public string _mesh = "";
    public string _icon = "";
    public ItemType _type = ItemType.Craftable;
    public int _value = 0;
    public int _heal = 0;
    public int _damage = 0;
    public int _amount = 0;

    public void OnCollection()
    {
        Item temp = new Item();

        temp.Name = _name;
        temp.ID = _id;
        temp.Description = _description;
        temp.Type = _type;
        temp.Value = _value;
        temp.Damage = _damage;
        temp.Heal = _heal;
        temp.MeshName = _mesh;
        temp.Amount = _amount;
        temp.Icon = Resources.Load("Icons/" + _icon) as Texture2D;

        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        if (temp.Type == ItemType.Coins)
        {
            inventory.money += temp.Amount;
        }
        else if (temp.Type == ItemType.Consumable || temp.Type == ItemType.Craftable)
        {
            int found = 0;
            int addMe = 0;
            for (int i = 0; i < inventory.inv.Count; i++)
            {
                if (temp.ID == inventory.inv[i].ID)
                {
                    found = 1;
                    addMe = i;
                }
            }
            if (found == 1)
            {
                inventory.inv[addMe].Amount += temp.Amount;
            }
            else
            {
                inventory.inv.Add(temp);
            }
        }
        else
        {
            inventory.inv.Add(temp);
        }
        Destroy(this.gameObject);
    }
}
