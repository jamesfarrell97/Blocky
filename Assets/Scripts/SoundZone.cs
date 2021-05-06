using UnityEngine;

public class SoundZone : MonoBehaviour
{
    [SerializeField]
    [Range(5, 30)]
    float timeout = 15f;

    [SerializeField]
    AudioSource[] sounds;

    private PlayerController player;
    private float time = 0;

    void FixedUpdate()
    {
        if (player == null) return;

        time += Time.fixedDeltaTime;

        if (time > timeout)
        {
            sounds[Random.Range(0, sounds.Length - 1)].Play();
            time = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            player = other.gameObject.GetComponentInParent<PlayerController>();
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
    }
}
