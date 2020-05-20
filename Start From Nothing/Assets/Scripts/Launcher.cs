
using System;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameController gameController;

    public int launchRangeX = 5;
    public int launchRangeY = 5;
    public float gravityForce;
    float currentGravity;

    public float upwardForce;
    public float launchTime = 1f;
    float launchTimer = 0f;

    float height = 0f;
    

    Vector2 startPoint;
    Vector2 endPoint;
    
    bool landed = false;
    bool launch = false;

    public bool isSeed = false;

    public Action Landed;
    

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        Launch();


    }

    private Vector2 FindEndPoint(Vector2 start)
    {
        Vector2 end = Vector2.zero;

        if (isSeed)
        {
            do
            {
                end.x = start.x + UnityEngine.Random.Range(-launchRangeX, launchRangeX);
                end.y = start.y + UnityEngine.Random.Range(-launchRangeY, launchRangeY);
            } while (!gameController.tilemapController.IsWithinBounds(end));
        }
        else
        {
            end.x = start.x + UnityEngine.Random.Range(-launchRangeX, launchRangeX);
            end.y = start.y + UnityEngine.Random.Range(-launchRangeY, (int)(launchRangeY / 2));
        }


        return end;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (launch)
        {
            PerformLaunch();
        }

        if (landed)
        {
            if (Landed != null)
            {
                Landed.Invoke();
            }
            GameObject.Destroy(this);
        }
    }

    public void Launch()
    {
        PrepareForLaunch();
        launch = true;
    }

    private void PrepareForLaunch()
    {
        startPoint = transform.position;
        endPoint = FindEndPoint(startPoint);
        currentGravity = gravityForce + UnityEngine.Random.value;

    }

    void PerformLaunch()
    {

        height += (upwardForce  + currentGravity) * Time.deltaTime;

        if (height < 0)
        {
            height = 0;
            landed = true;
        }


        launchTimer += Time.deltaTime;

        if(launchTimer > launchTime)
        {
            launchTimer = launchTime;
        }

        

        Vector2 newPosition = Vector2.Lerp(startPoint, endPoint, launchTimer / launchTime);
        newPosition.y += height;
        transform.position = newPosition;

        currentGravity -= (gravityForce * gravityForce) * Time.deltaTime;

        
    }


}
