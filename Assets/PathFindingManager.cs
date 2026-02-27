using Mono.Cecil;
using System.Threading;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    
    private int Dimension = 10;
    public int Center;
    public Node Node;
    public Node[,] Map;
    public Camera mainCamera;
    private void Start()
    {
        Center = (Dimension / 2) - Dimension;
        Map = new Node[Dimension, Dimension];

        StartMap();

        
    }
    public void StartMap()
    {
        for (int i = 0; i < Dimension ; i++)
        {
            for(int j = 0; j < Dimension; j++)
            {
                Node CurrNode = Instantiate(Node);
                CurrNode.transform.position = new Vector2(Center + i * 1.05f , Center + j * 1.05f );
                CurrNode.id = i*Dimension + j;
                //Debug.Log(CurrNode.id);
                Map[i, j] = Node;
            }
        }
    }

    
    public void RedrawMap(Node[,] Map)
    {
        for (int i = 0; i < Map.Length; i++)
        {
            for (int j = 0; j < Map.Length; j++)
            {
                Map[i, j].hasBeenVisited = true;
                SetState(Map[i, j]);
            }
        }
        
    }
    public void SetState(Node node)
    {
        if (node.hasBeenVisited)
        {
            node._spriteRenderer.color = Color.green;
        }
        Thread.Sleep(100);
    }
    
}
