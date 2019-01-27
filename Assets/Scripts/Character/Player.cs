using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    public Stat MyMana { get => mana;}

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

    private Vector3 min, max;
  

    protected override void Start()
    {
   
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y),transform.position.z);
        base.Update();
    }

   public void GetInpu()
    {



        Direction = Vector2.zero;

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
        if (Input.GetKey(KeybindsManeger.MyInstance.KeyBinds["UP"])) //вверх
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }
        if (Input.GetKey(KeybindsManeger.MyInstance.KeyBinds["LEFT"])) //влево
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }
        if (Input.GetKey(KeybindsManeger.MyInstance.KeyBinds["DOWN"]))//вниз
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeybindsManeger.MyInstance.KeyBinds["RIGHT"]))//вправо
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }
        //Управление с клавиатуры. конец

        if (isMoving)
        {
            StopAttack();
        }
        foreach (string action in KeybindsManeger.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindsManeger.MyInstance. ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);

            } 
        }
  
    }
    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;


    }
    /// <summary>
    /// Корутина для атаки
    /// </summary>
    /// <param name="spellIndex"></param>
    /// <returns></returns>
    private IEnumerator Attack(string spellName)
    {
        Transform currentTarget = MyTarget;

        Spell newSpell = SpellBook.MyInstance.CastSpell(spellName);

        IsAttacking = true;//индикатор того что активна атака

        MyAnimator.SetBool("attack", IsAttacking);//старт анимации атаки
        yield return new WaitForSeconds(newSpell.MyCastTime);// Экстримальный каст, сугубо для теста 

        if (currentTarget!= null && InLineOfSight())
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }
        StopAttack();// стоп атаки
    }

    public void CasteSpell(string spellName)
    {
        Block();

        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !isMoving && InLineOfSight()) //проверка готовности атаки
        {
            attackRoutine = StartCoroutine(Attack(spellName));
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

    /// <summary>
    /// Стоп атака
    /// </summary>
    public void StopAttack()
    {

        SpellBook.MyInstance.StopCasting();

        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking); //стоп анимация атаки

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

    }
}

