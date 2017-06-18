using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = ArrowManagerBehaviour.Instance.TrackedObj;
    }

    private void OnTriggerEnter(Collider other)
    {
        AttachArrow(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        AttachArrow(other.gameObject);
    }

    private void AttachArrow(GameObject collidingGameObj)
    {
        if (controller.GetHairTriggerDown()) {
            if (collidingGameObj.tag.Equals("Bow"))
            {
                ArrowManagerBehaviour.Instance.AttachArrowToBow();
            }
        }
    }
}
