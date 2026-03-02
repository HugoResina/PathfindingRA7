using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    public int nodeX, nodeY;
    public bool walkable;

    public bool hasBeenVisited;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost; 
    public Node parent;
    public SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public int GetDistance(Node nodeA, Node nodeB)
    {
         
         

        return Mathf.Abs(nodeA.nodeX - nodeB.nodeX) + Mathf.Abs(nodeA.nodeY - nodeB.nodeY);
    }

}
