using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPowerup : MonoBehaviour
{
    GameController gameController;
    SimpleAnimator anim;
    Launcher launcher;

    bool landed = false;

    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<SimpleAnimator>();
        launcher = GetComponent<Launcher>();
        launcher.Launch();
        anim.PlayFromRandom();
        launcher.Landed += Landed;
    }

    // Update is called once per frame
    void Update()
    {
        if (landed)
        {
            MoveTowardSun();
        }
    }

    private void MoveTowardSun()
    {
        Vector2 newPosition = transform.position;
        newPosition = Vector2.MoveTowards(newPosition, gameController.sun.sun.transform.position, speed * Time.deltaTime);
        transform.position = newPosition;
        

        if(Vector2.Distance(newPosition, gameController.sun.sun.transform.position) <= 0.05f)
        {
            gameController.sun.PowerUp();
            GameObject.Destroy(this.gameObject);
        }
    }

    private void Landed()
    {
        landed = true;
    }
}
