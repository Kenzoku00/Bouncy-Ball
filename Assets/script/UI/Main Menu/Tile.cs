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

        if(blocked)
        {
            gridManager.BlockNode(cords);
            List<BoxCollider> boxCollider = gameObject.GetComponents<BoxCollider>().ToList();
            boxCollider.ForEach(col => col.enabled = false);
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
