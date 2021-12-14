using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public GameObject bomb;
    public Transform[] spawnPlaces;
    public float minWait = .3f;
    public float maxWait = 1f;
    public float minForce = 12f;
    public float maxForce = 17f;

    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject go = null;
            float p = Random.Range(0, 100);

            if (p < 10)
            {
                go = bomb;
            } else
            {
                go = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            }

            GameObject fruit = Instantiate(go, t.position, t.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            Debug.Log("Fruit gets spawned");

            Destroy(fruit, 5f);
        }
    }
}
