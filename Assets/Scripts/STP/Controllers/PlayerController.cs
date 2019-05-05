using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Body;

    [SerializeField]
    private float m_speed;

    [SerializeField, Range(0, 1)]
    private float m_diagonalMovementLimiter;

    private Vector2 m_direction;

    // Start is called before the first frame update
    void Start()
    {
        m_direction = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
    }

    private void GetMovementInput()
    {
        m_direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (m_direction.x != 0 || m_direction.y != 0)
        {
            Animator.SetBool("Is Moving", true);
        }
        else
        {
            Animator.SetBool("Is Moving", false);
            m_direction = new Vector2(0, 0);
        }

        Animator.SetFloat("X Direction", m_direction.x);
        Animator.SetFloat("Y Direction", m_direction.y);

        if (m_direction.x != 0 && m_direction.y != 0)
        {
            m_direction *= m_diagonalMovementLimiter;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        Body.velocity = m_direction * Time.fixedDeltaTime * m_speed;
    }
}
