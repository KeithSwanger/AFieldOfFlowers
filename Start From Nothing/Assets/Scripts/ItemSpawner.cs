using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Seed, Flower, SunPowerup }

public class ItemSpawner : MonoBehaviour
{
    GameController gameController;
    public GameObject seed;
    public GameObject[] flowers;
    public GameObject sunPowerup;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void SpawnItem(Vector2 location, ItemType itemType)
    {
        if(itemType == ItemType.Seed)
        {
            Instantiate(seed, location, Quaternion.identity);
            gameController.soundManager.PlaySound(location, SoundType.SeedAppears);
        }
        else if(itemType == ItemType.Flower)
        {
            int flowerToSpawn = UnityEngine.Random.Range(0, flowers.Length);
            Instantiate(flowers[flowerToSpawn], location, Quaternion.identity);
        }
        else if(itemType == ItemType.SunPowerup)
        {
            Instantiate(sunPowerup, location, Quaternion.identity);
        }
    }
}
