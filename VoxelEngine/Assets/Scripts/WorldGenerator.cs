using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Terrain Settings")] public int sizeX;
    public int sizeZ;
    public int heightLevel;
    public float terrainDetail;
    public float terrainHeight;

    [SerializeField] private int _treeCount = 4;

    [Header("Prefabs")] [SerializeField] private List<GameObject> _spawnBlocks;
    [SerializeField] private GameObject[] _blockPrefabs;
    [SerializeField] private GameObject _playerPrefab;

    public static Transform ParentTransform;
    private int _seed;

    private void Awake()
    {
        InitializeSeed();
        ParentTransform = transform;
    }

    private void Start()
    {
        GenerateTerrain();
    }

    private void InitializeSeed()
    {
        _seed = Random.Range(100000, 999999);
    }

    private void GenerateTerrain()
    {
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                int maxY = CalculateHeight(x, z) + heightLevel;
                GenerateBlock(x, maxY, z, _blockPrefabs[0]);

                if (_treeCount > 0 && Random.Range(0, 100f) <= 1f)
                {
                    TreeGenerate(x, maxY, z);
                    _treeCount--;
                }


                if (sizeX / 2 == x && sizeZ / 2 == z)
                    SpawnPlayer(x, maxY + _playerPrefab.transform.localScale.y, z);

                for (int y = 0; y < maxY; y++)
                {
                    GenerateSubsurfaceBlock(x, y, z, maxY);
                }
            }
        }
    }

    private int CalculateHeight(int x, int z)
    {
        return (int)(Mathf.PerlinNoise((x / 2f + _seed) / terrainDetail, (z / 2f + _seed) / terrainDetail) *
                     terrainHeight);
    }

    private void GenerateBlock(int x, int y, int z, GameObject blockPrefab)
    {
        GameObject block = Instantiate(blockPrefab, new Vector3(x, y, z), Quaternion.identity);
        block.transform.parent = ParentTransform;

        _spawnBlocks.Add(block);
    }

    private void GenerateSubsurfaceBlock(int x, int y, int z, int maxY)
    {
        int dirtLayerThickness = Random.Range(1, 5);
        GameObject blockPrefab = (y >= maxY - dirtLayerThickness) ? _blockPrefabs[2] : _blockPrefabs[1];
        GenerateBlock(x, y, z, blockPrefab);
    }

    private void SpawnPlayer(int x, float y, int z)
    {
        var player = Instantiate(_playerPrefab, new Vector3(x, y, z), Quaternion.identity);
    }

    private void TreeGenerate(int x, int y, int z)
    {
        GameObject tree = new()
        {
            transform =
            {
                parent = ParentTransform
            },
            name = "Tree"
        };

        // Logs
        int height = Random.Range(4, 7);
        for (int i = 0; i < height; i++)
        {
            GameObject wood = Instantiate(_blockPrefabs[3], new Vector3(x, y + i, z), Quaternion.identity);
            wood.transform.parent = ParentTransform;
        }

        // Leaves
        float radius = ((float)height / 4) * 2;
        Vector3 center = new(x, y + height - 1, z);
        for (int i = -(int)radius; i < radius; i++)
        {
            for (int j = -(int)radius; j < radius; j++)
            {
                for (int k = -(int)radius; k < radius; k++)
                {
                    Vector3 position = new(i + center.x, j + center.y, k + center.z);

                    float distance = Vector3.Distance(center, position);

                    if (distance < radius)
                    {
                        if (!Physics.CheckBox(position, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity))
                        {
                            GameObject leaf = Instantiate(_blockPrefabs[4], position, Quaternion.identity);
                            leaf.transform.parent = ParentTransform;
                        }
                    }
                }
            }
        }
    }
}