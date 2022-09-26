using AssemblyCSharp.Assets.Logic.GameMap.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp.Assets.Logic.GameMap.Components
{
    public class MapGeneratorComponent : MonoBehaviour
    {
        public MapSettings mapSettings;
        public MapObjects mapObjects;

        public List<GameObject> currentUnits = new List<GameObject>();

        void Start()
        {
            MapObjectCreate();
            MapUnitCreate();
        }

        #region MAP INSTANTIATE 
        private void MapObjectCreate()
        {
            GameObject floorObject = Instantiate(mapObjects.Floor) as GameObject;
            floorObject.transform.position = Vector3.zero;
            floorObject.transform.localScale = Vector3.one * mapSettings.MapSize / 2f;
        }
        private void MapUnitCreate()
        {
            for (int i = 0; i < mapSettings.UnitCount; i++)
            {
                Vector3 unitPosition = new Vector3(
                    Random.Range(-mapSettings.MapSize / 2f, mapSettings.MapSize / 2f),
                    0,
                    Random.Range(-mapSettings.MapSize / 2f, mapSettings.MapSize / 2f)
                    );
                GameObject unitObject = Instantiate(mapObjects.Unit) as GameObject;

                unitObject.transform.position = unitPosition;

                currentUnits.Add(unitObject);
            }
        }
        #endregion
    }
}
