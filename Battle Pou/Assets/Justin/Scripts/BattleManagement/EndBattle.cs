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
    private Transform cam;
    public AudioSource[] audios;

    public GameObject overworldEnemy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void SettingUpDeletion()
    {
        cam = Camera.main.transform;
        battleArenaClone = FindObjectOfType<BattleManager>().gameObject;
        FindAnyObjectByType<CreateBattleArena>().battleMusic.Stop();
        FindAnyObjectByType<CreateBattleArena>().battleMusic.time = 0;
        audios = GameObject.FindGameObjectWithTag("Audio").GetComponents<AudioSource>();
        audios[4].Play();
    }

    public void GetEnemy(GameObject enemy)
    {
        overworldEnemy = enemy;
    }

    public void PrepareToDestroyEnemy()
    {
        StartCoroutine(DestroyEnemy());
    }

    public void DestroyBattleEnemy()
    {
        Destroy(FindObjectOfType<EnemyHandler>().gameObject);
    }
    public IEnumerator DestroyEnemy()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        Poof.instance.UsePoof(overworldEnemy.transform);
        Destroy(overworldEnemy);
    }

    private IEnumerator DestroyBattleField()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        Destroy(battleArenaClone);
    }

    public void StartingCoroutines()
    {
        StartCoroutine(SetCameraAtPosition());
        StartCoroutine(EnableOverworldEnemies());
        StartCoroutine(DestroyBattleField());
    }

    private IEnumerator SetCameraAtPosition()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        cam.position = CreateBattleArena.instance.oldCamPosition;
        cam.rotation = CreateBattleArena.instance.oldCamRotation;
    }

    private IEnumerator EnableOverworldEnemies()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        CreateBattleArena.instance.OverworldManagement();
    }

    public void MakePlayerInvincible()
    {
        FindObjectOfType<InvincibleFrames>().StartInvincibleFrames();
    }

    public void KeepEnemyAlife()
    {
        StartCoroutine(KeepImmuneFrames());
    }

    private IEnumerator KeepImmuneFrames()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        overworldEnemy.GetComponent<EnemyOverworld>().enabled = false;
        Renderer renderer = overworldEnemy.GetComponent<Renderer>();
        int amountOfFrames = 20;

        for (int i = 0; i < amountOfFrames; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        overworldEnemy.GetComponent<EnemyOverworld>().enabled = true;
    }
}
