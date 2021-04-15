using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cube;
    [SerializeField] Transform ring;
    [SerializeField] Transform light;
    [SerializeField] Transform camera;

    [SerializeField] [Range(0, 2)] float pulseDuration = 1f;
    [SerializeField] [Range(0, 2)] float rollDuration = 0.25f;

    private bool cubeAllowedRole = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (!cubeAllowedRole) return;

        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(Roll(Vector3.left));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartCoroutine(Roll(Vector3.forward));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            StartCoroutine(Roll(Vector3.back));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            StartCoroutine(Roll(Vector3.right));
        }
        else
        {
            PulseCube();
        }
    }

    private IEnumerator Roll(Vector3 direction)
    {
        cubeAllowedRole = false;
        
        float rollAngle = 90;
        float rollPercent = 0;
        float previousAngle = 0;

        var cubeRadius = cube.localScale.x / 2;
        var rollPoint = cube.position + (Vector3.down * cubeRadius) + (direction * cubeRadius);
        var rollAxis = Vector3.Cross(Vector3.up, direction);
        
        var endCubeRotation = cube.rotation * Quaternion.Euler(rollAxis * rollAngle);

        var endCubePosition = cube.position + direction;
        var endLightPosition = light.position + direction;
        var endCameraPosition = camera.position + direction;

        while (rollPercent < rollDuration)
        {
            yield return new WaitForEndOfFrame();
            rollPercent += Time.deltaTime;

            float currentAngle = (rollPercent / rollDuration) * 90;
            float rotateAngle = currentAngle - previousAngle;
            cube.RotateAround(rollPoint, rollAxis, rotateAngle);

            float currentPosition = (rollPercent / rollDuration);
            light.position = light.position + direction / rollPercent;
            camera.position = camera.position + direction / rollPercent;

            previousAngle = currentAngle;
        }

        cube.position = endCubePosition;
        cube.rotation = endCubeRotation;

        light.position = endLightPosition;
        camera.position = endCameraPosition;

        cubeAllowedRole = true;
    }
    
    private void PulseCube()
    {

    }
}
