using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Text _levelText, _winText, _restartText, _inputText, _playerNameText;
    [SerializeField] private GameObject _saveLoadGame;
    [SerializeField] Button[] buttons;
    public void UpdateLevel()
    {
        _winText.GetComponentInParent<Image>().enabled = false;
        _winText.text = " ";
        _restartText.text = "Restart";
        _playerNameText.text = Stats.PlayerName;
        int level = Stats.Level + 1;
        _levelText.text = level.ToString();
    }
    public void UpdateWin()
    {
        _winText.GetComponentInParent<Image>().enabled = true;
        _winText.text = "Congratulations!";
        _restartText.text = "Next Level";
    }
    public void GameOver()
    {
        _winText.GetComponentInParent<Image>().enabled = true;
        _winText.text = "Congratulations! \n All the Nonograms are solved ";
    }
    public void RestartLevelIfLose()
    {
        _winText.GetComponentInParent<Image>().enabled = true;
        _winText.text = "Lives are over";
    }
    public void SaveInputText ()
    {
        Stats.PlayerName = _inputText.text;
    }
    public void LoadGameScroll() 
    {
        List<string> players = _saveLoadGame.GetComponent<SaveLoadGame>().GetPlayerNames();
        for(int i = 0; i < buttons.Length; i++)
        {
            if (i < players.Count)
            {
                buttons[i].GetComponentInChildren<Text>().text = players[i];
                buttons[i].GetComponent<Button>().enabled = true;
            }
            else break;
        }
    }
}
