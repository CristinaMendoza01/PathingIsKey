using UnityEngine;

public class EmptyObjectGenerator : MonoBehaviour
{
    public int numberOfObjectsX; // Número de objetos a generar en el eje X
    public int numberOfObjectsZ; // Número de objetos a generar en el eje Z
    public float separation = 10f; // La separación entre los objetos
    public GameObject Platform;
    public int waterRiverRow;

    public int lavaRiverRow;

    // private GameObject[] StonePlatforms;
    // private int StonePlatforms_size;

    private void Start()
    {
        GeneratePlatforms();

        // StonePlatforms = GameObject.FindGameObjectsWithTag("Stone");
        // StonePlatforms_size = StonePlatforms.Length;
    }

    // private void Update(){

    //     if(StonePlatforms_size >= 82){
    //         Debug.Log(StonePlatforms_size);
    //     }
    // }
    public void GeneratePlatforms(){
        GameObject waterRiver = GameObject.FindGameObjectWithTag("WaterRiver");
        GameObject lavaRiver = GameObject.FindGameObjectWithTag("LavaRiver");
        //Debug.Log(waterRiver);
        for (int i = 0; i < numberOfObjectsX; i++)
        {
            for (int j = 0; j < numberOfObjectsZ; j++)
            {
                // Calcula la posición del próximo objeto
                Vector3 position = new Vector3((i+1) * separation, 0, (j+1) * separation);
                
                // Genera un nuevo objeto vacío
                GameObject newObj = Instantiate(Platform);
                newObj.transform.position = position;
                //if(newObj.transform.GetComponent<PlatformController>().notValidPos) Destroy(newObj);
                // if (j == waterRiverRow){
                //     newObj.GetComponent<PlatformController>().UpdatePlatform("Water");
                //     newObj.transform.SetParent(waterRiver.transform);
                // }
                // if(j == lavaRiverRow){
                //     newObj.GetComponent<PlatformController>().UpdatePlatform("Lava");
                //     newObj.transform.SetParent(lavaRiver.transform);
                // }
            }
        }
    }
}
