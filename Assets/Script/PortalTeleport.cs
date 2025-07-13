using System.Collections;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public CharacterController player;

    private bool canTeleport = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            StartCoroutine(Teleport());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }

    private IEnumerator Teleport()
    {
        canTeleport = false;

        player.enabled = false;

        // Offset the player forward from the target portal to avoid instant retrigger
        Vector3 exitOffset = teleportTarget.forward * 1.5f;
        player.transform.position = teleportTarget.position + exitOffset;
        player.transform.rotation = teleportTarget.rotation;

        yield return new WaitForSeconds(0.1f); // Small buffer delay

        player.enabled = true;
    }
}
