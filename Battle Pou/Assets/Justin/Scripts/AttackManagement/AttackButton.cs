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

    public TMP_Text attackNameText, attackDescriptionText;
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
