using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class TeleportBeam : MonoBehaviour
{
    public Transform owner;
    public float teleportDistance = 3.0f;
    public float laserThickness = 0.01f;
    
    public OVRInput.Button teleportButton = OVRInput.Button.Four;

    public Color color = Color.red;

    private GameObject laser;

    private RaycastHit hitInfo;
    

    void Start()
    {
        laser = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        laser.transform.parent = this.transform;
        laser.transform.localScale = Vector3.zero;
        laser.transform.localPosition = Vector3.zero;
        laser.GetComponent<CapsuleCollider>().enabled = false;
        laser.GetComponent<Renderer>().material.color = color;
    }

    bool castLaser(out float distance)
    {
        var ray = new Ray(transform.position, transform.rotation * Vector3.forward * teleportDistance);

        var hit = Physics.Raycast(ray, out hitInfo, teleportDistance);
        distance = hit ? hitInfo.distance : teleportDistance;

        laser.transform.localScale = new Vector3(laserThickness, laserThickness, distance);
        laser.transform.localPosition = new Vector3(0.0f, 0.0f, distance / 2);

        return hit;
    }

    void teleport(float height = 0.5f)
    {
        owner.position = new Vector3(hitInfo.point.x, height, hitInfo.point.z);
    }

    void clearLaser()
    {
        laser.transform.position = Vector3.zero;
        laser.transform.localScale = Vector3.zero;
    }

    private bool wasTeleporting = false;
    private bool teleporting = false;
    void Update()
    {
        clearLaser();
        if (OVRInput.GetDown(teleportButton))
        {
            teleporting = true;
        }
            
        if (teleporting)
        {
            float distance;
            var hit = castLaser(out distance);
            
            if (OVRInput.GetUp(teleportButton) && hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("teleportable"))
            {
                teleporting = false;
                teleport();
                clearLaser();

            }

        } // Check if was teleporting last frame, incase button was let go and wasn't teleported
        
    }
}
