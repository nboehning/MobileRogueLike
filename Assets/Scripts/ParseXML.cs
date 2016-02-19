using UnityEngine;
using System.Collections;
using System.Xml;

public class ParseXML : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        TextAsset xmlFile = Resources.Load("CDCatalog") as TextAsset;

        XmlDocument xmlDoc = new XmlDocument();
        // 3 types of reading XML docs
            // XmlDocument - entire document
            // XmlTextReader - doesn't cache, forward moving
            // XmlReader - does cache, forward moving

        xmlDoc.LoadXml(xmlFile.text);

        //Debug.Log(xmlDoc.DocumentElement.Name);                     // Gets the name of the root element (DocumentElement)
        //Debug.Log(xmlDoc.DocumentElement.GetAttribute("title"));     // Pull attribute from the root element (DocumentElement)
        //Debug.Log(xmlDoc.DocumentElement.FirstChild.Name);
        //Debug.Log(xmlDoc.DocumentElement.FirstChild.FirstChild.InnerText);


        //Debug.Log(xmlDoc.DocumentElement

        foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
        {
            if (XmlConvert.ToInt32(node.LastChild.InnerText) > 2000)
            {
                Debug.Log("SKIPPED");
            }
            else if(XmlConvert.ToInt32(node.LastChild.InnerText) > 1990)
            {
                Debug.Log("1990 song");
                foreach(XmlNode node2 in node.ChildNodes)
                {
                    if(node2.Name == "TITLE")
                    {
                        Debug.Log("title: " + node2.InnerText);
                    }
                    else if(node2.Name == "ARTIST")
                    {
                        Debug.Log("artist: " + node2.InnerText);
                    }
                    else if (node2.Name == "COUNTRY")
                    {
                        Debug.Log("country: " + node2.InnerText);
                    }
                    else if (node2.Name == "COMPANY")
                    {
                        Debug.Log("company: " + node2.InnerText);
                    }
                }
            }
            else if (XmlConvert.ToInt32(node.LastChild.InnerText) > 1980)
            {
                Debug.Log("1980 song");
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (node2.Name == "TITLE")
                    {
                        Debug.Log("title: " + node2.InnerText);
                    }
                    else if (node2.Name == "ARTIST")
                    {
                        Debug.Log("artist: " + node2.InnerText);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}