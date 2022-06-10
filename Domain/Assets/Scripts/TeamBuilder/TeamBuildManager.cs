using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBuildManager : MonoBehaviour
{
    public UDListScriptableObject dataListSO;

    public Canvas canvas;
    public GameObject UIBackground;
    public GameObject charIconBounds;
    public ScrollRect scrollRect;
    public CharSelectIcon charSelectIcon;

    public GameObject gridMarkerParent;
    public HexParent hexParent;
    private float yValPositionOffset = 235;
    public List<Vector3> hexTileLocalPositions;

    public CharGridMarker marker;
    public List<CharGridMarker> markList;
    public GameObject enemyTileGrid;
    public List<CharGridMarker> enemyMarkList;

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
        PlayerCollectionData newCollection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));

        for (int i = 0; i < newCollection.individualDataList.Count; i++)
        {
            CharSelectIcon temp = Instantiate(charSelectIcon, charIconBounds.transform);
            temp.SetInitial(this, (dataListSO.uDList[newCollection.individualDataList[i].unitId],
                newCollection.individualDataList[i]), i);
        }

        hexTileLocalPositions = new List<Vector3>();
        for (int i = 0; i < hexParent.hexColliders.Count; i++)
        {
            hexTileLocalPositions.Add(hexParent.hexColliders[i].transform.localPosition);

        }

        SetEnemyIcons();
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
            icon.CharUsed();
            CharGridMarker temp = Instantiate(marker, gridMarkerParent.transform);
            temp.SetInitial(collider.transform.position, icon.compositeData, i);
            collider.occupied = true;
            markList.Add(temp);
        }
    }

    //CALL BEFORE SCENE LOAD!!!
    public void ExportTeam()
    {
        TeamMessenger dontDestroy = Instantiate(teamMessenger);
        GameObject.DontDestroyOnLoad(dontDestroy);
        foreach (CharGridMarker mark in markList)
        {
            teamData.AddUnitData(new UnitRuntimeData(mark.compositeData), mark.positionId);
        }
        //FIXME
        dontDestroy.stageId = stageObject.stage;
        dontDestroy.teamData = teamData;
    }

    public void SetEnemyIcons()
    {
        foreach (CharGridMarker gridMarker in enemyMarkList)
        {
            Destroy(gridMarker.gameObject);
        }
        enemyMarkList = new List<CharGridMarker>();
        DataSerialization serializer = new DataSerialization();
        StageDataCollection stageList = serializer.DeserializeStageData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/StageData.json"));
        PrimitiveTeamData currentStageData = stageList.stageDataList[stageObject.stage];
        for (int i = 0; i < currentStageData.dataList.Count; i++)
        {
            CharGridMarker temp = Instantiate(marker, enemyTileGrid.transform);
            temp.SetEnemyInitial(hexTileLocalPositions[currentStageData.positionList[i] - 24] - new Vector3(0, yValPositionOffset, 0),
                dataListSO.uDList[currentStageData.dataList[i].unitId].unitSprite);
            /*
            temp.SetEnemyInitial(hexTileLocalPositions[currentStageData.positionList[i] - 24] -
                new Vector3(0,(enemyTileGrid.transform.localPosition.y - hexParent.transform.localPosition.y), 0),
                dataListSO.uDList[currentStageData.dataList[i].unitId].unitSprite);
            */
            enemyMarkList.Add(temp);
        }
    }
}
