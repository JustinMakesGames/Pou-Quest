using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public Transform player;
    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<BattlePlayerMovement>().transform;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Vector3.Distance(player.position, transform.GetChild(0).position) < 0.5f)
        {
            PlayerHandler.Instance.TakeDamage(FindObjectOfType<EnemyHandler>().attackPower);
            Destroy(gameObject);
        }
    }
}
