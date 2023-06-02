using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverController : MonoBehaviour
{
    public bool CanRemove = false;
    public GameObject block;

    private AudioSource breakStoneAudio;
    public AudioClip breakStoneSound;

    void OnTriggerStay(Collider col){
        //Debug.Log(transform.parent.gameObject.GetComponent<PickaxeController>().isGrabbed);
        if((col.CompareTag("Stone") || col.CompareTag("Obsidian")) && transform.parent.gameObject.GetComponent<PickaxeController>().isGrabbed){
            CanRemove = true;
            block = col.transform.gameObject;
            // if(block != null) breakStoneAudio.PlayOneShot(breakStoneSound, 1.0f);
            // if(col.CompareTag("Stone")) block.transform.GetComponent<PlatformController>().Stone.SetActive(false);
            // if(col.CompareTag("Obsidian")) block.transform.GetComponent<PlatformController>().Obsidian.SetActive(false);

        }
        if(col.CompareTag("Platform")){
            CanRemove = false;
        }
    }
}
