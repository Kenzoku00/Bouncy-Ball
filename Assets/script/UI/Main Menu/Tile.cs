using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool blocked;

    public Vector2Int cords;

    GridManager gridManager;

    // Start is called before the first frame update
    void Start()
    {
        SetCords();
        List<BoxCollider> boxCollider = gameObject.GetComponents<BoxCollider>().ToList();

        if(blocked)
        {
            gridManager.BlockNode(cords);   
            boxCollider.ForEach(col => col.enabled = false);
        }
        else {
            boxCollider.ForEach(col => col.enabled = true);
        }
    }

    private void SetCords()
    {
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int z = (int)transform.position.z;

        cords = new Vector2Int(x / gridManager.UnityGridSize, z / gridManager.UnityGridSize);
    }
}
