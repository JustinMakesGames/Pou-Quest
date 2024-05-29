using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PoisonThrowBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 forwardDir;
    private Vector3 upDir;
    public float upSpeed;

    public float speed;
    public ParticleSystem explosion;

    public AnimationCurve curve;
    public float timer;
    public float curveSpeed;
    public float multiplier;

    private Quaternion rotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SetRotationRight());
        StartCoroutine(Curve());
        
    }

    private IEnumerator SetRotationRight()
    {
        while (transform.rotation != rotation)
        {
            Vector3 lookRotation = transform.position - FindObjectOfType<EnemyHandler>().transform.position;
            rotation = Quaternion.LookRotation(lookRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Mathf.Infinity);
            print(rotation);
            yield return null;
        }
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.GetComponent<BattleManager>()!= null & timer > 0.5f)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            explosion.Play();
            transform.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 1f);
        }
    }

    private IEnumerator Curve()
    {
        float timeForCurve = 0;
        while (timeForCurve <= 1f)
        {
            timeForCurve += Time.deltaTime  * curveSpeed;
            var key = curve.Evaluate(timeForCurve);
            rb.velocity = transform.forward * speed + Vector3.up * key * multiplier;
            yield return null;
        }
        

        
    }
}
