using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstManager : MonoBehaviour
{
    // - circuit - s
    public List<Transform> waypoints = new List<Transform>();
    // - circuit - e

    public Text AmountOfRocks;

    public bool enableRockSpawn = true;

    public GameObject Rock;


    public GameObject[] spawnRocks;

    public GameObject spwanArea;
    BoxCollider spwanAreaCollider;

    public int rockSpawnAmount;
    public MoveToGoal_excavator checkAmtOfRocks;


    public ChangeTerrainHeight_lineMarker getTrainDataFromWorkArea;


    // - circuit - s
    private void OnDrawGizmosSelected()
    {
        if (waypoints.Count > 1)
        {
            Vector3 prev = waypoints[0].position;
            for (int i = 1; i < waypoints.Count; i++)
            {
                Vector3 next = waypoints[i].position;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
            Gizmos.DrawLine(prev, waypoints[0].position);
        }
    }
    // - circuit - e



    void SpawnRock()
    {
        if (enableRockSpawn)
        {
            
            for (int i = 0; i < rockSpawnAmount; i++)
            {
                Instantiate(Rock, Return_RandomPosition(), Quaternion.identity);
            }

        }

        //Debug.Log("Destroy spawnBox Collider!");
        Destroy(spwanAreaCollider.GetComponent<BoxCollider>());

    }


    void SpawnRockInWorkArea()
    {
        if (enableRockSpawn)
        {
            for (int i = 0; i < rockSpawnAmount; i++)
            {
                Instantiate(Rock, Return_WorkAreaPosition(i), Quaternion.identity);
            }
        }
    }


    Vector3 Return_WorkAreaPosition(int i)
    {

        // 생성 위치에 콜라이더를 생성시킨다.
        // 생성위치의 중심점을 찾아낸다. Vector3 originPos. 작업영역의 포인트들에서 중심위치 찾기

        // 위치값 데이터 확인
        //for (int k = 1; k < getTrainDataFromWorkArea.getWorkAreaList.Count; k++)
        //    Debug.Log("getTrainDataFromWorkArea.getWorkAreaArray : " + getTrainDataFromWorkArea.getWorkAreaList[k]);
        float posX = Mathf.Abs(getTrainDataFromWorkArea.leftBottom.Item1 - getTrainDataFromWorkArea.getWorkAreaList[i].Item1);

        float posY = Mathf.Abs(getTrainDataFromWorkArea.leftBottom.Item2 - getTrainDataFromWorkArea.getWorkAreaList[i].Item2);

        // test
        Vector3 respawnPos = new Vector3(posX + (posX * 0.1f), 50f, posY + (posY * 0.1f));

        return respawnPos;
    }




    public void CheckAmoutOfRocks()
    {
        if (spawnRocks != null)
        {
            spawnRocks = GameObject.FindGameObjectsWithTag("Rocks");
            // Debug.Log("spawnRocks:" + spawnRocks.Length);
            AmountOfRocks.text = spawnRocks.Length.ToString();
        }else
        {
            checkAmtOfRocks.WorkDone();
        }

    }


    Vector3 Return_RandomPosition()
    {
        Vector3 originPos = spwanArea.transform.position;
        float range_X = spwanAreaCollider.bounds.size.x;
        float range_Y = spwanAreaCollider.bounds.size.y;
        float range_Z = spwanAreaCollider.bounds.size.z;
        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, range_Z);

        Vector3 respawnPosition = originPos + RandomPostion;
        return respawnPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        //spwanAreaCollider = spwanArea.GetComponent<BoxCollider>();
        //SpawnRock();
    }

    public void StartWork()
    {
        // 작업이 시작되기 전에 작업범위, 높이를 설정해야 한다. 조건문으로 판단
        // if 작업법위, 높이 지정되었는지 확인
        //    SpawnRock(); 작업 실행
        // else
        //    "작업범위와 높이를 설정하시오"


        Debug.Log("StartWork!");

        if (getTrainDataFromWorkArea.getWorkAreaList != null)
        {

            Debug.Log("Start...!");
            // collder를 생성하거나 줌심 위치를 추출해야 한다.

            //spwanAreaCollider = spwanArea.GetComponent<BoxCollider>();
            SpawnRockInWorkArea();
        }else
        {
            Debug.Log("Calcualte work area first.");
        }

    }


    private void Update()
    {
        CheckAmoutOfRocks();
    }





}
