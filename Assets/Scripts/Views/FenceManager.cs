using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");

    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");

    }

}
