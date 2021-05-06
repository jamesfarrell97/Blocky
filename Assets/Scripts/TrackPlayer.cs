using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - player.position;
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + offset.z);
    }
}
