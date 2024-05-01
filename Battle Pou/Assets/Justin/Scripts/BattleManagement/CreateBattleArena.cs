using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBattleArena : MonoBehaviour
{
    public static CreateBattleArena instance;

    public Transform arenaSpawn;
    public GameObject battleArena;
    private GameObject battleArenaClone;
    private Transform enemySpawn;
    private Transform cameraPosition;
    private Transform cam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        cam = Camera.main.transform;
    }
    public void DeactivateOverworldScripts()
    {
        PlayerOverworld playerScript = GameObject.FindObjectOfType<PlayerOverworld>();
        CamMovement camScript = GameObject.FindObjectOfType<CamMovement>();
        EnemyOverworld[] enemyScripts = GameObject.FindObjectsOfType<EnemyOverworld>();
        foreach (EnemyOverworld script in enemyScripts)
        {
            script.enabled = false;
        }
        playerScript.enabled = false;
        camScript.enabled = false;
    }

    public void MakeBattleArena()
    {
        battleArenaClone = Instantiate(battleArena, arenaSpawn.position, Quaternion.identity);

    }

    public void SpawnEnemy(GameObject enemy)
    {
        enemySpawn = battleArenaClone.transform.Find("EnemySpawn");
        Instantiate(enemy, enemySpawn.position, Quaternion.Euler(0, 180, 0));
    }



    public void SetCameraInPosition()
    {
        cameraPosition = battleArenaClone.transform.Find("CameraSpawn");
        cam.position = cameraPosition.position;
        cam.rotation = cameraPosition.rotation;
    }
}
