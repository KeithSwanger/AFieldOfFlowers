using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public float growthSlowingModifier;
    private float primaryGrowth = 0f;
    private bool primaryGrowthComplete = false;
    private SimpleAnimator anim;

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<SimpleAnimator>();
        anim.Reset();
        anim.spriteRenderer.sortingOrder = (int)(-transform.position.y * 100);
        
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Equippable" && gameController.sun.IsSunActive() && gameController.equippedItem != null && gameController.equippedItem.GetEquipableType() == EquipableType.Sun)
        {
            AddGrowth(gameController.sun.sun.transform.position, gameController.sun.growMultiplier);
        }
    }


    public void AddGrowth(Vector2 location, float growMultiplier)
    {
        if (!primaryGrowthComplete)
        {
            float distance = Vector2.Distance(transform.position, location);

            if (distance < 1)
            {
                distance = 1;
            }

            primaryGrowth += growMultiplier / distance / growthSlowingModifier * Time.deltaTime;

            if (primaryGrowth > 1f)
            {
                primaryGrowth = 1f;
                primaryGrowthComplete = true;

                SpawnPowerups();
                gameController.soundManager.PlaySound(transform.position, SoundType.LaunchPetals);
            }

            anim.SetFrameToPercentage(primaryGrowth);
        }
    }

    private void SpawnPowerups()
    {
        Vector2 spawnLocation = this.transform.position;
        spawnLocation.y += 1.7f;

        for (int i = 0; i < UnityEngine.Random.Range(1, 50); i++)
        {
            gameController.itemSpawner.SpawnItem(spawnLocation, ItemType.SunPowerup);
        }
    }
}
