using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    Node[,] grid;
    /// <summary>
    /// 保存网格大小
    /// </summary>
    public Vector2 gridSize;
    /// <summary>
    /// 节点半径
    /// </summary>
    public float nodeRadius;
    /// <summary>
    /// 节点直径
    /// </summary>
    float nodeDiameter;
    Vector2 box;
    /// <summary>
    /// 射线图层
    /// </summary>
    public LayerMask WhatLayer;

    public Transform player;

    /// <summary>
    /// 每个方向网格数的个数
    /// </summary>
    public int gridCntX, gridCntY;

    /// <summary>
    /// 保存路径列表
    /// </summary>
    public List<Node> path = new List<Node>();
    // Use this for initialization
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        // 四舍五入
        gridCntX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridCntY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        grid = new Node[gridCntX, gridCntY];
        CreateGrid();
        //player = GameObject.Find("Player").GetComponent<Transform>();
    }
    /// <summary>
    ///返回地图大小的属性
    /// </summary>
    public int MaxSize
    {
        get
        {
            return gridCntX * gridCntY;
        }
    }
    private void CreateGrid()
    {
        Vector3 startPoint = transform.position - gridSize.x * 0.5f * Vector3.right
            - gridSize.y * 0.5f * Vector3.up;
        for (int i = 0; i < gridCntX; i++)
        {
            for (int j = 0; j < gridCntY; j++)
            {
                Vector3 worldPoint = startPoint + Vector3.right * (i * nodeDiameter + nodeRadius)
                    + Vector3.up * (j * nodeDiameter + nodeRadius);
                box = new Vector2(nodeDiameter - 0.1f, nodeDiameter - 0.1f);
                //此节点是否可走
                bool walkable = !(Physics2D.OverlapBox(worldPoint, box, 90, WhatLayer));
                //i，j是二维数组的索引
                grid[i, j] = new Node(walkable, worldPoint, i, j);
            }
        }
    }

    public Node GetFromPos(Vector3 pos)
    {
        float percentX = (pos.x + gridSize.x * 0.5f - transform.position.x) / (gridSize.x);
        float percentY = (pos.y + gridSize.y * 0.5f - transform.position.y) / (gridSize.y);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridCntX - 1) * percentX);
        int y = Mathf.RoundToInt((gridCntY - 1) * percentY);
        return grid[x, y];
    }
    void OnDrawGizmos()
    {
        //画出网格边缘
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x,gridSize.y,0));
        //画不可走网格
        if (grid == null)
            return;
        Node playerNode = GetFromPos(new Vector3(player.position.x,player.position.y+1,0));
        foreach (var item in grid)
        {
            Gizmos.color = item._canWalk ? Color.white : Color.red;
            if(!item._canWalk)
                Gizmos.DrawCube(item._worldPos, new Vector3(nodeDiameter - 0.1f, nodeDiameter - 0.1f,0));
        }
        //画路径
        if (path != null)
        {
            foreach (var item in path)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(item._worldPos, new Vector3(nodeDiameter - 0.1f, nodeDiameter - 0.1f, 0));
            }
        }
        //画玩家
        if (playerNode != null && playerNode._canWalk)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(playerNode._worldPos, Vector3.one * (nodeDiameter - 0.1f));
        }
    }

    public List<Node> GetNeibourhood(Node node)
    {
        List<Node> neibourhood = new List<Node>();
        //相邻上下左右格子
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int tempX = node._gridX + i;
                int tempY = node._gridY + j;

                if (tempX < gridCntX && tempX > 0 && tempY > 0 && tempY < gridCntY)
                {
                    neibourhood.Add(grid[tempX, tempY]);
                }
            }
        }
        return neibourhood;
    }
}
