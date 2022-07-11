using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{

    // Gameplay
    private float chunkSpawnZ;
    private Queue<Chunk> activeChunks = new Queue<Chunk>();
    private List<Chunk> chunkPool = new List<Chunk>();

    // Configurable fields
    [SerializeField] private int firstChunkSpawnPosition = 10;
    [SerializeField] private int chunkOnScreen = 5;
    [SerializeField] private float despawnDistance = 5f;

    [SerializeField] private List<GameObject> chunkPrefabs;
    [SerializeField] private Transform cameraTransform;


    private void Awake()
    {
        ResetWorld();
    }

    private void Start()
    {
        // Check if we have an empty chunkPrefab list
        if (chunkPrefabs.Count == 0)
        {
            Debug.LogError("No chunk prefabs found");
            return;
        }

        // Try to assign the camera if its not already assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        ScanPosition();
    }


    /// <summary>
    /// Scan position of the camera to spawn new chunks
    /// </summary>
    private void ScanPosition()
    {
        float cameraZ = cameraTransform.position.z;
        Chunk lastChunk = activeChunks.Peek();


        // if we're far enough - spawn new chunk and delete the last one
        if (cameraZ >= lastChunk.transform.position.z + lastChunk.chunkLength + despawnDistance)
        {
            SpawnNewChunk();
            DeleteLastChunk();
        }
    }

    private void SpawnNewChunk()
    {
        // Get a random index for which prefab to spawn
        int randomIndex = Random.Range(0, chunkPrefabs.Count);

        // Does it already exist within our pool
        Chunk chunk = chunkPool.Find(x => !x.gameObject.activeSelf && x.name == (chunkPrefabs[randomIndex].name + "(Clone)"));

        // If it doesnt exist. Create a chunk if we were not able to reuse
        if (chunk == null)
        {
            GameObject go = Instantiate(chunkPrefabs[randomIndex], transform);
            chunk = go.GetComponent<Chunk>();
        }

        // Place the object and show it
        chunk.transform.position = new Vector3(0, 0, chunkSpawnZ);
        chunkSpawnZ += chunk.chunkLength;


        // Store the valuem to reuse in our pool
        activeChunks.Enqueue(chunk);
        chunk.ShowChunk();
    }

    private void DeleteLastChunk()
    {
        Chunk chunk = activeChunks.Dequeue();
        chunk.HideChunk();
        chunkPool.Add(chunk);
    }

    public void ResetWorld()
    {
        // Reset the chunkSpawnZ
        chunkSpawnZ = firstChunkSpawnPosition;

        for (int i = activeChunks.Count; i != 0; i--)
        {
            DeleteLastChunk();
        }


        for (int i = 0; i < chunkOnScreen; i++)
        {
            SpawnNewChunk();
        }
    }
}
