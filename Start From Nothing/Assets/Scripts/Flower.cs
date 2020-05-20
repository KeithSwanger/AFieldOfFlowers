using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    GameController gameController;

    bool primaryGrowthComplete = false;
    float primaryGrowth = 0f;
    float growthSlowingModifier = 5;
    private SimpleAnimator anim;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<SimpleAnimator>();
        anim.Reset();
        anim.spriteRenderer.sortingOrder = (int)(-transform.position.y * 100);

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
            }

            anim.SetFrameToPercentage(primaryGrowth);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Equippable" && gameController.sun.IsSunActive() && gameController.equippedItem != null && gameController.equippedItem.GetEquipableType() == EquipableType.Sun)
        {
            AddGrowth(gameController.sun.sun.transform.position, gameController.sun.growMultiplier);
        }
    }
}
