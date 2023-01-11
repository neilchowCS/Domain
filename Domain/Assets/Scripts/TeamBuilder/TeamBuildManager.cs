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
    public List<CharSelectIcon> charSelectIconList;

    public GameObject gridMarkerParent;
    public HexParent hexParent;
    private float yValPositionOffset = 235;
    public List<Vector3> hexTileLocalPositions;

    public BaseUnitIcon marker;
    public List<BaseUnitIcon> markList;
    public List<int> positionList;
    public GameObject enemyTileGrid;
    public List<BaseUnitIcon> enemyMarkList;

    public TeamMessenger teamMessenger;

    public int stageId;

    public int maxTeamSize = 4;

    // Start is called before the first frame update
    public void Init()
    {
        foreach(CharSelectIcon icon in charSelectIconList)
        {
            Destroy(icon.gameObject);
        }
        charSelectIconList = new();
        DataSerialization serializer = new DataSerialization();
        markList = new();
        positionList = new();
        PlayerCollectionData newCollection = serializer.DeserializeCollection(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerCollection.json"));

        for (int i = 0; i < newCollection.individualDataList.Count; i++)
        {
            CharSelectIcon temp = Instantiate(charSelectIcon, charIconBounds.transform);
            temp.SetInitial(this, (dataListSO.uDList[newCollection.individualDataList[i].unitId],
                newCollection.individualDataList[i]), i);
            charSelectIconList.Add(temp);
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
            BaseUnitIcon temp = Instantiate(marker, gridMarkerParent.transform);
            temp.transform.position = collider.transform.position;
            temp.transform.localScale = new Vector3(.6f, .6f, .6f);
            temp.InitButton(dataListSO, icon.compositeData.Item2);

            //temp.SetInitial(collider.transform.position, icon.compositeData, i);
            collider.occupied = true;

            //team messenger
            markList.Add(temp);
            positionList.Add(i);
        }
    }

    //CALL BEFORE SCENE LOAD!!!
    public void ExportTeam()
    {
        TeamMessenger dontDestroy = Instantiate(teamMessenger);
        GameObject.DontDestroyOnLoad(dontDestroy);

        DataSerialization serializer = new DataSerialization();
        StageDataCollection stageData = serializer.DeserializeStageData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/StageData.json"));

        dontDestroy.teamRecord = new BattleRecord(stageData.stageDataList[stageId]);
        dontDestroy.stageId = stageId;

        for (int i = 0; i < markList.Count; i++)
        {
            dontDestroy.teamRecord.AddItem(true, markList[i].individualData, positionList[i]);
        }
    }

    public void SetEnemyIcons()
    {
        foreach (BaseUnitIcon gridMarker in enemyMarkList)
        {
            Destroy(gridMarker.gameObject);
        }
        enemyMarkList = new();
        DataSerialization serializer = new DataSerialization();
        StageDataCollection stageList = serializer.DeserializeStageData(
            System.IO.File.ReadAllText(Application.persistentDataPath + "/StageData.json"));
        PrimitiveTeamData currentStageData = stageList.stageDataList[stageId];
        for (int i = 0; i < currentStageData.dataList.Count; i++)
        {
            BaseUnitIcon temp = Instantiate(marker, enemyTileGrid.transform);
            /*
            temp.SetEnemyInitial(hexTileLocalPositions[currentStageData.positionList[i] - 24] - new Vector3(0, yValPositionOffset, 0),
                dataListSO.uDList[currentStageData.dataList[i].unitId].unitSprite);
            */
            temp.transform.localPosition = hexTileLocalPositions[currentStageData.positionList[i] - 24]
                - new Vector3(0, yValPositionOffset, 0);
            temp.transform.localScale = new Vector3(.6f, .6f, .6f);
            temp.InitButton(dataListSO, currentStageData.dataList[i]);
            /*
            temp.SetEnemyInitial(hexTileLocalPositions[currentStageData.positionList[i] - 24] -
                new Vector3(0,(enemyTileGrid.transform.localPosition.y - hexParent.transform.localPosition.y), 0),
                dataListSO.uDList[currentStageData.dataList[i].unitId].unitSprite);
            */
            enemyMarkList.Add(temp);
        }
    }
}
