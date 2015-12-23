using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour {
    public Terrain terrain;
	// Use this for initialization
	void Start () {
        generateTerrain();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void generateTerrain()
    {
        //        int size = 512;
        float sampleArea = 128f;
        TerrainData td = terrain.terrainData;
        float[,] heights = new float[td.heightmapWidth, td.heightmapHeight];
        for(int x = 0; x < td.heightmapWidth; x++)
        {
            for(int y = 0; y < td.heightmapHeight; y++)
            {
                float val = ((Mathf.PerlinNoise(x / (sampleArea*4), y / (sampleArea*4))*10) + (Mathf.PerlinNoise(x / sampleArea, y / sampleArea))) / 11;
                if(x < 5)
                {
                    Debug.Log(val);
                }
                heights[x, y] = val;
//                heights[x, y] = Mathf.Sqrt(Mathf.PerlinNoise(x / sampleArea, y / sampleArea));
            }
        }
        td.SetHeights(0, 0, heights);
        Debug.Log("terrain regenerated");
//        td.SetHeightsDelayLOD(64, 64, heights);
    }
}
