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
        float halfHeight = Camera.main.orthographicSize;
        float halfWidth = halfHeight * Camera.main.aspect;

        float minX = area.bounds.min.x + halfWidth - 3;
        float maxX = area.bounds.max.x - halfWidth + 3;
        float minY = area.bounds.min.z + halfHeight - 5; // Assuming top-down view on the XZ plane
        float maxY = area.bounds.max.z - halfHeight - 1;

        cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
        cameraPos.z = Mathf.Clamp(cameraPos.z, minY, maxY);
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
                
                Collider col = colliderObject.GetComponent<BoxCollider>();
                foreach (Collider o in colliderObject.GetComponents<Collider>())
                {
                    o.isTrigger = false;
                }
                return col;
            }
        }
        return currentCol;
    }

    float CalculateXOffset(float hitPointX)
    {
        float distance = transform.position.x - hitPointX;
        return distance;
    }

    float CalculateZOffset(float hitPointZ)
    {
        float distance = transform.position.z - hitPointZ;
        return distance;
    } 

    

}
