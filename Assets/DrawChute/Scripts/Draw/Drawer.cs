using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Vector3> spawnPoints = new List<Vector3>();

    [SerializeField] private GameObject meshPartPrefab;

    private void SendRaycast()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Draw(hit);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            GenerateObject();
            ClearBoard();
        }
    }

    private void ClearBoard()
    {
        _lineRenderer.positionCount = 0;
        spawnPoints.Clear();
    }

    // I could use LineRenderer's BakeMesh() method but as i saw on reference video you made it this way
    private void GenerateObject()
    {
        GameObject createdMesh = new GameObject("Mesh");
        createdMesh.transform.position = Vector3.zero;
        foreach (var spawnPoint in spawnPoints)
        {
            var meshPart = Instantiate(meshPartPrefab, spawnPoint, Quaternion.identity);
            meshPart.transform.parent = createdMesh.transform;
            meshPart.transform.position += new Vector3(0, 2, 3);
        }

        createdMesh.AddComponent<Rigidbody>();
    }

    private void Draw(RaycastHit hit)
    {
        var drawArea = hit.collider.gameObject.GetComponent<DrawArea>();
        if (!drawArea)
            return;

        Vector3 linePos = hit.point;
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, linePos);
        if (_lineRenderer.positionCount > 2)
        {
            CheckSpawn(linePos);
        }

        _lineRenderer.gameObject.SetActive(true);
    }

    private void CheckSpawn(Vector3 point)
    {
        var lrPositionCount = _lineRenderer.positionCount;
        if (Vector3.Distance(_lineRenderer.GetPosition(lrPositionCount - 2), point ) > 0.02f)
        {
            spawnPoints.Add(point);
        }
    }

    private void Update()
    {
        SendRaycast();
    }
}