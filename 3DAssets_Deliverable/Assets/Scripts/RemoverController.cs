using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverController : MonoBehaviour
{
    public bool CanRemove = false;
    public GameObject stone;

    void OnTriggerStay(Collider col){
        //Debug.Log(transform.parent.gameObject.GetComponent<PickaxeController>().isGrabbed);
        if(col.CompareTag("Stone") && transform.parent.gameObject.GetComponent<PickaxeController>().isGrabbed){
            CanRemove = true;
            stone = col.transform.gameObject;
        }
        if(col.CompareTag("Platform")){
            CanRemove = false;
        }
    }
    
    
}
