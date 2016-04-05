using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class LoadTiles : MonoBehaviour {

    // Holds the .xml file
    public TextAsset[] mapInformation;

    // Array of the tiles from the tileset
    private Sprite[] sprites;

    private int spawnGID;
    private int exitGID;
     
    public int curMap;
    public Camera camera;
    GameObject tileParent;

    XmlDocument xmlDoc;

    // Use this for initialization
    void Start()
    {
        tileParent = GameObject.Find("TileParent");
        //LoadMap(Random.Range(0, mapInformation.Length));
        LoadMap(1);
    }

    void LoadMap(int mapNumber)
    {

        xmlDoc = new XmlDocument();

        switch (mapNumber)
        {
            case 0:
                curMap = 0;
                xmlDoc.LoadXml(mapInformation[0].text);
                break;
            case 1:
                // Load the tileset into the sprites array
                sprites = Resources.LoadAll<Sprite>("OutdoorTileset");
                curMap = 1;
                gameObject.transform.localPosition = new Vector3(2.242f, 1.438f, -9.93f);
                camera.orthographicSize = 1.6f;
                xmlDoc.LoadXml(mapInformation[1].text);
                break;
            case 2:
                curMap = 2;
                break;
        }


        // Maneuver the camera
        Camera.main.transform.position = new Vector3(0, 0f, -10f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        XmlNodeList layerNames = xmlDoc.GetElementsByTagName("layer");

        // For each layer that exists
        foreach (XmlNode layerInfo in layerNames)
        {
            switch (layerInfo.Attributes["name"].Value)
            {
                case "Obstacles":
                    ParseXml(layerInfo, true);
                    break;
                case "Interactive":
                    ParseXml(layerInfo, false, true);
                    break;
                default:
                    ParseXml(layerInfo);
                    break;
            }
        }
    }
    void ParseXml(XmlNode layerInfo, bool isObstacle = false, bool isInteractive = false)
    {
        // Get/Set tile width and height
        XmlNode tilesetInfo = xmlDoc.SelectSingleNode("map").SelectSingleNode("tileset");

        float tileWidth = int.Parse(tilesetInfo.Attributes["tilewidth"].Value) / (float)100;
        float tileHeight = int.Parse(tilesetInfo.Attributes["tileheight"].Value) / (float)100;

        // Pull out the data node
        XmlNode tempNode = layerInfo.SelectSingleNode("data");

        int layerWidth = int.Parse(layerInfo.Attributes["width"].Value);
        int layerHeight = int.Parse(layerInfo.Attributes["height"].Value);

        int mapLocationVertical, mapLocationHorizontal;
        mapLocationVertical = layerHeight - 1;
        mapLocationHorizontal = 0;


        foreach (XmlNode tile in tempNode.SelectNodes("tile"))
        {
            int spriteValue = int.Parse(tile.Attributes["gid"].Value);

            if (spriteValue > 0)
            {
                GameObject tempSprite;

                // Create a temp gameobject and add a sprite renderer to it
                if(isInteractive)
                    tempSprite = new GameObject("InteractiveTile");
                else if (isObstacle)
                    tempSprite = new GameObject("ObstacleTile");
                else
                    tempSprite = new GameObject("GenericTile");

                tempSprite.transform.parent = tileParent.transform;

                SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();

                // Add components to make it a trigger
                if (isInteractive)
                {
                    tempSprite.AddComponent<BoxCollider2D>();
                    tempSprite.GetComponent<BoxCollider2D>().size = new Vector2(tileWidth, tileHeight);
                    tempSprite.GetComponent<BoxCollider2D>().isTrigger = true;
                    
                }
                // Add components to make tile a wall
                else if (isObstacle)
                {
                    tempSprite.AddComponent<BoxCollider2D>();
                    tempSprite.GetComponent<BoxCollider2D>().size = new Vector2(tileWidth, tileHeight);
                    tempSprite.AddComponent<Rigidbody2D>();
                    tempSprite.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    tempSprite.GetComponent<Rigidbody2D>().gravityScale = 0;
                }

                // Set the different values needed for the renderer
                renderer.sprite = sprites[spriteValue - 1];
                renderer.sortingLayerName = layerInfo.Attributes["name"].Value;

                // Determine the location that the tile will be at
                float locationX = tileWidth * mapLocationHorizontal;
                float locationY = tileHeight * mapLocationVertical;

                Vector3 newLocation = new Vector3(locationX, locationY);

                // Set the position of the game object to the determined location
                tempSprite.transform.position = newLocation;
            }

            // Increment our location along the horizontal
            mapLocationHorizontal++;

            // Determine if we are at last tile of the row
            if (mapLocationHorizontal % layerWidth == 0)
            {
                // Increase the vertical location
                mapLocationVertical--;
                // Reset the horizontal location
                mapLocationHorizontal = 0;
            }
        }
    }
}