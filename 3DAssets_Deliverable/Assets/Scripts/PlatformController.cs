using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject Stone;
    public GameObject Obsidian;
    public GameObject Water;
    public GameObject Lava;
    // public GameObject Obstacle;

    public bool notValidPos;

    private AudioSource stoneAudio;
    public AudioClip stoneSound;

    private AudioSource dropWaterAudio;
    public AudioClip dropWaterSound;

    private AudioSource lavaAudio;
    public AudioClip lavaSound;

    private AudioSource obsidianAudio;
    public AudioClip obsidianSound;

    private GameObject waterRiverObj;
    public bool inRiver;


    // Start is called before the first frame update
    void Awake()
    {        
        // Stone = this.gameObject.transform.GetChild(0).transform.gameObject;
        // Obsidian = this.gameObject.transform.GetChild(1).transform.gameObject;
        // Lava = this.gameObject.transform.GetChild(2).transform.gameObject;
        // Water = this.gameObject.transform.GetChild(3).transform.gameObject;

        //Debug.Log(Stone.transform.name);
        Stone.SetActive(false);
        //Debug.Log(Obsidian.transform.name);
        Obsidian.SetActive(false);
        //Debug.Log(Lava.transform.name);
        Lava.SetActive(false);
        //Debug.Log(Water.transform.name);
        Water.SetActive(false);

        // Obstacle.SetActive(false);


        notValidPos = false;

        stoneAudio = GetComponent<AudioSource>();
        dropWaterAudio = GetComponent<AudioSource>();
        lavaAudio = GetComponent<AudioSource>();
        obsidianAudio = GetComponent<AudioSource>();

        inRiver = false;
        
        waterRiverObj = GameObject.FindGameObjectWithTag("WaterRiver");

    }

    // Update is called once per frame
    void Update()
    {
        if(inRiver){
            Debug.Log("MOVING STONE");
            this.transform.position += Vector3.right * Time.deltaTime * waterRiverObj.GetComponent<WaterRiver>().FlowSpeed * -waterRiverObj.GetComponent<WaterRiver>().direction;
        }
    }

    public void UpdatePlatform(string name){

        switch (name) {
            case "Stone":
                //Debug.Log("Stone");
                Stone.SetActive(true);
                Lava.SetActive(false);
                transform.gameObject.tag = "Stone";
                //AUDIO STONE
                stoneAudio.PlayOneShot(stoneSound, 1.0f); 
                break;
            case "Obsidian":
                //Debug.Log("Obsidian");
                Obsidian.SetActive(true);
                Water.SetActive(false);
                transform.gameObject.tag = "Obsidian";
                //AUDIO OBSIDIAN
                obsidianAudio.PlayOneShot(obsidianSound, 1.0f);
                break;
            case "Lava":
                //Debug.Log("Lava");
                Lava.SetActive(true);
                //AUDIO LAVA
                lavaAudio.PlayOneShot(lavaSound, 1.0f); 
                break;
            case "Water":
                //Debug.Log("ININININININ");
                //Debug.Log("Water");
                Water.SetActive(true);
                //AUDIO WATER
                dropWaterAudio.PlayOneShot(dropWaterSound, 1.0f); 
                break;
            case "Empty":
                //Debug.Log("Empty");
                Stone.SetActive(false);
                Obsidian.SetActive(false);
                Lava.SetActive(false);
                Water.SetActive(false);
                transform.gameObject.tag = "Empty";
                break;    
        }
    }
    
    void OnTriggerEnter(Collider col){
        if (col.CompareTag("Platform") || col.CompareTag("Stone"))
        {
            Destroy(this.gameObject);
        }
        if(col.CompareTag("WaterRiver") && this.transform.CompareTag("Stone")){
            Debug.Log("MOVING STONE");
            inRiver = true;
            this.transform.gameObject.tag = "WaterStone";
        }
        if(col.CompareTag("Boundary")){
            Destroy(this.gameObject);
            GameObject.FindGameObjectWithTag("Generator").GetComponent<EmptyObjectGenerator>().GeneratePlatforms();
        }
        if(col.CompareTag("Obsidian") && this.transform.CompareTag("WaterStone")){
            inRiver = false;
            this.transform.position = new Vector3(col.transform.position.x  + (10 * waterRiverObj.GetComponent<WaterRiver>().direction), this.transform.position.y, this.transform.position.z);
        }
        if(col.CompareTag("WaterStone") && (col.transform.position == this.transform.position)) Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider col){
        if(col.CompareTag("WaterRiver") && this.transform.CompareTag("WaterStone")){
            if(waterRiverObj.transform.position == waterRiverObj.GetComponent<WaterRiver>().originalPos){
                inRiver = true;
            }
        }
    }

//     public WaterRiver river; // Puedes asignar esto en el Inspector de Unity

    void OnDestroy() {
        if(this.transform.tag == "Obsidian") {
            waterRiverObj.GetComponent<WaterRiver>().RiverFlow = true;
        }   
    }

}
