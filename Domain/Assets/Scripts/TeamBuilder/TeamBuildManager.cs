using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBuildManager : MonoBehaviour
{
    public UDListScriptableObject dataListSO;

    public Image charIconBounds;
    public CharSelectIcon charSelectIcon;
    public GameObject charSelectParent;
    public HexParent hexParent;
    public CharGridMarker marker;
    public List<CharGridMarker> markList;
    public TeamMessenger teamMessenger;
    public StageNumberTesting stageObject;

    public int maxTeamSize = 4;
    public TeamData teamData;

    // Start is called before the first frame update
    void Start()
    {
        DataSerialization serializer = new DataSerialization();
        markList = new List<CharGridMarker>();
        teamData = new TeamData();
        UnitIndividualCollection newCollection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));

        for (int i = 0; i < newCollection.collection.Count; i++)
        {
            CharSelectIcon temp = Instantiate(charSelectIcon, charIconBounds.transform);
            temp.SetInitial(temp.transform.localPosition + new Vector3(-450 + (300 * i), 0, 0),
                (dataListSO.uDList[newCollection.collection[i].unitId], newCollection.collection[i]));
            temp.manager = this;
        }

        for (int i = 0; i < dataListSO.uDList.Count; i++)
        {
            
            //FIXME
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IconReleased(CharSelectIcon icon)
    {
        //FIXME dynamically generate, replace with foreach
        for (int i = 0; i < hexParent.hexColliders.Count; i++)
        {
            HexCollider collider = hexParent.hexColliders[i];
            if (collider.GetComponentInParent<PolygonCollider2D>().bounds.Contains(icon.transform.position))
            {
                IconHit(icon, collider, i);
                return;
            }
        }
    }

    public void IconHit(CharSelectIcon icon, HexCollider collider, int i)
    {
        if (!collider.occupied && markList.Count < maxTeamSize)
        {
            CharGridMarker temp = Instantiate(marker, charSelectParent.transform);
            temp.SetInitial(collider.transform.position, icon.compositeData, i);
            collider.occupied = true;
            markList.Add(temp);
        }
    }

    public void ExportTeam()
    {
        TeamMessenger dontDestroy = Instantiate(teamMessenger);
        GameObject.DontDestroyOnLoad(dontDestroy);
        foreach(CharGridMarker mark in markList)
        {
            teamData.AddUnitData(new UnitRuntimeData(mark.compositeData), mark.positionId);
        }
        //FIXME
        dontDestroy.stageId = stageObject.stage;
        dontDestroy.teamData = teamData;
    }

    public void ExitToBattle()
    {
        ExportTeam();
        SceneLoader.LoadBattle();
    }
}
