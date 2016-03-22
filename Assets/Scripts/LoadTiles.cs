using UnityEngine;
using System.Collections;
using System.Xml;

public class LoadTiles : MonoBehaviour {

    // Holds the .xml file
    public TextAsset[] mapInformation;

    // A temporary tile object
    public GameObject tempCube;

    // Array of the tiles from the tileset
    private Sprite[] sprites;

    public int numMaps;

    GameObject tileParent;

    // Use this for initialization
    void Start()
    {
        tileParent = GameObject.Find("TileParent");
        LoadMap(Random.Range(0, mapInformation.Length));
    }

    void LoadMap(int mapNumber)
    {
        // Load the tileset into the sprites array
        sprites = Resources.LoadAll<Sprite>("roguelikeSheet_transparent");

        XmlDocument xmlDoc = new XmlDocument();

        switch(mapNumber)
        {
            case 0:
                xmlDoc.LoadXml(mapInformation[0].text);
                break;
            case 1:
                xmlDoc.LoadXml(mapInformation[1].text);
                break;
        }
        

        // Maneuver the camera
        Camera.main.transform.position = new Vector3(0, 0f, -10f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        XmlNodeList layerNames = xmlDoc.GetElementsByTagName("layer");
        Debug.Log(layerNames.Count);

        XmlNode tilesetInfo = xmlDoc.SelectSingleNode("map").SelectSingleNode("tileset");
        
        float tileWidth = int.Parse(tilesetInfo.Attributes["tilewidth"].Value) / (float)100;
        float tileHeight = int.Parse(tilesetInfo.Attributes["tileheight"].Value) / (float)100;
       
        // For each layer that exists
        foreach (XmlNode layerInfo in layerNames)
        {
            int layerWidth = int.Parse(layerInfo.Attributes["width"].Value);
            int layerHeight = int.Parse(layerInfo.Attributes["height"].Value);

            if(layerInfo.Attributes["name"].Value == "Background")
            {
                // Pull out the data node
                XmlNode tempNode = layerInfo.SelectSingleNode("data");

                int mapLocationVertical, mapLocationHorizontal;
                mapLocationVertical = layerHeight - 1;
                mapLocationHorizontal = 0;

                foreach (XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    if(spriteValue > 0)
                    {
                        // Create a temp gameobject and add a sprite renderer to it
                        GameObject tempSprite = new GameObject("TestBackground");
                        SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();

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
                    if(mapLocationHorizontal % layerWidth == 0)
                    {
                        // Increase the vertical location
                        mapLocationVertical--;
                        // Reset the horizontal location
                        mapLocationHorizontal = 0;
                    }
                }
            }
            else if(layerInfo.Attributes["name"].Value == "Obstacles")
            {
                // Pull out the data node
                XmlNode tempNode = layerInfo.SelectSingleNode("data");

                int mapLocationVertical, mapLocationHorizontal;
                mapLocationVertical = layerHeight - 1;
                mapLocationHorizontal = 0;

                foreach (XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    if (spriteValue > 0)
                    {
                        // Create a temp gameobject and add a sprite renderer to it
                        GameObject tempSprite = new GameObject("TestObstacles");
                        SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();
                        tempSprite.AddComponent<BoxCollider2D>();
                        tempSprite.GetComponent<BoxCollider2D>().size = new Vector2(tileWidth, tileHeight);
                        tempSprite.AddComponent<Rigidbody2D>();
                        tempSprite.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                        tempSprite.GetComponent<Rigidbody2D>().gravityScale = 0;

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
            else if(layerInfo.Attributes["name"].Value == "Interactive")
            {
                // Pull out the data node
                XmlNode tempNode = layerInfo.SelectSingleNode("data");

                int mapLocationVertical, mapLocationHorizontal;
                mapLocationVertical = layerHeight - 1;
                mapLocationHorizontal = 0;

                foreach (XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    if (spriteValue > 0)
                    {
                        // Create a temp gameobject and add a sprite renderer to it
                        GameObject tempSprite = new GameObject("TestInteractive");
                        SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();
                        tempSprite.AddComponent<BoxCollider2D>();

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
            else if(layerInfo.Attributes["name"].Value == "Foreground")
            {
                // Pull out the data node
                XmlNode tempNode = layerInfo.SelectSingleNode("data");

                int mapLocationVertical, mapLocationHorizontal;
                mapLocationVertical = layerHeight - 1;
                mapLocationHorizontal = 0;

                foreach (XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    if (spriteValue > 0)
                    {
                        // Create a temp gameobject and add a sprite renderer to it
                        GameObject tempSprite = new GameObject("TestForeground");
                        SpriteRenderer renderer = tempSprite.AddComponent<SpriteRenderer>();

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
	}

    void ParseXml()
    {
        
    }
}