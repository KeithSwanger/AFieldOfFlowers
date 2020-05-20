using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Tilemap cameraBoundTilemap;
    float cameraHeight;
    float cameraWidth;

    public float cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
        cameraHeight = 2f * cam.orthographicSize;
        cameraWidth = cameraHeight * cam.aspect;
        
        Vector3 newPosition = cam.transform.position;

        if (newPosition.x < cameraWidth / 2f + cameraBoundTilemap.transform.position.x)
        {
            newPosition.x = cameraWidth / 2f + cameraBoundTilemap.transform.position.x;
        }
        else if (newPosition.x > cameraBoundTilemap.transform.position.x + (cameraBoundTilemap.cellBounds.xMax * cameraBoundTilemap.cellSize.x) - cameraWidth / 2f)
        {
           newPosition.x = cameraBoundTilemap.transform.position.x + (cameraBoundTilemap.cellBounds.xMax * cameraBoundTilemap.cellSize.x) - (cameraWidth / 2f);
        }

        if (newPosition.y < cameraHeight / 2f + cameraBoundTilemap.transform.position.y)
        {
            newPosition.y = cameraHeight / 2f + cameraBoundTilemap.transform.position.y;
        }
        else if (newPosition.y > cameraBoundTilemap.transform.position.y + (cameraBoundTilemap.cellBounds.yMax * cameraBoundTilemap.cellSize.y) - cameraHeight / 2f)
        {
            newPosition.y = cameraBoundTilemap.transform.position.y + (cameraBoundTilemap.cellBounds.yMax * cameraBoundTilemap.cellSize.y) - (cameraHeight / 2f);
        }

        cam.transform.position = newPosition;
        
        
    }

    public Vector2 GetCameraSize()
    {
        return new Vector2(cameraWidth, cameraHeight);
    }

    private void HandleInput()
    {
        Vector3 moveVector = new Vector2();
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = Input.GetAxisRaw("Vertical");

        if (moveVector != Vector3.zero)
        {
            moveVector = moveVector.normalized;
        }

        if (moveVector != Vector3.zero)
        {
            cam.transform.position += moveVector * cameraSpeed * Time.deltaTime;
        }
    }
}
