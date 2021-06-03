namespace EuclidianSpacetime.Rendering
{
    public struct DimensionInfo
    {
        public int NumSamples { get; }
        public double Offset { get; }
        /// <summary>
        /// the distance between samples, i.e., 1/resolution
        /// </summary>
        public double LinearUnitsPerSample { get; }

        /// <param name="linearUnitsPerSample">the distance between samples, i.e., 1/resolution</param>
        public DimensionInfo(int numSamples, double offset, double linearUnitsPerSample)
        {
            NumSamples = numSamples;
            Offset = offset;
            LinearUnitsPerSample = linearUnitsPerSample;
        }

        public double GetSampleCoord(int idCoord)
        {
            return Offset + idCoord * LinearUnitsPerSample;
        }
    }
}
