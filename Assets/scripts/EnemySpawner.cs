using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;

    public float maxLife = 2f; //[s]
    public float xLimit;

    private float spawnNext = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            //spawn de meteoritos
            spawnNext = Time.time + (60/spawnRatePerMinute);
            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            //forzamos el spawn arriba de la pantalla, y jugamos con los X
            Vector2 spawnPosition = new Vector2(rand, 7f);

            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxLife);
        }
    }
}
