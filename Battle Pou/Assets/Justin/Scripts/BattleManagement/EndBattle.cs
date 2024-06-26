using JetBrains.Annotations;
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
    public GameObject stats;
    public bool isInBattle;
    public bool isInvincible;

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

        if (overworldEnemy.GetComponent<EnemyOverworld>() != null)
        {
            FindObjectOfType<SpawnLoot>().SpawningLoot(overworldEnemy.transform.position, overworldEnemy.GetComponent<EnemyOverworld>().hasKey);
        }
        
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
        Cursor.lockState = CursorLockMode.None;
        cam.position = CreateBattleArena.instance.oldCamPosition;
        cam.rotation = CreateBattleArena.instance.oldCamRotation;
    }

    private IEnumerator EnableOverworldEnemies()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        yield return new WaitUntil(() => Fading.instance.blackScreen.color.a <= 0.01f);
        OverworldManagement();
    }

    public void OverworldManagement()
    {
        MonoBehaviour[] playerScripts = FindObjectOfType<PlayerOverworld>().GetComponents<MonoBehaviour>();
        CamMovement camScript = GameObject.FindObjectOfType<CamMovement>();
        EnemyOverworld[] enemyScripts = GameObject.FindObjectsOfType<EnemyOverworld>();
        foreach (EnemyOverworld script in enemyScripts)
        {
            script.enabled = true;
        }
        foreach (MonoBehaviour script in playerScripts)
        {
            script.enabled = true;
        }
        camScript.enabled = true;
        stats.SetActive(true);
        FindAnyObjectByType<StatsChangeOverworld>().Change();
        AudioRef.instance.ambient.Play();
        CreateBattleArena.instance.isBoss = false;
        isInBattle = false;
    }

    public void MakePlayerInvincible()
    {
        FindObjectOfType<InvincibleFrames>().StartInvincibleFrames();
    }

    public void KeepEnemyAlife()
    {
        StartCoroutine(KeepImmuneFrames());
        StartCoroutine(KeepingScriptFalse());
        
    }

    private IEnumerator KeepImmuneFrames()
    {
        yield return new WaitUntil(() => Camera.main.fieldOfView <= 30);
        yield return new WaitUntil(() => Fading.instance.blackScreen.color.a <= 0.01f);
        yield return null;
        isInvincible = true;
        overworldEnemy.GetComponent<EnemyOverworld>().enabled = false;
        Renderer renderer = overworldEnemy.GetComponentInChildren<Renderer>();
        int amountOfFrames = 20;

        for (int i = 0; i < amountOfFrames; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            renderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        isInvincible = false;
        overworldEnemy.GetComponent<EnemyOverworld>().enabled = true;
    }

    private IEnumerator KeepingScriptFalse()
    {
        while (isInvincible)
        {
            if (isInBattle)
            {
                StopCoroutine(KeepImmuneFrames());
                isInvincible = false;
            }

            yield return null;
        }
    }
}
