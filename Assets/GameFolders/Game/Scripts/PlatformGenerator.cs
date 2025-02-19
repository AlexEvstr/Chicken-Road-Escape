using UnityEngine;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private int PlatformSelector;
    private float[] platformWidths;
    public ObjectPooler[] objPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    public StarGenerator theStarGenerator;
    public float randomStarThreshold;

    public float randomSpikeThreshold;
    public ObjectPooler spikePool;

    public float powerUpHeight;
    public ObjectPooler powerUpPool;
    public float powerUpThreshold;

    void Start()
    {
        platformWidths = new float[objPools.Length];

        for (int i = 0; i < objPools.Length; i++)
        {
            platformWidths[i] = objPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;
        theStarGenerator = FindObjectOfType<StarGenerator>();
    }

    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            PlatformSelector = Random.Range(0, objPools.Length);

            heightChange = transform.position.y + Random.Range(-maxHeightChange, maxHeightChange);
            heightChange = Mathf.Clamp(heightChange, minHeight, maxHeight);

            List<Vector3> occupiedPositions = new List<Vector3>();

            transform.position = new Vector3(
                transform.position.x + (platformWidths[PlatformSelector] / 2) + distanceBetween,
                heightChange,
                transform.position.z
            );

            GameObject newPlatform = objPools[PlatformSelector].getPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            //if (Random.Range(0f, 100f) < powerUpThreshold)
            //{
            //    Vector3 powerUpPos = transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight / 2, powerUpHeight), 0f);
            //    if (!IsPositionOccupied(powerUpPos, occupiedPositions, 1f))
            //    {
            //        GameObject newPowerUp = powerUpPool.getPooledObject();
            //        newPowerUp.transform.position = powerUpPos;
            //        newPowerUp.SetActive(true);
            //        occupiedPositions.Add(powerUpPos);
            //    }
            //}

            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                int attempts = 10;
                bool foundPosition = false;

                while (!foundPosition && attempts > 0)
                {
                    //float spikePositionX = Random.Range(-platformWidths[PlatformSelector] / 2 + 1f, platformWidths[PlatformSelector] / 2 - 1f);
                    float spikePositionX = 0;
                    Vector3 spikePos = transform.position + new Vector3(spikePositionX, 6f, 0f);

                    if (!IsPositionOccupied(spikePos, occupiedPositions, 1f))
                    {
                        GameObject newSpike = spikePool.getPooledObject();
                        newSpike.transform.position = spikePos;
                        newSpike.transform.rotation = transform.rotation;
                        newSpike.SetActive(true);
                        occupiedPositions.Add(spikePos);
                        foundPosition = true;
                    }
                    attempts--;
                }
            }

            if (Random.Range(0f, 100f) < randomStarThreshold)
            {
                Vector3 starPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                if (!IsPositionOccupied(starPos, occupiedPositions, 1f))
                {
                    theStarGenerator.spawnStars(starPos);
                    occupiedPositions.Add(starPos);
                }
            }

            transform.position = new Vector3(
                transform.position.x + (platformWidths[PlatformSelector] / 2),
                transform.position.y,
                transform.position.z
            );
        }
    }

    private bool IsPositionOccupied(Vector3 position, List<Vector3> occupiedPositions, float tolerance = 1f)
    {
        foreach (var occupiedPos in occupiedPositions)
        {
            if (Vector3.Distance(position, occupiedPos) < tolerance)
                return true;
        }
        return false;
    }
}