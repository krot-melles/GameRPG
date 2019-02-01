using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private NPC currentTarget;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()

    {  //Выбор таргета 
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null) // Если есть любой HIT
            {
                if (currentTarget != null) // Если есть выбраный таргет
                {
                    currentTarget.DeSelect(); // сброс таргета
                }
                currentTarget = hit.collider.GetComponent<NPC>(); // смена таргета
                player.MyTarget = currentTarget.Select(); // присвоение таргета игроку

                UIManager.MyInstance.ShowTargetFrame(currentTarget);
            }

            else //отмена таргета
            {
                UIManager.MyInstance.HideTargetFrame();
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = null;
                player.MyTarget = null;

            }

        }
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if (hit.collider != null && hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<NPC>().Interact();
            }
        }

    }
}
