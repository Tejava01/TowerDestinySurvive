using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMelee : ZombieBase
{
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_moveDir = new Vector2(Vector2.left.x * speed, 0);
        m_railTag = gameObject.tag;
    }

    protected override void OnZombieStart()
    {
        ProtRefreshVelocity();
        ProtMove();
    }

    protected override void OnZombieUpdate()
    {
        ProtCheckVelocity();
        ProtJumpTimer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(MapConst.c_tagHero))
        {
            m_attacking = true;
            ProtAttack();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (m_attacking == false && m_canJump == true && collision.gameObject.CompareTag(m_railTag))
        {
            ProtJump();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(MapConst.c_tagHero))
        {
            m_attacking = false;
            ProtMove();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_attacking==true && m_isKnockback==false && collision.gameObject.CompareTag(m_railTag))
        {
            StartCoroutine(CoKnockback());
        }
    }

    //------------------------------------------------------

    private IEnumerator CoKnockback()
    {
        m_isKnockback = true;
        m_moveDir.x = Vector2.right.x * speed;

        yield return new WaitForSeconds(0.25f);

        m_moveDir.x = Vector2.left.x * speed;
        m_isKnockback = false;
    }
}
