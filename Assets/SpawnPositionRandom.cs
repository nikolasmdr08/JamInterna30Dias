using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositionRandom : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public Transform zLimitUp;
    public Transform zLimitDown;
    public Transform xLimitLeft;
    public Transform xLimitRight;


    private void Awake()
    {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                spawnPoints[i].transform.position = new Vector3(Random.Range(xLimitLeft.position.x, xLimitRight.position.x), -0.8f, Random.Range(zLimitUp.position.z, zLimitDown.position.z));
            }
    }

}
