using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class TimeText : MonoBehaviour
{
    private void Start()
    {
        string time = string.Format("{0:00}", Time.timeSinceLevelLoad.ToString("0.00").Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ":"));
        gameObject.GetComponent<TMP_Text>().text = time;
    }
}
