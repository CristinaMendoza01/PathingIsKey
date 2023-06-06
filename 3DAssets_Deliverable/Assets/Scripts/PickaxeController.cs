using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    public bool isGrabbed = false;
    public List<GameObject> players = new List<GameObject>();
    private Vector3 OriginalPos;
    private int tmp_pS;

    private Vector3 direction;
    private Vector3 lastdir;

    private float dir_angle;

    private AudioSource breakStoneAudio;
    public AudioClip breakStoneSound;

    public GameObject PlatGen;

    // Start is called before the first frame update
    void Start()
    {
        tmp_pS = 1;
        OriginalPos = transform.position;
        breakStoneAudio = GetComponent<AudioSource>(); 
        direction = players[tmp_pS -1].GetComponent<PlayerMovement>().direction;
    }

    // Update is called once per frame
    void Update()
    {   
        if(isGrabbed && tmp_pS != -1)
        {
            this.gameObject.transform.position = players[tmp_pS -1].transform.position;
            lastdir = direction;
            direction = players[tmp_pS -1].GetComponent<PlayerMovement>().direction;
            direction.y = 0;

            //POINTS AT PLAYER'S DIRECTION
            if(direction.magnitude >= 0.3f){
                dir_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion rotation;
                if(dir_angle != 90 || dir_angle != -90 || dir_angle != 0 || dir_angle != 180 || dir_angle != -180){
                    rotation = Quaternion.Euler(90f,0f, -dir_angle);
                }else{
                    rotation = Quaternion.Euler(90f,0f, dir_angle);
                }
                this.transform.rotation = rotation;
            }

            //IF CAN REMOVE THE STONE AND GOES DOWN WITH THE PICKAXE --> Destroy the object.
            if(this.gameObject.transform.GetChild(0).gameObject.GetComponent<RemoverController>().CanRemove && players[tmp_pS -1].transform.position.y <= 1){
                if(this.gameObject.transform.GetChild(0).gameObject.GetComponent<RemoverController>().block != null) breakStoneAudio.PlayOneShot(breakStoneSound, 1.0f);               
                Destroy(this.gameObject.transform.GetChild(0).gameObject.GetComponent<RemoverController>().block);
                PlatGen.transform.GetComponent<EmptyObjectGenerator>().GeneratePlatforms();
            }
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            if(!isGrabbed){
                isGrabbed = true;
                tmp_pS = col.gameObject.GetComponent<PlayerMovement>().playerIndex;
            }
        }
        if (col.CompareTag("Bucket")){
            isGrabbed = false;
            transform.position = OriginalPos;
        }
    }
}
