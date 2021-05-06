using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly float MIN_DISTANCE = 0.05f;
    
    [SerializeField] Animator animator;

    [SerializeField] [Range(0, 4)] float rayGroundDistance = 0.75f;
    [SerializeField] [Range(0, 4)] float rayWallDistance = 1.75f;

    [SerializeField] [Range(0, 2)] float moveDuration = 1f;

    private bool allowedMove = true;

    private Vector3 startPosition;
    private Vector3 cameraPosition;

    private float cubeRadius;
    
    void Start()
    {
        InputManager.Instance.SetPlayer(this);
        startPosition = transform.position;
        cubeRadius = transform.localScale.x / 2;
    }
    
    void FixedUpdate()
    {
        PlayerInput();

        if (!allowedMove) return;

        if (CanFall())
            StartCoroutine(Fall());
    }

    private Vector3 origin;
    private Vector3 direction;
    private Vector3 destination;

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(Vector3.right);
        }
    }

    public void MovePlayer(Vector3 direction)
    {
        if (!allowedMove) return;

        if (CanFall()) return;

        if (CanMove(direction))
            StartCoroutine(Roll(direction));
    }

    private IEnumerator Roll(Vector3 direction)
    {
        allowedMove = false;

        float rollAngle = 90;
        float rollTime = 0;

        var radius = transform.localScale.x / 2;
        var rollPoint = transform.position + (Vector3.down * radius) + (direction * radius);
        var rollAxis = Vector3.Cross(Vector3.up, direction);

        var endRotation = transform.rotation * Quaternion.Euler(rollAxis * rollAngle);
        var endPosition = transform.position + (direction * transform.localScale.x);

        float previousAngle = 0;
        while (rollTime < moveDuration)
        {
            yield return new WaitForEndOfFrame();
            rollTime += Time.deltaTime;

            float currentAngle = (rollTime / moveDuration) * rollAngle;
            float rotateAngle = currentAngle - previousAngle;

            transform.RotateAround(rollPoint, rollAxis, rotateAngle);

            float currentPosition = (rollTime / moveDuration);

            previousAngle = currentAngle;
        }

        transform.position = endPosition;
        transform.rotation = endRotation;
        allowedMove = true;

        PlayLandSound();
    }

    private IEnumerator Fall()
    {
        allowedMove = false;
        
        var radius = transform.localScale.x / 2;
        
        var endPosition = transform.position + (Vector3.down * radius);

        float fallTime = 0;
        while (fallTime < moveDuration)
        {
            yield return new WaitForEndOfFrame();
            fallTime += Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, endPosition, fallTime);
        }

        transform.position = endPosition;
        allowedMove = true;
    }
    
    private bool CanMove(Vector3 direction)
    {
        foreach (RaycastHit rayCast in Physics.RaycastAll(transform.position, direction, rayWallDistance))
        {
            if (rayCast.collider.tag.Equals("Wall") || rayCast.collider.tag.Contains("Ground"))
            {
                AudioManager.Instance.Play("Path Blocked");
                return false;
            }
        }

        return true;
    }

    private bool CanFall()
    {
        foreach (RaycastHit rayCast in Physics.RaycastAll(transform.position, Vector3.down, rayGroundDistance))
        {
            if (rayCast.collider.tag.Contains("Ground"))
            {
                return false;
            }
        }

        return true;
    }

    private void PlayLandSound()
    {
        switch (GetGroundMaterial())
        {
            case "Wood Ground":
                AudioManager.Instance.Play("Blocky Land Wood " + Random.Range(1, 4));
                AudioManager.Instance.Play("Air Escape " + Random.Range(1, 4));
                break;

            case "Stone Ground":
                AudioManager.Instance.Play("Blocky Land Stone " + Random.Range(1, 4));
                AudioManager.Instance.Play("Air Escape " + Random.Range(1, 4));
                AudioManager.Instance.Play("Dust Crunch " + Random.Range(1, 4));
                break;

            case "Grass Ground":
                AudioManager.Instance.Play("Blocky Land Grass " + Random.Range(1, 4));
                break;
        }
    }

    private string GetGroundMaterial()
    {
        foreach (RaycastHit rayCast in Physics.RaycastAll(transform.position, Vector3.down, rayGroundDistance))
        {
            if (rayCast.collider.tag.Contains("Ground"))
            {
                return rayCast.collider.tag;
            }
        }

        return "Wood Ground";
    }
}
