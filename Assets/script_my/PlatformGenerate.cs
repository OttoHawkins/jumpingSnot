using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerate : MonoBehaviour
{
    public GameObject staticPlatformPrefab;       
    public GameObject movingPlatformPrefab;       
    public GameObject disappearingPlatformPrefab;    
    public GameObject pulsatingPlatformPrefab;       

    public float platformSpacingMin = 2f;          
    public float platformSpacingMax = 4f;          
    public float spawnRangeX = 1.7f;                

    private float highestPlatformY = 0f;            
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private bool lastWasDisappearing = false;    

    void Start()
    {

        for (int i = 0; i < 10; i++)
        {
            GeneratePlatform();
        }
    }

    void Update()
    {
        if (Camera.main.transform.position.y + 10f > highestPlatformY)
        {
            GeneratePlatform();
        }
    }

    void GeneratePlatform()
    {
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = Random.Range(-spawnRangeX, spawnRangeX);
        spawnPosition.y = highestPlatformY + Random.Range(platformSpacingMin, platformSpacingMax);
        GameObject platformPrefab;

        if (lastWasDisappearing)
        {
          
            float platformTypeChance = Random.value;
            if (platformTypeChance < 0.5f)
            {
                platformPrefab = staticPlatformPrefab; 
            }
            else
            {
                platformPrefab = movingPlatformPrefab; 
            }
            lastWasDisappearing = false;
        }
        else
        {
            float platformTypeChance = Random.value;
            if (platformTypeChance < 0.4f)
            {
                platformPrefab = staticPlatformPrefab;
                lastWasDisappearing = false;           
            }
            else if (platformTypeChance < 0.7f)
            {
                platformPrefab = movingPlatformPrefab;
                lastWasDisappearing = false;          
            }
            else if (platformTypeChance < 0.9f)
            {
                platformPrefab = disappearingPlatformPrefab; 
                lastWasDisappearing = true;              
            }
            else
            {
                platformPrefab = pulsatingPlatformPrefab; 
                lastWasDisappearing = false;             
            }
        }
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        spawnedPlatforms.Add(newPlatform);

        highestPlatformY = spawnPosition.y;
    }
}