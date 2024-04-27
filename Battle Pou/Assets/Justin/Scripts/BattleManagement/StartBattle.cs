using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBattle : MonoBehaviour
{
    public static StartBattle instance;

    public delegate void StartBattleDelegate();
    public event StartBattleDelegate startingBattle;
    public Transform arenaSpawn;
    public Image blackScreen;
    public GameObject battleArena;
    public float blackScreenFadeSpeed;

    private Transform cam;

    private float fovSpeed = 30f;   
    private GameObject battleArenaClone;    
    private Transform cameraPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        startingBattle += DeactivateOverworldScripts;
        startingBattle += ActivateIEnumerator;
        startingBattle += MakeBattleArena;
        cam = Camera.main.transform;
        
    }
    public void ActivateEvent()
    {
        startingBattle();
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

    public void ActivateIEnumerator()
    {
        StartCoroutine(FovChange());
        StartCoroutine(BlackScreenFading());
    }

    private IEnumerator FovChange()
    {
        while (Camera.main.fieldOfView >= 30)
        {
            Camera.main.fieldOfView -= fovSpeed * Time.deltaTime;
            yield return null;
        }
        Camera.main.fieldOfView = 60;       
    }

    private IEnumerator BlackScreenFading()
    {
        while (blackScreen.color.a < 0.98f)
        {
            blackScreen.color += new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }
        SetCameraInPosition();

        while (blackScreen.color.a >= 0)
        {
            blackScreen.color -= new Color(0, 0, 0, blackScreenFadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void MakeBattleArena()
    {
        battleArenaClone = Instantiate(battleArena, arenaSpawn.position, Quaternion.identity);
        
    }

    private void SetCameraInPosition()
    {
        cameraPosition = battleArenaClone.transform.Find("CameraSpawn");
        cam.position = cameraPosition.position;
        cam.rotation = cameraPosition.rotation;
    }




}
