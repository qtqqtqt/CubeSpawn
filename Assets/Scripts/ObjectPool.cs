using System.Collections;
using UnityEngine;
using TMPro;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject cubePrefab;
    [SerializeField] int poolSize;
    [SerializeField] TMP_InputField speedInput;
    [SerializeField] TMP_InputField spawnRateInput;
    [SerializeField] TMP_InputField distanceInput;

    GameObject[] pool;
    float speed;
    float distance;

    private void Start()
    {
        PopulatePool();
    }

    public void SubmitValues()
    {
        float.TryParse(spawnRateInput.text, out float spawnTime);
        float.TryParse(speedInput.text, out speed);
        float.TryParse(distanceInput.text, out distance);

        if (spawnTime <= 0 || speed <= 0 || distance <= 0) return;

        StartSpawning(spawnTime);
    }

    private void StartSpawning(float spawnTime)
    {
        if (spawnTime <= 0) return;
        StopAllCoroutines();
        StartCoroutine(SpawnCube(spawnTime));
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(cubePrefab, transform);
            pool[i].SetActive(false);
        }
    }

    private void SetValues(GameObject cubeObject, float speed, float distance)
    {
        CubeMover cube = cubeObject.GetComponent<CubeMover>();

        cube.Speed = speed;
        cube.DistanceToTravel = distance;
    }

    private Vector3 GenerateSpawnPoint()
    {
        float xPos = Random.Range(-10f, 10f);

        return new Vector3(xPos, 0, 0);
    }

    private void EnableObjectInPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = GenerateSpawnPoint();
                SetValues(obj, speed, distance);
                obj.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnCube(float spawnTime)
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTime);
        }

    }
}
