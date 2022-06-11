using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carwheel : MonoBehaviour
{
    public WheelCollider whls;
    private Vector3 wheelposition = new Vector3();
    private Quaternion wheelrotation = new Quaternion();
   

  
    void Update()
    {
        whls.GetWorldPose(out wheelposition, out wheelrotation);
        transform.position = wheelposition;
        transform.rotation = wheelrotation;
    }
}
