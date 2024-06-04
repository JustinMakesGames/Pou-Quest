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

    public bool isBoss;

    public GameObject battleEnemy;
    public int showDialogOption;
    public GameObject optionScreen;

    private void Start()
    {
        dialogPanel = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(0).gameObject;
        nameText = dialogPanel.transform.GetChild(0).GetComponent<TMP_Text>();
        dialogText = dialogPanel.transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void StartDialog()
    {
        //textSpeed = Save.instance.saveData.textSpeed;
        dialogPanel.SetActive(true);
        StartCoroutine(Dialog());
    }

    IEnumerator Dialog()
    {
        nameText.text = npcName;
        if (questNpc)
        {
            quest = QuestGenerator.instance.GenerateQuest();
            lines.Add(quest);

        }
        for (int i = 0; i < lines.Count; i++)
        {
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
            }
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
    }

    private void StartBossBattle()
    {
        BattleTransition.instance.StartBattle();
        CreateBattleArena.instance.SpawnEnemy(battleEnemy);
        FindAnyObjectByType<TriggerPouDialog>().GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("BossMusic").GetComponent<AudioSource>().Play();
        EndBattle.instance.GetEnemy(gameObject);
    }
}
