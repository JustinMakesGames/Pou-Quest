using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public TMP_Text nameText;
    public bool questNpc;
    public GameObject dialogPanel;
    public List<string> lines = new();
    public string npcName;
    public string quest;
    public float textSpeed = 1;
    public bool isInDialog;
    public bool isBoss;
    public AudioSource[] audios;
    public GameObject battleEnemy;
    public int showDialogOption;
    public GameObject optionScreen;
    public bool options;
    private void Start()
    {
        if (options)
        {
            optionScreen = GameObject.FindGameObjectWithTag("Choice").transform.GetChild(0).gameObject;
        }
        audios = AudioRef.instance.dialog;
        dialogPanel = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(0).gameObject;
        nameText = dialogPanel.transform.GetChild(0).GetComponent<TMP_Text>();
        dialogText = dialogPanel.transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void StartDialog()
    {
        //textSpeed = Save.instance.saveData.textSpeed;
        if (!isInDialog)
        {
            dialogPanel.SetActive(true);
            StartCoroutine(Dialog());
            isInDialog = true;
        }
    }

    IEnumerator Dialog()
    {
        EnemyOverworld[] enemies = FindObjectsOfType<EnemyOverworld>();
        foreach (var enemy in enemies)
        {
            enemy.enabled = false;
        }
        nameText.text = npcName;
        if (questNpc)
        {
            Quest q = QuestGenerator.instance.GenerateQuest();
            quest = q.quest;
            lines.Add(quest);

        }
        for (int i = 0; i < lines.Count; i++)
        {
            audios[0].Play();
            if (i == showDialogOption & optionScreen != null)
            {
                optionScreen.SetActive(true);
            }
            if (lines[i] == quest)
            {
                if (quest.StartsWith("Find my pages"))
                {
                    nameText.text = "Slenderman (real not fake)";
                }
            }
            dialogText.text = "";
            for (int c = 0; c < lines[i].ToCharArray().Length; c++)
            {
                dialogText.text += lines[i].ToCharArray()[c];
                yield return new WaitForSeconds(textSpeed * 0.1f);
                if (c == lines[i].ToCharArray().Length - 1)
                {
                    audios[0].Stop();
                }
            }
            audios[0].Stop();
            yield return new WaitUntil(() => Input.GetButtonDown("Fire1"));
            if (lines[i] == quest && questNpc)
            {
                lines.Remove(quest);
            }
        }
        if (optionScreen == null && GetComponent<OverworldNPCS>() != null)
        {
            GetComponent<OverworldNPCS>().FinishDialog();
        }

        if (isBoss)
        {
            StartBossBattle();
        }
        dialogPanel.SetActive(false);
        isInDialog = false;
        FindObjectOfType<Interact>().isAlreadyInteracting = false;
        if (!options && !isBoss)
        {
            FindAnyObjectByType<PlayerOverworld>().enabled = true;
        }
        foreach (var enemy in enemies)
        {
            enemy.enabled = true;
        }
    }

    private void StartBossBattle()
    {
        CreateBattleArena.instance.isBoss = true;
        BattleTransition.instance.StartBattle();
        CreateBattleArena.instance.SpawnEnemy(battleEnemy);
        
        FindAnyObjectByType<TriggerPouDialog>().GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("BossMusic").GetComponent<AudioSource>().Play();
        EndBattle.instance.GetEnemy(gameObject);
    }
}
