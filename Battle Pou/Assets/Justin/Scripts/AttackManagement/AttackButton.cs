using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public Transform attack;
    public int position;
    public bool isEquipped;

    public string attackName, attackDescription;

    private Transform description;
    public TMP_Text attackNameText, attackDescriptionText;

    private void Awake()
    {
        description = GameObject.FindGameObjectWithTag("Description").transform;
        attackNameText = description.GetChild(0).GetComponent<TMP_Text>();
        attackDescriptionText = description.GetChild(1).GetComponent<TMP_Text>();
    }
    public void OnClicked()
    {
        print("On Clicked");
        HandleAttackSwitch.instance.attackButtons.Add(this);
        HandleAttackSwitch.instance.HandleAttackSwitching();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        attackNameText.text = attackName;
        attackDescriptionText.text = attackDescription;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attackNameText.text = "";
        attackDescriptionText.text = "";
    }
}
