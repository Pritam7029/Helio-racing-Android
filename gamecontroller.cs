using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamecontroller : MonoBehaviour
{
    [SerializeField] private int carnum;
    [SerializeField] private carpointer _carlist;
    [SerializeField] private GameObject _car;
    [SerializeField] private carinfo _carinfo;
    [SerializeField] private selectmenu _selectmenu;


    [Header("UI elements")]
    public Image acceleration;
    public Image braking;
    public Image stability;
    public Image nitro;

    private void Awake()
    {
        carnum = _selectmenu.carpointer;
        _car = _carlist.cars[carnum];
        _carinfo = _car.GetComponent<carinfo>();

    }

    private void Update()
    {
        getcarinfo();
    }

    private void getcarinfo()
    {
        acceleration.fillAmount = _carinfo.Acceleration;
        braking.fillAmount = _carinfo.Braking;
        stability.fillAmount = _carinfo.Stability;
        nitro.fillAmount = _carinfo.Nitro;

    }


}
