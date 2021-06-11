using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float moveSpeed;

    private Vector3 input = Vector2.zero;

    private void Update()
    {
        GetInput();
        Move();
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        input = new Vector2(horizontal, vertical).normalized;
    }

    private void Move()
    {
        transform.position += input * moveSpeed * Time.deltaTime;
    }
}
