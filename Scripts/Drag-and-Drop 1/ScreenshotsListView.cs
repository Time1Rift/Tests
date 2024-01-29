using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotsListView : MonoBehaviour
{
    [SerializeField] private ScreenshotView _tempLate;
    [SerializeField] private Transform _container;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Transform _parent;

    private void Awake()
    {
        Render(new List<Screenshot>()
        {
            new Screenshot(_defaultSprite, DateTime.Now),
            new Screenshot(_defaultSprite, DateTime.Now),
            new Screenshot(_defaultSprite, DateTime.Now),
        });
    }

    private void Render(List<Screenshot> screenshots)
    {
        foreach (var screenshot in screenshots)
        {
            ScreenshotView view = Instantiate(_tempLate, _container);
            view.Init(_parent);
            view.Render(screenshot);
        }
    }
}