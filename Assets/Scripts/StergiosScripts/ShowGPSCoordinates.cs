using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGPSCoordinates : MonoBehaviour
{
    public Canvas text;
    private bool switchText = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
       
    }
 
    public void ChangeText(bool currentValue){
 
        switchText = currentValue;
       
 
        if (switchText){
            text.enabled = true;
        }
        else {
            text.enabled = false;
        }
    }
}
