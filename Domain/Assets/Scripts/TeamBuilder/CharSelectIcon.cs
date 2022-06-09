using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharSelectIcon : MonoBehaviour
{
    public TeamBuildManager manager;
    public (UnitDataScriptableObject, UnitIndividualData) compositeData;
    public int heirarchyIndex;

    public float initialPos;
    public Vector3 newPos;
    public float holdTimer = 0;

    public Image image;
    public Image progressFill;
    public TextMeshProUGUI levelText;
    public BoxCollider boxCollider;
    public GameObject empty;
    public GameObject instantiatedEmpty;

    private bool scrolling = false;
    private bool drag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        initialPos = this.transform.parent.transform.localPosition.y;
        scrolling = false;
    }

    public void OnMouseDrag()
    {
        newPos = Input.mousePosition;

        if (drag)
        {
            newPos.z = 10;
            this.transform.position = Camera.main.ScreenToWorldPoint(newPos);
        }
        else if (!scrolling)
        {
            if (Mathf.Abs(this.transform.parent.transform.localPosition.y - initialPos) > 5)
            {
                scrolling = true;
                holdTimer = 0;
                UpdateFill();
            }
            else
            {
                holdTimer += Time.deltaTime;
                if (holdTimer >= 0.2f)
                {
                    if (holdTimer >= .45f)
                    {
                        drag = true;
                        instantiatedEmpty = Instantiate(empty, this.transform.parent);
                        instantiatedEmpty.transform.SetSiblingIndex(heirarchyIndex);
                        this.transform.SetParent(manager.UIBackground.transform);
                        manager.scrollRect.enabled = false;
                        holdTimer = 0;
                    }
                    UpdateFill();
                }
            }
        }

    }

    public void OnMouseUp()
    {
        holdTimer = 0;
        UpdateFill();
        scrolling = false;
        if (drag)
        {
            drag = false;
            Destroy(instantiatedEmpty);
            this.transform.SetParent(manager.charIconBounds.transform);
            this.transform.SetSiblingIndex(this.heirarchyIndex);
            manager.scrollRect.enabled = true;
            manager.IconReleased(this);
            LayoutRebuilder.MarkLayoutForRebuild((RectTransform)manager.charIconBounds.transform);
        }
    }

    public void SetInitial(TeamBuildManager manager,
        (UnitDataScriptableObject, UnitIndividualData) compositeData,
        int heirarchyIndex)
    {
        this.manager = manager;
        this.compositeData = compositeData;
        image.sprite = compositeData.Item1.unitSprite;
        levelText.text = compositeData.Item2.level + "";
        this.heirarchyIndex = heirarchyIndex;
    }

    public void CharUsed()
    {
        boxCollider.enabled = false;
        image.color = Color.gray;
    }

    public void UpdateFill()
    {
        progressFill.fillAmount = (holdTimer - 0.2f) * 4;
    }
}
