using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float frameRate = 12;
    public bool playOnStart = false;

    private int currentFrame = 0;

    private bool animate = false;

    private float frameLength;

    private float frameTimer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        frameLength = 1f / frameRate;
        if (playOnStart)
        {
            Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        if(frameTimer <= 0f)
        {
            NextFrame();
            frameTimer = frameLength;
        }

        frameTimer -= Time.deltaTime;

        spriteRenderer.sprite = sprites[currentFrame];
    }

    private void NextFrame()
    {
        currentFrame++;

        if(currentFrame >= sprites.Length)
        {
            currentFrame = 0;
        }
    }

    public void GoToFrame(int frame)
    {
        if(frame >= sprites.Length)
        {
            return;
        }

        Stop();
        currentFrame = frame;
        spriteRenderer.sprite = sprites[frame];
    }

    public void SetFrameToPercentage(float percent)
    {
        if(percent < 0f || percent > 1f)
        {
            return;
        }

        Stop();

        int frame = (int)((sprites.Length - 1) * percent);
        spriteRenderer.sprite = sprites[frame];
        currentFrame = frame;
    }

    public void Play()
    {
        animate = true;
    }

    public void PlayFromRandom()
    {
        currentFrame = UnityEngine.Random.Range(0, sprites.Length);

        animate = true;
        
    }

    public void Stop()
    {
        animate = false;

    }

    public void Reset()
    {
        Stop();
        spriteRenderer.sprite = sprites[0];
        currentFrame = 0;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
}
