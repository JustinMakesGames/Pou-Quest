using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float size;
    public GameObject pou;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + size, gameObject.transform.localScale.y + size);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - size, gameObject.transform.localScale.y - size);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - size, gameObject.transform.localScale.y - size);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Restart()
    {
        Save.instance.ClearData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void PlayGame()
    {
        Camera.main.GetComponent<Animator>().enabled = true;
        pou.GetComponent<Animator>().enabled = true;
    }
    public void TurnOn()
    {
        FindAnyObjectByType<PlayerOverworld>().enabled = true;
    }
}
