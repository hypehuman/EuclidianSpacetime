using MathNet.Numerics.LinearAlgebra;
using System;

namespace EuclidianSpacetime.Textures
{
    public interface ILinearSpectralTexture : ITexture
    {
        Vector<double> A { get; }
        Vector<double> B { get; }
    }

    public class LinearSpectralTexture : ILinearSpectralTexture
    {
        public Vector<double> A { get; }
        public Vector<double> B { get; }

        public LinearSpectralTexture(Vector<double> a, Vector<double> b)
        {
            A = a;
            B = b;
        }

        public ARGB ColorAt(Vector<double> position)
        {
            var bMinA = B - A;
            var posMinA = position - A;
            var wavelengthFactor = (bMinA * posMinA) / (bMinA * bMinA);
            var wavelength = wavelengthFactor * (WavelengthB - WavelengthA) + WavelengthA;
            var color = WavelengthToRGB(wavelength);
            return color;
        }

        private static double WavelengthA => 380;
        private static double WavelengthB => 781;

        private const double Gamma = 0.80;

        /// <summary>
        /// Adapted from Tarc's StackOverflow answer at https://stackoverflow.com/a/14917481/1269598, which was in turn adapted from Earl F. Glynn's web page "Spectra Lab Report" at http://www.efg2.com/Lab/ScienceAndEngineering/Spectra.htm
        /// </summary>
        private static RGB WavelengthToRGB(double wavelength)
        {
            double factor;
            double Red, Green, Blue;

            if ((wavelength >= 380) && (wavelength < 440))
            {
                Red = -(wavelength - 440) / (440 - 380);
                Green = 0.0;
                Blue = 1.0;
            }
            else if ((wavelength >= 440) && (wavelength < 490))
            {
                Red = 0.0;
                Green = (wavelength - 440) / (490 - 440);
                Blue = 1.0;
            }
            else if ((wavelength >= 490) && (wavelength < 510))
            {
                Red = 0.0;
                Green = 1.0;
                Blue = -(wavelength - 510) / (510 - 490);
            }
            else if ((wavelength >= 510) && (wavelength < 580))
            {
                Red = (wavelength - 510) / (580 - 510);
                Green = 1.0;
                Blue = 0.0;
            }
            else if ((wavelength >= 580) && (wavelength < 645))
            {
                Red = 1.0;
                Green = -(wavelength - 645) / (645 - 580);
                Blue = 0.0;
            }
            else if ((wavelength >= 645) && (wavelength < 781))
            {
                Red = 1.0;
                Green = 0.0;
                Blue = 0.0;
            }
            else
            {
                Red = 0.0;
                Green = 0.0;
                Blue = 0.0;
            }

            // Let the intensity fall off near the vision limits

            if ((wavelength >= 380) && (wavelength < 420))
            {
                factor = 0.3 + 0.7 * (wavelength - 380) / (420 - 380);
            }
            else if ((wavelength >= 420) && (wavelength < 701))
            {
                factor = 1.0;
            }
            else if ((wavelength >= 701) && (wavelength < 781))
            {
                factor = 0.3 + 0.7 * (780 - wavelength) / (780 - 700);
            }
            else
            {
                factor = 0.0;
            }


            // Don't want 0^x = 1 for x <> 0
            var r = Red == 0 ? (byte)0 : (byte)Math.Round(byte.MaxValue * Math.Pow(Red * factor, Gamma));
            var g = Green == 0 ? (byte)0 : (byte)Math.Round(byte.MaxValue * Math.Pow(Green * factor, Gamma));
            var b = Blue == 0 ? (byte)0 : (byte)Math.Round(byte.MaxValue * Math.Pow(Blue * factor, Gamma));

            return new(r, g, b);
        }
    }
}
