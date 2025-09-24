using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Camera will follow player
    public GameObject player;

    //Camera offset to focus on player
    private Vector3 offset = new Vector3(0, 0, -10);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //LateUpdate just in case camera is glitchy
    void LateUpdate()
    {
        //Updates cameras position to player position
        transform.position = player.transform.position + offset;
    }
}
