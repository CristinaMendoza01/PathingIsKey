using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    private bool isGrabbed = false;
    private bool CanDropWater = false;
    private bool CanDropLava = false;
    public List<GameObject> players = new List<GameObject>();
    private int tmp_pS;
    private Vector3 OriginalPos;

    public GameObject WaterBucket;
    public GameObject LavaBucket;
    public GameObject EmptyPlatform;
    
    private AudioSource fillWaterAudio;
    public AudioClip fillWaterSound;
    // Start is called before the first frame update
    void Start()
    {
        tmp_pS = -1;
        OriginalPos = transform.position;
        WaterBucket.SetActive(false);
        LavaBucket.SetActive(false);
        fillWaterAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(isGrabbed && tmp_pS != -1){
            this.gameObject.transform.position = players[tmp_pS -1].transform.position;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            isGrabbed = true;
            tmp_pS = col.gameObject.GetComponent<PlayerMovement>().playerIndex;
        }
        if(col.CompareTag("Pickaxe")){
            isGrabbed = false;
            //transform.position = OriginalPos;
        }
        if(col.CompareTag("Bucket")){
            isGrabbed = false;
            //transform.position = OriginalPos;
        }
        if(col.CompareTag("Water")){
            fillWaterAudio.PlayOneShot(fillWaterSound, 1.0f); 
            CanDropWater = true;
            WaterBucket.SetActive(true);
            LavaBucket.SetActive(false);
        }
        if(col.CompareTag("Lava")){
            CanDropLava = true;
            LavaBucket.SetActive(true);
            WaterBucket.SetActive(false);
        }
        if(col.CompareTag("Empty") && transform.position.y <= 1 && isGrabbed){
            if(CanDropLava){
                //Mirar si agua activa
                if(col.gameObject.transform.GetChild(2).gameObject.activeSelf){
                    //SI --> Hacer aparecer obsidiana
                    EmptyPlatform.transform.GetComponent<PlatformController>().UpdatePlatform("Obsidian");
                }
                else {
                    //NO --> Hacer aparecer lava
                    EmptyPlatform.transform.GetComponent<PlatformController>().UpdatePlatform("Lava");

                }
                CanDropLava = false;
                LavaBucket.SetActive(false);
            } 
            if(CanDropWater){
                //Mirar si lava activa
                if(col.gameObject.transform.GetChild(3).gameObject.activeSelf){
                    //SI --> Hacer aparecer piedra
                    EmptyPlatform.transform.GetComponent<PlatformController>().UpdatePlatform("Stone");
                }
                else {
                    
                    //NO --> Hacer aparecer agua
                    EmptyPlatform.transform.GetComponent<PlatformController>().UpdatePlatform("Water");
                }
                CanDropWater = false;
                WaterBucket.SetActive(false);
            }
        }
    }
}
