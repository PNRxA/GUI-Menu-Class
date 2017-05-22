using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            if (Physics.Raycast(interact, out hitInfo, 10))
            {
                if (hitInfo.collider.CompareTag("Item"))
                {
                    Debug.Log("Hit item!");
                    ItemHandler handler = hitInfo.transform.GetComponent<ItemHandler>();
                    if (handler != null)
                    {
                        handler.OnCollection();
                    }
                }
            }
        }
    }
}
