using System.Diagnostics;
using UnityEngine;

public class Optimization : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            

            stopwatch.Stop();
            UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
        }
    }
}