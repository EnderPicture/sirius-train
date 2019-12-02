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
    public GameObject Track;

    public Color[] scheme1 = {
        new Color(0.213726f,0.117647f,0.072549f),
        new Color(0.347059f,0.264706f,0.186275f),
        new Color(0.409804f,0.286275f,0.164706f),
        new Color(0.480392f,0.292157f,0.115687f),
        new Color(0.482353f,0.239216f,0.070588f),
    };
    public Color[] scheme2 = {
        new Color(0.213726f,0.117647f,0.072549f),
        new Color(0.347059f,0.264706f,0.186275f),
        new Color(0.409804f,0.286275f,0.164706f),
        new Color(0.480392f,0.292157f,0.115687f),
        new Color(0.482353f,0.239216f,0.070588f),
    };
    public Color[] scheme3 = {
        new Color(0.213726f,0.117647f,0.072549f),
        new Color(0.347059f,0.264706f,0.186275f),
        new Color(0.409804f,0.286275f,0.164706f),
        new Color(0.480392f,0.292157f,0.115687f),
        new Color(0.482353f,0.239216f,0.070588f),
    };
    public Color[] colorScheme;

    GameManager Manager;


    private void Start()
    {
        Manager = GetComponent<GameManager>();
        int part = PlayerPrefs.GetInt("Level");
        if (part == 1)
        {
            colorScheme = scheme1;
        }
        else if (part == 2)
        {
            colorScheme = scheme2;
        }
        else if (part == 3)
        {
            colorScheme = scheme3;
        }
        GameObject Terrain = new GameObject();
        Terrain.transform.parent = transform;
        Terrain.name = "Terrain";
        for (int i = 0; i < Chunks.Length; i++)
        {
            Chunks[i] = new Chunk(Terrain, ChunkWidth, colorScheme, Track, Manager.level);
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
        float[] TreesDepth = { 2, 12 };

        GameObject[] Mountains;
        GameObject MountainsLayer;
        float[] MountainsDepth = { 50, 100 };

        GameObject[] Hills;
        GameObject HillsLayer;
        float[] HillsDepth = { 13, 49 };

        GameObject[] HillsFront;
        GameObject HillsFrontLayer;
        float[] HillsFrontDepth = { -.5f, -3.5f };


        GameObject TracksLayer;


        float MaxBrightness = 100;

        int CurrentIndex = 0;

        Color[] ColorScheme;

        float Width;

        GameObject Track;

        public Chunk(GameObject parent, float width, Color[] color, GameObject track, int mode)
        {
            Track = track;
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
            TreesLayer.transform.position = new Vector3(0, -0.96f, TreesDepth[0]);
            TreesLayer.name = "Trees";

            MountainsLayer = new GameObject();
            MountainsLayer.transform.parent = Con.transform;
            MountainsLayer.transform.position = new Vector3(0, 2.48f, MountainsDepth[0]);
            MountainsLayer.name = "Mountains";

            HillsLayer = new GameObject();
            HillsLayer.transform.parent = Con.transform;
            HillsLayer.transform.position = new Vector3(0, -1.21f, HillsDepth[0]);
            HillsLayer.name = "Hills";

            HillsFrontLayer = new GameObject();
            HillsFrontLayer.transform.parent = Con.transform;
            HillsFrontLayer.transform.position = new Vector3(0, -0.27f, HillsFrontDepth[0]);
            HillsFrontLayer.name = "HillsFront";

            Mountains = LoadSprites("Mountains/LessCalm", 10, MountainsLayer, new Vector3(2, 1, 1), "Background");
            Trees = LoadSprites("Trees", 5, TreesLayer, new Vector3(0.6f, 0.6f, 0.6f), "Background");
            Hills = LoadSprites("Mountains/Calm", 10, HillsLayer, new Vector3(2, 1, 1), "Background");
            HillsFront = LoadSprites("Mountains/Calm", 10, HillsFrontLayer, new Vector3(2, 1, 1), "Foreground");


            TracksLayer = new GameObject();
            TracksLayer.transform.parent = Con.transform;
            if (mode == Config.BULLET)
            {
                TracksLayer.transform.position = new Vector3(0, -1.5849f, 0);
            }
            else if (mode == Config.DIESEL)
            {
                TracksLayer.transform.position = new Vector3(0, -1.6183f, 0);
            }
            else if (mode == Config.STEAM)
            {
                TracksLayer.transform.position = new Vector3(0, -1.677f, 0);
            }
            TracksLayer.name = "Tracks";

            for (int c = 0; c < 30; c++)
            {
                GameObject newTrack = Object.Instantiate(Track);
                newTrack.transform.parent = TracksLayer.transform;
                newTrack.transform.localScale = new Vector3(.2f, .2f, .2f);
                SpriteRenderer spriteRenderer = newTrack.GetComponent<SpriteRenderer>();
                spriteRenderer.sortingOrder = 20;
                newTrack.transform.localPosition = new Vector3(c * 4, 0, 0);
            }

            setPosIndex(0);
        }

        public void setPosIndex(int x)
        {
            CurrentIndex = x;
            Vector3 position = Con.transform.position;
            position.x = CurrentIndex * Width;
            Con.transform.position = position;

            RandomSpriteMix(Mountains, MountainsDepth, 10, .2f, ColorScheme[3]);
            RandomSpriteMix(Trees, TreesDepth, 0, .2f, ColorScheme[2]);
            RandomSpriteMix(Hills, HillsDepth, 5, .2f, ColorScheme[1]);
            RandomSpriteMix(HillsFront, HillsFrontDepth, 0, .2f, ColorScheme[0]);
        }

        void RandomSpriteMix(GameObject[] sprites, float[] depth, float HeightDeltaMultiplier, float randomChance, Color color)
        {
            foreach (GameObject sprite in sprites)
            {
                float random = Random.Range(0f, Mathf.PerlinNoise(sprite.transform.position.x, 0));
                if (random > randomChance)
                {
                    Vector3 spritePos = sprite.transform.localPosition;
                    float zRange = depth[1] - depth[0];
                    spritePos.z = Mathf.Round(Mathf.PerlinNoise(sprite.transform.position.x, 1000) * zRange);
                    spritePos.y = Mathf.PerlinNoise(sprite.transform.position.x * 10, spritePos.z) * HeightDeltaMultiplier;
                    sprite.transform.localPosition = spritePos;

                    SpriteRenderer spriteRenderer = sprite.GetComponent<SpriteRenderer>();
                    float brightness = .3f * (spritePos.z / zRange);
                    spriteRenderer.color = new Color(color.r + brightness, color.g + brightness, color.b + brightness);
                    spriteRenderer.sortingOrder = -(int)sprite.transform.position.z;
                    sprite.SetActive(true);
                }
                else
                {
                    sprite.SetActive(false);
                }
            }

        }


        public GameObject[] LoadSprites(string path, int duplicateAmount, GameObject parent, Vector3 scale, string layer)
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
