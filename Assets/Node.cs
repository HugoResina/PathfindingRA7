using UnityEngine;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    //public float nodeX, nodeY;
    public bool hasBeenVisited;
    public int Heuristic;
    public int id;
    public SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void asdf()
    {
        Debug.Log("adsfasdf");
    }

}
