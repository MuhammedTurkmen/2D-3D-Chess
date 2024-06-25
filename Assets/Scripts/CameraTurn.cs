using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    [SerializeField]
    private float turnSpeed;

    private float speedX;
    private Vector3 centerPos;

    private void Awake()
    {
        centerPos = new Vector3(7f, 0, 7f);
    }

    // center 1.75 + (4 * 1.5)

    void Update()
    {
        speedX = -Input.GetAxis("Horizontal");
        transform.RotateAround(centerPos, Vector3.up, speedX * turnSpeed * Time.deltaTime);
    }
}