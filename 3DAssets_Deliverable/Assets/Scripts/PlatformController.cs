using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject Stone;
    public GameObject Obsidian;
    public GameObject Water;
    public GameObject Lava;

    public bool notValidPos;
    // Start is called before the first frame update
    void Start()
    {        
        // Stone = this.gameObject.transform.GetChild(0).transform.gameObject;
        // Obsidian = this.gameObject.transform.GetChild(1).transform.gameObject;
        // Lava = this.gameObject.transform.GetChild(2).transform.gameObject;
        // Water = this.gameObject.transform.GetChild(3).transform.gameObject;

        Debug.Log(Stone.transform.name);
        Stone.SetActive(false);
        Debug.Log(Obsidian.transform.name);
        Obsidian.SetActive(false);
        Debug.Log(Lava.transform.name);
        Lava.SetActive(false);
        Debug.Log(Water.transform.name);
        Water.SetActive(false);

        notValidPos = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePlatform(string name){
        switch (name) {
            case "Stone":
                Stone.SetActive(true);
                Lava.SetActive(false);
                //AUDIO STONE
                break;
            case "Obsidian":
                Obsidian.SetActive(true);
                Water.SetActive(false);
                //AUDIO OBSIDIAN
                break;
            case "Lava":
                Lava.SetActive(true);
                //AUDIO LAVA
                break;
            case "Water":
                //Debug.Log("ININININININ");
                Water.SetActive(true);
                //AUDIO WATER
                break;
        }
    }
    
    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Platform") || col.CompareTag("Stone")){
            //Debug.Log("aqui"+col.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
