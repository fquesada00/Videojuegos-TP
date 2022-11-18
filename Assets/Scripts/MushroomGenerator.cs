using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class MushroomGenerator : MonoBehaviour
{

    [SerializeField] private GameObject[] _mushroomPrefabs;
    [SerializeField] private int _mushroomCount;

    public bool autoUpdate = false;

    [SerializeField] private int _seed;

    [SerializeField] private Vector2 heightRange = new Vector2(110, 400);

    public void generate()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;
        
        //get terrain bounds
        Vector3 terrainPos = terrain.transform.position;
        float terrainWidth = terrainData.size.x;
        float terrainLength = terrainData.size.z;

        //Destroy Mushrooms

        GameObject parent = GameObject.Find("MushroomsGenerator");
        if (parent != null)
        {
            DestroyImmediate(parent);
        }
        //create parent
        parent = new GameObject("MushroomsGenerator");
        parent.transform.parent = transform;

        //set seed
        Random.InitState(_seed);

        //create mushrooms
        for (int i = 0; i < _mushroomCount; i++)
        {
            //get random prefab
            GameObject mushroomPrefab = _mushroomPrefabs[Random.Range(0, _mushroomPrefabs.Length)];

            //get random position
            float x,y,z;
            Vector3 pos;

            do{
                x = Random.Range(0, terrainWidth);
                z = Random.Range(0, terrainLength);
                y = terrain.SampleHeight(new Vector3(x, 0, z));
                pos = new Vector3(x, y, z) + terrainPos;
            } while (y < heightRange.x || y > heightRange.y);



            //get rotation normal to terrain
            Vector3 normal = terrainData.GetInterpolatedNormal(x / terrainWidth, z / terrainLength);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, normal);


            GameObject mushroom = Instantiate(mushroomPrefab, pos, rot);
            mushroom.transform.parent = parent.transform;

            //set static
            mushroom.isStatic = true;
            
        }

    }
}
