using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRiver : MonoBehaviour
{
    public int direction;
    public Vector3 originalPos;
    public bool RiverFlow;
    public bool isBlocked;
    public int FlowSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
        direction = Random.Range(-1,1);
        if (direction >= 0){
            direction = 1;
        }
        else{
            direction = -1;
        }
        
        RiverFlow = true;
        isBlocked = false;

        Debug.Log(direction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Stone")){
            collider.gameObject.GetComponent<PlatformController>().inRiver = true;
        }
        // if(collider.CompareTag("Obsidian")){
        //     Debug.Log("BLOCKED");
        //     Vector3 displacement = new Vector3 (collider.transform.position.x - (((this.transform.localScale.x *10 ) / 2 + this.transform.position.x )*direction), 0, 0);
        //     this.transform.position = this.transform.position + displacement;
        // }
    }

    void OnTriggerStay(Collider collider) {
        if(collider.CompareTag("Obsidian") && RiverFlow){
            float displacement = collider.transform.position.x - (((this.transform.localScale.x *10 ) / 2 )* (-direction));
            this.transform.position = new Vector3(displacement, this.transform.position.y, this.transform.position.z);
            RiverFlow = false;
            isBlocked = true;
        }
    }


    // void OnTriggerExit(Collider collider) {
    //     if(collider.CompareTag("Obsidian")){
    //         this.transform.position = originalPos;
    //     }
        
    // }

    public void GoToOriginalPos(){
        this.transform.position = originalPos;
    }
}

