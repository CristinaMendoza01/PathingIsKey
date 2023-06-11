using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    }

    public void GeneratePlatforms(){
        GameObject waterRiver = GameObject.FindGameObjectWithTag("WaterRiver");
        GameObject lavaRiver = GameObject.FindGameObjectWithTag("LavaRiver");
        for (int i = 0; i < numberOfObjectsX; i++)
        {
            for (int j = 0; j < numberOfObjectsZ; j++)
            {
                // Position of the next object
                Vector3 position = new Vector3((i+1) * separation, 0, (j+1) * separation);
                
                // Generates a new object with the Platform prefab
                GameObject newObj = Instantiate(Platform);
                newObj.transform.position = position;

                // SI LO HA HECHO UNA VEZ, MIRA QUE NO SE SOBREPONGA CON OTRA EMPTYPLATFORM CREADA ANTERIORMENTE
                // SI SE SOBREPONE NO DEBERIA CREARSE
                if (Done)
                {
                    // Get the size of the platform
                    Vector3 platformSize = newObj.GetComponent<BoxCollider>().size;

                    // Calculate the bounds of the platform
                    Bounds platformBounds = new Bounds(newObj.transform.position, platformSize);

                    // Check for overlapping colliders with the "EmptyPlatform" tag
                    Collider[] overlappingColliders = new Collider[10]; // Adjust the size as per your needs
                    int numOverlappingColliders = Physics.OverlapBoxNonAlloc(platformBounds.center, platformSize / 2, overlappingColliders);

                    bool overlapWithEmpty = false;

                    // Check each overlapping collider
                    for (int r = 0; r < numOverlappingColliders; r++)
                    {
                        Collider collider = overlappingColliders[r];
                        if (collider.CompareTag("Empty"))
                        {
                            overlapWithEmpty = true;
                            break;
                        }
                    }

                    if (overlapWithEmpty)
                    {
                        Destroy(newObj); // Destroy the newly instantiated platform
                    }
                }


            }
        }
        Done = true;
    }
}
