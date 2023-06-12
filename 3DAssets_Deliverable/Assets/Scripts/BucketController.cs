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

    public GameObject WaterBucket;
    public GameObject LavaBucket;
    public GameObject EmptyPlatform;
    
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

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            if(!isGrabbed){
                isGrabbed = true;
                tmp_pS = col.gameObject.GetComponent<PlayerMovement>().playerIndex;
            }   
        }
        if(col.CompareTag("Pickaxe") && !col.transform.GetComponent<PickaxeController>().isGrabbed){
            isGrabbed = false;
        }
        if(col.CompareTag("Bucket") && !col.transform.GetComponent<BucketController>().isGrabbed){
            isGrabbed = false;
        }
        if(col.CompareTag("Water")){
            fillWaterAudio.PlayOneShot(fillWaterSound, 1.0f); 
            CanDropWater = true;
            WaterBucket.SetActive(true);
            if(CanDropLava){
                CanDropLava = false;
                LavaBucket.SetActive(false);
            }
        }
        if(col.CompareTag("Lava")){
            fillLavaAudio.PlayOneShot(fillLavaSound, 1.0f); 
            CanDropLava = true;
            LavaBucket.SetActive(true);
            if(CanDropWater){
                CanDropWater = false;
                WaterBucket.SetActive(false);
            }
        }
        if(col.CompareTag("Empty") && transform.position.y <= 1 && isGrabbed){
            //Debug.Log("Empty");
            //Chequea que la casilla de enfrente sea agua y que el cubo este vacio
            if(col.gameObject.transform.GetChild(3).gameObject.activeSelf && !WaterBucket.activeSelf && !LavaBucket.activeSelf){
                fillWaterAudio.PlayOneShot(fillWaterSound, 1.0f); 
                //actualiza el cubo y la plataforma
                CanDropWater = true;
                WaterBucket.SetActive(true);
                col.transform.GetComponent<PlatformController>().UpdatePlatform("Empty");
            }
            //cheque que la casilla de enfrente sea lava y el cubo este vacio
            else if(col.gameObject.transform.GetChild(2).gameObject.activeSelf && !WaterBucket.activeSelf && !LavaBucket.activeSelf){
                //actualiza el cubo y la plataforma
                CanDropLava = true;
                LavaBucket.SetActive(true);
                col.transform.GetComponent<PlatformController>().UpdatePlatform("Empty");
            }
            else if(CanDropLava){
                //Mirar si agua activa
                //Debug.Log(col.gameObject.transform.GetChild(3).gameObject.activeSelf);
                if(col.gameObject.transform.GetChild(3).gameObject.activeSelf){
                    //SI --> Hacer aparecer obsidiana
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Obsidian");
                }
                else {
                    //NO --> Hacer aparecer lava
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Lava");

                }
                CanDropLava = false;
                LavaBucket.SetActive(false);
            } 
            else if(CanDropWater){
                //Mirar si lava activa
                //Debug.Log(col.gameObject.transform.GetChild(2).gameObject.activeSelf);
                
                if(col.gameObject.transform.GetChild(2).gameObject.activeSelf){
                    //SI --> Hacer aparecer piedra
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Stone");
                }
                else {
                    
                    //NO --> Hacer aparecer agua
                    col.transform.GetComponent<PlatformController>().UpdatePlatform("Water");
                }
                CanDropWater = false;
                WaterBucket.SetActive(false);
            }
        }
    }








    // void OnTriggerStay(Collider col){
    //     if(col.CompareTag("Player") && col.CompareTag("Bucket")){
    //         isGrabbed = false;
    //     }
    // }
}
