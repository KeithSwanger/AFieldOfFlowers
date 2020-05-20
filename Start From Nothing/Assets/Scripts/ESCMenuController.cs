using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCMenuController : MonoBehaviour
{
    
    public Canvas ESCCanvas;

    [HideInInspector]
    public bool isShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        if (ESCCanvas.isActiveAndEnabled)
        {
            isShowing = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        ESCCanvas.gameObject.SetActive(true);
        isShowing = true;
    }

    public void Hide()
    {
        ESCCanvas.gameObject.SetActive(false);
        isShowing = false;

    }
}
