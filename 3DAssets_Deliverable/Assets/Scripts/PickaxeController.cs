using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeController : MonoBehaviour
{
    private bool isGrabbed = false;
    public List<GameObject> players = new List<GameObject>();
    private Vector3 OriginalPos;
    private int tmp_pS;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log(isGrabbed);
        if(isGrabbed){
            transform.position = players[tmp_pS -1].transform.position;
            if(Input.GetKeyDown(KeyCode.R)){
                RemoveStone();
            }
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            isGrabbed = true;
            tmp_pS = GameObject.Find("PluginController").GetComponent<PluginConnector>().playerSelected;

        }
        if(col.CompareTag("Bucket")){
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
            }
        }

    }
}
