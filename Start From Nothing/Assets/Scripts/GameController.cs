using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipableType {Seed, Sun}

public class GameController : MonoBehaviour
{
    public SunController sun;
    public CameraController cameraController;
    public TilemapController tilemapController;
    public ItemSpawner itemSpawner;
    public CompleteScreenController completeScreen;
    public ESCMenuController ESCMenu;
    public SoundManager soundManager;

    public Vector2 mousePosition;

    public IEquippable equippedItem = null;
    IEquippable hovering = null;

    int goalTilesRestored = 1000;
    public int tilesRestored = 0;

    public bool gameCompleteScreenShown = false;


    private void Start()
    {
        goalTilesRestored = tilemapController.tilemapWidth * tilemapController.tilemapHeight;
    }


    public void EquipItem(IEquippable item)
    {
        if (equippedItem == null)
        {
            if (item.Equip())
            {
                equippedItem = item;
                soundManager.PlaySound(transform.position, SoundType.EquipItem);
                Cursor.visible = false;
            }
        }
    }

    public void UnequipItem()
    {
        if (equippedItem != null)
        {
            if (equippedItem.Unequip())
            {
                equippedItem = null;
                Cursor.visible = true;
            }
        }
    }
    

    public IEquippable GetEquippedItem()
    {
        return equippedItem;
    }


    private void Update()
    {

        mousePosition = cameraController.cam.ScreenToWorldPoint(Input.mousePosition);

        if (equippedItem == null)
        {
            RaycastToEquip();

        }

        if(tilesRestored == goalTilesRestored && !gameCompleteScreenShown)
        {
            UnequipItem();
            completeScreen.Show();
            gameCompleteScreenShown = true;
        }


        if (Input.GetKeyDown(KeyCode.Escape) && equippedItem == null)
        {
            if (ESCMenu.isShowing)
            {
                ESCMenu.Hide();
            }
            else
            {
                ESCMenu.Show();
            }
        }
    }



    private void RaycastToEquip()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        IEquippable itemFound = null;
        if(hits != null)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if(hit.collider.gameObject.tag == "Equippable")
                {
                    itemFound = hit.collider.gameObject.GetComponent<IEquippable>();
                    break;
                }
            }

            if (itemFound != null)
            {
                if( hovering != itemFound)
                {
                    if (hovering != null)
                    {
                        hovering.OnRayExit();
                    }
                    hovering = itemFound;
                    hovering.OnRayEnter();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    EquipItem(itemFound);
                }
            }
            else
            {
                if (hovering != null)
                {
                    hovering.OnRayExit();
                    hovering = null;
                }
            }
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
