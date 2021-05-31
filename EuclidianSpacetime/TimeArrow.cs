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
        /// <summary>
        /// the change-of-basis matrix (also called transition matrix)
        /// The last dimension is timelike.
        /// </summary>
        Matrix<double> TransitionMatrix { get; }
    }

    public class TimeArrow : ITimeArrow
    {
        public Matrix<double> TransitionMatrix { get; }

        private TimeArrow(Matrix<double> transitionMatrix)
        {
            TransitionMatrix = transitionMatrix;
        }

        public static ITimeArrow FromBasis(Matrix<double> basis)
        {
            return new TimeArrow(basis.Inverse());
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
    }
}
