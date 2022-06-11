using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleInputNamespace;
using UnityEngine.SceneManagement;

public class newcontrol : MonoBehaviour
{

    [Header("CONTROLS")]
    [Range(0f,10f)]public float sensitivity;
    [Header("Variables")]
    
    public WheelCollider[] wls = new WheelCollider[4];
    public GameObject[] meshes = new GameObject[4];
    public float wheeltorque;
    public float deftorque;
    public float speedlimit;
    public float steermax;
    public float adddownforcevalue = 50;
    private GameObject centerofmass;
    public float radius;
    public float brakepower;
    public float[] slip = new float[4];
    public float KPH;
    public float carver;
    public  float carhor;
    public bool handbrake;
    public float mobver;
    public float mobhor;
    
    private Rigidbody rbody;
    //audioplay
    public AudioSource audioen;
    public AudioSource backgroundaudio;

    public float maxspeed;
    public AudioSource breaks;
    public float maxrpm;
    public float wheeltrque;
    public float minvol;
    private pausemenu ppr;

    [Header("REFERENCES")]
    private carinfo _carinfo;
    [SerializeField] private float Acc;
    [SerializeField] private float braek;
    [SerializeField] private float stab;
    [SerializeField] private float nitro;


    public void Start()
    {
        backgroundaudio.Play();
        audioen.Play();
        getobjects();
    }


    
    
 private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "car select") return;
        movevehicle();
        
        steervehicle();
        movemeshes();
        vehiclebrake();
        dragvehicle();
        setrpm();
        breaksound();
        spedlim();
        getcarcharacters();


        audioplay();
        adddownforce();
        getfriction();
      
        carver = Input.GetAxis("Vertical");
        carhor = Input.GetAxis("Horizontal");
        
         handbrake = Input.GetKey(KeyCode.Space);
        // mobver = Input.acceleration.z;
        // mobhor = Input.acceleration.x;
        // carver = SimpleInput.GetAxis("Vertical");
       //  carhor = Input.acceleration.x * sensitivity;
        
         KPH = rbody.velocity.magnitude * 3.6f;
    }
    private void movevehicle()
    {
        if (carver >= 0)
        {
            for (int i = 0; i < wls.Length; i++)
            { wls[i].motorTorque = carver * wheeltorque * (Acc / 4f);
            }
        }
        else if(carver <= 0)
        {
            for (int i = 0; i < wls.Length; i++)
            {
                wls[i].motorTorque = carver * wheeltorque * (Acc / 4f);
            }
        }  else      
        {  for (int i = 0; i < wls.Length ; i++)
            { wls[i].motorTorque =0; }
        }
      
    }
    private void  setrpm()
    {
        if (KPH > maxspeed)
        {
            for (int i = 0; i < wls.Length; i++)
            { wls[i].motorTorque = maxrpm; }
        }
    }

    private void steervehicle()
    {
        //ackreemansteer
        if (carhor > 0)
        {   wls[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * carhor;
            wls[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * carhor;
        }
        else if (carhor < 0)
        {   wls[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * carhor;
            wls[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * carhor;
        }
        else
        {   wls[0].steerAngle = 0;
            wls[1].steerAngle = 0;

        }
        if (Input.GetKey(KeyCode.E))
        {
            radius = 1; 
        }
        else
        {
            radius = 10;
        }
        
    }
    private void  vehiclebrake()
    {
        if (handbrake)
        {   rbody.drag = 4.5f;
            rbody.angularDrag = 1.5f;
           wls[0].brakeTorque = wls[1].brakeTorque = wls[2].brakeTorque = wls[3].brakeTorque =   brakepower * KPH * (braek / 4f);
            breaks.Play();
        }
        if(!handbrake)
        {  rbody.drag = 0.1f;
           rbody.angularDrag = 0.1f;
            wls[0].brakeTorque = wls[1].brakeTorque = wls[2].brakeTorque = wls[3].brakeTorque = 0;
            breaks.Play();
        }
         }
    private void movemeshes()
    {
        Vector3 wheelPosition = Vector3.zero;
        Quaternion wheelRotation = Quaternion.identity;

        for (int i = 0; i < 4; i++)
        {
            wls[i].GetWorldPose(out wheelPosition, out wheelRotation);
            meshes[i].transform.position = wheelPosition;
            meshes[i].transform.rotation = wheelRotation;
        }
    }
   private void dragvehicle()
    {
        if (slip[0] >=1)
        {   rbody.drag = 1;
            rbody.angularDrag = 1.5f;
        }
        else if(slip[1] >= 1)
        {   rbody.drag = 1;
            rbody.angularDrag = 1.5f;
        }
        else
        {
            rbody.drag = 0.23f;
            rbody.angularDrag = 0.23f;
        }
    }
     private void adddownforce()
    {
        rbody.AddForce(-transform.up * adddownforcevalue * rbody.velocity.magnitude);
    }
    private void getobjects()
    {
        _carinfo = GetComponent<carinfo>();
        rbody = GetComponent<Rigidbody>();
        centerofmass = GameObject.Find("mass");
        rbody.centerOfMass = centerofmass.transform.localPosition;
        KPH = rbody.velocity.magnitude * 3.6f;
        
    }
    private void getfriction()
    {
        for(int i = 0; i < wls.Length; i++)
        {   WheelHit wheelHit;
            wls[i].GetGroundHit(out wheelHit);
              slip[i] = wheelHit.forwardSlip; }
    }
    private void audioplay()
    {
        audioen.pitch = KPH / maxspeed;
        backgroundaudio.volume = KPH / 40f;

    }
    
    private void breaksound()
    {
        if (handbrake)
        {
            breaks.Play();
        }
        else
        {
            breaks.Stop();
        }
    }
    private void spedlim()
    {
        if (speedlimit < KPH)
        {
            wls[2].motorTorque = 0f;
            wls[3].motorTorque = 0f;
            wls[3].brakeTorque = 300f;
            wls[2].brakeTorque = 300f;
        }
        else
        {
            wheeltorque = deftorque;
        }
    }
        public void getcarcharacters()
        {
        Acc =   _carinfo.Acceleration * 200f;
        braek = _carinfo.Braking * 200f;
        stab =  _carinfo.Stability * 200f;
        nitro = _carinfo.Nitro * 200f;
        }
   
    
   
}
