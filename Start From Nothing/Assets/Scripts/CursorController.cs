using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    GameController gameController;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = gameController.mousePosition;
    }

    public void SetVisible(bool visible)
    {
        if (!visible)
        {
            Color newColor = sr.color;
            newColor.a = 0f;
            sr.color = newColor;
        }
        else
        {
            Color newColor = sr.color;
            newColor.a = 1f;
            sr.color = newColor;
        }
    }
}
