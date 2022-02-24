using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Frame
{
    public Vector3 Position;
    //public _ Anim; 
}
public class Phantom : MonoBehaviour
{
    private List<Frame> Frames;
    private List<Frame> FramesGhost;
    private float TimeRecord;
    
    public bool PlayGhost;
    public string GhostName;
    public GameObject Ghost;

    void Awake()
    {
        Frames = new List<Frame>();

        if (PlayGhost)
        {
            Ghost.SetActive(true);
            LoadFile(GhostName);
        }
        else
        {
            Ghost.SetActive(false);
        }
    }

    public void SaveFile(string name)
    {

        PhantomData asset = ScriptableObject.CreateInstance<PhantomData>();

        asset.TimeRecord = TimeRecord;
        asset.Frames = Frames;

        AssetDatabase.CreateAsset(asset, "Assets/Ghosts/"+name+".asset");
        AssetDatabase.SaveAssets();
    }
    
    public void LoadFile(string name)
    {
        PhantomData phantom = AssetDatabase.LoadAssetAtPath<PhantomData>("Assets/Ghosts/" + name + ".asset");
        FramesGhost = phantom.Frames;
    }
    
    void FixedUpdate()
    {
        if (!Game.Instance.Playing) return;
        

        Frame frame = new Frame();
        frame.Position = Game.Instance.Player.transform.position;
        Frames.Add(frame);
            
        TimeRecord += Time.deltaTime;
        

        if (PlayGhost)
        {
            if (FramesGhost.Count > 0)
            {
                Frame frameGhost = FramesGhost[0];
                FramesGhost.RemoveAt(0);
                Ghost.transform.position = frameGhost.Position;
            }
        }
        
    }
}
