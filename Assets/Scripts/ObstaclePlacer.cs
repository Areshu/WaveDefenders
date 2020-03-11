using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstaclePlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles;

    private float obstacleCheckRadius = 1f;
    private int maxAttemptsPerObstacle = 20;

    private const float MIN_X_POINT = 3f;
    private const float MAX_X_POINT = 17f;
    private const float MIN_Z_POINT = 2f;
    private const float MAX_Z_POINT = 8f;
    // Start is called before the first frame update
    void Start()
    {
        placeObstacles();
    }

    private void placeObstacles()
    {

        PrepareObstaclesList();

        foreach (GameObject obj in obstacles)
        {
            PlaceObstacle(obj);
        }
    }

    private void PrepareObstaclesList()
    {
        int obstaclesNum = Random.Range(3, 6);
        int obstaclesToHide = obstacles.Count - obstaclesNum;

        if (obstaclesToHide > 0)
        {
            for (int i = 0; i < obstaclesToHide; i++)
            {
                int aux = Random.Range(0, obstacles.Count);
                obstacles[aux].SetActive(false);
                obstacles.RemoveAt(aux);
            }
        }
    }

    private Vector3 GetObstaclePosition(GameObject obstacle)
    {
        Vector3 size = obstacle.GetComponent<NavMeshObstacle>().size;
        float posX = Random.Range((MIN_X_POINT + size.x / 2), (MAX_X_POINT - size.x / 2));
        float posZ = Random.Range((MIN_Z_POINT + size.z / 2), (MAX_Z_POINT - size.z / 2));

        Vector3 pos = new Vector3(posX, obstacle.transform.position.y, posZ);

        return pos;
    }

    private void PlaceObstacle(GameObject obstacle)
    {
        bool validPosition = false;
        int spawnAttempts = 0;
        Vector3 position = Vector3.zero;
        Collider[] colliders;

        //Set obstacle check radius
        obstacleCheckRadius = SetObstacleCheckRadius(obstacle.GetComponent<NavMeshObstacle>().size);
        
        // El size del navmesh es para escala 1. Esto es por si la escala es diferente.
        obstacleCheckRadius *= obstacle.transform.localScale.x;

        Debug.Log("+++" + obstacle.name + "CheckRadius: " + obstacleCheckRadius);

        while (!validPosition && spawnAttempts < maxAttemptsPerObstacle)
        {
            spawnAttempts++;

            position = GetObstaclePosition(obstacle);

            validPosition = true;
            colliders = Physics.OverlapSphere(position, obstacleCheckRadius);

            foreach (Collider col in colliders)
            {
                if (col.tag == "Obstacle")
                {
                    validPosition = false;
                    Debug.Log(">>>> " + obstacle.name + "Collision with " + col.gameObject.name);
                }
            }
        }

        if (validPosition)
            obstacle.transform.position = position;
        else
        {
            obstacle.SetActive(false);
            Debug.LogWarning("Ha superado el numero de intentos para colocar el obstaculo " + obstacle.name + " correctamente.");
        }
    }

    private float SetObstacleCheckRadius(Vector3 size)
    {
        if (size.x >= size.z)
            return size.x;
        else
            return size.z;
    }

}
