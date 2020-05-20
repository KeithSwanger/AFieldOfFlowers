using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteScreenController : MonoBehaviour
{
    public Canvas endScreenCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        endScreenCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        endScreenCanvas.gameObject.SetActive(false);
    }
}
