using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public GameObject CenterOfMass;
    
    private SteamVR_TrackedObject trackedObj;
    
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = ArrowManagerBehaviour.Instance.TrackedObj;
    }

    private void OnTriggerEnter(Collider other)
    {
        AttachArrow(other);
        
        if (TagUtility.IsShootableEntity(other.gameObject.tag))
        {
            Debug.Log(other.name);
            
            var healthBehaviour = other.GetComponent<HealthBehaviour>();
            if (healthBehaviour)
            {
                healthBehaviour.ReceiveDamage();
            }
            
            var boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
            var rigidBody = GetComponent<Rigidbody>();   
            rigidBody.isKinematic = true;
            rigidBody.velocity = Vector3.zero;
            
            Destroy(gameObject, 10f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        AttachArrow(other);
    }
    
    private void AttachArrow(Component other)
    {
        if (Controller.GetHairTriggerDown()) {
            if (TagUtility.IsAttachable(other.gameObject.tag))
            {
                ArrowManagerBehaviour.Instance.AttachArrowToBow();
            }
        }
    }
}
