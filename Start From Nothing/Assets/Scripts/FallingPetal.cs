using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPetal : MonoBehaviour
{
    MainMenuController mainMenu;

    Camera cam;

    float speed = 10;

    Vector2 startingLocation;
    Vector2 endingLocation;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenuController").GetComponent<MainMenuController>();
        cam = mainMenu.mainCamera;

        startingLocation = cam.transform.position;
        startingLocation.y += cam.orthographicSize * 2f + 2f;
        startingLocation.x += UnityEngine.Random.Range(-20, 20);
        transform.position = startingLocation;

        endingLocation = cam.transform.position;
        endingLocation.y -= (cam.orthographicSize * 2f + 5f);
        endingLocation.x += UnityEngine.Random.Range(-20, 20);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endingLocation, speed * Time.deltaTime);
        

        if(transform.position.y <= endingLocation.y + 2f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
