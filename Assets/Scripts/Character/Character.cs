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

    private Vector2 direction;

    private Rigidbody2D myRigidbody;
    
    public bool IsAttacking { get; set; }

    protected Coroutine attackRoutine;
    [SerializeField]
    protected Transform hitBox;
    [SerializeField]
    protected Stat health;
    public Transform MyTarget { get; set; }
    public Stat MyHealth
    {
        get { return health; }
    }


    public bool isMoving
    {
        get
        {
            return Direction.x != 0 || Direction.y != 0;

        }
    }

    public Vector2 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed;  }

    public bool IsAlive
    {

        get
        {
            return health.MyCurrentValue > 0;
        }
    }

    public Animator MyAnimator { get ; set ; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health.Initialized(initHealth, initHealth);
        myRigidbody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
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
        if (IsAlive)
        {
            //движение персонажей по терайну
            myRigidbody.velocity = Direction.normalized * Speed;
        }

                        
    }
    public void HandleLayers()

    {
        if (IsAlive)
        {
            if (isMoving)
            {
                //Анимация передвижения
                AnimateMovement(Direction);
                ActivateLayer("WalkLayer");

                MyAnimator.SetFloat("x", Direction.x);
                MyAnimator.SetFloat("y", Direction.y);

            }
            //анимация атаки
            else if (IsAttacking)
            {
                ActivateLayer("AttackLayer");
            }
            else
            {
                //анимация отдыха
                ActivateLayer("IdleLayer");
            }

        }
        else
        {
            ActivateLayer("DeathLayer");
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
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);

        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public virtual void TakeDamage(float damage, Transform source)
    {
        

        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0)
        {
            Direction = Vector2.zero;
            myRigidbody.velocity = Direction;
            MyAnimator.SetTrigger("die");

        }

    }
}

