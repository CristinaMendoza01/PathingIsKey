using UnityEngine;

public class EmptyObjectGenerator : MonoBehaviour
{
    public int numberOfObjectsX; // Número de objetos a generar en el eje X
    public int numberOfObjectsZ; // Número de objetos a generar en el eje Z
    public float separation = 10f; // La separación entre los objetos
    public GameObject Platform;
    public bool Done = false;

    private void Start()
    {
        GeneratePlatforms();

        // StonePlatforms = GameObject.FindGameObjectsWithTag("Stone");
        // StonePlatforms_size = StonePlatforms.Length;
    }

    public void GeneratePlatforms(){
        GameObject waterRiver = GameObject.FindGameObjectWithTag("WaterRiver");
        GameObject lavaRiver = GameObject.FindGameObjectWithTag("LavaRiver");
        for (int i = 0; i < numberOfObjectsX; i++)
        {
            for (int j = 0; j < numberOfObjectsZ; j++)
            {
                // Calcula la posición del próximo objeto
                Vector3 position = new Vector3((i+1) * separation, 0, (j+1) * separation);
                
                // Genera un nuevo objeto vacío
                GameObject newObj = Instantiate(Platform);
                newObj.transform.position = position;

                // SI LO HA HECHO UNA VEZ, MIRA QUE NO SE SOBREPONGA CON OTRA EMPTYPLATFORM CREADA ANTERIORMENTE
                // SI SE SOBREPONE NO DEBERIA CREARSE
                // if(Done){

                // }
            }
        }
        Done = true;
    }
}
