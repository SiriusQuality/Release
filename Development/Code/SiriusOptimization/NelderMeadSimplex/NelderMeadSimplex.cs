using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiriusOptimization.Base;
using SiriusModel.InOut;
using SiriusModel.Optimization.Base;

namespace SiriusOptimization.NelderMeadSimplex
{
    public sealed class SimplexConstant
    {
        private double _value;
        private double _initialPerturbation;

        public SimplexConstant(double value, double initialPerturbation)
        {
            _value = value;
            _initialPerturbation = initialPerturbation;
        }

        /// <summary>
        /// The value of the constant
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }

        // The size of the initial perturbation
        public double InitialPerturbation
        {
            get { return _initialPerturbation; }
            set { _initialPerturbation = value; }
        }
    }
    public class NelderMeadSimplex: IAlgorithm
    {

        public static OptimizationProblem OptProblem { get; set; }

        private static readonly double JITTER = 1e-10d;// a small value used to protect against floating point noise
        private SimplexConstant[] constants;
        private int numDimensions;
        private int numVertices;
        private Vector[] vertices;
        private double[] errorValues;
        private int evaluationCount;
        private ErrorProfile errorProfile;
        private double reflectionPointValue;
        private double expansionPointValue;

        public NelderMeadSimplex(OptimizationProblem optimizationProblem)
        {
            NelderMeadSimplex.OptProblem = optimizationProblem;
            constants = new SimplexConstant[OptProblem.fitnessFunctions.paramValueDictioSize];
            int count = 0;
            foreach (string subModel in OptProblem.fitnessFunctions.ParamValueDictio.Keys.ToList())
            {
                foreach (string parameterName in OptProblem.fitnessFunctions.ParamValueDictio[subModel].Keys.ToList())
                {
                    OptiParameter completeParam = OptProblem.Opt_Parameters.Find(ii => ii.Name == parameterName);
                    constants[count] = new SimplexConstant((completeParam.Max + completeParam.Min) / 2, (completeParam.Max - completeParam.Min) / 2 /10);
                    count++;
                }
            }
        }

        public void InitAlgo()
        {
            // create the initial simplex
            numDimensions = constants.Length;
            numVertices = numDimensions + 1;
            vertices = _initializeVertices(constants);
            errorValues = new double[numVertices];

            evaluationCount = 0;

            Dictionary<string, Dictionary<string, double>> fitnessModeValue;//rmse, rme or rmsre
            Dictionary<string, Dictionary<string, double>> absMaxErr;
            errorValues = _initializeErrorValues(vertices, out fitnessModeValue, out absMaxErr);
        }

        public double StepAlgo(out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr)
        {

                errorProfile = _evaluateSimplex(errorValues);
                // attempt a reflection of the simplex
                reflectionPointValue = _tryToScaleSimplex(-1.0, ref errorProfile, vertices, errorValues, out fitnessModeValue, out absMaxErr);
                ++evaluationCount;
                if (reflectionPointValue >= errorValues[errorProfile.LowestIndex])
                {
                    // it's better than the best point, so attempt an expansion of the simplex
                    expansionPointValue = _tryToScaleSimplex(2.0, ref errorProfile, vertices, errorValues, out fitnessModeValue, out absMaxErr);
                    ++evaluationCount;
                }
                else if (reflectionPointValue <= errorValues[errorProfile.NextHighestIndex])
                {
                    // it would be worse than the second best point, so attempt a contraction to look
                    // for an intermediate point
                    double currentWorst = errorValues[errorProfile.HighestIndex];
                    double contractionPointValue = _tryToScaleSimplex(0.5, ref errorProfile, vertices, errorValues, out fitnessModeValue, out absMaxErr);
                    ++evaluationCount;
                    if (contractionPointValue <= currentWorst)
                    {
                        // that would be even worse, so let's try to contract uniformly towards the low point; 
                        // don't bother to update the error profile, we'll do it at the start of the
                        // next iteration
                        _shrinkSimplex(errorProfile, vertices, errorValues, out fitnessModeValue, out absMaxErr);
                        evaluationCount += numVertices; // that required one function evaluation for each vertex; keep track
                    }
                }
            ComponenttoParamValue(vertices[errorProfile.LowestIndex].Components);//errorValues[errorProfile.LowestIndex]
            return OptProblem.fitnessFunctions.Compute(out fitnessModeValue, out absMaxErr,true);
        }

        /// <summary>
        /// Evaluate the objective function at each vertex to create a corresponding
        /// list of error values for each vertex
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        private static double[] _initializeErrorValues(Vector[] vertices, out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr)
        {
            double[] errorValues = new double[vertices.Length];

            fitnessModeValue = null;//we don't need the outputs
            absMaxErr = null;
            for (int i = 0; i < vertices.Length; i++)
            {
                ComponenttoParamValue(vertices[i].Components);
                errorValues[i] = OptProblem.fitnessFunctions.Compute(out fitnessModeValue, out absMaxErr,false);
            }
            return errorValues;
        }

        /// <summary>
        /// Check whether the points in the error profile have so little range that we
        /// consider ourselves to have converged
        /// </summary>
        /// <param name="errorProfile"></param>
        /// <param name="errorValues"></param>
        /// <returns></returns>
        private static bool _hasConverged(double convergenceTolerance, ErrorProfile errorProfile, double[] errorValues)
        {
            double range = 2 * Math.Abs(errorValues[errorProfile.HighestIndex] - errorValues[errorProfile.LowestIndex]) /
                (Math.Abs(errorValues[errorProfile.HighestIndex]) + Math.Abs(errorValues[errorProfile.LowestIndex]) + JITTER);
            if (range < convergenceTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Examine all error values to determine the ErrorProfile
        /// </summary>
        /// <param name="errorValues"></param>
        /// <returns></returns>
        private static ErrorProfile _evaluateSimplex(double[] errorValues)
        {
            ErrorProfile errorProfile = new ErrorProfile();
            if (errorValues[0] < errorValues[1])
            {
                errorProfile.HighestIndex = 0;
                errorProfile.NextHighestIndex = 1;
            }
            else
            {
                errorProfile.HighestIndex = 1;
                errorProfile.NextHighestIndex = 0;
            }

            for (int index = 0; index < errorValues.Length; index++)
            {
                double errorValue = errorValues[index];
                if (errorValue >= errorValues[errorProfile.LowestIndex])
                {
                    errorProfile.LowestIndex = index;
                }
                if (errorValue < errorValues[errorProfile.HighestIndex])
                {
                    errorProfile.NextHighestIndex = errorProfile.HighestIndex; // downgrade the current highest to next highest
                    errorProfile.HighestIndex = index;
                }
                else if (errorValue < errorValues[errorProfile.NextHighestIndex] && index != errorProfile.HighestIndex)
                {
                    errorProfile.NextHighestIndex = index;
                }
            }

            return errorProfile;
        }

        /// <summary>
        /// Construct an initial simplex, given starting guesses for the constants, and
        /// initial step sizes for each dimension
        /// </summary>
        /// <param name="simplexConstants"></param>
        /// <returns></returns>
        private static Vector[] _initializeVertices(SimplexConstant[] simplexConstants)
        {
            int numDimensions = simplexConstants.Length;
            Vector[] vertices = new Vector[numDimensions + 1];
            GaussianDistribution gaussian;
            // define one point of the simplex as the given initial guesses
            Vector p0 = new Vector(numDimensions);
            for (int i = 0; i < numDimensions; i++)
            {
                gaussian  = new GaussianDistribution(simplexConstants[i].Value, simplexConstants[i].InitialPerturbation);
                p0[i] = gaussian.NextGaussian(simplexConstants[i].Value, simplexConstants[i].InitialPerturbation);
            }

            // now fill in the vertices, creating the additional points as:
            // P(i) = P(0) + Scale(i) * UnitVector(i)
            vertices[0] = p0;
            for (int i = 0; i < numDimensions; i++)
            {
                gaussian = new GaussianDistribution(simplexConstants[i].InitialPerturbation, simplexConstants[i].InitialPerturbation/2);
                double scale = simplexConstants[i].InitialPerturbation;
                Vector unitVector = new Vector(numDimensions);
                unitVector[i] = 1;
                vertices[i + 1] = p0.Add(unitVector.Multiply(gaussian.NextGaussian(simplexConstants[i].InitialPerturbation, simplexConstants[i].InitialPerturbation / 2)));
            }
            return vertices;
        }

        /// <summary>
        /// Test a scaling operation of the high point, and replace it if it is an improvement
        /// </summary>
        /// <param name="scaleFactor"></param>
        /// <param name="errorProfile"></param>
        /// <param name="vertices"></param>
        /// <param name="errorValues"></param>
        /// <returns></returns>
        private static double _tryToScaleSimplex(double scaleFactor, ref ErrorProfile errorProfile, Vector[] vertices, double[] errorValues, out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr)
        {
            // find the centroid through which we will reflect
            Vector centroid = _computeCentroid(vertices, errorProfile);

            // define the vector from the centroid to the high point
            Vector centroidToHighPoint = vertices[errorProfile.HighestIndex].Subtract(centroid);

            // scale and position the vector to determine the new trial point
            Vector newPoint = centroidToHighPoint.Multiply(scaleFactor).Add(centroid);

            // evaluate the new point
            ComponenttoParamValue(newPoint.Components);
            double newErrorValue = OptProblem.fitnessFunctions.Compute(out fitnessModeValue, out absMaxErr,false);

            // if it's better, replace the old high point
            if (newErrorValue > errorValues[errorProfile.HighestIndex])
            {
                vertices[errorProfile.HighestIndex] = newPoint;
                errorValues[errorProfile.HighestIndex] = newErrorValue;
            }

            return newErrorValue;
        }

        /// <summary>
        /// Contract the simplex uniformly around the lowest point
        /// </summary>
        /// <param name="errorProfile"></param>
        /// <param name="vertices"></param>
        /// <param name="errorValues"></param>
        private static void _shrinkSimplex(ErrorProfile errorProfile, Vector[] vertices, double[] errorValues, out Dictionary<string, Dictionary<string, double>> fitnessModeValue, out Dictionary<string, Dictionary<string, double>> absMaxErr)
        {
            Vector lowestVertex = vertices[errorProfile.LowestIndex];

            fitnessModeValue = null; // we don't need those output
            absMaxErr = null; 

            for (int i = 0; i < vertices.Length; i++)
            {
                if (i != errorProfile.LowestIndex)
                {
                    vertices[i] = (vertices[i].Add(lowestVertex)).Multiply(0.5);
                    ComponenttoParamValue(vertices[i].Components);
                    errorValues[i] = OptProblem.fitnessFunctions.Compute(out fitnessModeValue, out absMaxErr, false);
                }
            }
        }

        /// <summary>
        /// Compute the centroid of all points except the worst
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="errorProfile"></param>
        /// <returns></returns>
        private static Vector _computeCentroid(Vector[] vertices, ErrorProfile errorProfile)
        {
            int numVertices = vertices.Length;
            // find the centroid of all points except the worst one
            Vector centroid = new Vector(numVertices - 1);
            for (int i = 0; i < numVertices; i++)
            {
                if (i != errorProfile.HighestIndex)
                {
                    centroid = centroid.Add(vertices[i]);
                }
            }
            return centroid.Multiply(1.0d / (numVertices - 1));
        }

        ///<summary>
        ///Transfert the value of a component to fitness parameters in order to compute fitness
        ///</summary>
        private static void ComponenttoParamValue( double[] component)
        {
            int i = 0;

            foreach (string subModel in OptProblem.fitnessFunctions.ParamValueDictio.Keys.ToList())
            {
                foreach (string parameterName in OptProblem.fitnessFunctions.ParamValueDictio[subModel].Keys.ToList())
                {

                    OptiParameter completeParam = OptProblem.Opt_Parameters.Find(ii => ii.Name == parameterName);

                    OptProblem.fitnessFunctions.ParamValueDictio[subModel][parameterName] = component[i];
                    i++;
                }
            }
        }

        public string Name() { return "NedlerMeadSimplex"; }
        public int NbOfRounds() { return OptProblem.OptimizationParameters.SimplexNbOfRound; }
        public int NbOfGeneration() { return OptProblem.OptimizationParameters.SimplexNbOfGeneration; }
        public double StopFitness() { return OptProblem.OptimizationParameters.SimplexStopFitness; } 
        public void EndAlgo()
        {
        }

        private sealed class ErrorProfile
        {
            private int _highestIndex;
            private int _nextHighestIndex;
            private int _lowestIndex;

            public int HighestIndex
            {
                get { return _highestIndex; }
                set { _highestIndex = value; }
            }

            public int NextHighestIndex
            {
                get { return _nextHighestIndex; }
                set { _nextHighestIndex = value; }
            }

            public int LowestIndex
            {
                get { return _lowestIndex; }
                set { _lowestIndex = value; }
            }
        }
        private class Vector
        {
            private double[] _components;
            private int _nDimensions;
            public Vector(int dimensions)
            {
                _components = new double[dimensions];
                _nDimensions = dimensions;
            }

            public int NDimensions
            {
                get { return _nDimensions; }
            }

            public double this[int index]
            {
                get { return _components[index]; }
                set { _components[index] = value; }
            }

            public double[] Components
            {
                get { return _components; }
            }

            /// <summary>
            /// Add another vector to this one
            /// </summary>
            /// <param name="v"></param>
            /// <returns></returns>
            public Vector Add(Vector v)
            {
                if (v.NDimensions != this.NDimensions)
                    throw new ArgumentException("Can only add vectors of the same dimensionality");

                Vector vector = new Vector(v.NDimensions);
                for (int i = 0; i < v.NDimensions; i++)
                {
                    vector[i] = this[i] + v[i];
                }
                return vector;
            }

            /// <summary>
            /// Subtract another vector from this one
            /// </summary>
            /// <param name="v"></param>
            /// <returns></returns>
            public Vector Subtract(Vector v)
            {
                if (v.NDimensions != this.NDimensions)
                    throw new ArgumentException("Can only subtract vectors of the same dimensionality");

                Vector vector = new Vector(v.NDimensions);
                for (int i = 0; i < v.NDimensions; i++)
                {
                    vector[i] = this[i] - v[i];
                }
                return vector;
            }

            /// <summary>
            /// Multiply this vector by a scalar value
            /// </summary>
            /// <param name="scalar"></param>
            /// <returns></returns>
            public Vector Multiply(double scalar)
            {
                Vector scaledVector = new Vector(this.NDimensions);
                for (int i = 0; i < this.NDimensions; i++)
                {
                    scaledVector[i] = this[i] * scalar;
                }
                return scaledVector;
            }

            /// <summary>
            /// Compute the dot product of this vector and the given vector
            /// </summary>
            /// <param name="v"></param>
            /// <returns></returns>
            public double DotProduct(Vector v)
            {
                if (v.NDimensions != this.NDimensions)
                    throw new ArgumentException("Can only compute dot product for vectors of the same dimensionality");

                double sum = 0;
                for (int i = 0; i < v.NDimensions; i++)
                {
                    sum += this[i] * v[i];
                }
                return sum;
            }

            public override string ToString()
            {
                string[] components = new string[_components.Length];
                for (int i = 0; i < components.Length; i++)
                {
                    components[i] = _components[i].ToString();
                }
                return "[ " + string.Join(", ", components) + " ]";
            }
        }
    }
}
