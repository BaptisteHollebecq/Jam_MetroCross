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

    public bool Record;
    public bool Play;

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
        if (Record)
        {
            Frame frame = new Frame();
            frame.Position = Game.Instance.Player.transform.position;
        }

        if (Play)
        {
            
        }
        
    }
}
