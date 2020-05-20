using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, IEquippable
{
    private bool equipped = false;
    private bool wasEquipped = false;
    private GameController gameController;
    private SimpleAnimator anim;

    EquipableType equipableType = EquipableType.Seed;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<SimpleAnimator>();
        anim.spriteRenderer.sortingOrder = (int)(-transform.position.y * 100);
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
            
            PerformEquippedBehaviour();
        }

        if(!wasEquipped && Input.GetMouseButtonUp(0) && equipped)
        {
            wasEquipped = true;
        }

        anim.spriteRenderer.sortingOrder = (int)(-transform.position.y * 100);

    }

    void PerformEquippedBehaviour()
    {
        Vector2 newPosition = gameController.mousePosition;
        transform.position = newPosition;

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameController.UnequipItem();
        }

        if (Input.GetMouseButtonUp(0) && wasEquipped)
        {
            if (gameController.tilemapController.IsWithinBounds(gameController.mousePosition))
            {
                Vector3Int cell = gameController.tilemapController.tilemap.WorldToCell(gameController.mousePosition);

                if(gameController.tilemapController.groundTileData[cell.x, cell.y].primaryGrowthComplete)
                {
                    gameController.itemSpawner.SpawnItem(gameController.mousePosition, ItemType.Flower);
                    gameController.UnequipItem();
                    gameController.soundManager.PlaySound(transform.position, SoundType.PlantSeed);
                    GameObject.Destroy(this.gameObject);
                }
            }
        }


    }

    public bool Equip()
    {
        equipped = true;
        wasEquipped = false;
        anim.Reset();

        return true;
    }

    public bool Unequip()
    {
        equipped = false;
        anim.Play();
        return true;
    }

    public void OnRayEnter()
    {

    }

    public void OnRayExit()
    {
        //throw new System.NotImplementedException();
    }

    public EquipableType GetEquipableType()
    {
        return equipableType;
    }
}
