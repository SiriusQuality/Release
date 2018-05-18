using System;

namespace SiriusModel.Model.Base
{
    ///<summary>
    ///The class Pool contains structural, labile, daily labile and dead value (use for Ni and DM compartments).
    ///</summary>
    public class Pool
    {
        #region fields 

        ///<summary>
        ///The structural quantity.
        ///</summary>
        private double struc;

        ///<summary>
        ///The labile quantity.
        ///</summary>
        private double labile;

        ///<summary>
        ///The dead quantity.
        ///</summary>
        private double dead;

        ///<summary>
        ///The lost quantity by degradation.
        ///</summary>
        private double lost;


        ///<summary>
        ///The daily labile quantity.
        ///</summary>
        private double dailyLabile;

        #endregion 

        ///<summary>
        ///Initial constructor.
        ///</summary>
        public Pool()
        {
            struc = 0;
            labile = 0;
            dead = 0;
            dailyLabile = 0;
            lost = 0;
        }

        ///<summary>
        ///Copy constructor.
        ///</summary>
        ///<param name="toCopy">The pool to copy.</param>
        public Pool(Pool toCopy)
        {
            struc = toCopy.struc;
            labile = toCopy.labile;
            dead = toCopy.dead;
            dailyLabile = toCopy.dailyLabile;
            lost = toCopy.lost;
        }
        
        ///<summary>
        ///Init this pool for a new simulation.
        ///</summary>
        public void Init()
        {
            struc = 0;
            labile = 0;
            dead = 0;
            dailyLabile = 0;
            lost = 0;
        }

        ///<summary>
        ///Init this pool for a new day step.
        ///</summary>
        public void InitDayStep()
        {
            DailyLabile = 0;
        }
        
        public void InitDayStepDailyLabile(double value)
        {
            DailyLabile = value;
        }

        ///<summary>
        ///Get or set the daily labile of this pool.
        ///</summary>
        public double DailyLabile
        {
            get { return dailyLabile; }
            private set
            {
                dailyLabile = value;
                UniverseLink.RoundZero(ref dailyLabile);
                Check.IsNumber(dailyLabile);
                Check.IsLessOrEqual(dailyLabile, labile);
                Check.IsPositiveOrZero(dailyLabile);
            }
        }

        ///<summary>
        ///Get or set structural part of this pool.
        ///</summary>
        public double Struct
        {
            get { return struc; }
            set
            {
                struc = value;
                UniverseLink.RoundZero(ref struc);
                Check.IsNumber(struc);
                Check.IsPositiveOrZero(struc);
            }
        }
	    
        ///<summary>
        ///Get or set labile part of this pool.
        ///</summary>
        public double Labile
        {
            get { return labile; }
            set 
            {
                labile = value;
                UniverseLink.RoundZero(ref labile);
                Check.IsNumber(labile);
                Check.IsPositiveOrZero(labile);
                dailyLabile = Math.Min(dailyLabile, labile);
                Check.IsNumber(dailyLabile);
            }
        }

        ///<summary>
        ///Get or set dead part of this pool.
        ///</summary>
        public double Dead
        {
            get { return dead; }
            set
            {
                dead = value;
                UniverseLink.RoundZero(ref dead);
                Check.IsNumber(dead);
                Check.IsPositiveOrZero(dead);
            }
        }

        ///<summary>
        ///Get or set lost part of this pool.
        ///</summary>
        public double Lost
        {
            get { return lost; }
            set
            {
                lost = value;
                UniverseLink.RoundZero(ref lost);
                Check.IsNumber(lost);
                Check.IsPositiveOrZero(lost);
            }
        }


        public double Green
        {
            get { return Struct + Labile; }
        }

        public double Total
        {
            get { return Green + Dead; }
        }

        ///<summary>
        ///Update from the daily labile pool (also remove from labile pool).
        ///</summary>
        ///<param name="need">The needed quantity to remove from daily labile.</param>
        ///<returns>The needed quantity if enougth quantity in daily labile or the quantity of daily labile.</returns>
        public double UptakeDailyLabile(double need)
        {
            Check.IsNumber(need);
            double found;
    	
            if (need <= dailyLabile)
            {
                found = need;
                DailyLabile -= need;
                Labile -= need;
            }
            else
            {
                found = dailyLabile;
                Labile -= dailyLabile;
                DailyLabile = 0;
            }
            Check.IsNumber(labile);
            Check.IsNumber(dailyLabile);
            return found;
        }
    }
}