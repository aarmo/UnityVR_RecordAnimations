using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Extensions
{
    public static void Serialize(this AnimationClip obj, string path)
    {
        using (var fs = new FileStream(path, FileMode.Create))
        {
            try
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, obj);
            }
            finally
            {
                fs.Close();
            }
        }
    }

    public static AnimationClip Deserialize(string path)
    {
        using (var fs = new FileStream(path, FileMode.Open))
        {
            try
            {
                var formatter = new BinaryFormatter();
                return (AnimationClip)formatter.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
