using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.Xna.Framework.Graphics;

namespace ModernRonin.Terrarium.Rendering.Windows.Utilities
{
    public static class DebugHelpers
    {
        static async Task SaveAsPng(Texture2D texture, string code, int width, int height)
        {
            var fileName = code.Replace("!", "-").Replace("?", "_").Replace(".", "X") + ".png";
            Debug.WriteLine($"saving {fileName} to {ApplicationData.Current.LocalFolder.Path}...");
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                texture.SaveAsPng(stream, width, height);
                stream.Flush();
            }
        }
    }
}