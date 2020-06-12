using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CtrlSlots : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
   // public bool canHold;//三个都可以放置为true
    public List<CtrlSlotItem> ctrlItems;
    Vector3 originPos;

    void Awake()
    {
        foreach (CtrlSlotItem i in ctrlItems)
        {
            i.ChangeState(0);
        }
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = transform.position;
        GetComponent<Image>().raycastTarget  = false;


        foreach (CtrlSlotItem i in ctrlItems)
        {
            i.ChangeState(1);
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
        pos.y = pos.y + 160f;
        transform.position = pos;
 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach(CtrlSlotItem i in ctrlItems)
        {
            if(i.canHold==false)
            {
              //  canHold = false;
                transform.position = originPos;//不可放置放回原位
                foreach (CtrlSlotItem ci in ctrlItems)
                {
                    ci.ChangeState(0);
                }
                return;
            }

        }

        //可以放置消除这个，然后刷新一个新的 在下面 然后调用管理器设置开关
        Destroy(this.gameObject);
        LevelManager.Instance.RefreshCtrlSlots(1);
        LevelManager.Instance.SetSlot();

        GetComponent<Image>().raycastTarget = true ;


        //放回原位


    }

   
}
