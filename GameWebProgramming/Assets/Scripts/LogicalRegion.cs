using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicalRegion : MonoBehaviour
{
    float xSize;
    float zSize;
    Vector3 size;
    public int row; //���� ����
    public int column; //���� ����
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
    //������ ��� �߾ӿ� ���� ������Ʈ ����
    public void SpawnAll()
    {
        int tileIndex = row * column; 
        //2���� �迭 ��� 1���� �迭�� ����ϱ� ���ؼ� int���
        Vector3 tmp = Vector3.zero;
        for(int i = 0; i < tileIndex; i++)
        {
            int nR = i / column;
            int nC = i % column;
            Vector3 pos = Vector3.zero;
            pos.x = xStartpos + cellxSize * nC + cellxSize * 0.5f;
            pos.y = 1f;
            pos.z = zStartpos - cellzSize * nR - cellzSize * 0.5f;
            //���� ������Ʈ�� ��ġ ���
            GameObject tmpObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tmpObj.name = "Monster";
            tmpObj.transform.position = pos;
        }
    }
    public void GetMapsize()
    {
        //1.Collider�� �����ϴ°�?
        //2.���ؽ� �˻�
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
        //Debug.Log(xSize + " X " + zSize + "�������� ���̴�.");
        Vector3 tmp1 = new Vector3(xMin, 0, zMin);
        Vector3 tmp2 = new Vector3(xMax, 0, zMax);
        Vector3 worldMin = transform.TransformPoint(tmp1);
        Vector3 worldMax = transform.TransformPoint(tmp2);
        size.x = (worldMax.x - worldMin.x);
        size.z = (worldMax.z - worldMin.z);
        Debug.Log(size.x + " X " + size.z + "�������� ���̴�.");
    }
    void Update()
    {
        
    }
}
