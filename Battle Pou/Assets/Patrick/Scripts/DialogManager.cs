using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TMP_Text dialogText;
    public TMP_Text nameText;
    public bool clicked;
    public GameObject dialogPanel;
    public string[] lines;
    public string npcName;
    public float textSpeed = 1;

    public int showDialogOption;
    public GameObject optionScreen;

    private void Start()
    {
        dialogPanel = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(0).gameObject;
        nameText = dialogPanel.transform.GetChild(0).GetComponent<TMP_Text>();
        dialogText = dialogPanel.transform.GetChild(1).GetComponent<TMP_Text>();
    }
    private void Update()
    {
        if (dialogPanel.activeSelf)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                clicked = true;
            }
        }
    }

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        StartCoroutine(Dialog());
    }

    IEnumerator Dialog()
    {
        nameText.text = npcName;
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == showDialogOption)
            {
                optionScreen.SetActive(true);
            }
            dialogText.text = "";
            for (int c = 0; c < lines[i].ToCharArray().Length; c++)
            {
                dialogText.text += lines[i].ToCharArray()[c];
                yield return new WaitForSeconds(textSpeed * 0.1f);
            }
            yield return new WaitUntil(() => clicked);
            clicked = false;
        }
        dialogPanel.SetActive(false);
    }
}
