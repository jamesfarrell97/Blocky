using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector] public PlayerController player;

    private bool moveForward;
    private bool moveBack;
    private bool moveLeft;
    private bool moveRight;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Update()
    {
        if (player == null) return;

        if (moveForward) player.MovePlayer(Vector3.forward);
        else if (moveBack) player.MovePlayer(Vector3.back);
        else if (moveLeft) player.MovePlayer(Vector3.left);
        else if (moveRight) player.MovePlayer(Vector3.right);
    }

    public void Stop()
    {
        moveForward = false;
        moveBack = false;
        moveLeft = false;
        moveRight = false;
    }

    public void MoveForward()
    {
        moveForward = true;
    }

    public void MoveBack()
    {
        moveBack = true;
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
    }
}