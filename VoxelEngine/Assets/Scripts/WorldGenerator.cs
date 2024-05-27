using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public int sizeX;
    public int sizeZ;

    public float terDetail;
    public float terHeight;
    public int seed;

    [SerializeField] private List<GameObject> _spawnBlocks;
    [SerializeField] private GameObject[] _blocks;

    private Transform _parentTransform;

    private void Awake()
    {
        seed = Random.Range(100000, 999999);

        _parentTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                int maxY = (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);

                GameObject grass = Instantiate(_blocks[0], new Vector3(x, maxY, z), Quaternion.identity);
                grass.transform.parent = _parentTransform;

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayer = Random.Range(1, 5);
                    if (y >= maxY - dirtLayer)
                    {
                        GameObject dirt = Instantiate(_blocks[2], new Vector3(x, y, z), Quaternion.identity);
                        dirt.transform.parent = _parentTransform;
                    }
                    else
                    {
                        GameObject stone = Instantiate(_blocks[1], new Vector3(x, y, z), Quaternion.identity);
                        stone.transform.parent = _parentTransform;
                    }
                }
            }
        }
    }
}