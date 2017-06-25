using UnityEngine;

public class MoveUtility
{
    public static bool CanTeleport(Transform positionTransform, RaycastHit hit, Vector3 difference, LayerMask mask){
        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);
        return hitMask == mask.value;
    }

    public static void ShowLaser(GameObject laser, GameObject reticle, RaycastHit hit, Vector3 controllerPosition,
        LayerMask mask){
        ShowLaser(laser, reticle, hit, controllerPosition, mask, Color.green, Color.red);
    }

    public static void ShowLaser(GameObject laser, GameObject reticle, RaycastHit hit, Vector3 controllerPosition,
        LayerMask mask, Color raycastValidColor, Color raycastFailedColor){
        if (!laser || !reticle)
            return;

        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(controllerPosition, hit.point, .5f);
        laser.transform.LookAt(hit.point);
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y,
            hit.distance);

        reticle.SetActive(true);
        reticle.transform.position = hit.point;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);

        if (mask.value == hitMask) {
            laser.GetComponent<Renderer>().material.color = raycastValidColor;
        }
        else {
            laser.GetComponent<Renderer>().material.color = raycastFailedColor;
        }
    }
}