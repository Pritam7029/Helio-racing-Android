using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class path : MonoBehaviour
{
    public Color linecolor;

    private List<Transform> nodes = new List<Transform>();

   
        private void OnDrawGizmos()
        {
        Gizmos.color = linecolor;

        Transform[] pathtransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i < pathtransforms.Length; i++)
        {
            if(pathtransforms[i] != transform)
            {
                nodes.Add(pathtransforms[i]);
            }
        }
        for(int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentnode = nodes[i].position;
            Vector3 previousnode = Vector3.zero;
            if(i > 0)
            {
                previousnode = nodes[i - 1].position;
            }else if (i == 0 &  nodes.Count > 1)
            {
            previousnode = nodes[nodes.Count - 1].position;
        }
        Gizmos.DrawLine(previousnode, currentnode);
            Gizmos.DrawSphere(currentnode, 0.3f);
        }
    }



}
