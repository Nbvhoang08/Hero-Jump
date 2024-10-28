using UnityEngine;
using System.Collections.Generic;



namespace Script
{
    [CreateAssetMenu(fileName = "DataGame", menuName = "ScriptableObjects/DataGame", order = 1)]
    public class DataGame : ScriptableObject
    {
        public List<GameObject> characterPrefabs;
        public GameObject selectedCharacter;
        public List<Sprite> terrainSprites;
        public List<int> characterPrices; // Giá của từng nhân vật
        public Sprite selectedTerrain;
        private void OnEnable()
        {
            if (characterPrefabs != null && characterPrefabs.Count > 0)
            {
                selectedCharacter = characterPrefabs[0];
            }
            if (terrainSprites.Count > 0 && terrainSprites != null)
            {
                selectedTerrain = terrainSprites[0];
            }
        }
    }
}