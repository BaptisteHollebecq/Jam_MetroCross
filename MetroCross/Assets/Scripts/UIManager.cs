using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FirebaseWebGL.Examples.Utils;
using FirebaseWebGL.Scripts.FirebaseBridge;
using FirebaseWebGL.Scripts.Objects;
using TMPro;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text Timer;
    public TMP_InputField Pseudo;
    public TMP_InputField Ghost;

    public TMP_Text ResultTimer;
    
    public GameObject GameUI;
    public GameObject MenuUI;

    public List<TMP_Text> boards;
    
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
        //ShowLeaderBoard();
    }
    
    void Update()
    {

    }

    public void OnPlay()
    {
        String pseudo = Pseudo.text.Replace(' ', '_');
        if (String.IsNullOrEmpty(pseudo)) return;

        Game.Instance.PlayerName = pseudo;
        Game.Instance.Ghost.PlayGhost = false;
        
        StartGame();
    }

    public void OnPlayVersus()
    {
        String pseudo = Pseudo.text.Replace(' ', '_');
        if (String.IsNullOrEmpty(pseudo)) return;
        
        Game.Instance.PlayerName = pseudo;
        
        String ghost = Ghost.text.Replace(' ', '_');
        if (String.IsNullOrEmpty(ghost) || Game.Instance.Ghost.LoadFile(ghost) == null) return;
        
        Game.Instance.Ghost.GhostName = ghost;
        Game.Instance.Ghost.PlayGhost = true;
        
        StartGame();
    }

    public void StartGame()
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
        Game.Instance.Playing = true;

        Game.Instance.StartGame();
    }

    public void UpdateTimer(float time)
    {
        Timer.text = ParseTimer(time);
    }

    public string ParseTimer(float time)
    {
        string minute = ""+Mathf.Floor(time / 60);
        minute = minute.PadLeft(2, '0');
        
        string secondes = String.Format("{0:0.00}", time % 60);
        secondes = secondes.PadLeft(5, '0');
        
        return  minute + ":" + secondes;
    }

    public void ShowResult()
    {
        MenuUI.SetActive(true);
        GameUI.SetActive(false);

        ResultTimer.gameObject.SetActive(true);
        ResultTimer.text = Timer.text;
        
        ShowLeaderBoard();
    }

    public void ShowLeaderBoard()
    {
        FirebaseFirestore.GetDocumentsInCollection("map1", gameObject.name, "OnShowLeaderBoard", "DisplayErrorObject");
    }
    
    public void DisplayErrorObject(string error)
    {
        var parsedError = StringSerializationAPI.Deserialize(typeof(FirebaseError), error) as FirebaseError;
        boards[0].text  = error;
        Debug.LogError(error);
    }

    public void OnShowLeaderBoard(string data)
    {
        boards[0].text = data.Split(':')[0].Replace("{","");
    }

    /*
    string [] fileEntries = Directory.GetFiles("Assets/Ghosts/");

    List<float> scores = new List<float>();
    List<string> names = new List<string>();


    foreach (string file in fileEntries)
    {
        if (file.Contains(".meta")) continue;
        
        PhantomData phantom = AssetDatabase.LoadAssetAtPath<PhantomData>(file);

        string name = file.Replace("Assets/Ghosts/", "").Replace(".asset", "");
        float score = phantom.TimeRecord;

        int idx = 0;
        while (true)
        {
            if (scores.Count <= idx)
            {
                scores.Add(score);
                names.Add(name);
                break;
            }

            if (scores[idx] > score)
            {
                scores.Insert(idx,score);
                names.Insert(idx,name);
                break;
            }

            idx++;
        }
    }

    for (int i = 0; i < 5; i++)
    {
        if (i >= scores.Count) break;
        boards[i].text = names[i] + " : " + ParseTimer(scores[i]);
        
    }*/
    
}
