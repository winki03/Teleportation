using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalView : MonoBehaviour
{
    public Transform playerCamera; // Main camera (player)
    public Transform portal;       // This portal
    public Transform otherPortal;  // The opposite portal
    public Camera portalCam;       // The camera that renders the view

    void LateUpdate()
    {
        Vector3 playerOffset = playerCamera.position - otherPortal.position;
        portalCam.transform.position = portal.position + playerOffset;

        // Match rotation
        Quaternion difference = portal.rotation * Quaternion.Inverse(otherPortal.rotation);
        portalCam.transform.rotation = difference * playerCamera.rotation;

        // Optional: Oblique clipping
        SetObliqueClipPlane(portalCam, portal);
    }

    void SetObliqueClipPlane(Camera cam, Transform portalPlane)
    {
        Vector3 pos = portalPlane.position;
        Vector3 normal = -portalPlane.forward;

        Matrix4x4 matrix = cam.worldToCameraMatrix;
        Vector3 camSpacePos = matrix.MultiplyPoint(pos);
        Vector3 camSpaceNormal = matrix.MultiplyVector(normal).normalized;
        float d = -Vector3.Dot(camSpacePos, camSpaceNormal);

        Vector4 clipPlane = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, d);
        cam.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);
    }
}
