using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalRegion : MonoBehaviour
{
    float xSize;
    float zSize;
    Vector3 size;
    public int row; //행의 개수
    public int column; //열의 개수
    float cellxSize;
    float cellzSize;
    float xStartpos;
    float zStartpos;
    void Start()
    {
        GetMapsize();
        cellxSize = size.x / (float)column;
        cellzSize = size.z / (float)row;
        xStartpos = transform.position.x - size.x * 0.5f;
        zStartpos = transform.position.z + size.z * 0.5f;
        SpawnAll();
    }
    //격자의 모든 중앙에 게임 오브젝트 생성
    public void SpawnAll()
    {
        int tileIndex = row * column; 
        //2차원 배열 대신 1차원 배열로 계산하기 위해서 int사용
        Vector3 tmp = Vector3.zero;
        for(int i = 0; i < tileIndex; i++)
        {
            int nR = i / column;
            int nC = i % column;
            Vector3 pos = Vector3.zero;
            pos.x = xStartpos + cellxSize * nC + cellxSize * 0.5f;
            pos.y = 1f;
            pos.z = zStartpos - cellzSize * nR - cellzSize * 0.5f;
            //게임 오브젝트의 위치 계산
            GameObject tmpObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tmpObj.name = "Monster";
            tmpObj.transform.position = pos;
        }
    }
    public void GetMapsize()
    {
        //1.Collider가 존재하는가?
        //2.버텍스 검사
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Vector3[] vertexs = meshFilter.mesh.vertices;
        float xMin, xMax, zMin, zMax;
        xMin = vertexs[0].x;
        xMax = vertexs[0].x;
        zMin = vertexs[0].z;
        zMax = vertexs[0].z;
        for(int i = 1; i < vertexs.Length; i++)
        {
            if (xMin > vertexs[i].x)
                xMin = vertexs[i].x;
            if (zMin > vertexs[i].z)
                zMin = vertexs[i].z;
            if (xMax < vertexs[i].x)
                xMax = vertexs[i].x;
            if (zMax < vertexs[i].z)
                zMax = vertexs[i].z;
        }
        //xSize = (xMax - xMin) * transform.localScale.x;
        //zSize = (zMax - zMin) * transform.localScale.z;
        //Debug.Log(xSize + " X " + zSize + "사이즈의 맵이다.");
        Vector3 tmp1 = new Vector3(xMin, 0, zMin);
        Vector3 tmp2 = new Vector3(xMax, 0, zMax);
        Vector3 worldMin = transform.TransformPoint(tmp1);
        Vector3 worldMax = transform.TransformPoint(tmp2);
        size.x = (worldMax.x - worldMin.x);
        size.z = (worldMax.z - worldMin.z);
        Debug.Log(size.x + " X " + size.z + "사이즈의 맵이다.");
    }
    void Update()
    {
        
    }
}
