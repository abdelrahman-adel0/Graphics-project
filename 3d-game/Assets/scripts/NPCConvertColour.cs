using UnityEngine;

public class NPCConvertColour : MonoBehaviour
{
    private NPCFollower followerScript;
    private Renderer rend;

    public bool isFollowing = false;

    void Start()
    {
        followerScript = GetComponent<NPCFollower>();
        rend = GetComponentInChildren<Renderer>();

        followerScript.enabled = false; // start NOT following
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFollowing)
        {
            isFollowing = true;

            followerScript.enabled = true;

            // copy player color
            Renderer playerRend = other.GetComponentInChildren<Renderer>();
            rend.material.color = playerRend.material.color;
        }
    }
}