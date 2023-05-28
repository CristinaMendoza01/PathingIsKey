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
    public GameObject WaterPlatform;
    public GameObject LavaPlatform;
    
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
        // Vector3 PlatformCenterPos = players[tmp_pS -1].transform.gameObject.GetComponent<PlayerMovement>().tmppos;
        // Vector3 clamppos_offset = players[tmp_pS -1].transform.gameObject.GetComponent<PlayerMovement>().tmppos_offset;
        // //Debug.Log(isGrabbed);
        // if(isGrabbed && tmp_pS != -1){
        //     this.gameObject.transform.position = players[tmp_pS -1].transform.position;
        // }
        // if((CanDropWater || CanDropLava) && players[tmp_pS -1].transform.position.y <= 1){
        //     //"Divide" the stone platform in 4 parts 
        //         //    o   o             NUMBERS = parts, o = Zones to drop bucket content, lines = limitation of every zone.
        //         // o  1 | 2  o
        //         //   -------
        //         // o  3 | 4  o
        //         //    o   o
        //         // Como?
        //         // Dos posibles zonas para soltar el/la agua/lava por cada parte, si esta por un costado u otro. 
        //         // Mirar en que limite clampeado esta, si pos.x == clamp.x && pos.z != clamp.z --> Drop en x dir, si pos.z == clamp.x && pos.x != clamp.x--> drop en z dir.
        //         // Si pos.x && pos.z == clamp --> mirar la ultima direccion habilitada.
        //         // OBSERVACION: CLamp puede ser negativo o postivio (partes 1 y 2 positivo, 3 y 4 negativo)
        //         // Propongo: Hacer las plataformas la mitad que la plataforma original
        //         if(transform.position.x >= (PlatformCenterPos.x + clamppos_offset.x) - 1){ //clamp +x 
        //             if(transform.position.z < (PlatformCenterPos.z + clamppos_offset.z) && transform.position.z >= PlatformCenterPos.z ){// +z
        //                 Vector3 direction = new Vector3(1,0,0);
        //                 if(CanDropWater) GameObject newObj = Instantiate(WaterPlatform);
        //                 if(CanDropLava) GameObject newObj = Instantiate(LavaPlatform);
        //                 //INITIALPLATFORM
        //                 float x = (PlatformCenterPos.x + clamppos_offset.x) + clamppos_offset/2;
        //                 float y = PlatformCenterPos.y;
        //                 float z = (PlatformCenterPos.x + clamppos_offset.z/2);
        //                 //STONEPATH

        //                 Vector3 DropPos = new Vector3(x,y,z);

        //                 newObj.transform.position = DropPos;
        //                 newObj.transform.rotation = Quaternion.identity;
        //                 //DropElement(direction, 2, 1);
        //             }else{// -z
        //                 Vector3 direction = new Vector3(1,0,0);
        //                 //DropElement(direction, 1, 1);
        //             }
        //         }
        //         if(transform.position.x == PlatformCenterPos.x - clamppos_offset.x){// clamp -x
        //             if(transform.position.z < (PlatformCenterPos.z + clamppos_offset.z) && transform.position.z >= PlatformCenterPos.z ){// +z
        //                 Vector3 direction = new Vector3(-1,0,0);
        //                 //DropElement(direction, 4, 1);
        //             }else{// -z
        //                 Vector3 direction = new Vector3(-1,0,0);
        //                 //DropElement(direction, 3, 1);
        //             }
        //         }
        //         if(transform.position.z == PlatformCenterPos.z + clamppos_offset.z){ //clamp +z
        //             if(transform.position.x < PlatformCenterPos.x + clamppos_offset.x && transform.position.x >= PlatformCenterPos.x ){// +x
        //                 Vector3 direction = new Vector3(0,0,1);
        //                 //DropElement(direction, 2, 2);
        //             }else{// -x
        //                 Vector3 direction = new Vector3(0,0,1);
        //                 //DropElement(direction, 1, 2);
        //             }
        //         }
        //         if(transform.position.z == PlatformCenterPos.z - clamppos_offset.z){ //clamp -z
        //             if(transform.position.x < PlatformCenterPos.x + clamppos_offset.x && transform.position.x >= PlatformCenterPos.x ){// +x
        //                 Vector3 direction = new Vector3(0,0,-1);
        //                 //DropElement(direction, 4, 2);
        //             }else{// -x
        //                 Vector3 direction = new Vector3(0,0,-1);
        //                 //DropElement(direction, 3, 2);
        //             }
        //         }
        //         //if()//clamp +-z y +-x
        // }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            isGrabbed = true;
            tmp_pS = col.gameObject.GetComponent<PlayerMovement>().playerIndex;
            Debug.Log(tmp_pS);
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
    }
}
