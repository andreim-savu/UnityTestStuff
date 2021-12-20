using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Cube : MonoBehaviour
{
    bool moving = false;
    public List<Cube> neighbours = new List<Cube>();

    [SerializeField] Color bottom;
    [SerializeField] Color top;

    Renderer r;

    private void Awake()
    {
        r = GetComponent<Renderer>();
    }

    private void OnMouseDown()
    {
        MoveCube(5);
    }

    public async void MoveCube(float height)
    {
        if (moving || height <= 0) { return; }
        MoveNeighbours(height);
        float t = 0;
        moving = true;
        while (t < 2)
        {
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(0, height, Mathf.PingPong(t, 1)), transform.position.z);
            t += Time.deltaTime;
            await Task.Yield();
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        moving = false;
    }

    async void MoveNeighbours(float height)
    {
        await Task.Delay(500);
        foreach (Cube neighbour in neighbours)
        {
            neighbour.MoveCube(height);
        }
    }

    public void SetNeighbours(Vector2 pos)
    {
        Cube neighbour = GridController.Instance.GetCube(pos + new Vector2(1, 0));
        if (neighbour) { neighbours.Add(neighbour); }
        neighbour = GridController.Instance.GetCube(pos + new Vector2(0, 1));
        if (neighbour) { neighbours.Add(neighbour); }
        neighbour = GridController.Instance.GetCube(pos - new Vector2(1, 0));
        if (neighbour) { neighbours.Add(neighbour); }
        neighbour = GridController.Instance.GetCube(pos - new Vector2(0, 1));
        if (neighbour) { neighbours.Add(neighbour); }
    }
}
