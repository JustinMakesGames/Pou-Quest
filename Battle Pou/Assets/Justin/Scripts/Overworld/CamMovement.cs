using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform player;
    public float speed;
    public LayerMask ground;
    public Collider area;
    public GameObject spawn;

    private Vector3 offset;
    private Vector3 cameraPos;
    private float cameraXOffset;
    private float cameraZOffset;

    public static CamMovement instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        CalculateOffsets();

        player = FindObjectOfType<PlayerOverworld>().transform;
        offset = player.position - transform.position;
        
    }

    private void LateUpdate()
    {
        cameraPos = player.position - offset;
        ClampingCamera(); 
        transform.position = Vector3.Lerp(transform.position, cameraPos, speed * Time.deltaTime);
    }

    private void ClampingCamera()
    {
        
        cameraPos.x = Mathf.Clamp(cameraPos.x, area.bounds.min.x - cameraXOffset, area.bounds.max.x + cameraXOffset);
        cameraPos.z = Mathf.Clamp(cameraPos.z, area.bounds.min.z + cameraZOffset, area.bounds.max.z - cameraZOffset);
    }

    public void ChangeCollider(Collider collider)
    {
        area = collider; 
    }

    private void CalculateOffsets()
    {
        
        Vector2 upperLeftCam = new Vector2(0, Screen.height);
        Ray ray = Camera.main.ScreenPointToRay(upperLeftCam);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
        {
            cameraXOffset = CalculateXOffset(hit.point.x);
            cameraZOffset = CalculateYOffset(hit.point.z);
            
        }
        
    }

    float CalculateXOffset(float hitPointX)
    {
        float distance = hitPointX - transform.position.x;
        return distance;
    }

    float CalculateYOffset(float hitPointZ)
    {
        float distance = hitPointZ - transform.position.z;
        return distance;
    } 

    

}
