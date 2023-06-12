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

    public GameObject PlatGen;
    private GameObject waterRiverObj;
    public bool inRiver;

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

    public void UpdatePlatform(string name){

        switch (name) {
            case "Stone":
                Stone.SetActive(true);
                Lava.SetActive(false);
                transform.gameObject.tag = "Stone";
                //AUDIO STONE
                stoneAudio.PlayOneShot(stoneSound, 1.0f); 
                break;
            case "Obsidian":
                Obsidian.SetActive(true);
                Water.SetActive(false);
                transform.gameObject.tag = "Obsidian";
                //AUDIO OBSIDIAN
                obsidianAudio.PlayOneShot(obsidianSound, 1.0f);
                break;
            case "Lava":
                Lava.SetActive(true);
                //AUDIO LAVA
                lavaAudio.PlayOneShot(lavaSound, 1.0f); 
                break;
            case "Water":
                Water.SetActive(true);
                //AUDIO WATER
                dropWaterAudio.PlayOneShot(dropWaterSound, 1.0f); 
                break;
            case "Empty":
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
            //Debug.Log("MOVING STONE");
            inRiver = true;
            this.transform.gameObject.tag = "WaterStone";
        }

        //Stone goes to the limit of the map
        if(col.CompareTag("Boundary")){
            Debug.Log(ID[0].ToString() + ID[1].ToString());
            Debug.Log("DESTROYED STONE");
            PlatGen.transform.GetComponent<EmptyObjectGenerator>().FillPlatforms(ID[0],ID[1]);
            this.gameObject.SetActive(false);
            //PlatGen.transform.GetComponent<EmptyObjectGenerator>().GeneratePlatforms();
        }

        //Stone in river collides with obsidian
        if(col.CompareTag("Obsidian") && this.transform.CompareTag("WaterStone")){
            inRiver = false;
            this.transform.position = new Vector3(col.transform.position.x  + (10 * waterRiverObj.GetComponent<WaterRiver>().direction), this.transform.position.y, this.transform.position.z);
        }

        //
        //if(col.CompareTag("WaterStone") && (col.transform.position == this.transform.position)) Destroy(this.gameObject);


    }

    void OnTriggerStay(Collider col){
        if(col.CompareTag("WaterRiver") && this.transform.CompareTag("WaterStone")){
            if(waterRiverObj.transform.position == waterRiverObj.GetComponent<WaterRiver>().originalPos){
                inRiver = true;
            }
        }
    }

//     public WaterRiver river; // Puedes asignar esto en el Inspector de Unity


}
