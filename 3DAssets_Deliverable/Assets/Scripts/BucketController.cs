using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    public bool isGrabbed = false;
    private bool CanDropWater = false;
    private bool CanDropLava = false;

    public List<GameObject> players = new List<GameObject>();
    private int tmp_pS;
    
    private Vector3 OriginalPos;
    
    //GameObject
    public GameObject WaterBucket;
    public GameObject LavaBucket;
    public GameObject EmptyPlatform;
    
    //AUDIO
    private AudioSource fillWaterAudio;
    public AudioClip fillWaterSound;

    private AudioSource fillLavaAudio;
    public AudioClip fillLavaSound;

    // Start is called before the first frame update
    void Start()
    {
        tmp_pS = -1;
        OriginalPos = transform.position;
        WaterBucket.SetActive(false);
        LavaBucket.SetActive(false);
        fillWaterAudio = GetComponent<AudioSource>();
        fillLavaAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(isGrabbed && tmp_pS != -1){
            this.gameObject.transform.position = players[tmp_pS -1].transform.position;
        }else{
            transform.position = OriginalPos;
        }
    }

    //Check collisions to control the mechanics
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            if(!isGrabbed){
                isGrabbed = true;
                tmp_pS = col.gameObject.GetComponent<PlayerMovement>().playerIndex;
            }   
        }
        //Check collision with pickaxe to change the tool
        if(col.CompareTag("Pickaxe") && !col.transform.GetComponent<PickaxeController>().isGrabbed){
            isGrabbed = false;
        }
        //Check collision with bucket to change the tool
        if(col.CompareTag("Bucket") && !col.transform.GetComponent<BucketController>().isGrabbed){
            isGrabbed = false;
        }
        //Fill the bucket with water
        if(col.CompareTag("Water")){
            fillWaterAudio.PlayOneShot(fillWaterSound, 1.0f); 
            CanDropWater = true;
            WaterBucket.SetActive(true);
            if(CanDropLava){
                CanDropLava = false;
                LavaBucket.SetActive(false);
            }
        }
        //Fill the bucket with lava
        if(col.CompareTag("Lava")){
            fillLavaAudio.PlayOneShot(fillLavaSound, 1.0f); 
            CanDropLava = true;
            LavaBucket.SetActive(true);
            if(CanDropWater){
                CanDropWater = false;
                WaterBucket.SetActive(false);
            }
        }
        //Fill the platform with the element filled
        if(col.CompareTag("Empty") && transform.position.y <= 1 && isGrabbed){
            //Check if the plaform ahead is water and the bucket is empty
            if(col.gameObject.transform.GetChild(3).gameObject.activeSelf && !WaterBucket.activeSelf && !LavaBucket.activeSelf){
                fillWaterAudio.PlayOneShot(fillWaterSound, 1.0f); 
                //Update the bucket and platform
                CanDropWater = true;
                WaterBucket.SetActive(true);
                col.transform.GetComponent<PlatformController>().UpdatePlatform("Empty");
            }
            //Check if the plaform ahead is water and the bucket is empty
            else if(col.gameObject.transform.GetChild(2).gameObject.activeSelf && !WaterBucket.activeSelf && !LavaBucket.activeSelf){
                //Update the bucket and platform
                CanDropLava = true;
                LavaBucket.SetActive(true);
                col.transform.GetComponent<PlatformController>().UpdatePlatform("Empty");
            }
            else if(CanDropLava){
                //Water is active?
                if(col.gameObject.transform.GetChild(3).gameObject.activeSelf){
                    //Yes --> Make obsidian
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Obsidian");
                }
                else {
                    //No --> Put lava
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Lava");

                }
                CanDropLava = false;
                LavaBucket.SetActive(false);
            } 
            else if(CanDropWater){
                //Lava is active?
                if(col.gameObject.transform.GetChild(2).gameObject.activeSelf){
                    //Yes --> Make stone
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Stone");
                }
                else {
                    //No --> Put water
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Water");
                }
                CanDropWater = false;
                WaterBucket.SetActive(false);
            }
        }
    }
}
