using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public GameObject player;
    public GameObject child;
    public float speed;
    private newcontrol rr;
  //  [Range(0f, 50f)] public float smoothtime = 8f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rr = player.GetComponent<newcontrol>();
        child = GameObject.FindGameObjectWithTag("carcam");
      //  child = GameObject.FindWithTag("carcam");
    }

    private void FixedUpdate()
    {
        follow();
    }
   private void follow()
    {
        speed = Mathf.Lerp(speed, rr.KPH / 4, Time.deltaTime);
       // speed = rr.KPH / smoothtime;
        gameObject.transform.position = Vector3.Lerp(transform.position,child.transform.position,Time.deltaTime * speed);
        gameObject.transform.LookAt(player.gameObject.transform.position);
    }
    private void LateUpdate()
    {
       
    }
}
