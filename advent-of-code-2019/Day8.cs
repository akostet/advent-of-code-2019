using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace advent_of_code_2019
{
    public static class Day8
    {
        private static readonly int ImageWidth = 25;
        private static readonly int ImageHeight = 6;

        public static int Problem1(string input)
        {
            var layerPixelCount = ImageHeight * ImageWidth;
            List<string> layers = new List<string>();

            var layerCount = 0;
            var layerWithFewestZeroDigits = 0;
            var fewestZeroDigits = int.MaxValue;

            //Read the data into layers
            while ((double)layerCount < (double)input.Length/(double)layerPixelCount)
            {
                var layerChars = input.Skip(layerPixelCount * layerCount).Take(layerPixelCount);
                var layer = new string(layerChars.ToArray());
                layers.Add(layer);

                var numberOfZeros = layer.Count(character => character == '0');

                if(numberOfZeros < fewestZeroDigits)
                {
                    fewestZeroDigits = numberOfZeros;
                    layerWithFewestZeroDigits = layerCount;
                }

                layerCount++;
            }              
                
            return layers[layerWithFewestZeroDigits].Count(character => character == '1') * layers[layerWithFewestZeroDigits].Count(character => character == '2');
        }

        public static string Problem2(string input)
        {
            var layerPixelCount = ImageHeight * ImageWidth;
            List<string> layers = new List<string>();

            var resultingImage = new char[ImageHeight, ImageWidth];

            for (int y = 0; y < ImageHeight; y++)
            {
                for (int x = 0; x < ImageWidth; x++)
                {
                    resultingImage[y, x] = '2';
                }
                
            }
            
            var layerCount = 0;

            //Read the data into layers
            while ((double)layerCount < ((double)input.Length / (double)layerPixelCount))
            {
                var layerChars = input.Skip(layerPixelCount * layerCount).Take(layerPixelCount).ToArray();
                var layer = new string(layerChars);
                layers.Add(layer);

                for (var y = 0; y < ImageHeight; y++)
                {
                    for (var x = 0; x < ImageWidth; x++)
                    {
                        if (resultingImage[y, x] == '2')
                            resultingImage[y, x] = layerChars[x + (ImageWidth*y)];
                    }
                }

                layerCount++;
            }
            for (var y = 0; y < ImageHeight; y++)
            {
                for (var x = 0; x < ImageWidth; x++)
                {
                    var c = resultingImage[y, x] == '0' ? ' ' : '*';
                    Console.Write("{0,2}", c);
                }
                Console.WriteLine();
            }
            return "LJECH";
        }
    }
}
