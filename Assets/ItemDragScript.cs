using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //Transform DragGO;
    //Transform DragGOParent;
    Transform lastParent;
    Transform mainCanvas;

    public Image profile;
    public Text count;
    public Text id;

    private ItemInfo item;

    public ItemInfo Item
    {
        get => item;
        set
        {
            item = value;
            item.GameObjectItem = this.gameObject;
            profile.sprite = Resources.Load<Sprite>(item.Profile);
            count.text = item.Count.ToString();
            id.text = item.specialId.ToString();
            item.UpdateCountUI += () => { count.text = item.Count.ToString(); };
        }
    }

    private void Awake()
    {
        mainCanvas = GameObject.Find("Canvas").transform;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (!eventData.pointerEnter.name.Contains("Item")) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //DragGO = eventData.pointerEnter.transform;
        //DragGOParent = DragGO.parent;
        lastParent = this.transform.parent;
        this.gameObject.transform.SetParent(mainCanvas);
        this.gameObject.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //if (DragGO == null) return;
        this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, Camera.main.gameObject.transform.forward.z);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //if (DragGO == null || DragGOParent == null) { return; }
        this.gameObject.GetComponent<Image>().raycastTarget = true;
        if (eventData.pointerEnter == null) { Restore(); return; }

        if (eventData.pointerEnter.tag == ("Slot"))
        {
            if (item.slotType == SlotType.Equipment)
            {
                GameManagerInVillage.Player.TryUnequipItem(item.Type);
                Release();
            }
            else
            {
                MoveToEmpty(eventData);
            }
            return;
        }

        if (eventData.pointerEnter.tag == ("Equipment") && item.Type != ItemType.Consume)
        {
            GameManagerInVillage.Player.TryEquipItem(item);
            Release();
            return;
        }

        if (eventData.pointerEnter.name.Contains("Item") && eventData.pointerEnter.transform.parent.gameObject.tag == "Slot"
            && item.slotType != SlotType.Equipment)
        {
            Exchange(eventData);
            return;
        }

        Restore();
    }

    private void Release()
    {
        //DragGO = null;
        //DragGOParent = null;
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
        //DragGO.SetParent(DragGOParent);
        //DragGO.localPosition = Vector3.zero;
        //DragGO = null;
        //DragGOParent = null;

        this.gameObject.transform.SetParent(lastParent);
        this.gameObject.transform.localPosition = Vector3.zero;
        lastParent = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelMenu, Camera.main.ScreenToWorldPoint(eventData.position) + Camera.main.transform.forward * 10);
            if (item.slotType == SlotType.Equipment)
            {
                GameManagerInVillage.Player.TryUnequipItem(item.Type);
                return;
            }
            if (item.slotType == SlotType.Inventory)
            {
                if (item.Type == ItemType.Consume)
                {
                    //GameManagerInVillage.Player.TryUseItem(item);
                }
                else
                {
                    GameManagerInVillage.Player.TryEquipItem(item);
                }
                return;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item.Type == ItemType.Consume)
        {
            PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelItemDetail, () =>
            {
                PanelManagerInVillage.Instance.Panels[NameMap.PanelItemDetail].Item = item;
                RectTransform PanelRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelItemDetail].GameObjectPanel.GetComponent<RectTransform>();
                RectTransform AlignedObjectRect = this.gameObject.GetComponent<RectTransform>();
                PanelRect.position = AlignedObjectRect.position;
                PanelRect.anchoredPosition += new Vector2(0, +PanelRect.rect.height / 2 + AlignedObjectRect.rect.height / 2);

                //Debug.Log(PanelRect.anchoredPosition3D);
                //Debug.Log(PanelRect.anchoredPosition);
                //Debug.Log(PanelRect.localPosition);
                //Debug.Log(PanelRect.position);

                if (PanelRect.localPosition.y + PanelRect.rect.height / 2 > GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height / 2)
                {
                    PanelRect.anchoredPosition += new Vector2(0, -PanelRect.rect.height - AlignedObjectRect.rect.height);
                }
            });
        }
        else
        {
            if (Item.slotType == SlotType.Inventory)
            {
                PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelEquipDetail, () =>
                {
                    PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipDetail].Item = item;
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
            if (Item.slotType == SlotType.Equipment)
            {
                PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelEquipDetail, () =>
                {
                    PanelManagerInVillage.Instance.Panels[NameMap.PanelEquipDetail].Item = item;
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
