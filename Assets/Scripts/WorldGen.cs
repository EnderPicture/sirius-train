using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public GameObject Station;


    float ChunkWidth = 100;

    int Style = 0;

    Chunk[] Chunks = new Chunk[3];
    public Transform Locator;

    Color[] scheme1 = {
        new Color(0.427451f,0.235294f,0.145098f),
        new Color(0.694118f,0.529412f,0.372549f),
        new Color(0.819608f,0.572549f,0.329412f),
        new Color(0.960784f,0.584314f,0.231373f),
        new Color(0.964706f,0.478431f,0.141176f),
    };


    private void Start()
    {
        GameObject Terrain = new GameObject();
        Terrain.transform.parent = transform;
        Terrain.name = "Terrain";
        for (int i = 0; i < Chunks.Length; i++)
        {
            Chunks[i] = new Chunk(Terrain, ChunkWidth, scheme1);
        }
    }

    private void Update()
    {
        int posIndex = Mathf.RoundToInt((Locator.transform.position.x - ChunkWidth / 2) / ChunkWidth);

        int maxPosIndex = MaxChunkIndex(Chunks).PosIndex;
        if (posIndex >= maxPosIndex)
        {
            Chunks[MinChunkIndex(Chunks).ArrayIndex].setPosIndex(maxPosIndex + 1);
        }

        int minPosIndex = MinChunkIndex(Chunks).PosIndex;
        if (posIndex <= minPosIndex)
        {
            Chunks[MaxChunkIndex(Chunks).ArrayIndex].setPosIndex(minPosIndex - 1);
        }
    }

    ChunkLoc MinChunkIndex(Chunk[] chunks)
    {
        int minPosIndex = int.MaxValue;
        int minIndex = -1;
        for (int i = 0; i < chunks.Length; i++)
        {
            int cPosIndex = chunks[i].GetCurrentIndex();
            if (cPosIndex < minPosIndex)
            {
                minPosIndex = cPosIndex;
                minIndex = i;
            }
        }
        return new ChunkLoc(minPosIndex, minIndex);
    }
    ChunkLoc MaxChunkIndex(Chunk[] chunks)
    {
        int maxPosIndex = int.MinValue;
        int maxIndex = -1;
        for (int i = 0; i < chunks.Length; i++)
        {
            int cPosIndex = chunks[i].GetCurrentIndex();
            if (cPosIndex > maxPosIndex)
            {
                maxPosIndex = cPosIndex;
                maxIndex = i;
            }
        }
        return new ChunkLoc(maxPosIndex, maxIndex);
    }

    class ChunkLoc
    {
        public int PosIndex;
        public int ArrayIndex;
        public ChunkLoc(int posIndex, int arrayIndex)
        {
            PosIndex = posIndex;
            ArrayIndex = arrayIndex;
        }
    }

    class Chunk
    {
        GameObject[] Trees;

        GameObject Con;
        GameObject TreesLayer;

        float TreesDepth = 10;

        float MaxBrightness = 100;

        int CurrentIndex = 0;

        Color Color;

        float Width;
        public Chunk(GameObject parent, float width, Color[] color)
        {
            Width = width;

            Color = color[0];

            // main container for the whole chunk
            Con = new GameObject();
            Con.transform.position = new Vector3(0, 0, 0);
            Con.transform.parent = parent.transform;
            Con.name = "Chunk";

            // trees layer inside the chunk
            TreesLayer = new GameObject();
            TreesLayer.transform.parent = Con.transform;
            TreesLayer.transform.position = new Vector3(0, 0, TreesDepth);
            TreesLayer.name = "Trees";

            Object[] loadedTrees = Resources.LoadAll("Trees", typeof(GameObject));
            int duplicateAmount = 5;
            Trees = new GameObject[loadedTrees.Length * duplicateAmount];
            for (int i = 0; i < loadedTrees.Length; i++)
            {
                for (int j = 0; j < duplicateAmount; j++)
                {
                    GameObject newTree = Object.Instantiate((GameObject)loadedTrees[i]);

                    // set up tree
                    newTree.transform.parent = TreesLayer.transform;
                    newTree.transform.localPosition = new Vector3(0, 0, 0);
                    newTree.transform.localScale = new Vector3(.5f, .5f, .5f);
                    Vector3 position = newTree.transform.position;
                    position.x = Random.Range(0f, Width);
                    newTree.transform.position = position;
                    SpriteRenderer spriteRenderer = newTree.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = 5;
                    spriteRenderer.sortingLayerName = "Background";
                    spriteRenderer.color = Color;
                    Trees[i * duplicateAmount + j] = newTree;
                }
            }

            setPosIndex(0);
        }

        public void setPosIndex(int x)
        {
            CurrentIndex = x;
            Vector3 position = Con.transform.position;
            position.x = CurrentIndex * Width;
            Con.transform.position = position;

            foreach (GameObject tree in Trees)
            {
                float random = Random.Range(0f, Mathf.PerlinNoise(tree.transform.position.x, 0));
                if (random > .3f)
                {
                    Vector3 treePos = tree.transform.localPosition;
                    treePos.y = Mathf.PerlinNoise(tree.transform.position.x,0);
                    treePos.z = Mathf.Round(Mathf.PerlinNoise(tree.transform.position.x,1000)*5);
                    tree.transform.localPosition = treePos;

                    SpriteRenderer spriteRenderer = tree.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = -(int)treePos.z;


                    tree.SetActive(true);
                } else {
                    tree.SetActive(false);
                }
            }
        }

        public int GetCurrentIndex()
        {
            return CurrentIndex;
        }
    }


    public void GenStation(int numberOfStations, int mode, Train train)
    {
        /*
        mode 1 = short
        mode 2 = long
        mode 3 = random short long
        */

        GameObject stationCon = new GameObject();
        stationCon.transform.SetParent(transform);
        stationCon.name = "Stations";

        List<EndTrainStation> stations = new List<EndTrainStation>();

        float loc = 0;
        for (int c = 0; c < numberOfStations; c++)
        {
            GameObject newStation = Instantiate(Station);
            newStation.transform.SetParent(stationCon.transform);

            Vector3 pos = newStation.transform.localPosition;
            pos.x = loc;

            newStation.transform.localPosition = pos;

            stations.Add(newStation.GetComponent<EndTrainStation>());

            if (mode == 1)
            {
                loc += Random.Range(Config.ShortMinDisStation, Config.ShortMaxDisStation);
            }

            switch (mode)
            {
                case 1:
                    loc += Random.Range(Config.ShortMinDisStation, Config.ShortMaxDisStation);
                    break;
                case 2:
                    loc += Random.Range(Config.LongMinDisStation, Config.LongMaxDisStation);
                    break;
                case 3:
                    loc += Random.Range(Config.ShortMinDisStation, Config.LongMaxDisStation);
                    break;
                default:
                    loc += Random.Range(Config.ShortMinDisStation, Config.ShortMaxDisStation);
                    break;
            }
        }

        train.SetupStations(stations);

    }

    public void SetStyle(int style)
    {
        Style = style;
    }
}
