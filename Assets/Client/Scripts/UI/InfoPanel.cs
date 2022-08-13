using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/// <summary>
/// InfoPanel is a more detailed panel for tile,building and entity information
/// </summary>
public class InfoPanel : MonoBehaviour
{
    public enum ContentType
    {
        Tile,
        Building,
        Entity,
    }

    public ContentType? CurrentType { get; private set; } = null;
    [HideInInspector] public Tilemap map;
    public List<ContentPanel> contens;

    #region Resource
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    #endregion

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("GameMap").GetComponent<Tilemap>();
    }

    public void HideAll()
    {
        contens.ForEach(x => x.gameObject.SetActive(false));
    }

    public void Load(object obj)
    {
        if(obj.GetType() == typeof(Vector3Int))
        {
            CurrentType = ContentType.Tile;
            HideAll();

            Vector3Int pos = (Vector3Int)obj;
            if (!map.HasTile(pos)) return;

            Tile tile = map.GetTile<Tile>(pos);
            TileDefiniton definition = DefinitionRegistry.Instance.Find(map.GetTileName(pos));

            icon.sprite = tile.sprite;
            title.text = definition.tileName;
            description.text = definition.description;

            contens.Find(x => x.type == CurrentType).gameObject.SetActive(true);
            contens.Find(x => x.type == CurrentType).Load(obj);
        }
        else if(obj is AbstractBuilding)
        {
            CurrentType = ContentType.Building;
            HideAll();
            contens.Find(x => x.type == CurrentType).gameObject.SetActive(true);
            contens.Find(x => x.type == CurrentType).Load(obj);
        }
        else
        {
            //TODO: Entity
        }
    }

}
