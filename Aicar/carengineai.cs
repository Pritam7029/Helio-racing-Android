using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carengineai : MonoBehaviour
{
    [Header("sensors")]
    public float sensorlengh;
    public float sidesnesorpos = 0.6f;
    //public float sidesensorpos = 0.4f;
    public Vector3 startpos = new Vector3(0f,0.5f,1.7f);
    public float sidesensorangle = 30f;
    private bool avoiding = false;
    private float targetSteerAngle = 0;


    [Header("variables")]
    public Transform path;
    public float maxsteerangle = 45f;
    public float turningspeed;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelrr;
    public WheelCollider wheelrL;
    public float Aispeed = 50f;
    public float currentspeed;
    public float maxspeed;
    private List<Transform> nodes;
    public int currentnode = 0;
    public float mindistence;
    private Vector3 centerofmass;
    private Rigidbody rb;
    public bool isbreaking;
    public float maxbreakingtorque;
   // public AudioSource engineaudio;

   




    void Start()
    {
        // rb = GetComponent<Rigidbody>();

        //  engineaudio.Play();
        GetComponent<Rigidbody>().centerOfMass = centerofmass;

        Transform[] pathtransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathtransforms.Length; i++)
        {
            if (pathtransforms[i] != path.transform)
            {
                nodes.Add(pathtransforms[i]);
            }
        }
    }


    private void FixedUpdate()
    {
        sensors();
        applysteer();
        Drive();
        LerpToSteerAngle();
        breaking();
        checkwaypointdistence();
        // audioplay();
    }
   
    private void sensors()
    {
        RaycastHit hit;
        Vector3 sensorstartpos = transform.position ;
        sensorstartpos += transform.forward * startpos.z;
        sensorstartpos += transform.up * startpos.y;
        float avoidmultiply = 0;
        avoiding = false;

        // front right sensor
        sensorstartpos += transform.right * sidesnesorpos;
        if (Physics.Raycast(sensorstartpos, transform.forward, out hit, sensorlengh))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorstartpos, hit.point);
                avoiding = true;
                avoidmultiply -= 1f;
            }
           
        }
       
        // front right angle sensor
        
       else if (Physics.Raycast(sensorstartpos, Quaternion.AngleAxis(sidesensorangle,transform.up) * transform.forward, out hit, sensorlengh))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorstartpos, hit.point);
                avoiding = true;
                avoidmultiply -= 0.5f;
            }
           
        }

        // front left sensor
        sensorstartpos -= transform.right * sidesnesorpos * 2;
        if (Physics.Raycast(sensorstartpos, transform.forward, out hit, sensorlengh))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorstartpos, hit.point);
                avoiding = true;
                avoidmultiply += 1f;
            }
           
        }
       
        // front left angle sensor

      else  if (Physics.Raycast(sensorstartpos, Quaternion.AngleAxis(-sidesensorangle, transform.up) * transform.forward, out hit, sensorlengh))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(sensorstartpos, hit.point);
                avoiding = true;
                avoidmultiply += 0.5f;
            }
           
        }

        //front center sensor
        if (avoidmultiply == 0)
        {
            if (Physics.Raycast(sensorstartpos, transform.forward, out hit, sensorlengh))
            {
                if (!hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(sensorstartpos, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0)
                    {
                        avoidmultiply = -1f;
                    }
                    else
                    {
                        avoidmultiply = 1f;
                    }
                }

            }
        }
        if (avoiding)
        {
            targetSteerAngle = maxsteerangle * avoidmultiply;
           //  wheelFL.steerAngle = maxsteerangle * avoidmultiply;
           // wheelFR.steerAngle = maxsteerangle * avoidmultiply;
        }
       
    }
    private void  applysteer()
    {
        if (avoiding) return;
        Vector3 relativevector = transform.InverseTransformPoint(nodes[currentnode].position);
        relativevector /= relativevector.magnitude;
        float newsteer = (relativevector.x / relativevector.magnitude) * maxsteerangle;
        targetSteerAngle = newsteer;
       // wheelFL.steerAngle = newsteer;
       //  wheelFR.steerAngle = newsteer;
    }
    private void Drive()
    {
        currentspeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;
       // currentspeed = rb.velocity.magnitude * 3.6f;
        if (currentspeed < maxspeed && !isbreaking)
        {
            wheelFL.motorTorque = Aispeed;
            wheelFR.motorTorque = Aispeed;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }
    private void checkwaypointdistence()
    {
        if(Vector3.Distance(transform.position , nodes[currentnode].position) < mindistence)
        {
            if(currentnode == nodes.Count - 1f)
            {
                currentnode = 0;
            }
            else
            {
                currentnode ++;
            }
        }
    }
    private void breaking()
    {
        if (isbreaking)
        {
            wheelrL.brakeTorque = maxbreakingtorque;
            wheelrr.brakeTorque = maxbreakingtorque;
        }
        else
        {
            wheelrL.brakeTorque = 0;
            wheelrr.brakeTorque = 0;
        }
    }
    private void LerpToSteerAngle()
    {
        wheelFL.steerAngle = Mathf.Lerp(wheelFL.steerAngle, targetSteerAngle, Time.deltaTime * turningspeed);
        wheelFR.steerAngle = Mathf.Lerp(wheelFR.steerAngle, targetSteerAngle, Time.deltaTime * turningspeed);
    }
}
