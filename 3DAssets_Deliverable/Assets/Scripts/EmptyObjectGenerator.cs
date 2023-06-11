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

   public void GeneratePlatforms()
{
    GameObject waterRiver = GameObject.FindGameObjectWithTag("WaterRiver");
    GameObject lavaRiver = GameObject.FindGameObjectWithTag("LavaRiver");

    for (int i = 0; i < numberOfObjectsX; i++)
    {
        for (int j = 0; j < numberOfObjectsZ; j++)
        {
            // Position of the next object
            Vector3 position = new Vector3((i+1) * separation, 0, (j+1) * separation);

            // Get the size of the platform prefab
            Vector3 platformSize = Platform.GetComponent<BoxCollider>().size;

            // Calculate the bounds of the platform
            Bounds platformBounds = new Bounds(position, platformSize);

            // Check for overlapping colliders with the "EmptyPlatform" or "Obstacle" tag
            Collider[] overlappingColliders = new Collider[10]; // Adjust the size as per your needs
            int numOverlappingColliders = Physics.OverlapBoxNonAlloc(platformBounds.center, platformSize / 2, overlappingColliders);

            bool overlapWithEmptyOrObstacle = false;

            // Check each overlapping collider and if in the position there is no object with the "EmptyPlatform" or "Obstacle" tag, then generate a new object
            for (int r = 0; r < numOverlappingColliders; r++)
            {
                Collider collider = overlappingColliders[r];
                if (collider.CompareTag("Empty") || collider.CompareTag("Obstacle"))
                {
                    overlapWithEmptyOrObstacle = true;
                    break;
                }
            }

            if (!overlapWithEmptyOrObstacle)
            {
                // Generates a new platform
                GameObject newObj = Instantiate(Platform);
                newObj.transform.position = position;
            }
        }
    }
    Done = true;
}

}
