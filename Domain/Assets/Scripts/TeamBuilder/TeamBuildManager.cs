using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBuildManager : MonoBehaviour
{
    public UDListScriptableObject dataListSO;
    public CharSelectIcon charSelectIcon;
    public GameObject charSelectParent;
    public HexParent hexParent;
    public CharGridMarker marker;
    public List<CharGridMarker> markList;

    public int maxTeamSize = 4;
    public TeamData teamData;

    // Start is called before the first frame update
    void Start()
    {
        markList = new List<CharGridMarker>();
        teamData = new TeamData();
        for (int i = 0; i < dataListSO.uDList.Count; i++)
        {
            CharSelectIcon temp = Instantiate(charSelectIcon, charSelectParent.transform);
            temp.SetInitial(temp.transform.localPosition + new Vector3(300 * i, 0, 0), dataListSO.uDList[i]);
            temp.manager = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IconReleased(CharSelectIcon icon)
    {
        foreach (HexCollider collider in hexParent.hexColliders)
        {
            //Debug.Log(icon.transform.position.z);
            if (collider.GetComponentInParent<PolygonCollider2D>().bounds.Contains(icon.transform.position))
            {
                IconHit(icon, collider);
                return;
            }
        }
    }

    public void IconHit(CharSelectIcon icon, HexCollider collider)
    {
        if (!collider.occupied && markList.Count < maxTeamSize)
        {
            CharGridMarker temp = Instantiate(marker, charSelectParent.transform);
            temp.SetInitial(collider.transform.position, icon.unitData);
            collider.occupied = true;
            markList.Add(temp);
        }
    }
}
