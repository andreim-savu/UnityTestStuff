using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public static GridController Instance;

    [SerializeField] Cube cubeObject;

    private Dictionary<Vector2, Cube> cubes;

    [SerializeField] int width = 15;
    [SerializeField] int height = 15;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateGrid();

        foreach (KeyValuePair<Vector2, Cube> kvPair in cubes)
        {
            kvPair.Value.SetNeighbours(kvPair.Key);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<Vector2, Cube> kvPair in cubes)
        {
            float y = Mathf.PerlinNoise((kvPair.Key.x + Time.time) / 10.0f, (kvPair.Key.y + Time.time) / 10.0f) * 5;
            kvPair.Value.transform.position = new Vector3(kvPair.Key.x, y, kvPair.Key.y);
        }
    }

    void CreateGrid()
    {
        cubes = new Dictionary<Vector2, Cube>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                float y = Mathf.PerlinNoise(i/10.0f, j/10.0f) * 5;
                print(y);
                Vector3 pos = new Vector3(i, y, j);

                Cube newCube = Instantiate(cubeObject, transform);
                newCube.transform.position = pos;

                cubes.Add(new Vector2(j, i), newCube);
            }
        }
    }

    public Cube GetCube(Vector2 pos)
    {
        if (cubes.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        else
        {
            return null;
        }
    }
}
