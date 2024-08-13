using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        groundCheckDistance,
        wallCheckDistance,
        movementSpeed,
        maxHealth,
        knockbackDuration;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Vector2 knockbackSpeed;

    [SerializeField]
    private GameObject
        hitParticle,
        deadChunkParticle,
        deadBloodParticle;

    private float
        currentHealth,
        knockbackStartTime;

    private int
        facingDirection,
        damageDirection;

    private Vector2 movement;



    private bool
        groundDetection,
        wallDetection;

    private GameObject alive;
    private Rigidbody2D aliveRB;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRB = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>(); 

        facingDirection = 1;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    #region Moveing State

    private void EnterMovingState()
    {

    }
    private void UpdateMovingState()
    {
        groundDetection = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetection = Physics2D.Raycast(groundCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(!groundDetection || wallDetection)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, aliveRB.velocity.y);
            aliveRB.velocity = movement;
        }
    }
    private void ExitMovingState()
    {

    }

    #endregion

    #region Knockback State

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliveRB.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    #endregion

    #region Dead State

    private void EnterDeadState()
    {

        //Spawn chunks
        Instantiate(deadChunkParticle, alive.transform.position, deadChunkParticle.transform.rotation);
        Instantiate(deadBloodParticle, alive.transform.position, deadBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    #endregion

    #region Other Function

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }    
        // Hit particle

        if(currentHealth >= 0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        alive.transform.Rotate(0f, 180f, 0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState(); break;
            case State.Knockback:
                ExitKnockbackState(); break;
            case State.Dead:
                ExitDeadState(); break;
        }
        switch (state)
        {
            case State.Moving:
                EnterMovingState(); break;
            case State.Knockback:
                EnterKnockbackState(); break;
            case State.Dead:
                EnterDeadState(); break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

    }

    #endregion
}
