using UnityEngine;

[System.Serializable]
public class Ellipse
{
    public float xAxis;
    public float yAxis;

    //Init the Ellipse with x and y values;
    public Ellipse(float x, float y)
    {
        this.xAxis = x;
        this.yAxis = y;
    }

    //Calculate the position on Ellipse.
    public Vector2 Calculate(float t)
    {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }
}