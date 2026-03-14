using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    public Transform player;
    public GameObject tilePrefab;
    public int tilesCount = 7;
    public float tileSize = 10f;

    private LinkedList<GameObject> activeTiles = new LinkedList<GameObject>();

    void Start()
    {
        int half = tilesCount / 2;
        for (int i = -half; i <= half; i++)
        {
            GameObject tile = Instantiate(tilePrefab, new Vector3(i * tileSize, 0, 0), Quaternion.identity);
            tile.transform.SetParent(transform);
            activeTiles.AddLast(tile);
        }
    }

    void Update()
    {
        if (player.position.x > activeTiles.Last.Value.transform.position.x - (tileSize * (tilesCount / 2)))
        {
            MoveLeftToRight();
        }
        if (player.position.x < activeTiles.First.Value.transform.position.x + (tileSize * (tilesCount / 2)))
        {
            MoveRightToLeft();
        }
    }

    void MoveLeftToRight()
    {
        GameObject first = activeTiles.First.Value;
        activeTiles.RemoveFirst();

        float newX = activeTiles.Last.Value.transform.position.x + tileSize;
        first.transform.position = new Vector3(newX, 0, 0);

        activeTiles.AddLast(first);
    }

    void MoveRightToLeft()
    {
        GameObject last = activeTiles.Last.Value;
        activeTiles.RemoveLast();

        float newX = activeTiles.First.Value.transform.position.x - tileSize;
        last.transform.position = new Vector3(newX, 0, 0);

        activeTiles.AddFirst(last);
    }
}
