using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public Transform player;

   

    
    private void LateUpdate()
    {
        Vector3 newposition = player.position;
        newposition.y = transform.position.y;
        transform.position = newposition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
    
}
