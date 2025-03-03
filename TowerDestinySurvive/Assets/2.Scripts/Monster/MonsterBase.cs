using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] protected float speed = 2.5f;

    protected Animator m_animator;
    protected Rigidbody2D m_rigidbody;
    protected Vector2 m_moveDir;
    protected string m_railTag;

    //-------------------------------------------------------------

    protected abstract void ProtMove();
    protected abstract void ProtAttack();
    protected abstract void ProtDie();
}
