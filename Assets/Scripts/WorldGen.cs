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

    public Color[] scheme1 = {
        new Color(0.213726f,0.117647f,0.072549f),
        new Color(0.347059f,0.264706f,0.186275f),
        new Color(0.409804f,0.286275f,0.164706f),
        new Color(0.480392f,0.292157f,0.115687f),
        new Color(0.482353f,0.239216f,0.070588f),
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
        GameObject Con;

        GameObject[] Trees;
        GameObject TreesLayer;
        float[] TreesDepth = { 20, 40 };

        GameObject[] Mountains;
        GameObject MountainsLayer;
        float[] MountainsDepth = { 50, 100 };

        GameObject[] Hills;
        GameObject HillsLayer;
        float[] HillsDepth = { 10, 19 };

        GameObject[] HillsFront;
        GameObject HillsFrontLayer;
        float[] HillsFrontDepth = { -.5f, -1.5f };




        float MaxBrightness = 100;

        int CurrentIndex = 0;

        Color[] ColorScheme;

        float Width;
        public Chunk(GameObject parent, float width, Color[] color)
        {
            Width = width;

            ColorScheme = color;

            // main container for the whole chunk
            Con = new GameObject();
            Con.transform.position = new Vector3(0, 0, 0);
            Con.transform.parent = parent.transform;
            Con.name = "Chunk";

            // trees layer inside the chunk
            TreesLayer = new GameObject();
            TreesLayer.transform.parent = Con.transform;
            TreesLayer.transform.position = new Vector3(0, 1.3f, TreesDepth[0]);
            TreesLayer.name = "Trees";
            
            MountainsLayer = new GameObject();
            MountainsLayer.transform.parent = Con.transform;
            MountainsLayer.transform.position = new Vector3(0, 0, MountainsDepth[0]);
            MountainsLayer.name = "Mountains";

            HillsLayer = new GameObject();
            HillsLayer.transform.parent = Con.transform;
            HillsLayer.transform.position = new Vector3(0, -3.69f, HillsDepth[0]);
            HillsLayer.name = "Hills";

            HillsFrontLayer = new GameObject();
            HillsFrontLayer.transform.parent = Con.transform;
            HillsFrontLayer.transform.position = new Vector3(0, -4.75f, HillsFrontDepth[0]);
            HillsFrontLayer.name = "HillsFront";

            Mountains = LoadSprites("Mountains/LessCalm", 10, MountainsLayer, ColorScheme[3], new Vector3(3,1,1), "Background");
            Trees = LoadSprites("Trees", 5, TreesLayer, ColorScheme[2], new Vector3(.5f,.5f,.5f), "Background");
            Hills = LoadSprites("Mountains/Calm", 10, HillsLayer, ColorScheme[1], new Vector3(3,1,1), "Background");
            HillsFront = LoadSprites("Mountains/Calm", 10, HillsFrontLayer, ColorScheme[0], new Vector3(3,1,1), "Foreground");

            setPosIndex(0);
        }

        public void setPosIndex(int x)
        {
            CurrentIndex = x;
            Vector3 position = Con.transform.position;
            position.x = CurrentIndex * Width;
            Con.transform.position = position;

            RandomSpriteMix(Mountains, MountainsDepth, 3, 0);
            RandomSpriteMix(Trees, TreesDepth, 0, .3f);
            RandomSpriteMix(Hills, HillsDepth, 0, 0);
            RandomSpriteMix(HillsFront, HillsFrontDepth, 0, 0);
        }

        void RandomSpriteMix(GameObject[] sprites, float[] depth, float HeightDeltaMultiplier, float randomChance)
        {
            foreach (GameObject sprite in sprites)
            {
                float random = Random.Range(0f, Mathf.PerlinNoise(sprite.transform.position.x, 0));
                if (random > randomChance)
                {
                    Vector3 spritePos = sprite.transform.localPosition;
                    spritePos.y = Mathf.PerlinNoise(sprite.transform.position.x, 0) * HeightDeltaMultiplier;
                    spritePos.z = Mathf.Round(Mathf.PerlinNoise(sprite.transform.position.x, 1000) * (depth[1] - depth[0]));
                    sprite.transform.localPosition = spritePos;

                    SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingOrder = -(int)sprite.transform.position.z;
                    sprite.SetActive(true);
                }
                else
                {
                    sprite.SetActive(false);
                }
            }

        }


        public GameObject[] LoadSprites(string path, int duplicateAmount, GameObject parent, Color color, Vector3 scale, string layer)
        {
            Object[] loadedSprites = Resources.LoadAll(path, typeof(GameObject));

            GameObject[] sprites = new GameObject[loadedSprites.Length * duplicateAmount];
            for (int i = 0; i < loadedSprites.Length; i++)
            {
                for (int j = 0; j < duplicateAmount; j++)
                {
                    GameObject newTree = Object.Instantiate((GameObject)loadedSprites[i]);

                    // set up sprites
                    newTree.transform.parent = parent.transform;
                    newTree.transform.localPosition = new Vector3(0, 0, 0);
                    newTree.transform.localScale = scale;
                    Vector3 position = newTree.transform.position;
                    position.x = Random.Range(0f, Width);
                    newTree.transform.position = position;
                    SpriteRenderer spriteRenderer = newTree.GetComponent<SpriteRenderer>();
                    spriteRenderer.sortingLayerName = layer;
                    spriteRenderer.color = color;
                    sprites[i * duplicateAmount + j] = newTree;
                }
            }
            return sprites;
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
