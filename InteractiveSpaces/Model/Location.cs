namespace InteractiveSpaces.Models;

public abstract class Location 
{
    public int Id { get; set; } 

}


public class LocationGPS : Location {

    public float Latitude { get; set; }

    public float Longitude { get; set; }
}


public class Location3D : Location {
    public Location3D(float x, float y, float z, float rotX, float rotY, float rotZ, float scaleX, float scaleY, float scaleZ)
    {
        X = x;
        Y = y;
        Z = z;
        RotX = rotX;
        RotY = rotY;
        RotZ = rotZ;
        ScaleX = scaleX;
        ScaleY = scaleY;
        ScaleZ = scaleZ;
    }

    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public float RotX { get; set; }

    public float RotY { get; set; }

    public float RotZ { get; set; }

    public float ScaleX { get; set; }

    public float ScaleY { get; set; }

    public float ScaleZ { get; set; }


}