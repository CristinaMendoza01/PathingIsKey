using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public int playerIndex;
    //[SerializeField] private bool isValidpos = true;

    private Vector3 tmppos;
    private Vector3 tmppos_offset;

    // Variable to calculate the direction
    private Vector3 lastPosition;
    [HideInInspector] public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        tmppos = new Vector3(79.5f, 0, 76.8f);
        tmppos_offset = new Vector3(0f, 0, 0f);
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the direction
        direction = (transform.position - lastPosition).normalized;
        lastPosition = transform.position;
        //Debug.Log(direction);
        //Debug.Log(isValidpos);
         
        // transform.position 
    }

    public void setPosition(Vector3 pos)
    {
        //switch playerIndex
        //if (isValidpos) transform.position = pos;
        //else transform.position = tmppos;

        float x = Mathf.Clamp(pos.x, tmppos.x - tmppos_offset.x, tmppos.x + tmppos_offset.x);
        float y = Mathf.Clamp(pos.y, tmppos.y - tmppos_offset.y, tmppos.y + tmppos_offset.y);
        float z = Mathf.Clamp(pos.z, tmppos.z - tmppos_offset.z, tmppos.z + tmppos_offset.z);

        transform.position = new Vector3(x, y, z);

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

    //void OnTriggerStay(Collider col){
    //    if(col.CompareTag("Platform") || col.CompareTag("Stone")){
    //        //tmppos = transform.position;
    //        tmppos = col.transform.position;
    //        tmppos_offset = col.transform.localScale*10 / 2;
    //        //isValidpos = true;
    //    }
    //}

    //void OnTriggerExit(Collider col){
    //    if(col.CompareTag("Platform") || col.CompareTag("Stone")){
    //        //isValidpos = false;
    //    }
    //}
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Platform") || col.CompareTag("Stone"))
        {
            //isValidpos = true;
            tmppos = col.transform.position;
            tmppos_offset = col.transform.localScale * 10 / 2;
        }
    }

}
