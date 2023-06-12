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
        originalPos = this.transform.position; //Store the original position
        direction = Random.Range(-1,1); //Random river flow.
        if (direction >= 0){
            direction = 1;
        }
        else{
            direction = -1;
        }
        
        RiverFlow = true;
        isBlocked = false;
    }

    void OnTriggerEnter(Collider collider){
        //if collides with the stone, stone is in the river. Update inRiver flag.
        if(collider.CompareTag("Stone")){
            collider.gameObject.GetComponent<PlatformController>().inRiver = true;
        }
    }

    //When an obsidian platform collides with the river --> block the river flow.
    void OnTriggerStay(Collider collider) {
        if(collider.CompareTag("Obsidian") && RiverFlow){
            float displacement = collider.transform.position.x - (((this.transform.localScale.x *10 ) / 2 )* (-direction));
            this.transform.position = new Vector3(displacement, this.transform.position.y, this.transform.position.z);
            RiverFlow = false;
            isBlocked = true;
        }
    }

    //Set the position of the river like it was in the start.
    public void GoToOriginalPos(){
        this.transform.position = originalPos;
    }
}

