using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattle : MonoBehaviour
{
    public static EndBattle instance;

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
    }
    public void SettingUpDeletion()
    {
        battleArenaClone = FindObjectOfType<BattleManager>().gameObject;


    }

    public void DestroyEnemy(GameObject enemy)
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
