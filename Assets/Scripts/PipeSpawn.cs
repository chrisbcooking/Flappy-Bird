using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public List<GameObject> pipePrefabs;
    public GameObject instantiatedPipe;
    private float spawnRate = 4.5f;
    private float timer = 0f;
    private float heightOffset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe(pipePrefabs[0]);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            float pipeChance = Random.Range(0f, 1f);

            if(pipeChance <= 0.1f) // 10%
            {
                SpawnPipe(pipePrefabs[2]);
            }
            else if (pipeChance <= 0.25f) // 25%
            {
                SpawnPipe(pipePrefabs[1]);
            }
            else
            {
                SpawnPipe(pipePrefabs[2]);
            }

            timer = 0;
        }
    }
    void SpawnPipe(GameObject pipe)
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        instantiatedPipe = Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint,highestPoint), 0), transform.rotation, gameObject.transform);
    }
}
