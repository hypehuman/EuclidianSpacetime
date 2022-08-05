namespace EuclidianSpacetime.Rendering
{
    /// <param name="LinearUnitsPerSample">the distance between samples, i.e., 1/resolution</param>
    public readonly record struct DimensionInfo(int NumSamples, double Offset, double LinearUnitsPerSample)
    {
        public double GetSampleCoord(int idCoord)
        {
            return Offset + idCoord * LinearUnitsPerSample;
        }
    }
}
