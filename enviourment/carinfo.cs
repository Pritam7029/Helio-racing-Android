using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carinfo : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Acceleration;
    [Range(0f, 1f)]
    public float Braking;
    [Range(0f, 1f)]
    public float Nitro;
    [Range(0f, 1f)]
    public float Stability;

    public int carcost;
    public bool owned, notowned;
    private void Start()
    {


        if (PlayerPrefs.GetInt("owned") == 1)
        {
            notowned = true;
            owned = false;
        }else if(PlayerPrefs.GetInt("owned") == 0)
        {
            owned = true;
            notowned=false;
        }

      }
}
