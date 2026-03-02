using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    
    private int Size = 10;
    public int Center;
    public Node Node;
    public Node[,] Map;
    private Node StartN;
    private Node EndN;
    public List<Node> OpenSet = new List<Node>();
    public List<Node> ClosedSet = new List<Node>();
    public List<Node> Nodes = new List<Node>();
    
    public Camera mainCamera;
    private void Start()
    {
        Center = (Size / 2) - Size;
        Map = new Node[Size, Size];
        
        StartMap();
      
        OpenSet.Add(StartN);
        //StartN._spriteRenderer.color = Color.white;
        //EndN._spriteRenderer.color = Color.black;

        StartCoroutine( (AStar(OpenSet)));

    }
    public void StartMap()
    {
        for (int X = 0; X < Size ; X++)
        {
            for(int Y = 0; Y < Size; Y++)
            {
                Node CN = Instantiate(Node);
                CN.nodeX = Y;
                CN.nodeY = X;
                CN.transform.position = new Vector2(Center + CN.nodeX * 1.05f , Center + CN.nodeY * 1.05f );
                //CN.id = CN.nodeX*Dimension + CN.nodeY;
                Nodes.Add(CN);
                //CN.Heuristic = Mathf.Abs(CN.nodeX - EndN.nodeX) + Mathf.Abs(CN.nodeY - EndN.nodeY);
                //Debug.Log(CurrNode.id); 
                Map[X, Y] = CN;
            }
        }
        StartN = Nodes[0];
        EndN = Nodes[Nodes.Count - 1];
        RedrawMap();
        foreach (Node node in Nodes)
        {
            node.hCost = node.GetDistance(node, EndN);
        }
    }
   
    
    public void RedrawMap()
    {
        //for (int i = 0; i < Map.Length; i++)
        //{
        //    for (int j = 0; j < Map.Length; j++)
        //    {
        //        Map[i, j].hasBeenVisited = true;
        //        SetState(Map[i, j]);
        //    }
        //}
        //OpenSet[0].hasBeenVisited = true;

        //Debug.Log(Nodes[67].id);
        Debug.Log("entro bucle");
        foreach (Node n in Nodes)
        {
            
            
            //n._spriteRenderer.color = new Color( 0.01f * ((float)n.id + 1 / 255),0,0);
            //Debug.Log(n.id + " r: " + n._spriteRenderer.color.r);
            SetState(n);
        }
        
    }
    public IEnumerator AStar(List<Node> NodeList)
    {
     

        foreach(Node n in Nodes) n.gCost = int.MaxValue;
        StartN.gCost = 0;

        while (OpenSet.Count> 0)
        {

            Node CurrentNode = OpenSet.OrderBy(n => n.fCost).First();

            //NodeList.OrderBy(Node => Node.fCost);
            
            if (CurrentNode.Equals(EndN))
            {
                //guanya
                Debug.Log("has guanyat");
                RetracePath();
                break;
            }

            OpenSet.Remove(CurrentNode);
            ClosedSet.Add(CurrentNode);
            CurrentNode.hasBeenVisited = true;

            foreach(Node neighbour in GetNeighbours(CurrentNode))
            {
                if (ClosedSet.Contains(neighbour)) continue;

                int CostMoveToNeighbour = CurrentNode.gCost + 1;
                

                if(CostMoveToNeighbour < neighbour.gCost)
                {
                    neighbour.gCost = CostMoveToNeighbour;
                    neighbour.parent = CurrentNode;

                    if (!OpenSet.Contains(neighbour))
                        OpenSet.Add(neighbour);
                }
            }


            RedrawMap();
            CurrentNode._spriteRenderer.color = Color.black;
            yield return new WaitForSeconds(0.1f);
        }
    }
    //un fil random de reddit suggereix una cosa aixi com una forma més optima de recorrer els veins 
    public IEnumerable<Node> GetNeighbours(Node n)
    {
        int[] dx = { 1, -1, 0, 0 }, dy = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            //int nx = n.nodeY + dx[i], ny = n.nodeX + dy[i];
            int nx = n.nodeX + dx[i], ny = n.nodeY + dy[i];
            if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
            {
                //Map[nx, ny].parent = n;
                yield return Map[nx, ny];
            }
              
        }
    }

    public void SetState(Node node)
    {
        if (node.hasBeenVisited)
        {
            node._spriteRenderer.color = Color.green;
        }
    }
    void RetracePath()
    {
        Node curr = EndN;
        while (curr != StartN)
        {
            curr._spriteRenderer.color = Color.blue;
            curr = curr.parent;
        }
    }
}
