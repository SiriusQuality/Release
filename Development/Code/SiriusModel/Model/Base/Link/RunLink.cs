using System;

namespace SiriusModel.Model.Base.Link
{
    ///<summary>
    ///This class is a base class for class which have a link to a Run instance.
    ///This class represent association between a Run and its components. This must 
    ///be initialized with a not null reference to a Run. 
    ///</summary>
    public class RunLink : IDisposable
    {
        ///<summary>
        ///Create a new RunLink with a specified Run instance.
        ///</summary>
        ///<param name="aRun">The run reference, must be not null.</param>
        ///<exception cref="ArgumentNullException">if <paramref name="aRun"/> is null</exception>
        public RunLink(Run aRun)
        {
            Check.IsNotNull(aRun);
            Run = aRun;
        }

        ///<summary>
        ///Get the reference to the run. This is never null.
        ///</summary>
        public Run Run { get; private set; }

        #region expose Run properties

        ///<summary>
        ///Get the Universe copy at a given date.
        ///</summary>
        ///<param name="theDate">The date of the Universe to get.</param>
        ///<returns>The Universe found or null.</returns>
        public Universe GetUniverse(DateTime theDate)
        {
            return Run.GetUniverse(theDate);
        }

        ///<summary>
        ///Get the current Universe, i.e the universe created at the beginning of the simulation.
        ///</summary>
        public Universe CurrentUniverse
        {
            get { return Run.CurrentUniverse; }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            Run = null;
        }

        #endregion
    }
}