using System;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;
namespace Script
{
    public class ChooseCanvas : UICanvas
    {
        [SerializeField] private List<Button> selectionCharacter = new List<Button>();
        [SerializeField] private List<Button> selectionMap = new List<Button>();
        [SerializeField] private GameObject selectionBounder;
        [SerializeField] private GameObject selectionMapBounder;
        [SerializeField] private Text coinText;
        [SerializeField] private List<Text> characterPriceTexts; // Text để hiển thị giá của các nhân vật
        public DataGame characterData;

        private int coins;
        private List<bool> unlockedCharacters = new List<bool>();

        private void Start()
        {
            LoadPlayerData();
            UpdateCharacterPriceTexts();

            // Gán sự kiện click cho từng nút trong danh sách
            for (int i = 0; i < selectionCharacter.Count; i++)
            {
                int index = i; // Lưu trữ chỉ số hiện tại để sử dụng trong lambda
                selectionCharacter[i].onClick.AddListener(() => SelectCharacter(index));
            }

            for (int i = 0; i < selectionMap.Count; i++)
            {
                int index = i;
                selectionMap[i].onClick.AddListener(() => SelectMap(index));
            }
        }

        private void Update()
        {
            coinText.text  = PlayerPrefs.GetInt("Coins", 0).ToString();
            
        }

        private void SelectCharacter(int index)
        {
            if (index >= 0 && index < characterData.characterPrefabs.Count)
            {
                if (unlockedCharacters[index] || coins >= characterData.characterPrices[index])
                {
                    if (!unlockedCharacters[index])
                    {
                        coins -= characterData.characterPrices[index];
                        unlockedCharacters[index] = true;
                        SavePlayerData();
                        UpdateCharacterPriceTexts();
                    }

                    characterData.selectedCharacter = characterData.characterPrefabs[index];
                    Debug.Log("Selected character: " + characterData.selectedCharacter.name);
                    MoveSelectionBounder(selectionCharacter[index].transform.position);
                }
                else
                {
                    Debug.Log("Not enough coins to unlock this character.");
                }
            }
        }

        private void MoveSelectionBounder(Vector3 position)
        {
            if (selectionBounder != null)
            {
                selectionBounder.transform.position = position;
            }
        }

        private void SelectMap(int index)
        {
            if (index >= 0 && index < characterData.terrainSprites.Count)
            {
                characterData.selectedTerrain = characterData.terrainSprites[index];
                Debug.Log("Selected terrain: " + characterData.selectedTerrain.name);
                MoveSelectionMapBounder(selectionMap[index].transform.position);
            }
        }

        private void MoveSelectionMapBounder(Vector3 position)
        {
            if (selectionMapBounder != null)
            {
                selectionMapBounder.transform.position = position;
            }
        }

        private void LoadPlayerData()
        {
            coins = PlayerPrefs.GetInt("Coins", 0);
            for (int i = 0; i < characterData.characterPrefabs.Count; i++)
            {
                unlockedCharacters.Add(PlayerPrefs.GetInt("CharacterUnlocked_" + i, 0) == 1);
            }
        }

        private void SavePlayerData()
        {
            PlayerPrefs.SetInt("Coins", coins);
            for (int i = 0; i < unlockedCharacters.Count; i++)
            {
                PlayerPrefs.SetInt("CharacterUnlocked_" + i, unlockedCharacters[i] ? 1 : 0);
            }
            PlayerPrefs.Save();
        }

        private void UpdateCharacterPriceTexts()
        {
            for (int i = 0; i < characterPriceTexts.Count; i++)
            {
                if (unlockedCharacters[i])
                {
                    characterPriceTexts[i].text = "Unlocked";
                }
                else
                {
                    characterPriceTexts[i].text = characterData.characterPrices[i].ToString();
                }
            }
        }

        public void backBtn()
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<HomCanvas>();
        }

        public void GoBtn()
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<LoadingCanvas>();
        }
    }
        
        
        
    }
