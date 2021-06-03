using MathNet.Numerics.LinearAlgebra;
using System;

namespace EuclidianSpacetime
{
    /// <summary>
    /// The default "arrow of time" is a unit vector pointing in the direction of the N-1th axis.
    /// In other words, the last coordinate is the timelike dimension, and the other coordinates are spacelike.
    /// Any arbitrary arrow of time is defined by rotating spacetime about a given vector by a given angle, and then travelling through it with the default arrow of time.
    /// Essentially, this defines a new orthonormal basis for the spacetime.
    /// </summary>
    public interface ITimeArrow
    {
        Vector<double> Convert(Vector<double> v);
        Vector<double> ConvertBack(Vector<double> v);
    }

    public class TimeArrow : ITimeArrow
    {
        private Matrix<double> BasisUnitColumns { get; }

        /// <summary>
        /// the change-of-basis matrix (also called transition matrix)
        /// The last dimension is timelike.
        /// </summary>
        private Matrix<double> TransitionMatrix { get; }

        private TimeArrow(Matrix<double> basisUnitColumns)
        {
            BasisUnitColumns = basisUnitColumns;
            TransitionMatrix = BasisUnitColumns.Inverse();
        }

        public static ITimeArrow FromBasis(Matrix<double> basisUnitColumns)
        {
            return new TimeArrow(basisUnitColumns);
        }

        public static ITimeArrow Default(int n)
        {
            return new TimeArrow(Matrix<double>.Build.DiagonalIdentity(n));
        }

        public static ITimeArrow FromRotation(Vector<double> rotationAxis, double rotationAngle)
        {
            throw new NotImplementedException("Haven't implemented rotations around ");
            //return FromBasis(
        }

        public Vector<double> Convert(Vector<double> v)
        {
            return TransitionMatrix * v;
        }

        public Vector<double> ConvertBack(Vector<double> v)
        {
            return BasisUnitColumns * v;
        }
    }
}
