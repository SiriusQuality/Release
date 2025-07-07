using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csMTG.Utilities
{
    static public class RootUtilities
    {
         public const double epsilon =1E-10;

        static public Random rnd;

        static public double dRandUnif()
        /* Random determination of a double in a flat distribution between 0 an 1*/
        {
            
            double rand;
            rand = rnd.NextDouble();
            if (rand < epsilon) { rand = epsilon; }
            return (rand);
        }

        static public double RandGaussien(double av, double sd)
        {  /* Random determination of a double in a gaussian distribution with average av and standard deviation sd */
            double randGaussien, rand1, rand2;

            rand1 = dRandUnif();
            rand2 = dRandUnif();
            randGaussien = av + (sd * Math.Sqrt(-Math.Log(rand1)) * Math.Cos(Math.PI * rand2) * 1.414);
            return (randGaussien);
        } 

        static public double randAngRad(int seed)
        {   /* Random determination of an radial angle between 0 - 2*Pi */

            return (2.0 * Math.PI * dRandUnif());
        } 

        static public double[] rotZ(double[] u, double teta)
        /* Rotation of "u" of a "teta" angle around the axis (Oz) */
        {
            double[] v = new double[3];

            v[0] = (u[0] * Math.Cos(teta)) - (u[1] * Math.Sin(teta));
            v[1] = (u[0] * Math.Sin(teta)) + (u[1] * Math.Cos(teta));
            v[2] = u[2];

            return v;
        }
        /****************************************************************************/
        /****************************************************************************/
        static public double[] rotVect(double omega, double[] u, double[] x)

        /* Vector rot_x in the 3D space
          from the rotation of the  vector x around an axis whom u is an unitaty vector.
          The rotation is done with an angle omega in radian. */
        {

            double[] rot_x = new double[3];

            double uscalx;   /* scalar product u.x  */
            double[] uvectx;   /* cross product u^x */

            uscalx = prodScal(u, x);
            uvectx=prodVect(u, x);

            rot_x[0] = ((1 - Math.Cos(omega)) * uscalx * u[0])
                + (Math.Cos(omega) * x[0]) + (Math.Sin(omega) * uvectx[0]);
            rot_x[1] = ((1 - Math.Cos(omega)) * uscalx * u[1])
                + (Math.Cos(omega) * x[1]) + (Math.Sin(omega) * uvectx[1]);
            rot_x[2] = ((1 - Math.Cos(omega)) * uscalx * u[2])
                + (Math.Cos(omega) * x[2]) + (Math.Sin(omega) * uvectx[2]);

            return rot_x;

        }  
           /****************************************************************************/
           /****************************************************************************/
       public static double[] prodVect(double[] u, double[] v)
        /* Cross product of two vector u and v
          in the 3D space. */
        {
            double[] u_vect_v = new double[3];

            u_vect_v[0] = (u[1] * v[2]) - (v[1] * u[2]);
            u_vect_v[1] = (u[2] * v[0]) - (v[2] * u[0]);
            u_vect_v[2] = (u[0] * v[1]) - (v[0] * u[1]);

            return u_vect_v;

        }   
            /****************************************************************************/
            /****************************************************************************/
        static public double[] norm(double[] u)
        /* Normalisation of the vector u in the 3D space.*/
        {

            double[] un = new double[3];
            double norU;
            norU = Math.Sqrt((u[0] * u[0]) + (u[1] * u[1]) + (u[2] * u[2]));
            if (norU < epsilon)
            {
                //throw new Exception("ATTENTION, vecteur nul ! Sa norme vaut : "+norU);
                norU = epsilon;
            }
            else
            {
                un[0] = u[0] / norU;
                un[1] = u[1] / norU;
                un[2] = u[2] / norU;
            }

            return un;

        }  

        /****************************************************************************/
        /****************************************************************************/
        static public double prodScal(double[] u, double[] v)
        /* Scalar product of 2 vectors u and v in the 3D space. */
        {
            double prodScal;
            prodScal = (u[0] * v[0]) + (u[1] * v[1]) + (u[2] * v[2]);
            return (prodScal);
        }


    }
}
