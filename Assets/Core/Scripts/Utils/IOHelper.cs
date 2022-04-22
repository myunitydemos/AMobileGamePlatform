using System.Collections;
using System.IO;
using UnityEngine;

public class IOHelper
{
   public static byte[] SafeReadAllBytes(ref string path)
    {
        try
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            if (!File.Exists(path))
            {
                return null;
            }
            File.SetAttributes(path, FileAttributes.Normal);
            return File.ReadAllBytes(path);
        } 
        catch(System.Exception ex)
        {
            Debug.LogError(string.Format("SafeReadAllBytes Failed. Path = {0} with err = {1}", path, ex.Message));
            return null;
        }
    }
}
