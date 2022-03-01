using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomData
{
    public List<Frame> Frames;
    public float TimeRecord;

    public string GetJSON()
    {
        string result = "{";
        result += "TimeRecord:" + TimeRecord+",";
        result += "Frames:[";
        foreach (Frame frame in Frames)
        {
            break;
            result += "{Position:{"+frame.Position.x+","+frame.Position.y+","+frame.Position.z+"},";
            result += "OnSkate:" + frame.OnSkate + ",";
            result += "OnHitWall:" + frame.OnHitWall + "}";
        }
        return result+"]}";
    }
}
