using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int playerIndex;
    [SerializeField] private bool isValidpos = true;

    [SerializeField] private Vector3 tmppos;

    // Variable to calculate the direction
    private Vector3 lastPosition;
    [HideInInspector] public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        tmppos = transform.position;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the direction
        direction = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;
        //Debug.Log(isValidpos);
        if(!isValidpos) transform.position = tmppos;
        // transform.position 
    }

    public void setPosition(Vector3 pos)
    {
        //switch playerIndex
        transform.position = pos;
        // if(isValidpos){
        //     transform.position = pos;
        // }else{
        //     transform.position = tmppos;
        // }
    }

    // public bool CanMove(Vector3 newPos)
    // {
    //     // Solo permite moverse al jugador si está en una zona válida
    //     if (isValid)
    //     {
    //         lastValidPosition = transform.position; // Almacena la posición actual antes de moverse
    //         return true;
    //     }
    //     else
    //     {
    //         // Si el jugador intenta moverse a una zona inválida, vuelve a la última posición válida conocida
    //         transform.position = lastValidPosition;
    //         return false;
    //     }
    // }

    void OnTriggerStay(Collider col){
        if(col.CompareTag("Platform") || col.CompareTag("Platform")){
            tmppos = transform.position;
            isValidpos = true;
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("Stone")){
            isValidpos = false;
            transform.position = tmppos;
        }
    }
}
