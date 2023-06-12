using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmptyObjectGenerator : MonoBehaviour
{
    public int numberOfObjectsX; // Number of objects to generate in X axis
    public int numberOfObjectsZ; // Number of objects to generate in Y axis
    public float separation = 10f; // Separation for each platform center
    public GameObject Platform;
    public bool Done = false;

    private void Start()
    {
        //Generate platform on start.
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

                // Get the size of the platform
                Vector3 platformSize = new Vector3(9.5f, 8f, 9.5f);

                // Calculate the bounds of the platform
                Bounds platformBounds = new Bounds(position, platformSize);

                // Check for overlapping colliders with the "Empty" tag
                Collider[] overlappingColliders = new Collider[]{}; // Adjust the size as per your needs
                int numOverlappingColliders = Physics.OverlapBoxNonAlloc(platformBounds.center, platformSize / 2, overlappingColliders);

                bool overlapWithEmpty = false;

                // Check each overlapping collider
                for (int r = 0; r < overlappingColliders.Length; r++)
                {
                    Collider collider = overlappingColliders[r];
                    if (collider.CompareTag("Empty") || collider.CompareTag("Platform") || collider.CompareTag("Stone") || collider.CompareTag("Obstacle") || collider.CompareTag("Obsidian"))
                    {
                        overlapWithEmpty = true;
                        break;
                    }
                }
                
                //If is not overlapping instantiate the prefab emptyplatform. 
                if (!overlapWithEmpty)
                {
                    GameObject newObj = Instantiate(Platform, position, Quaternion.identity);
                    if(j == 8){
                        newObj.GetComponent<PlatformController>().UpdatePlatform("Stone");
                    }
                    newObj.GetComponent<PlatformController>().ID[0] = i;
                    newObj.GetComponent<PlatformController>().ID[1] = j;
                }
                
            }
        }
    }

    //Fill the space of the position set with a emptyplatform prefab.
    public void FillPlatforms(int i, int j){
        Vector3 position = new Vector3((i+1) * separation, 0, (j+1) * separation);
        GameObject newObj = Instantiate(Platform, position, Quaternion.identity);
        newObj.GetComponent<PlatformController>().ID[0] = i;
        newObj.GetComponent<PlatformController>().ID[1] = j;
    }

}
