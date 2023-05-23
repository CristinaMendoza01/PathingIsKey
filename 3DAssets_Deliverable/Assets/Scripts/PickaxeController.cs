using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    private bool isGrabbed = false;
    public List<GameObject> players = new List<GameObject>();
    private Vector3 OriginalPos;
    private int tmp_pS;

    private Vector3 direction;

    private AudioSource breakStoneAudio;
    public AudioClip breakStoneSound;

    // Start is called before the first frame update
    void Start()
    {
        tmp_pS = -1;
        OriginalPos = transform.position;
        breakStoneAudio = GetComponent<AudioSource>(); 

    }

    // Update is called once per frame
    void Update()
    {   
        if(isGrabbed && tmp_pS != -1)
        {
            this.gameObject.transform.position = players[tmp_pS -1].transform.position;
            direction = players[tmp_pS -1].GetComponent<PlayerMovement>().direction;
            if(direction.x==0 && direction.z==1){
                transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
            }
            if(direction.x==0 && direction.z==-1){
                transform.rotation = Quaternion.AngleAxis(90, Vector3.left);
            }
            if(direction.x==1 && direction.z==0){
                transform.rotation = Quaternion.AngleAxis(90, Vector3.back);
            }
            if(direction.x==-1 && direction.z==0){
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }
            if(Input.GetKeyDown(KeyCode.R)) {
                RemoveStone();                
            }
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            isGrabbed = true;
            tmp_pS = col.gameObject.GetComponent<PlayerMovement>().playerIndex;
        }
        if (col.CompareTag("Bucket")){
            isGrabbed = false;
            //transform.position = OriginalPos;
        }
    }

    private void RemoveStone(){
        //To detect stone in front of us we use a raycast
        Vector3 playerDirection = players[tmp_pS - 1].GetComponent<PlayerMovement>().direction;
        playerDirection = players[tmp_pS - 1].transform.TransformDirection(playerDirection);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerDirection, out hit, 2.0f)){
            if (hit.transform.CompareTag("Stone")){
                Destroy(hit.transform.gameObject);
                breakStoneAudio.PlayOneShot(breakStoneSound, 1.0f);
            }
        }
    }
}
