namespace EuclidianSpacetime.Rendering
{
    public struct DimensionInfo
    {
        public int NumSamples { get; }
        public double Offset { get; }
        public double SamplesPerLinearUnit { get; }

        public DimensionInfo(int numSamples, double offset, double samplesPerLinearUnit)
        {
            NumSamples = numSamples;
            Offset = offset;
            SamplesPerLinearUnit = samplesPerLinearUnit;
        }

        public double GetSampleCoord(int idCoord)
        {
            return Offset + idCoord / SamplesPerLinearUnit;
        }
    }
}
