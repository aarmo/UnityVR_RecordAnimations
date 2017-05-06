using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Extensions
{
    public static void Serialize<T>(this T obj, string path)
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

    public static T Deserialize<T>(string path)
    {
        using (var fs = new FileStream(path, FileMode.Open))
        {
            try
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fs);
            }
            finally
            {
                fs.Close();
            }
        }
    }
}

public enum ControllerButton
{
    None,
    Scripted,
    Grip,
    Trigger,
    Pad
}
