using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickingItem : MonoBehaviour
{
    public Item item;
    public bool isTouched;
    public GameObject itemTextGameObject;
    public TMP_Text itemText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & !isTouched)
        {
            InventoryManager.instance.AddItem(item);
            isTouched = true;
            StartCoroutine(ShowText());
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private IEnumerator ShowText()
    {
        itemTextGameObject.SetActive(true);
        itemText.text = "You got an " + item.name + "!";
        yield return new WaitForSeconds(5);
        itemTextGameObject.SetActive(false);
        Destroy(gameObject);

    }
}
