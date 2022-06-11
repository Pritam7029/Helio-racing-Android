using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedometer : MonoBehaviour
{
    public newcontrol rr;
    public GameObject needle;
    private float startposition = 210.8f, endposition = -37.8f;
    private float desierposition;
    public  float vehiclespeed;

    void Start()
    {
        //rr = gameObject.GetComponent<newcontrol>();
    }

   
    void Update()
    {
        vehiclespeed = rr.KPH;
        updateneedle();
    }
    public void updateneedle()
    {
        desierposition = startposition - endposition;
        float temp = vehiclespeed / 180f;
        needle.transform.eulerAngles = new Vector3(0, 0, (startposition - temp * desierposition));
    }
}
