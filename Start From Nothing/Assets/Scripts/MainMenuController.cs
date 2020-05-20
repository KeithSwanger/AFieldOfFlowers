using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject fallingPetal;

    float petalSpawnTime = 0.2f;
    float petalSpawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        petalSpawnTimer = petalSpawnTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(petalSpawnTimer <= 0f)
        {
            Instantiate(fallingPetal);
            petalSpawnTimer = petalSpawnTime;
        }


        petalSpawnTimer -= Time.deltaTime;

    }
}
