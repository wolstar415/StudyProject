using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Prefabs;

    public float minSpawnDelay = 5;
    public float maxSpawnDelay = 7;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Spawn",1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Spawn()
    {
        var randomObject = Prefabs[Random.Range(0, Prefabs.Length)];

        Instantiate(randomObject, transform.position, Quaternion.identity);

        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }
}
