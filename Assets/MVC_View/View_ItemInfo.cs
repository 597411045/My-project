using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class View_ItemInfo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //Transform DragGO;
    //Transform DragGOParent;
    Transform lastParent;
    Transform Canvas;
    public SlotType slotType;
    public Control_ItemInfo c;

    public Text IdUI;
    public Text NameUI;
    public Image ProfileUI;
    public Text TypeUI;
    public Text ExpUI;
    public Text LevelUI;
    public Text QualityUI;
    public Text PowerUI;
    public Text PriceUI;
    public Text AtkUI;
    public Text LifeUI;
    public Text CountUI;
    public Text DescriptionUI;

    public void IdUIAction(object sender, int arg)
    {
        if(IdUI!=null)IdUI.text = arg.ToString();
    }

    public void CountUIAction(object sender, int arg)
    {
        if (CountUI != null) CountUI.text = arg.ToString();
    }

    public void DescriptionUIAction(object sender, string arg)
    {
        if (DescriptionUI != null) DescriptionUI.text = arg.ToString();
    }

    public void ProfileUIAction(object sender, string arg)
    {
        if (ProfileUI != null) ProfileUI.sprite = Resources.Load<Sprite>(arg);
    }

    private void Awake()
    {
        BuildUIElements();
    }
    public void BuildUIElements()
    {
        Canvas = GameObject.Find("Canvas").transform;
        ProfileUI = this.gameObject.GetComponent<Image>();
        CountUI = MyUtil.FindOneInChildren(this.transform, "Num").GetComponent<Text>();
        IdUI = MyUtil.FindOneInChildren(this.transform, "Id").GetComponent<Text>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        lastParent = this.transform.parent;
        this.gameObject.transform.SetParent(Canvas);
        this.gameObject.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, Camera.main.gameObject.transform.forward.z);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.gameObject.GetComponent<Image>().raycastTarget = true;
        if (eventData.pointerEnter == null) { Restore(); return; }

        if (eventData.pointerEnter.tag == ("Slot"))
        {
            if (slotType == SlotType.Equipment)
            {
                GameManagerInVillage.Player.TryUnequipItem(c.module.Type);
                Release();
            }
            else
            {
                MoveToEmpty(eventData);
            }
            return;
        }

        if (eventData.pointerEnter.tag == ("Equipment") && c.module.Type != ItemType.Consume)
        {
            GameManagerInVillage.Player.TryEquipItem(c.module);
            Release();
            return;
        }

        if (eventData.pointerEnter.name.Contains("Item") && eventData.pointerEnter.transform.parent.gameObject.tag == "Slot"
            && slotType != SlotType.Equipment)
        {
            Exchange(eventData);
            return;
        }

        Restore();
    }

    private void Release()
    {
        lastParent = null;
    }

    private void MoveToEmpty(PointerEventData eventData)
    {
        this.gameObject.transform.SetParent(eventData.pointerEnter.transform);
        this.gameObject.transform.localPosition = Vector3.zero;
    }
    private void Exchange(PointerEventData eventData)
    {
        this.gameObject.transform.SetParent(eventData.pointerEnter.transform.parent);
        this.gameObject.transform.localPosition = Vector3.zero;

        eventData.pointerEnter.transform.SetParent(lastParent);
        eventData.pointerEnter.transform.localPosition = Vector3.zero;
    }

    private void Restore()
    {
        this.gameObject.transform.SetParent(lastParent);
        this.gameObject.transform.localPosition = Vector3.zero;
        lastParent = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelMenu, Camera.main.ScreenToWorldPoint(eventData.position) + Camera.main.transform.forward * 10);
            if (slotType == SlotType.Equipment)
            {
                GameManagerInVillage.Player.TryUnequipItem(c.module.Type);
                return;
            }
            if (slotType == SlotType.Inventory)
            {
                if (c.module.Type == ItemType.Consume)
                {
                    //GameManagerInVillage.Player.TryUseItem(item);
                }
                else
                {
                    GameManagerInVillage.Player.TryEquipItem(c.module);
                }
                return;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (c.module.Type == ItemType.Consume)
        {
            PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelItemDetail, () =>
            {
                PanelManagerInVillage.Instance.Panels[NameMap.PanelItemDetail].Item = c.module;
                RectTransform PanelRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelItemDetail].GameObjectPanel.GetComponent<RectTransform>();
                RectTransform AlignedObjectRect = this.gameObject.GetComponent<RectTransform>();
                PanelRect.position = AlignedObjectRect.position;
                PanelRect.anchoredPosition += new Vector2(0, +PanelRect.rect.height / 2 + AlignedObjectRect.rect.height / 2);

                if (PanelRect.localPosition.y + PanelRect.rect.height / 2 > GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height / 2)
                {
                    PanelRect.anchoredPosition += new Vector2(0, -PanelRect.rect.height - AlignedObjectRect.rect.height);
                }
            });
        }
        else
        {
            if (slotType == SlotType.Inventory)
            {
                PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelEquipDetail, () =>
                {
                    PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipDetail].Item = c.module;
                    RectTransform PanelRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipDetail].GameObjectPanel.GetComponent<RectTransform>();
                    RectTransform AlignedObjectRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelInventory].GameObjectPanel.GetComponent<RectTransform>();
                    PanelRect.position = AlignedObjectRect.position;
                    PanelRect.anchoredPosition += new Vector2(-PanelRect.rect.width / 2 - AlignedObjectRect.rect.width / 2, 0);
                    //if (PanelRect.localPosition.y + PanelRect.rect.height / 2 > GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height / 2)
                    //{
                    //    PanelRect.anchoredPosition += new Vector2(0, -PanelRect.rect.height - AlignedObjectRect.rect.height);
                    //}
                });
            }
            if (slotType == SlotType.Equipment)
            {
                PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelEquipDetail, () =>
                {
                    PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipDetail].Item = c.module;
                    RectTransform PanelRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipDetail].GameObjectPanel.GetComponent<RectTransform>();
                    RectTransform AlignedObjectRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipmnemt].GameObjectPanel.GetComponent<RectTransform>();
                    PanelRect.position = AlignedObjectRect.position;
                    PanelRect.anchoredPosition += new Vector2(PanelRect.rect.width / 2 + AlignedObjectRect.rect.width / 2, 0);
                    //if (PanelRect.localPosition.y + PanelRect.rect.height / 2 > GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height / 2)
                    //{
                    //    PanelRect.anchoredPosition += new Vector2(0, -PanelRect.rect.height - AlignedObjectRect.rect.height);
                    //}
                });
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelItemDetail, null);
        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelEquipDetail, null);
    }

}
