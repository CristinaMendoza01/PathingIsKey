using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRiver : MonoBehaviour
{
    public int direction;
    private Vector3 originalPos;
    private bool RiverFlow;
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
// }

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class WaterRiver : MonoBehaviour
// {
//     public int direction;
//     private Vector3 originalPos;
//     //private int obsidianCount = 0; // Añade esto para llevar la cuenta de bloques de obsidiana

//     // Start is called before the first frame update
//     void Start()
//     {
//         originalPos = this.transform.position;
//         direction = Random.Range(-1,1);
//         if (direction >= 0){
//             direction = 1;
//         }
//         else{
//             direction = -1;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     void OnTriggerEnter(Collider collider){
//         if(collider.CompareTag("Stone")){
//             collider.gameObject.GetComponent<PlatformController>().inRiver = true;
//         }
//         if(collider.CompareTag("Obsidian")){
//             Debug.Log("INININIINNI");
//             //obsidianCount++; // Aumenta el conteo cuando entra un bloque de obsidiana
//             Vector3 displacement = new Vector3 (collider.transform.position.x - (((this.transform.localScale.x *10 ) / 2 + this.transform.position.x )*direction), 0, 0);
//             this.transform.position = this.transform.position + displacement;
//         }
//     }

    // void OnTriggerExit(Collider collider) {
    //     if(collider.CompareTag("Obsidian")){
    //         obsidianCount--; // Disminuye el conteo cuando sale un bloque de obsidiana
    //         if(obsidianCount == 0) // Solo vuelve a la posición original si no hay más bloques de obsidiana
    //             this.transform.position = originalPos;
    //     }
    // }

    // public void DecreaseObsidianCount() {
    //     obsidianCount--;
    //     if(obsidianCount == 0)
    //         this.transform.position = originalPos;
    // }


}

