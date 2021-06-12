using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private float moveSpeedBonus;
    private Vector3 input = Vector2.zero;

    public bool active;

    private void Start()
    {
        moveSpeedBonus = GameObject.FindWithTag("Weapon").GetComponent<Weapon>().GetStatOfType(Stat.StatType.Mobility).value;
    }

    private void Update()
    {
        if (active)
        {
            GetInput();
            Move();
        }
    }

    private void GetInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        input = new Vector2(horizontal, vertical).normalized;
    }

    private void Move()
    {
        transform.position += input * (moveSpeed + moveSpeedBonus) * Time.deltaTime;
    }
}
