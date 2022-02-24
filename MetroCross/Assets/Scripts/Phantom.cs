using System;
using System.Collections;
using System.Collections.Generic;
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
    public float TimeRecord;

    public bool Record;
    public bool Play;

    public GameObject Ghost;

    void Start()
    {
        Frames = new List<Frame>();
    }

    public void SaveFile(string name)
    {
        
    }
    
    public void LoadFile(string name)
    {
        
    }
    
    void FixedUpdate()
    {
        if (!Game.Instance.Playing) return;
        
        if (Record)
        {
            Frame frame = new Frame();
            frame.Position = Game.Instance.Player.transform.position;
            Frames.Add(frame);
            
            TimeRecord += Time.deltaTime;
        }

        if (Play)
        {
            Ghost.SetActive(true);
            Frame frame = Frames[0];
            Frames.RemoveAt(0);
        }
        
    }
}
