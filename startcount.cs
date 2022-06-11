using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startcount : MonoBehaviour
{
   //private newcontrol newcontrol;
   //public GameObject carcontrol;
    public GameObject countdown;
    // public AudioSource getready;
    // public AudioSource goaudio;
    public GameObject panle;
    public GameObject startbox1;
    public GameObject startbox2;
   
    void Start()
    {
        StartCoroutine(countup());
      //  carcontrol.SetActive(false);
        panle.SetActive(true);
        // bool newcontrol,handbrake = true;
        startbox1.SetActive(true);
        startbox2.SetActive(true);
    }
    IEnumerator countup()
    {
       // newcontrol.handbrake = true;
        yield return new WaitForSeconds(0.5f);
        countdown.GetComponent<Text>().text = "3";
       // getready.Play();
        countdown.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        countdown.SetActive(false);
        countdown.GetComponent<Text>().text = "2";
      //  getready.Play();
        countdown.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdown.SetActive(false);
        countdown.GetComponent<Text>().text = "1";
      //  getready.Play();
        countdown.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdown.SetActive(false);
      //  goaudio.Play();
      // carcontrol.SetActive(true);
        panle.SetActive(false);
        //  newcontrol.handbrake = false;
        startbox1.SetActive(false);
        startbox2.SetActive(false);


    }

 
}
