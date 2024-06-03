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

    private void Start()
    {
        itemTextGameObject = GameObject.FindGameObjectWithTag("Dialogue").transform.GetChild(0).gameObject;
        itemText = itemTextGameObject.transform.GetChild(1).GetComponent<TMP_Text>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & !isTouched & !FindObjectOfType<InvincibleFrames>().isInvincible)
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
