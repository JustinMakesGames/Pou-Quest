using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class HandleAttackSwitch : MonoBehaviour
{
    public static HandleAttackSwitch instance;

    public List<AttackButton> attackButtons;

    public GameObject attackPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            attackPanel.SetActive(!attackPanel.activeInHierarchy);
        }
    }

    public void HandleAttackSwitching()
    {
        if (attackButtons.Count <= 1) return;
        SwitchPositions();
        SwitchAttack();
        ClearArray();
    }

    private void SwitchPositions()
    {
        RectTransform rectTransform1 = attackButtons[0].button.GetComponent<RectTransform>();
        RectTransform rectTransform2 = attackButtons[1].button.GetComponent<RectTransform>();
        Vector2 tempPosition = rectTransform1.anchoredPosition;
        rectTransform1.anchoredPosition = rectTransform2.anchoredPosition;
        rectTransform2.anchoredPosition = tempPosition;

        int position1 = attackButtons[0].position;
        int position2 = attackButtons[1].position;

        attackButtons[0].position = position2;
        attackButtons[1].position = position1;
    }

    private void SwitchAttack()
    {
        if (attackButtons[0].isEquipped != attackButtons[1].isEquipped)
        {
            foreach (var attack in attackButtons)
            {
                if (!attack.isEquipped)
                {
                    attack.isEquipped = true;
                    PlayerHandler.Instance.attacks.Insert(attack.position, attack.attack);
                    print("Inserted " + attack.attack.name);
                }
                else
                {
                    attack.isEquipped = false;
                    PlayerHandler.Instance.attacks.Remove(attack.attack);
                    print("Removed " + attack.attack.name);
                }
            }
        }
        else
        {
            foreach (var attack in attackButtons)
            {
                PlayerHandler.Instance.attacks.Remove(attack.attack);
                PlayerHandler.Instance.attacks.Insert(attack.position, attack.attack);
                print(attack.attack.name + attack.position);
            }
        }
    }

    

    public void ClearArray()
    {
        attackButtons.Clear();
    }

    
}
