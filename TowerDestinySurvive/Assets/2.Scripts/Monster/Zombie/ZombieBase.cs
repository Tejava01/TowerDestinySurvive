using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZombieBase : MonsterBase
{
    [SerializeField] protected float jumpPower = 4f;
    [SerializeField] protected float minjumpCycleTime = 2.5f;
    [SerializeField] protected float maxjumpCycleTime = 10f;

    protected bool m_canJump = false;
    protected float m_jumpTimer = 0f;
    protected float m_jumpCycleTime = 0f;
    protected bool m_attacking = false;
    protected bool m_isKnockback = false;

    //-------------------------------------------------------------

    private void Start()
    {
        OnZombieStart();
    }

    private void Update()
    {
        OnZombieUpdate();
    }

    protected override void ProtAttack()
    {
        PrivClearAnim();
        m_animator.SetBool("IsAttacking", true);
    }

    protected override void ProtMove()
    {
        PrivClearAnim();
        m_animator.SetBool("IsIdle", true);
    }

    protected override void ProtDie()
    {
        PrivClearAnim();
        m_animator.SetBool("IsDead", true);
    }

    //-------------------------------------------------------------

    protected void ProtRefreshVelocity()
    {
        m_moveDir.y = m_rigidbody.velocity.y;
        m_rigidbody.velocity = m_moveDir;
    }

    protected void ProtCheckVelocity()
    {
        {
            ProtRefreshVelocity();
        }
    }

    protected void ProtJump()
    {
        m_canJump = false;
        m_rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    protected void ProtJumpTimer()
    {
        m_jumpTimer += Time.deltaTime;

        if (m_canJump == false && m_jumpTimer >= m_jumpCycleTime)
        {
            m_canJump = true;

            m_jumpTimer -= m_jumpCycleTime; //초과한 deltaTime만큼 손실 방지
            m_jumpCycleTime = Random.Range(minjumpCycleTime, maxjumpCycleTime);
        }
    }

    //-------------------------------------------------------------

    private void PrivClearAnim()
    {
        m_animator.Rebind();
    }

    //-------------------------------------------------------------

    protected virtual void OnZombieStart() { }
    protected virtual void OnZombieUpdate() { }
}
