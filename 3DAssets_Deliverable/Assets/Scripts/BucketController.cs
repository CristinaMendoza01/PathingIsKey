using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{
    private bool isGrabbed = false;
    public List<GameObject> players = new List<GameObject>();
    private int tmp_pS;
    private Vector3 OriginalPos;

    public GameObject WaterBucket;
    public GameObject LavaBucket;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPos = transform.position;
        WaterBucket.SetActive(false);
        LavaBucket.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log(isGrabbed);
        if(isGrabbed){
            transform.position = players[tmp_pS -1].transform.position;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Player")){
            isGrabbed = true;
            tmp_pS = GameObject.Find("PluginController").GetComponent<PluginConnector>().playerSelected;
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
            WaterBucket.SetActive(true);
            LavaBucket.SetActive(false);
        }
        if(col.CompareTag("Lava")){
            LavaBucket.SetActive(true);
            WaterBucket.SetActive(false);
        }
    }
}
