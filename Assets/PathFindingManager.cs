using Mono.Cecil;
using System.Threading;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    private int Size = 10;
    public int Dimension = 20;
    public int Center;
    public Node Node;
    public Node[,] Map;
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
                Instantiate(Node);
                Node.transform.position = new Vector2(Center + i, Center + j);
              
                Map[i, j] = Node;

                
            }
        }
    }
}
