using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    public GameObject[] powerupSpawnPoints;

    public GameObject[] powerUps;
    void OnEnable()
    {
        CoopManager.OnPowerUpInstantiationEvent += PowerUpSpawning;

        powerupSpawnPoints = GameObject.FindGameObjectsWithTag("PowerUpSpawnPoint");
    }
    void OnDisable()
    {
        CoopManager.OnPowerUpInstantiationEvent -= PowerUpSpawning;
    }

    void PowerUpSpawning()
    {
        Debug.Log("Power Up Spawn Started.. GO");
        int randNum = Random.Range(0, powerUps.Length);
        int randPosNum = Random.Range(0, powerupSpawnPoints.Length);
        Instantiate(powerUps[randNum], powerupSpawnPoints[randPosNum].transform.position, transform.rotation);
        Debug.Log("Power Up Spawn Finished");
    }
}
