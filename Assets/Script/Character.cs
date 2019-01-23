using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [Header("Скорость движения")]
    [SerializeField]
    private float speed;
    [Header("Статы")]
    [SerializeField]
    private float initHealth;

    protected Animator myAnimator;


    protected Vector2 direction;
    private Rigidbody2D myRigidbody;
    protected bool isAttacking = false;
    protected Coroutine attackRoutine;
    [SerializeField]
    protected Transform hitBox;
    [SerializeField]
    protected Stat health;
    public Stat MyHealth
    {
        get { return health; }
    }


    public bool isMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;

        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health.Initialized(initHealth, initHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        HandleLayers();

    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        //движение персонажей по терайну
        myRigidbody.velocity = direction.normalized * speed;
                        
    }
    public void HandleLayers()

    {
        if (isMoving)
        {
            //Анимация передвижения
            AnimateMovement(direction);
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        //анимация атаки
        else if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            //анимация отдыха
            ActivateLayer("IdleLayer");
        }

    }

    protected void AnimateMovement(Vector2 direction)
    {

    }
    /// <summary>
    /// Активатор анимаций
    /// </summary>
    /// <param name="layerName"></param>
       public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);

        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    /// <summary>
    /// Стоп атака
    /// </summary>
    public virtual void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking); //стоп анимация атаки

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

    }
    public virtual void TakeDamage(float damage)
    {
        health
            .MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            myAnimator.SetTrigger("die");

        }

    }
}

