using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public Vector3 oldCamPosition;
    public Quaternion oldCamRotation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        cam = Camera.main.transform;
    }
    public void OverworldManagement()
    {      
        oldCamPosition = cam.position;
        oldCamRotation = cam.rotation;
        PlayerOverworld playerScript = GameObject.FindObjectOfType<PlayerOverworld>();
        CamMovement camScript = GameObject.FindObjectOfType<CamMovement>();
        EnemyOverworld[] enemyScripts = GameObject.FindObjectsOfType<EnemyOverworld>();
        foreach (EnemyOverworld script in enemyScripts)
        {
            script.enabled = !script.enabled;
        }
        playerScript.enabled = !playerScript.enabled;
        camScript.enabled = !camScript.enabled;
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



    public void StartCameraChange()
    {
        StartCoroutine(SetCameraAtPosition());
    }

    private IEnumerator SetCameraAtPosition()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        cameraPosition = battleArenaClone.transform.Find("CameraSpawn");
        cam.position = cameraPosition.position;
        cam.rotation = cameraPosition.rotation;
    }
}
