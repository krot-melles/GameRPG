using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{

    private Rigidbody2D myRigidBody;
    [SerializeField]
    private float speed;
    /// <summary>
    /// Таргет Игрока
    /// </summary>
    public Transform MyTarget{ get; private set; }
    private Transform source;
    private int damage;



    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
 

    }
    public void Initialize(Transform target, int damage, Transform source)
    {
        this.MyTarget = target;
        this.damage = damage;
        this.source = source;

    }
    private void FixedUpdate()
    {
        if (MyTarget != null)
                    {
            //расчет направления 
            Vector2 direction = MyTarget.position - transform.position;
            //Движение за счет rigid body
            myRigidBody.velocity = direction.normalized * speed;
            //расчет поворота
            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //поворот
            transform.rotation = Quaternion.AngleAxis(angel, Vector3.forward);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HitBox" && collision.transform == MyTarget)
        {
            Character c = collision.GetComponentInParent<Character>();
            speed = 0;
            c.TakeDamage(damage, source);
            GetComponent<Animator>().SetTrigger("impact");
            myRigidBody.velocity = Vector2.zero;
            MyTarget = null;
        }
    }
}
