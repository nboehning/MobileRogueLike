using UnityEngine;
using System.Collections;
using System.Xml;

public class LoadTiles : MonoBehaviour {

    // Holds the .xml file
    public TextAsset mapInformation;

    // A temporary tile object
    public GameObject tempCube;

    // Array of the tiles from the tileset
    private Sprite[] sprites;

    // Use this for initialization
	void Start ()
    {
        // Load the tileset into the sprites array
        sprites = Resources.LoadAll<Sprite>("roguelikeSheet_transparent");
        Debug.Log(sprites.Length);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(mapInformation.text);

        // Maneuver the camera
        Camera.main.transform.position = new Vector3(9.9f, -9.9f, 10f);
        Camera.main.transform.rotation = Quaternion.Euler(0f, 180f, 180f);

        XmlNodeList layerNames = xmlDoc.GetElementsByTagName("layer");
        Debug.Log(layerNames.Count);

        XmlNode tilesetInfo = xmlDoc.SelectSingleNode("map").SelectSingleNode("tileset");
        
        float tileWidth = int.Parse(tilesetInfo.Attributes["tilewidth"].Value) / (float)100;
        float tileHeight = int.Parse(tilesetInfo.Attributes["tileheight"].Value) / (float)100;

        //float tileWidth = int.Parse(tilesetInfo.Attributes["tilewidth"].Value);
        //float tileHeight = int.Parse(tilesetInfo.Attributes["tileheight"].Value);

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
                mapLocationVertical = mapLocationHorizontal = 0;

                foreach(XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    if(spriteValue > 0)
                    {
                        // Create a temp gameobject and add a sprite renderer to it
                        GameObject tempSprite = new GameObject("Test");
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
                        mapLocationVertical++;
                        // Reset the horizontal location
                        mapLocationHorizontal = 0;
                    }
                }
            }
            else if(layerInfo.Attributes["name"].Value == "Obstacles")
            {

            }
            else if(layerInfo.Attributes["name"].Value == "Interactive")
            {

            }
            else if(layerInfo.Attributes["name"].Value == "Foreground")
            {
                // Pull out the data node
                XmlNode tempNode = layerInfo.SelectSingleNode("data");

                int mapLocationVertical, mapLocationHorizontal;
                mapLocationVertical = mapLocationHorizontal = 0;

                foreach (XmlNode tile in tempNode.SelectNodes("tile"))
                {
                    int spriteValue = int.Parse(tile.Attributes["gid"].Value);

                    if (spriteValue > 0)
                    {
                        // Create a temp gameobject and add a sprite renderer to it
                        GameObject tempSprite = new GameObject("Test");
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
                        mapLocationVertical++;
                        // Reset the horizontal location
                        mapLocationHorizontal = 0;
                    }
                }
            }
        }
	}
}