using UnityEngine;
using System.Collections;

public class ManequenRespawner : MonoBehaviour
{
    public GameObject manequenPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 3f;

    private GameObject currentManequen;

    void Start()
    {
        // Спавн первого манекена
        SpawnManequen();
    }

    // Запрос на респавн манекена
    public void RequestRespawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // Задержка перед респавном
        yield return new WaitForSeconds(respawnDelay);

        if (currentManequen != null)
            Destroy(currentManequen);

        SpawnManequen();
    }

    private void SpawnManequen()
    {
        // Рандомная позиция в радиусе 7 метров от spawnPoint
        Vector2 randomOffset = Random.insideUnitCircle * 7f;
        Vector3 randomPosition = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);

        // Поворот манекена на spawnPoint
        Vector3 lookDirection = spawnPoint.position - randomPosition;
        lookDirection.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        // Создание манекена
        currentManequen = Instantiate(manequenPrefab, randomPosition, lookRotation);
        currentManequen.GetComponent<ManequenController>().respawner = this;
    }
}
