using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy_PathFinding : MonoBehaviour
{
    //public Transform player, EndPoint;
    Grid grid;
    public List<Node> path = new List<Node>();

    // Use this for initialization
    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //if (startNode._canWalk && endNode._canWalk)

    public List<Node> FindingPath(Vector3 StarPos, Vector3 EndPos)
    {
        List<Node> result = new List<Node>();
        Node startNode = grid.GetFromPos(StarPos);
        Node endNode = grid.GetFromPos(EndPos);
        //Instantiate the capacity of the heap according to the size of the map
        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);
        while (openSet.Count() > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            //openSet.Remove(currentNode);
            closeSet.Add(currentNode);
            if (currentNode == endNode)
            {
                result = GeneratePath(startNode,endNode);
                break;
            }
            //Determining the best node around
            foreach (var item in grid.GetNeibourhood(currentNode))
            {
                if (!item._canWalk || closeSet.Contains(item))
                    continue;
                int newCost = currentNode.gCost + GetDistanceNodes(currentNode, item);
                if (newCost < item.gCost || !openSet.Contains(item))
                {
                    item.gCost = newCost; 
                    item.hCost = GetDistanceNodes(item, endNode);
                    //Record parent node
                    item.parent = currentNode;
                    if (!openSet.Contains(item))
                        openSet.Add(item);
                    else
                        openSet.UpdateItem(item);
                }
            }
        }
        return result;
    }

    List<Node> GeneratePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node temp = endNode;
        while (temp != startNode)
        {
            path.Add(temp);
            temp = temp.parent;
        }
        //List reversal
        path.Reverse();
        grid.path = path;
        this.path = path;
        return path;
/*        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        grid.path = path;
        return waypoints;*/
    }

/*    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1]._gridX - path[i]._gridX, path[i - 1]._gridY - path[i]._gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i]._worldPos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }*/

    int GetDistanceNodes(Node a, Node b)
    {
        //Estimation of weights, diagonal algorithm depending on whether there are more squares on the X-axis or the Y-axis Diagonal shift can be calculated
        int cntX = Mathf.Abs(a._gridX - b._gridX);
        int cntY = Mathf.Abs(a._gridY - b._gridY);
        if (cntX > cntY)
        {
            return 14 * cntY + 10 * (cntX - cntY);
        }
        else
        {
            return 14 * cntX + 10 * (cntY - cntX);
        }

        //Manhattan Algorithm
        //return Mathf.Abs(a._gridX - b._gridX) * 10 + Mathf.Abs(a._gridY - b._gridY) * 10;
    }
}
