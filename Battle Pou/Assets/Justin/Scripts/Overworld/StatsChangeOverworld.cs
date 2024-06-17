using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsChangeOverworld : MonoBehaviour
{
    public void Change()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        sliders[0].maxValue = PlayerHandler.Instance.maxHp;
        sliders[1].maxValue = PlayerHandler.Instance.maxSp;
        sliders[0].value = PlayerHandler.Instance.hp;
        sliders[1].value = PlayerHandler.Instance.sp;
        sliders[0].GetComponentInChildren<TMP_Text>().text = PlayerHandler.Instance.hp + "/" + PlayerHandler.Instance.maxHp;
        sliders[1].GetComponentInChildren<TMP_Text>().text = PlayerHandler.Instance.sp + "/" + PlayerHandler.Instance.maxSp;
    }
}
