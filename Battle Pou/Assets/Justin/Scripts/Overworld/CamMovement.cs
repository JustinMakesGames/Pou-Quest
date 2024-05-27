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
    public GameObject colliderObject;
    private Vector3 offset;
    private Vector3 cameraPos;
    private float cameraXOffset;
    private float cameraZOffset;
    private bool isMakingNewCol;
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
        area = FindCollider();
        offset = player.position - transform.position;
        
    }

    private void LateUpdate()
    {
        cameraPos = player.position - offset;
        if (area != null)
        {
            ClampingCamera();
        }
        transform.position = Vector3.Lerp(transform.position, cameraPos, speed * Time.deltaTime);
    }

    private void ClampingCamera()
    {
        
        cameraPos.x = Mathf.Clamp(cameraPos.x, area.bounds.min.x - cameraXOffset, area.bounds.max.x + cameraXOffset);
        cameraPos.z = Mathf.Clamp(cameraPos.z, area.bounds.min.z + cameraZOffset, area.bounds.max.z - cameraZOffset);
    }

    public void ChangeCollider()
    {
        area = FindCollider();
    }

    private void CalculateOffsets()
    {
        
        Vector2 upperLeftCam = new Vector2(0, Screen.height);
        Ray ray = Camera.main.ScreenPointToRay(upperLeftCam);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ground))
        {
            cameraXOffset = CalculateXOffset(hit.point.x);
            cameraZOffset = CalculateZOffset(hit.point.z);
            
        }
        
    }
    Collider FindCollider()
    {
        Collider currentCol = area;
        RaycastHit hit;
        if (Physics.Raycast(player.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider != null)
            {
                foreach (Collider c in colliderObject.GetComponents<Collider>())
                {
                    Destroy(c);
                }
                colliderObject.AddComponent<BoxCollider>();
                colliderObject.transform.localScale = new Vector3(8, 8, 8);
                Collider col = colliderObject.GetComponent<BoxCollider>();
                foreach (Collider o in colliderObject.GetComponents<Collider>())
                {
                    o.isTrigger = true;
                }
                return col;
            }
        }
        return currentCol;
    }

    float CalculateXOffset(float hitPointX)
    {
        float distance = hitPointX - transform.position.x;
        return distance;
    }

    float CalculateZOffset(float hitPointZ)
    {
        float distance = hitPointZ - transform.position.z;
        return distance;
    } 

    

}
