using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Frame
{
    public Vector3 Position;
    public bool OnSkate;
    public bool OnHitWall;
}
public class Phantom : MonoBehaviour
{
    private List<Frame> _frames;
    private List<Frame> _framesGhost;
    private float _timeRecord;

    private Animator _anim;
    private bool _onSkate;
    
    [Header("Ghost replay")]
    public bool PlayGhost;
    public string GhostName;
    
    [Header("Components")]
    public GameObject Ghost;
    public GameObject Skate;

    void Awake()
    {
        _frames = new List<Frame>();
        _anim = Ghost.GetComponent<Animator>();
    }

    public void SaveFile(string name)
    {

        PhantomData asset = ScriptableObject.CreateInstance<PhantomData>();

        asset.TimeRecord = _timeRecord;
        asset.Frames = _frames;

        AssetDatabase.CreateAsset(asset, "Assets/Ghosts/"+name+".asset");
        AssetDatabase.SaveAssets();
    }
    
    public PhantomData LoadFile(string name)
    {
        PhantomData phantom = AssetDatabase.LoadAssetAtPath<PhantomData>("Assets/Ghosts/" + name + ".asset");
        return phantom;
    }
    
    void FixedUpdate()
    {
        if (!Game.Instance.Playing) return;

        WriteFrame();

        if (PlayGhost) ReadFrame();
            
        _timeRecord += Time.deltaTime;
        UIManager.Instance.UpdateTimer(_timeRecord);

    }

    public void ReadFrame()
    {
        if (_framesGhost.Count > 0)
        {
            Frame frameGhost = _framesGhost[0];
            _framesGhost.RemoveAt(0);
                
            Ghost.transform.position = frameGhost.Position;
                
            if (frameGhost.OnSkate) GetSkate();
            else GetOffSkate();
                
            if (frameGhost.OnHitWall) _anim.SetFloat("Blend",1);
            else _anim.SetFloat("Blend",0);
        }
    }
    public void WriteFrame()
    {
        Frame frame = new Frame();
        frame.Position = Game.Instance.Player.transform.position;
        frame.OnHitWall = Game.Instance.Player.OnHitWall;
        frame.OnSkate = Game.Instance.Player.OnSkate;
        _frames.Add(frame);
    }
    
    private void GetSkate()
    {
        _anim.SetBool("OnSkate", true);
        Skate.SetActive(true);
    }

    private void GetOffSkate()
    {
        _anim.SetBool("OnSkate", false);
        Skate.SetActive(false);
    }

    public void StartGhost()
    {
        _timeRecord = 0;
        
        
        if (PlayGhost)
        {
            Ghost.SetActive(true);
            _framesGhost = LoadFile(GhostName).Frames;
        }
        else
        {
            Ghost.SetActive(false);
        }
    }
}
