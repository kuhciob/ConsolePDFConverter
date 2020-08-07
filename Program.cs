using System;

namespace ConsolePDFConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName;
            Console.WriteLine("Enter PDF-file name");
            try
            {
                FileName = Console.ReadLine();
                Console.WriteLine("Processing...");
                PDFConverter.AsImagesToJPGFiles(FileName);
                PDFConverter.AsImagesToSVGFiles(FileName);
                PDFConverter.AsImagesToFiles(FileName, ImageMagick.MagickFormat.Png);
                PDFConverter.AsOneImageToStream(FileName, ImageMagick.MagickFormat.Png);
                Console.WriteLine("Done ;)");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
