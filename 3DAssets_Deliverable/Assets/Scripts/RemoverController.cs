using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverController : MonoBehaviour
{
    public bool CanRemove = false;
    
    //GameObjects
    private GameObject block;
    public GameObject PlatGen;
    public GameObject WaterRiver;

    //Audios
    private AudioSource breakStoneAudio;
    public AudioClip breakStoneSound;

    void Start(){
        breakStoneAudio = GetComponent<AudioSource>(); 
    }

    void OnTriggerEnter(Collider col){

        if((col.CompareTag("Stone") || col.CompareTag("Obsidian")) && transform.parent.gameObject.GetComponent<PickaxeController>().isGrabbed){
            block = col.gameObject;

             //IF CAN REMOVE THE STONE AND GOES DOWN WITH THE PICKAXE --> Destroy the object.
            int ID_0 = -1;
            int ID_1 = -1; 
            if(transform.position.y <= 1){
                
                breakStoneAudio.PlayOneShot(breakStoneSound, 1.0f);
                if(block.transform.tag == "Obsidian"){
                    WaterRiver.gameObject.GetComponent<WaterRiver>().GoToOriginalPos();
                    WaterRiver.GetComponent<WaterRiver>().RiverFlow = true;
                }
                //NO PUEDE COGERLOS BIEN
                ID_0 = block.GetComponent<PlatformController>().ID[0];
                ID_1 = block.GetComponent<PlatformController>().ID[1];    
                block.SetActive(false);
                PlatGen.transform.GetComponent<EmptyObjectGenerator>().FillPlatforms(ID_0, ID_1);
            }

        }
        
    }
}
