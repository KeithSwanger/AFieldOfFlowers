using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour, IEquippable
{
    GameController gameController;

    EquipableType equipableType = EquipableType.Sun;


    private bool equipped = false;

    public SimpleAnimator anim;
    private SpriteRenderer spriteRenderer;

    public Action<Vector2, float> ReceiveSun;
    
    private CameraController cam;
    public GameObject sun;

    public float sunSpeed;

    public float growMultiplier;

    public float fullPower;
    public Transform[] fullRays;

    public float halfPower;
    public Transform[] halfRays;

    public float quarterPower;
    public Transform[] quarterRays;

    public float eigthPower;
    public Transform[] eigthRays;

    private bool active = false;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cam = gameController.cameraController;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.Play();
        spriteRenderer = anim.GetSpriteRenderer();
        spriteRenderer.color = new Color(1, 1, 1, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
            RunSunBehaviour();
        }



    }

    private void LateUpdate()
    {
        if (!equipped)
        {
            RunUIBehaviour();
        }
    }

    void RunUIBehaviour()
    {
        Vector2 cameraSize = cam.GetCameraSize();
        Vector2 newPosition = cam.cam.gameObject.transform.position;
        newPosition.x += cameraSize.x / 2 - 1.5f;
        newPosition.y += cameraSize.y / 2 - 1.5f;
        sun.transform.position = newPosition;
    }



    private void RunSunBehaviour()
    {
        Vector2 newPosition = cam.cam.ScreenToWorldPoint(Input.mousePosition);
        sun.transform.position = Vector2.MoveTowards(sun.transform.position, newPosition, sunSpeed * Time.deltaTime);

        

        if (active)
        {
            SendSun();
        }

        if (Input.GetMouseButton(0))
        {
            active = true;
            anim.Play();
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            active = false;
            anim.Reset();
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            gameController.UnequipItem();
        }


    }

    private void SendSun()
    {
        foreach (Transform location in fullRays)
        {
            ReceiveSun.Invoke(location.transform.position, fullPower * growMultiplier * Time.deltaTime);
        }

        foreach (Transform location in halfRays)
        {
            ReceiveSun.Invoke(location.transform.position, halfPower * growMultiplier * Time.deltaTime);
        }

        foreach (Transform location in quarterRays)
        {
            ReceiveSun.Invoke(location.transform.position, quarterPower * growMultiplier * Time.deltaTime);
        }

        foreach (Transform location in eigthRays)
        {
            ReceiveSun.Invoke(location.transform.position, eigthPower * growMultiplier * Time.deltaTime);
        }
        
    }

    public bool IsSunActive()
    {
        return active;
    }

    public bool Equip()
    {
        equipped = true;
        return true;
    }

    public void PowerUp()
    {
        growMultiplier += 0.015f;

        gameController.soundManager.PlaySound(transform.position, SoundType.Powerup);
        
    }

    public bool Unequip()
    {
        equipped = false;
        active = false;
        anim.Reset();
        return true;
    }

    private void OnMouseDown()
    {
    }
    
    private void OnMouseOver()
    {
        
    }

    private void OnMouseExit()
    {
    }

    public void OnRayEnter()
    {
        if (!equipped && !active)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    public void OnRayExit()
    {
        if (!equipped && !active)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.75f);
        }
    }

    public EquipableType GetEquipableType()
    {
        return equipableType;
    }
    
}
