using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexColoring : MonoBehaviour
{
    Color[] vertexColors;
    Mesh mesh;
    Vector3[] vertices;

    public Color lightUpColor;
    public Color standardColor;

    public Transform player;

    Vector3 _playerPos;
    // Start is called before the first frame update
    void Start()
    {
        _playerPos = player.position;

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        vertexColors = new Color[vertices.Length];


        int randomIndex = Random.Range(0, vertices.Length);
        for (int i = 0; i < vertexColors.Length; i++)
        {
            vertexColors[i] = standardColor;
        }

        vertexColors[randomIndex] = lightUpColor;
        vertexColors[randomIndex + 1] = lightUpColor;
        
        mesh.SetColors(vertexColors);

        
    }

    private void FixedUpdate()
    {
        _playerPos = player.position;
        ChangeColor();
    }



    private void ChangeColor()
    {
        int index = FindClosestVertexIndex();
        vertexColors[index] = lightUpColor;
        mesh.SetColors(vertexColors);
    }

    private int FindClosestVertexIndex()
    {
        Vector3 playerPos = transform.InverseTransformPoint(_playerPos);
        float minDistanceSqr = Mathf.Infinity;
        Vector3 nearestVertex = Vector3.zero;
        int nearestVertexIndex = 0;
        int i = 0;
        foreach(Vector3 vertex in vertices)
        {
            Vector3 diff = playerPos - vertex;
            float distSqr = diff.sqrMagnitude;

            if(distSqr < minDistanceSqr)
            {
                minDistanceSqr = distSqr;
                nearestVertex = vertex;
                nearestVertexIndex = i;
            }
            i++;
        }

        return nearestVertexIndex;
    }


}
