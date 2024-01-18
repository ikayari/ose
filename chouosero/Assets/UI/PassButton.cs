using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassButton : MonoBehaviour
{
    public bool canReversKoma = false;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!canReversKoma)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }
}
