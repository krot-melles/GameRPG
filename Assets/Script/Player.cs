using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Статы")]
    [SerializeField]
    private Stat mana;
    [SerializeField]
    private float initMana = 70;
    [Header("Обьекты ограничения зрения")]
    [SerializeField]
    private Block[] blocks;
    [Header("Точки выхода фаерболов/стрел")]
    [SerializeField]
    private Transform[] exitPoints;

    private int exitIndex = 2;

    private SpellBook spellBook;
    /// <summary>
    /// Таргет Игрока
    /// </summary>
    public Transform MyTarget { get; set; }



    protected override void Start()
    {
        spellBook = GetComponent<SpellBook>();
        mana.Initialized(initMana, initMana);

        //For Testing Only
        //MyTarget = GameObject.FindGameObjectWithTag("Enemy").transform;

        base.Start();
    }



    // Update is called once per frame
    protected override void Update()
    {
        //Управление с клавиатуры
        GetInpu();
        base.Update();
    }

    private void GetInpu()
    {

        direction = Vector2.zero;

        ///проверка ошибок
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 5;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 5;
        }


        //Управление с клавиатуры
        if (Input.GetKey(KeyCode.W)) //вверх
        {
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A)) //влево
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))//вниз
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))//вправо
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
        //Управление с клавиатуры. конец

  
    }
    /// <summary>
    /// Корутина для атаки
    /// </summary>
    /// <param name="spellIndex"></param>
    /// <returns></returns>
    private IEnumerator Attack(int spellIndex)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = spellBook.CastSpell(spellIndex);

        isAttacking = true;//индикатор того что активна атака

        myAnimator.SetBool("attack", isAttacking);//старт анимации атаки
        yield return new WaitForSeconds(newSpell.MyCastTime);// Экстримальный каст, сугубо для теста 

        if (currentTarget!= null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage);
        }
        StopAttack();// стоп атаки
    }

    public void CasteSpell(int spellIndex)
    {
        Block();

        if (MyTarget != null && !isAttacking && !isMoving && InLineOfSight()) //проверка готовности атаки
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }

        //Instantiate(spellPrefab[spellIndex], exitPoints[exitIndex].position, Quaternion.identity);
    }

     
    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            //Расчет растояния к цели
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }

        }

        return false;
    }
    private void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }
        blocks[exitIndex].Activate();
    
    }
    public override void StopAttack()
    {
        spellBook.StopCasting();

        base.StopAttack();
    }
}

