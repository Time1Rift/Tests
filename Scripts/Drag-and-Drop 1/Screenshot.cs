using System;
using UnityEngine;

[Serializable]
public class Screenshot
{
    private Sprite _image;
    private DateTime _time;

    public Screenshot(Sprite image, DateTime time)
    {
        _image = image;
        _time = time;
    }

    public Sprite Image => _image;
    public DateTime CreationTime => _time;
}