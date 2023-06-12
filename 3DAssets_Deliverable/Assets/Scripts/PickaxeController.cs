using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    private Vector3 OriginalPos;
    private Vector3 direction;
    private Vector3 lastdir;

    public List<GameObject> players = new List<GameObject>();
    private int tmp_pS;
    
    private float dir_angle;

    public bool isGrabbed = false;

    private AudioSource breakStoneAudio;
    public AudioClip breakStoneSound;

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
