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
            dialogText.text = "";
            for (int c = 0; c < lines[i].ToCharArray().Length; c++)
            {
                dialogText.text = dialogText.text + lines[i].ToCharArray()[c];
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitUntil(() => clicked);
            clicked = false;
            dialogText.text = "";
        }
        dialogPanel.SetActive(false);
    }
}
