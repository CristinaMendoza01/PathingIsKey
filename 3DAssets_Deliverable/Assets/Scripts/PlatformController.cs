using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    //GameObjects
    private GameObject waterRiverObj;
    public GameObject PlatGen;
    public GameObject Stone;
    public GameObject Obsidian;
    public GameObject Water;
    public GameObject Lava;

    public bool notValidPos;

    //AUDIO
    private AudioSource stoneAudio;
    public AudioClip stoneSound;

    private AudioSource dropWaterAudio;
    public AudioClip dropWaterSound;

    private AudioSource lavaAudio;
    public AudioClip lavaSound;

    private AudioSource obsidianAudio;
    public AudioClip obsidianSound;

    public bool inRiver;

    //Unique ID for each platform
    public int[] ID = new int[2];


    // Start is called before the first frame update
    void Awake()
    {        
        Stone.SetActive(false);

        Obsidian.SetActive(false);

        Lava.SetActive(false);

        Water.SetActive(false);

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
            this.transform.position += Vector3.right * Time.deltaTime * waterRiverObj.GetComponent<WaterRiver>().FlowSpeed * -waterRiverObj.GetComponent<WaterRiver>().direction;
        }
    }

    public void UpdatePlatform(string name){ //Updates the state of the platform

        switch (name) {
            case "Stone": //converts the platform into stone
                Stone.SetActive(true);
                Lava.SetActive(false);
                transform.gameObject.tag = "Stone";
                stoneAudio.PlayOneShot(stoneSound, 1.0f); 
                break;
            case "Obsidian"://converts the platform into obsidian
                Obsidian.SetActive(true);
                Water.SetActive(false);
                transform.gameObject.tag = "Obsidian";
                obsidianAudio.PlayOneShot(obsidianSound, 1.0f);
                break;
            case "Lava"://converts the platform into lava
                Lava.SetActive(true);
                lavaAudio.PlayOneShot(lavaSound, 1.0f); 
                break;
            case "Water"://converts the platform into water
                Water.SetActive(true);
                dropWaterAudio.PlayOneShot(dropWaterSound, 1.0f); 
                break;
            case "Empty"://converts the platform into emptyplatform
                Stone.SetActive(false);
                Obsidian.SetActive(false);
                Lava.SetActive(false);
                Water.SetActive(false);
                transform.gameObject.tag = "Empty";
                break;    
        }
    }
    
    void OnTriggerEnter(Collider col){

        //Possible overlap check
        if (col.CompareTag("Platform") || col.CompareTag("Stone") || col.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }

        //Stone in river
        if(col.CompareTag("WaterRiver") && this.transform.CompareTag("Stone")){
            inRiver = true;
            this.transform.gameObject.tag = "WaterStone";
        }

        //Stone goes to the limit of the map
        if(col.CompareTag("Boundary")){
            Debug.Log(ID[0].ToString() + ID[1].ToString());
            Debug.Log("DESTROYED STONE");
            PlatGen.transform.GetComponent<EmptyObjectGenerator>().FillPlatforms(ID[0],ID[1]);
            this.gameObject.SetActive(false);
        }

        //Stone in river collides with obsidian
        if(col.CompareTag("Obsidian") && this.transform.CompareTag("WaterStone")){
            inRiver = false;
            this.transform.position = new Vector3(col.transform.position.x  + (10 * waterRiverObj.GetComponent<WaterRiver>().direction), this.transform.position.y, this.transform.position.z);
        }
    }

    void OnTriggerStay(Collider col){
        if(col.CompareTag("WaterRiver") && this.transform.CompareTag("WaterStone")){
            if(waterRiverObj.transform.position == waterRiverObj.GetComponent<WaterRiver>().originalPos){
                inRiver = true;
            }
        }
    }
}
