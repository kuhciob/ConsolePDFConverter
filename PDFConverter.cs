using ImageMagick;
using System.Collections.Generic;
using System.IO;


namespace ConsolePDFConverter
{
    static class PDFConverter
    {
        private static MemoryStream SerializeToStream(object objectType)
        {
            MemoryStream stream = new MemoryStream();
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, objectType);
            return stream;
        }
        static public void AsImagesToFiles(string fileName, MagickFormat imageExtension)
        {
            var settings = new MagickReadSettings();
            settings.Density = new Density(300, 300);

            using (var images = new MagickImageCollection())
            {
                images.Read(fileName, settings);

                int page = 1;
                foreach (var image in images)
                {
                    image.Format = imageExtension;
                    image.Write($"{fileName}.Page{page.ToString()}.{imageExtension.ToString().ToLower()}");
                    page++;
                }
            }

        }
        static public void AsImagesToJPGFiles(string fileName)
        {
            var settings = new MagickReadSettings();
            settings.Density = new Density(300, 300);

            using (var images = new MagickImageCollection())
            {
                images.Read(fileName, settings);

                int page = 1;
                foreach (var image in images)
                {
                    image.Format = MagickFormat.Jpg;
                    image.Write($"{fileName}.Page{page}.jpg");
                    page++;
                }
            }
        }
        static public void AsImagesToSVGFiles(string fileName)
        {
            var settings = new MagickReadSettings();
            settings.Density = new Density(300, 300);

            using (var images = new MagickImageCollection())
            {
                images.Read(fileName, settings);

                int page = 1;
                foreach (var image in images)
                {
                    image.Format = MagickFormat.Svg;
                    image.Write($"{fileName}.Page{page}.svg");
                    page++;
                }
            }
        }
        static public List<Stream> AsImagesToListOfStreams(string fileName, MagickFormat imageExtension)
        {
            List<Stream> pages = new List<Stream>();

            var settings = new MagickReadSettings();
            settings.Density = new Density(300, 300);

            using (var images = new MagickImageCollection())
            {
                images.Read(fileName, settings);

                int page = 1;
                foreach (var image in images)
                {
                    Stream memoryStream = new MemoryStream();
                    image.Format = imageExtension;
                    image.Write(memoryStream);
                    pages.Add(memoryStream);

                    page++;
                }
            }
            return pages;
        }
        static public Stream AsOneImageToStream(string fileName, MagickFormat imageExtension)
        {
            Stream stream = new MemoryStream();

            var settings = new MagickReadSettings();
            settings.Density = new Density(300);

            using (var images = new MagickImageCollection())
            {
                images.Read(fileName, settings);

                using (var vertical = images.AppendVertically())
                {
                    vertical.Format = imageExtension;
                    vertical.Write(stream);
                }
            }
            return stream;
        }
    }
}
