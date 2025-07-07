using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csMTG.RootSystem;
using csMTG.Utilities;

namespace csMTG
{
    public class ArchiSimple : Gramene
    {

        #region Attributes
        double TT = 0;
        double cTT = 0;
        double prop = 0;//9;

        //int cursor { get; set; }
        int axisNumber { get; set; }
        int segmentNb { get; set; }
        int tipNb { get; set; }
        int seedId { get; set; }
        int seedAxisId { get; set; }

        int nbSem { get; set; }
        int nbAdv { get; set; }
        int nbRam { get; set; }

        int nbramCown { get; set; }

        public double RootDM;
        public double RootDMsem;
        public double RootDMadv;
        public double RootDMram;
        public double RootDMram_crown;
        int countday { get; set; }
        int seed { get; set; }

        public List<int> tipVertices { get; set; }

        public Dictionary<int, double> N_dead { get; set; }
        public Dictionary<int, List<Tuple<int,double>>> N_alive { get; set; }
        public Dictionary<int, List<double[]>> Coord_alive { get; set; }
        public Dictionary<string, Dictionary<string, Dictionary<double, double>>> Dist { get; set; }

        public Dictionary<string, Dictionary<string, Dictionary<int, double[]>>> Coord { get; set; }

        public Dictionary<string, Dictionary<string, Dictionary<int, double>>> Prop { get; set; }

        public double rate;
        public double source;
        public double sink;


        #endregion

        #region Constructor

        public ArchiSimple(double[] orgRS = null)
        {
            //RootUtilities.rnd = new Random(Guid.NewGuid().GetHashCode());
            RootUtilities.rnd = new Random(12345);
            //cursor = 0;
            axisNumber = 0;
            segmentNb = 0;
            tipNb = 0;
            seedId = -1;
            seedAxisId = -1;
            prop = 0;//9;

            TT = 0;
            cTT = 0;

            nbSem = 0;
            nbAdv = 0;
            nbRam = 0;
            RootDM = 0;
            nbramCown = 0;

            nbramCown = 0;
            RootDMsem = 0;
            RootDMadv = 0;
            RootDMram = 0;
            RootDMram_crown = 0;

            rate = 0;
            source = 0;
            sink = 0;

            countday = 0;
            //seed = Guid.NewGuid().GetHashCode//Mujica 2022 to activate aleatority 
            seed = 12345; //unactivate aleatority

            tipVertices = new List<int>();

            this.AddRoot(orgRS);
            this.AddSeed(orgRS);

            rld = new Dictionary<double, double>();
            rld_s = new Dictionary<double, double>();
            rld_ram = new Dictionary<double, double>();
            rld_sem = new Dictionary<double, double>();
            rld_nod = new Dictionary<double, double>();
            rld_ram_s = new Dictionary<double, double>();
            rld_sem_s = new Dictionary<double, double>();
            rld_nod_s = new Dictionary<double, double>();

            N_dead = new Dictionary<int, double>();
            N_alive = new Dictionary<int, List<Tuple<int, double>>>();
            Coord_alive = new Dictionary<int, List<double[]>>();

            Dist = new Dictionary<string, Dictionary<string, Dictionary<double, double>>>();

            Coord = new Dictionary<string, Dictionary<string, Dictionary<int, double[]>>>();

            Prop = new Dictionary<string, Dictionary<string, Dictionary<int, double>>>();


            //xmax = new double[3] { 0, 0, 0 };
            //xmin = new double[3] { 0, 0, 0 };
            //ymax = new double[3] { 0, 0, 0 };
            //ymin = new double[3] { 0, 0, 0 };
            //zmax = 0;
            //zp = 0;
            //xmip = 0;
            //ymip = 0;
            //xmap = 0;
            //ymap = 0;
        }


        public ArchiSimple(ArchiSimple toCopy)
        {
            axisNumber = toCopy.axisNumber;
            segmentNb = toCopy.segmentNb;
            tipNb = toCopy.tipNb;
            seedId = toCopy.seedId;
            seedAxisId = toCopy.seedAxisId;

            prop=toCopy.prop;
            TT = toCopy.TT;
            cTT = toCopy.cTT;

            nbSem = toCopy.nbSem;
            nbAdv = toCopy.nbAdv;
            nbRam = toCopy.nbRam;
            seed = toCopy.seed;
            RootDM = toCopy.RootDM;
            nbramCown = toCopy.nbramCown;
            RootDMsem = toCopy.RootDMsem;
            RootDMadv = toCopy.RootDMadv;
            RootDMram = toCopy.RootDMram;
            RootDMram_crown = toCopy.RootDMram_crown;

        rate = toCopy.rate;
            source = toCopy.source;
            sink = toCopy.sink;

            tipVertices = new List<int>();

            countday = toCopy.countday;

            foreach (int iv in toCopy.tipVertices)
            {
                tipVertices.Add(iv);
            }

            // Copy of the scales
            foreach (int vertex in toCopy.scale.Keys)
            {
                if (vertex != 0)
                    this.scale.Add(vertex, (int)toCopy.Scale(vertex));
            }


            // Copy of the complexes
            foreach (int complex in toCopy.complex.Keys)
                this.complex.Add(complex, (int)toCopy.Complex(complex));

            // Copy of the components
            foreach (int components in toCopy.components.Keys)
            {
                /*if (grameneToCopyFrom.Components(complex) != null)*/
                this.components.Add(components, toCopy.components[components]);

                //else this.components = new Dictionary<int, List<int>>();
            }

            // Copy of the parents
            foreach (int child in toCopy.parent.Keys)
            {
                if (child != 0)
                    this.parent.Add(child, (int)toCopy.Parent(child));
            }

            // Copy of the children
            foreach (int parent in toCopy.children.Keys)
                this.children.Add(parent, toCopy.Children(parent));

            // Copy of the properties
            //foreach (string label in grameneToCopyFrom.PropertyNames())
            //{
            //    if (this.properties.ContainsKey(label))
            //        this.properties[label] = grameneToCopyFrom.Property(label);
            //    else
            //        this.properties.Add(label, grameneToCopyFrom.Property(label));
            //}

            this.properties = new Dictionary<string, Dictionary<int, dynamic>>();

            foreach (string name in toCopy.PropertyNames())
            {

                this.properties.Add(name, new Dictionary<int, dynamic>());

                Dictionary<int, dynamic> property = toCopy.Property(name);

                foreach (int node in property.Keys)
                {
                    this.properties[name].Add(node, property[node]);
                }
            }

            this.rld = new Dictionary<double, double>();
            this.rsd = new Dictionary<double, double>();
            rld_s = new Dictionary<double, double>();
            rld_ram = new Dictionary<double, double>();
            rld_sem = new Dictionary<double, double>();
            rld_nod = new Dictionary<double, double>();
            rld_ram_s = new Dictionary<double, double>();
            rld_sem_s = new Dictionary<double, double>();
            rld_nod_s = new Dictionary<double, double>();

            foreach (double dpth in toCopy.rld.Keys)
            {
                rld.Add(dpth, toCopy.rld[dpth]);
                rsd.Add(dpth, toCopy.rsd[dpth]);


            }



            foreach (double dpth in toCopy.rld_s.Keys) rld_s.Add(dpth, toCopy.rld_s[dpth]);
            foreach (double dpth in toCopy.rld_ram.Keys) rld_ram.Add(dpth, toCopy.rld_ram[dpth]);
            foreach (double dpth in toCopy.rld_sem.Keys) rld_sem.Add(dpth, toCopy.rld_sem[dpth]);
            foreach (double dpth in toCopy.rld_nod.Keys) rld_nod.Add(dpth, toCopy.rld_nod[dpth]);
            foreach (double dpth in toCopy.rld_ram_s.Keys) rld_ram_s.Add(dpth, toCopy.rld_ram_s[dpth]);
            foreach (double dpth in toCopy.rld_sem_s.Keys) rld_sem_s.Add(dpth, toCopy.rld_sem_s[dpth]);
            foreach (double dpth in toCopy.rld_nod_s.Keys) rld_nod_s.Add(dpth, toCopy.rld_nod_s[dpth]);



            cursor = toCopy.cursor;
            nbPlants = toCopy.nbPlants;

            labelsOfScales = toCopy.labelsOfScales;

            //xmax = toCopy.xmax;
            //xmin = toCopy.xmin;
            //ymax = toCopy.ymax;
            //ymin = toCopy.xmax;
            //zmax = toCopy.zmax;
            //zp = toCopy.zp;
            //xmip = toCopy.xmip;
            //ymip = toCopy.ymip;
            //xmap = toCopy.xmap;
            //ymap = toCopy.ymap;


            N_dead = new Dictionary<int, double>(toCopy.N_dead);
            N_alive = new Dictionary<int, List<Tuple<int, double>>>(toCopy.N_alive);
            Coord_alive = new Dictionary<int, List<double[]>>(toCopy.Coord_alive);

            Dist = new Dictionary<string, Dictionary<string, Dictionary<double, double>>>(toCopy.Dist);

            Coord = new Dictionary<string, Dictionary<string, Dictionary<int, double[]>>>(toCopy.Coord);
            Prop = new Dictionary<string, Dictionary<string, Dictionary<int, double>>>(toCopy.Prop);




        }

        #endregion


        #region Cursor

        ///// <summary>
        ///// Gets the value of the cursor.
        ///// </summary>
        ///// <returns> Value of the cursor. </returns>
        //public int GetCursor()
        //{
        //    return cursor;
        //}

        ///// <summary>
        ///// Updates the value of the cursor.
        ///// </summary>
        ///// <param name="vertexId"> Vertex identifier on which will be placed the cursor. </param>
        //public void SetCursor(int vertexId)
        //{
        //    cursor = vertexId;
        //}

        #endregion

        #region GetID
        /// <summary>
        /// Retrieve the actual shoot's identifier.
        /// The cursor's scale should be equal to 3.
        /// In case the cursor's scale is lower than 3, the shoot needs to be created first.
        /// In case the cursor's scale is greater than 2, we will iteratively look for the complex until scale 3 is reached.
        /// </summary>
        /// <returns> The identifier of the shoot. </returns>
        int GetRootId(int iv = -1)
        {
            int rootId = (iv == -1) ? cursor : iv;

            if (Scale(rootId) == 3)
            {
                int plantId = (int)Complex(rootId);

                rootId = PlantHasRoot(plantId);

                if (rootId == 0)
                    AddRoot();
            }
            else
            {
                if (Scale(rootId) < 3)
                    AddRoot();
                else
                {
                    rootId = ComplexAtScale(rootId, 3);

                }
            }

            return rootId;

        }

        /// <summary>
        /// Retrieve the current axis' identifier.
        /// The cursor should be on the scale 4.
        /// If it's lower than that, we create a plant and shoot.
        /// If it's greater than that, we iteratively look for the complex at scale 4.
        /// </summary>
        /// <returns> Identifier of the axis. </returns>
        int GetAxisId(int iv = -1)
        {
            int axisId = (iv == -1) ? cursor : iv;

            //if (Scale(cursor) != 4)
            if (Scale(axisId) != 4)
            {
                //if (Scale(cursor) < 4)
                if (Scale(axisId) < 4)
                    axisId = AddAxis();
                else
                    axisId = ComplexAtScale(axisId, 4);
            }

            return axisId;
        }



        #endregion


        #region internal functions

        int TipParent(int tipId)
        {
            int segmentId = 0;

            if (Parent(tipId) != null)
            {
                segmentId = (int)Parent(tipId);
            }

            return segmentId;
        }

        #endregion


        #region Editing functions (AddCanopy, AddPlant,  AddRoot, AddAxis, AddSegment AddTip)

        protected void AddSeed(double[] orgSR)
        {
            int rootId = GetRootId();
            Dictionary<string, dynamic> seedAxislabel = new Dictionary<string, dynamic>();
            seedAxislabel.Add("label", "axis_seed");
            seedAxisId = AddComponent(rootId, seedAxislabel);
            Dictionary<string, dynamic> seedlabel = new Dictionary<string, dynamic>();
            seedlabel.Add("label", "seed");
            seedlabel.Add("origin_seed", orgSR);
            seedId = AddComponent(seedAxisId, seedlabel);
            SetARoot(seedId);
        }


        /// <summary>
        /// Add a root to a plant.
        /// A naming convention would be that the root and the plant will have the same label number.
        /// (e.g: plant0 is decomposed into root0, plant1 into root1 and so on).
        /// </summary>
        /// <returns> The identifier of the root created. </returns>
        protected int AddRoot(double[] orgRS = null)
        {
            int plantId = GetPlantId();

            if (PlantHasRoot(plantId) != 0)
                plantId = AddPlant();


            string plantNb = GetVertexProperties(plantId)["label"].Substring(5);
            double orient = 2.0 * Math.PI * RootUtilities.dRandUnif();

            Dictionary<string, dynamic> rootLabel = new Dictionary<string, dynamic>();
            rootLabel.Add("label", "root" + plantNb);
            rootLabel.Add("edge_type", "/");
            rootLabel.Add("orientation", orient);
            rootLabel.Add("originSR", orgRS);
            rootLabel.Add("volume", 0.0);
            rootLabel.Add("biomass", new List<double>());
            rootLabel.Add("rate", new List<double>());
            rootLabel.Add("mean_rate", 0.0);
            rootLabel.Add("lower_limit", new List<double>());
            rootLabel.Add("upper_limit", new List<double>());

            int rootId = AddComponent(plantId, rootLabel);

            SetCursor(rootId);

            return rootId;

        }

        /// <summary>
        /// Adds an axis to the plant.
        /// If the plant doesn't have a mainstem, it creates one. Its label is: "mainstem".
        /// If the plant already has one, it adds an axis on the mainstem. Its label is: "axis"+number of the axis.
        /// </summary>
        ///<param name="isAddedOnMainStem"> true, if the axes has to be added on mainstem 
        /// false, if added on a other axis. </param>
        ///<param name="parentAxisId"> Optional parameter 
        /// Id of the old axis where the new axis has to be created. </param>
        /// <returns> The identifier of the new axis added. </returns>
        private int AddAxis(int parentAxisId = 0, string axeType = "seminal")
        {
            int axisId;

            //int rootId = -1;
            //if (axeType == "seminal") rootId = GetRootId();

            Dictionary<string, dynamic> axisLabel = new Dictionary<string, dynamic>();
            axisLabel.Add("is_dead", false);

            switch (axeType)
            {
                case "seminal":
                    {
                        axisLabel.Add("label", "axis_sem" + nbSem);
                        axisLabel.Add("edge_type", "<");


                        axisId = AddChild(seedAxisId, axisLabel);
                        //axisId = AddChild(seedId, axisLabel);
                        break;
                    }
                case "adventice":
                    {
                        //int axisNumber = NbChildren(mainstemId) + 1;

                        axisLabel.Add("label", "axis_adv" + "_" + nbAdv);
                        axisLabel.Add("edge_type", "<");

                        axisId = AddChild(parentAxisId, axisLabel);

                        break;
                    }
                case "ramif":
                    {
                        //int axisNumber = NbChildren(mainstemId) + 1;

                        axisLabel.Add("label", "axis_ram" + nbRam);
                        axisLabel.Add("edge_type", "+");

                        axisId = AddChild(parentAxisId, axisLabel);

                        break;
                    }
                default:
                    {
                        axisId = 0;
                        break;
                    }
            }

            axisNumber++;

            SetCursor(axisId);

            return axisId;

        }
        
        /// <summary>
        /// Adds a tip to the current axis.
        /// The new tip is added as a component of the axis.
        /// </summary>
        /// <returns> The identifier of the tip. </returns>
        protected int AddTip(/*int axisId,*/ int parentId, double CumulTT = 0.0, double tipDiam = 0, double[] origin = null, string axeType = "", double[] direction = null, double segLength = 0.0, double DistDaughter = 0)
        {


            Dictionary<string, dynamic> tipLabel = new Dictionary<string, dynamic>();
            //tipLabel.Add("label", "tip" + tipNb);
            //if (!isRamif) tipLabel.Add("edge_type", "<");
            //else tipLabel.Add("edge_type", "+");
            tipLabel.Add("length", 0.0);
            tipLabel.Add("time_tip", 0);
            tipLabel.Add("direction", direction);
            tipLabel.Add("origin_tip", origin);
            tipLabel.Add("growth_direction", direction);
            tipLabel.Add("diametre_tip", tipDiam);
            tipLabel.Add("coordinates", origin);
            tipLabel.Add("distance_prim_initial", 0.0);
            tipLabel.Add("distance_prim_initial_ram", DistDaughter);
            tipLabel.Add("completed", false);
            tipLabel.Add("is_mature", false);
            tipLabel.Add("has_stop", false);
            tipLabel.Add("is_senile", false);
            tipLabel.Add("age", 0);
            tipLabel.Add("is_dead", false);
            tipLabel.Add("seg_length", segLength);
            tipLabel.Add("rate", 0.0);
            tipLabel.Add("TTcreate", CumulTT);


            switch (axeType)
            {
                case "seminal":
                    {

                        if (!tipLabel.ContainsKey("label")) tipLabel.Add("label", "axis_sem" + nbSem);
                        else tipLabel["label"] = "axis_sem" + nbSem;
                        if (!tipLabel.ContainsKey("edge_type")) tipLabel.Add("edge_type", "<");


                        break;
                    }
                case "adventice":
                    {
                        //int axisNumber = NbChildren(mainstemId) + 1;

                        if (!tipLabel.ContainsKey("label")) tipLabel.Add("label", "axis_adv"+ "_" + nbAdv);
                        else tipLabel["label"] = "axis_adv" + nbAdv;
                        if (!tipLabel.ContainsKey("edge_type")) tipLabel.Add("edge_type", "<");


                        break;
                    }
                case "ramif":
                    {
                        //int axisNumber = NbChildren(mainstemId) + 1;


                        if (!tipLabel.ContainsKey("label")) tipLabel.Add("label", "axis_ram" + nbRam);
                        else tipLabel["label"] = "axis_ram" + nbRam;
                        if (!tipLabel.ContainsKey("edge_type")) tipLabel.Add("edge_type", "+");


                        break;
                    }
                case "ramif_killCrown":
                    {
                        //int axisNumber = NbChildren(mainstemId) + 1;

                        nbramCown++;
                        if (!tipLabel.ContainsKey("label")) tipLabel.Add("label", "ramif_killCrown" +"_" + nbRam + "_" + nbramCown);
                        else tipLabel["label"] = "ramif_killCrown_" + nbRam + "_" + nbramCown;
                        if (!tipLabel.ContainsKey("edge_type")) tipLabel.Add("edge_type", "+");


                        break;
                    }
                default:
                    {
                        break;
                    }
            }



            //int tipId = AddChildAndComplex(parentId, -1,axisId, tipLabel)[0];
            int tipId = AddChild(parentId, tipLabel);
            //InsertParent(tipId, axisId);
            //InsertParent(tipId, parentId);


            tipNb++;
            SetCursor(tipId);
            tipVertices.Add(tipId);
            //return tipNb;
            return tipId;
        }

        /// <summary>
        /// Adds a segment to the current tip.
        /// The new segment is added between the last created sgment and the tip.
        /// </summary>
        /// <returns> The identifier of the tip. </returns>
        protected int AddSegment(int tipId, int time = 0, double diam = 0, double[] origin = null, double[] extrem = null, bool isRamif = false)
        {

            Dictionary<string, dynamic> segmentLabel = new Dictionary<string, dynamic>();
            segmentLabel.Add("label", "segment" + segmentNb);
            if (!isRamif) segmentLabel.Add("edge_type", "<");
            else segmentLabel.Add("edge_type", "+");
            segmentLabel.Add("time", time);
            segmentLabel.Add("necrose", false);
            segmentLabel.Add("diametre", diam);
            segmentLabel.Add("origin_position", origin);
            segmentLabel.Add("extrem_position", extrem);
            segmentLabel.Add("is_senescencing", false);
            segmentLabel.Add("is_dead", false);

            //int prevSeg = (int)Parent(tipId);
            //int segmentId = InsertParent(tipId, -1, segmentLabel);
            //AddComponent(GetAxisId(tipId),GetProperties(segmentId), segmentId);
            //AddChild(prevSeg, segmentId);

            segmentNb++;
            ////Console.WriteLine(GetAxisId(segmentId) + " " + time + " " + diam + " " + origine[0] + " " + origine[1] + " " + origine[2] + " " + extrem[0] + " " + extrem[1] + " " + extrem[2]);
            //SetCursor(segmentId);
            //return segmentId;
            return 0;
        }

        protected void Remove(int vertexId, bool reparent)
        {

            int newParentID = RemoveVertex(vertexId, reparent);
            RemoveVertexProperties(vertexId);
            if (reparent) SetCursor(newParentID);

        }

        #endregion

        #region Property utilities

        public dynamic GetProperties(int Id = 0, string prop = null)
        {
            dynamic value = null;
            if (prop != null) value = GetVertexProperties(Id)[prop];
            else value = GetVertexProperties(Id);

            return value;
        }

        public void SetProperties(int Id = 0, string prop = "origin", dynamic valToAdd = null)
        {

            Dictionary<string, dynamic> namesValues = GetVertexProperties(Id);

            if (namesValues.ContainsKey(prop)) namesValues[prop] = valToAdd;
            else namesValues.Add(prop, valToAdd);

            AddVertexProperties(Id, namesValues);

        }

        #endregion


        #region High Level Function


        public void CreateSeminal(int time, double cumulTT/*, double P_EmissionSpeedSem, int P_nbMaxSem*/, int nbSemToEmit,
                                    double P_angInitMeanVertSem, double P_angInitETVertSem,
                                    double P_propDiamSem, double P_diamMax)
        {
            //int nbSemToEmit = RootAxe.CalcNewSeminalNumber(cumulTT, P_EmissionSpeedSem, P_nbMaxSem) - nbSem;

            double[] vInit = new double[3];
            double[] dirInit = new double[3];
            double angRot, angI;


            /* inital axis direction */
            double[] orgSR = GetProperties(seedId, "origin_seed"); //Root System orientattion
            double angDep = GetProperties(GetRootId(seedAxisId), "orientation"); //Root System orientattion

            for (int isem = 0; isem < nbSemToEmit; isem++)
            {

                if (nbSem == 0) angI = RootUtilities.RandGaussien(0.0, 0.06); // émission de la radicule proche verticale (gravitropism initial fort)
                else angI = RootUtilities.RandGaussien(P_angInitMeanVertSem, P_angInitETVertSem); // angle par rapport Ã  la verticale
                vInit[0] = Math.Sin(angI);
                vInit[1] = 0.0;
                vInit[2] = Math.Cos(angI);
                angRot = angDep + RootUtilities.randAngRad(seed);
                dirInit = RootUtilities.rotZ(vInit, angRot);

                //int axisId = AddAxis(seedAxisId, "seminal");
               int id= AddTip(/*axisId,*/seedId, cumulTT, P_propDiamSem * P_diamMax, orgSR, "seminal", dirInit);
                //Console.WriteLine(isem);

                //if (!N_alive.ContainsKey(id)) N_alive.Add(id, new List<Tuple<int, double>>());
                //N_alive[id].Add(new Tuple<int, double>(-777, 0));


                //if (!Coord_alive.ContainsKey(id)) Coord_alive.Add(id, new List<double[]>());
                ///Coord_alive[id].Add(orgSR);

                nbSem++;
            }

        }

        public void CreateAdventice(int time/*,double cumulTT, double P_EmissionSpeedAdv, double P_EmissionAgeAdv, int P_nbMaxAdv*/, int nbAdvToEmit, double P_dBaseMaxAdv, double P_angInitMeanVertAdv, double P_angInitETVertAdv, double P_propDiamAdv, double P_diamMax, double lengthSegNorm, double CumulTT)
        {
            //int nbAdvToEmit = RootAxe.CalcNewAdvNumber(cumulTT, P_EmissionSpeedAdv, P_EmissionAgeAdv, P_nbMaxAdv) - nbAdv;

            //for (int iadv = 0; iadv < nbAdvToEmit; iadv++)
            for (int iadv = 0; iadv < nbAdvToEmit; iadv++)
            {

                double dBaseAdv = RootUtilities.dRandUnif() * P_dBaseMaxAdv;
                double currentDBase = 0;
                double[] vInit = new double[3];
                //int parentAxisId = GetAxisId();
                int segId = seedId;
                //int segId = tipVertices.Min(n => n);
                double angDep = GetProperties(GetRootId(seedAxisId), "orientation");
                double angI = RootUtilities.RandGaussien(P_angInitMeanVertAdv, P_angInitETVertAdv);
                vInit[0] = Math.Sin(angI);
                vInit[1] = 0.0;
                vInit[2] = Math.Cos(angI);
                double angRot = angDep + RootUtilities.randAngRad(seed);
                double[] dirInit = RootUtilities.rotZ(vInit, angRot);

                double[] tipCoor = GetProperties(seedId, "origin_seed");

                //traversal t = new traversal();

                //List<int> apices = tipVertices;
                ////List<int> apices = new List<int>(tipVertices);
                //tipVertices = new List<int>();


                //foreach (int apexv in /*apices*/tipVertices)
                //{

                //double[] tipDir = GetProperties(segId, "direction");
                //    double[] tipCoor = GetProperties(segId, "coordinates");
                double[] tipDaugthCoor = new double[3];
                tipDaugthCoor[0] = tipCoor[0];
                tipDaugthCoor[1] = tipCoor[1];
                tipDaugthCoor[2] = tipCoor[2];
                //int segv = (int)this.Parent(apexv);

                //while (!GetProperties(segv, "label").Contains("seed"))

                while (currentDBase <= dBaseAdv)
                {

                    //string label = GetProperties(segv, "label");

                    //if (label.Contains("segment"))
                    //{
                    //    posE = GetProperties(segv, "extrem_position");
                    //    posO = GetProperties(segv, "origin_position");

                    //currentDBase += Segment.SegLength(posE, posO);

                    //segv = (int)this.Parent(segv);
                    //segId = segv;

                    tipDaugthCoor[0] = tipDaugthCoor[0] + lengthSegNorm * dirInit[0];
                    tipDaugthCoor[1] = tipDaugthCoor[1] + lengthSegNorm * dirInit[1];
                    tipDaugthCoor[2] = tipDaugthCoor[2] + lengthSegNorm * dirInit[2];

                    currentDBase += lengthSegNorm;
                    //if (currentDBase >= dBaseAdv) break;

                    if (tipDaugthCoor[2] < 0) break;
                    //}
                }

                #endregion





                //}
                //double[] posInit = Segment.AdvOrigin(posO, posE, seed);
                double[] posInit = tipDaugthCoor;


                ////int axisId=AddAxis(/*seedAxisId*///parentAxisId, "adventice");
                int id=AddTip(/*axisId,*/ /*segId*/segId, CumulTT, P_propDiamAdv * P_diamMax, posInit, "adventice", dirInit);
                //Console.WriteLine(segId);
                //Console.WriteLine("X: {0} Y: {1} Z: {2}", posInit[0], posInit[1], posInit[2]);

                //if (!N_alive.ContainsKey(id)) N_alive.Add(id, new List<Tuple<int, double>>());
               // N_alive[id].Add(new Tuple<int, double>(-777, 0));


                //if (!Coord_alive.ContainsKey(id)) Coord_alive.Add(id, new List<double[]>());
                //Coord_alive[id].Add(tipCoor);

                
                nbAdv++;
            }

        }
        double prevphase = 0;
        public void GrowRoots(int time, double cumulTT, double dltTT, double P_TMD,
            double lengthSegNorm,
            List<SoilHorizon> soil, int P_DirTropismTrend, double P_TropismIntensity, double soilDepth,
            double creationDelay, double segMinLength,
            double P_diamMin, double P_slopeSpeedDiam,
            List<double> biomMax,
            double P_distRamif, double P_slopeDurationGrowthDiam2, double P_ageMaturityTip, double P_propDiamSem, double P_diamMax, double P_propDiamRamif, double P_coeffVarDiamRamif, double P_angLat,
            double P_slopeDurationLifeDiamTMD, double plantPerMeter, double phase, out double rate_out, double CumulTT, double P_TT1, double P_KN, double P_RamDmin)
        {
            CalcVolBiomassSR(time, dltTT, P_TMD, soil, P_diamMin, P_slopeSpeedDiam, soilDepth);

            List<double> rate = GetProperties(GetRootId(seedAxisId), "rate");
            List<double> Biom = GetProperties(GetRootId(seedAxisId), "biomass");
            double r = rate_out = CalcStatRate(phase, plantPerMeter, time, biomMax, Biom);
            rate.Add(r);
            //double rm = CalcTSatisMeanSR(time, rate);
            //SetProperties(GetRootId(seedAxisId), "mean_rate", rm);
            SetProperties(GetRootId(seedAxisId), "rate", rate);


            Dist = new Dictionary<string, Dictionary<string, Dictionary<double, double>>>();

            Coord = new Dictionary<string, Dictionary<string, Dictionary<int, double[]>>>();

            Prop = new Dictionary<string, Dictionary<string, Dictionary<int, double>>>();

            //List<int> apices = tipVertices;
            List<int> apices = new List<int>(tipVertices);
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {
                //Debug
                //traversal t = new traversal();
                //if (GetProperties(apexv, "is_dead")) continue;
                //foreach (int iv0 in t.IterativePreOrder(this, apexv))
                //{
                //Debug
                int iv = 0;

                GrowAxis(phase,/*Debug*/apexv/*Debug*//*iv0*/, iv, time, cumulTT, dltTT, P_TMD, lengthSegNorm, soil, P_DirTropismTrend, P_TropismIntensity, soilDepth, creationDelay, segMinLength, P_diamMin, P_slopeSpeedDiam, biomMax, P_distRamif, P_slopeDurationGrowthDiam2,
                    P_ageMaturityTip, P_propDiamSem, P_diamMax, P_propDiamRamif, P_coeffVarDiamRamif, P_angLat, plantPerMeter, CumulTT, P_TT1, P_KN, P_RamDmin);

                GrowTip(/*Debug*/apexv/*Debug*//*iv0*/, dltTT, P_ageMaturityTip, P_slopeDurationGrowthDiam2, P_slopeDurationLifeDiamTMD, P_TMD);

                string label = "";
                label = GetProperties(apexv, "label");

                double[] gd = GetProperties(apexv, "growth_direction");
                double a = Math.Acos(gd[2] / Math.Sqrt(gd[0] * gd[0] + gd[1] * gd[1] + gd[2] * gd[2]));

                double[] initd = GetProperties(apexv, "direction");
                double ai = Math.Acos(gd[2] / Math.Sqrt(initd[0] * initd[0] + initd[1] * initd[1] + initd[2] * initd[2]));

                //if (prevphase!=phase) {



                if (label.Contains("ram"))
                {

                    Build_Dist("ram", "Cohort_l", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "length"));
                    Build_Dist("ram", "Cohort_r", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "rate"));
                    Build_Dist("ram", "Cohort_d", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "diametre_tip"));

                    Build_Dist("ram", "CumTT", GetProperties(apexv, "TTcreate"), 30);
                    Build_Dist("ram", "ltip", GetProperties(apexv, "length"), 0.5);
                    Build_Dist("ram", "rtip", GetProperties(apexv, "rate"), 0.5);

                    Build_Dist("ram", "tipTT", GetProperties(apexv, "time_tip"), 25);
                    Build_Dist("ram", "is_sen", (GetProperties(apexv, "is_senile")) ? 1 : 0, 0);
                    Build_Dist("ram", "is_mat", GetProperties(apexv, "is_mature") ? 1 : 0, 0);
                    Build_Dist("ram", "age", GetProperties(apexv, "age"), 25);
                    Build_Dist("ram", "diam", GetProperties(apexv, "diametre_tip"), 0.05);
                    Build_Dist("ram", "distp", GetProperties(apexv, "distance_prim_initial"), 0.5);
                    Build_Dist("ram", "ang", a, 0.05);
                    Build_Dist("ram", "angi", ai, 0.05);
                    Build_Dist("ram", "X", GetProperties(apexv, "coordinates")[0], 10);
                    Build_Dist("ram", "Y", GetProperties(apexv, "coordinates")[1], 10);
                    //Build_Dist("ram", "Z", GetProperties(apexv, "coordinates")[2], 10);

                    //Build_Prop("ram", "tipTT", apexv, GetProperties(apexv, "time_tip"), 1);
                    //Build_Prop("ram", "is_sen", apexv, (GetProperties(apexv, "is_senile")) ? 1 : 0, 1);
                    //Build_Prop("ram", "is_mat", apexv, GetProperties(apexv, "is_mature") ? 1 : 0, 1);
                    //Build_Prop("ram", "age", apexv, GetProperties(apexv, "age"), 1);
                    //Build_Prop("ram", "diam", apexv, GetProperties(apexv, "diametre_tip"), 2);
                    //Build_Prop("ram", "distp", apexv, GetProperties(apexv, "distance_prim_initial"), 2);
                    //Build_Prop("ram", "ang", apexv, a, 4);
                    //Build_Prop("ram", "angi", apexv, ai, 4);

                    //Build_XY("ram", "coord", GetProperties(apexv, "coordinates"), apexv, 4);
                    //Build_XY("ram", "growth_dir", GetProperties(apexv, "growth_direction"), apexv, 4);
                    //Build_XY("ram", "init_dir", GetProperties(apexv, "direction"), apexv, 4);
                }
                else if (label.Contains("adv"))
                {

                    Build_Dist("adv", "Cohort_l", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "length"));
                    Build_Dist("adv", "Cohort_r", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "rate"));
                    Build_Dist("adv", "Cohort_d", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "diametre_tip"));

                    Build_Dist("adv", "CumTT", GetProperties(apexv, "TTcreate"), 30);
                    Build_Dist("adv", "ltip", GetProperties(apexv, "length"), 0.5);
                    Build_Dist("adv", "rtip", GetProperties(apexv, "rate"), 0.5);

                    Build_Dist("adv", "tipTT", GetProperties(apexv, "time_tip"), 25);
                    Build_Dist("adv", "is_sen", (GetProperties(apexv, "is_senile")) ? 1 : 0, 0);
                    Build_Dist("adv", "is_mat", GetProperties(apexv, "is_mature") ? 1 : 0, 0);
                    Build_Dist("adv", "age", GetProperties(apexv, "age"), 25);
                    Build_Dist("adv", "diam", GetProperties(apexv, "diametre_tip"), 0.05);
                    Build_Dist("adv", "distp", GetProperties(apexv, "distance_prim_initial"), 0.5);
                    Build_Dist("adv", "ang", a, 0.05);
                    Build_Dist("adv", "angi", ai, 0.05);
                    Build_Dist("adv", "X", GetProperties(apexv, "coordinates")[0], 10);
                    Build_Dist("adv", "Y", GetProperties(apexv, "coordinates")[1], 10);
                    //Build_Dist("adv", "Z", GetProperties(apexv, "coordinates")[2], 10);

                    //Build_Prop("adv", "tipTT", apexv, GetProperties(apexv, "time_tip"), 1);
                    //Build_Prop("adv", "is_sen", apexv, (GetProperties(apexv, "is_senile")) ? 1 : 0, 1);
                    //Build_Prop("adv", "is_mat", apexv, GetProperties(apexv, "is_mature") ? 1 : 0, 1);
                    //Build_Prop("adv", "age", apexv, GetProperties(apexv, "age"), 1);
                    //Build_Prop("adv", "diam", apexv, GetProperties(apexv, "diametre_tip"), 2);
                    //Build_Prop("adv", "distp", apexv, GetProperties(apexv, "distance_prim_initial"), 2);
                    //Build_Prop("adv", "ang", apexv, a, 4);
                    //Build_Prop("adv", "angi", apexv, ai, 4);

                    //Build_XY("adv", "coord", GetProperties(apexv, "coordinates"), apexv, 4);
                    //Build_XY("adv", "growth_dir", GetProperties(apexv, "growth_direction"), apexv, 4);
                    //Build_XY("adv", "init_dir", GetProperties(apexv, "direction"), apexv, 4);

                }
                else if (label.Contains("sem"))
                {

                    Build_Dist("sem", "Cohort_l", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "length"));
                    Build_Dist("sem", "Cohort_r", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "rate"));
                    Build_Dist("sem", "Cohort_d", GetProperties(apexv, "TTcreate"), 30, GetProperties(apexv, "diametre_tip"));

                    Build_Dist("sem", "CumTT", GetProperties(apexv, "TTcreate"), 30);
                    Build_Dist("sem", "ltip", GetProperties(apexv, "length"), 0.5);
                    Build_Dist("sem", "rtip", GetProperties(apexv, "rate"), 0.5);

                    Build_Dist("sem", "tipTT", GetProperties(apexv, "time_tip"), 25);
                    Build_Dist("sem", "is_sen", (GetProperties(apexv, "is_senile")) ? 1 : 0, 0);
                    Build_Dist("sem", "is_mat", GetProperties(apexv, "is_mature") ? 1 : 0, 0);
                    Build_Dist("sem", "age", GetProperties(apexv, "age"), 25);
                    Build_Dist("sem", "diam", GetProperties(apexv, "diametre_tip"), 0.05);
                    Build_Dist("sem", "distp", GetProperties(apexv, "distance_prim_initial"), 0.5);
                    Build_Dist("sem", "ang", a, 0.05);
                    Build_Dist("sem", "angi", ai, 0.05);
                    Build_Dist("sem", "X", GetProperties(apexv, "coordinates")[0], 10);
                    Build_Dist("sem", "Y", GetProperties(apexv, "coordinates")[1], 10);
                    //Build_Dist("sem", "Z", GetProperties(apexv, "coordinates")[2], 10);



                    //Build_Prop("sem", "tipTT", apexv, GetProperties(apexv, "time_tip"), 1);
                    //Build_Prop("sem", "is_sen", apexv, (GetProperties(apexv, "is_senile")) ? 1 : 0, 1);
                    //Build_Prop("sem", "is_mat", apexv, GetProperties(apexv, "is_mature") ? 1 : 0, 1);
                    //Build_Prop("sem", "age", apexv, GetProperties(apexv, "age"), 1);
                    //Build_Prop("sem", "diam", apexv, GetProperties(apexv, "diametre_tip"), 2);
                    //Build_Prop("sem", "distp", apexv, GetProperties(apexv, "distance_prim_initial"), 2);
                    //Build_Prop("sem", "ang", apexv, a, 4);
                    //Build_Prop("sem", "angi", apexv, ai, 4);



                    //Build_XY("sem", "coord", GetProperties(apexv, "coordinates"), apexv, 4);
                    //Build_XY("sem", "growth_dir", GetProperties(apexv, "growth_direction"), apexv, 4);
                    //Build_XY("sem", "init_dir", GetProperties(apexv, "direction"), apexv, 4);
                }


                //Build_Dist("all", "tipTT", GetProperties(apexv, "time_tip"), 1);
                //Build_Dist("all", "is_sen", (GetProperties(apexv, "is_senile")) ? 1 : 0, 1);
                //Build_Dist("all", "is_mat", GetProperties(apexv, "is_mature") ? 1 : 0, 1);
                //Build_Dist("all", "age", GetProperties(apexv, "age"), 1);
                //Build_Dist("all", "diam", GetProperties(apexv, "diametre_tip"), 2);
                //Build_Dist("all", "distp", GetProperties(apexv, "distance_prim_initial"), 2);
                //Build_Dist("all", "ang", a, 2);
                //Build_Dist("all", "angi", ai, 2);

                //Build_Prop("all", "tipTT", apexv, GetProperties(apexv, "time_tip"), 1);
                //Build_Prop("all", "is_sen", apexv, (GetProperties(apexv, "is_senile")) ? 1 : 0, 1);
                //Build_Prop("all", "is_mat", apexv, GetProperties(apexv, "is_mature") ? 1 : 0, 1);
                //Build_Prop("all", "age", apexv, GetProperties(apexv, "age"), 1);
                //Build_Prop("all", "diam", apexv, GetProperties(apexv, "diametre_tip"), 2);
                //Build_Prop("all", "distp", apexv, GetProperties(apexv, "distance_prim_initial"), 2);
                //Build_Prop("all", "ang", apexv, a, 2);
                //Build_Prop("all", "angi", apexv, ai, 2);


                //Build_XY("all", "coord", GetProperties(apexv, "coordinates"), apexv, 4);
                //Build_XY("all", "growth_dir", GetProperties(apexv, "growth_direction"), apexv, 4);
                //Build_XY("all", "init_dir", GetProperties(apexv, "direction"), apexv, 4);
                //}
                prevphase = phase;
                //Debug  
                //}
                //Debug


                tipVertices.Add(apexv);
            }

        }


        void Build_Dist(string type, string vartype, double k, double pres, double N = 1)
        {
            if (!Dist.ContainsKey(type))
            {
                Dist.Add(type, new Dictionary<string, Dictionary<double, double>>());

            }

            if (!Dist[type].ContainsKey(vartype))
            {
                Dist[type].Add(vartype, new Dictionary<double, double>());
            }

            double kr = 0.0;

            if (vartype == "is_sen" | vartype == "is_mat") { kr = k; }
            else if (k >= k - pres * 0.5 && k < k + pres * 0.5)
            {
                kr = pres * (int)(k / pres) + 0.25 * pres;
            }
            if (!Dist[type][vartype].ContainsKey(kr)) Dist[type][vartype].Add(kr, N);
            else Dist[type][vartype][kr] += N;
        }
        
        void Build_XY(string type, string vartype, double[] k, int apexv, int pres)
        {

            if (!Coord.ContainsKey(type))
            {
                Coord.Add(type, new Dictionary<string, Dictionary<int, double[]>>());

            }

            if (!Coord[type].ContainsKey(vartype))
            {
                Coord[type].Add(vartype, new Dictionary<int, double[]>());
            }


            if (!Coord[type][vartype].ContainsKey(apexv)) Coord[type][vartype].Add(apexv, k);
        }

        void Build_Prop(string type, string vartype, int apexv, double k, int pres)
        {


            if (!Prop.ContainsKey(type))
            {
                Prop.Add(type, new Dictionary<string, Dictionary<int, double>>());

            }

            if (!Prop[type].ContainsKey(vartype))
            {
                Prop[type].Add(vartype, new Dictionary<int, double>());
            }



            if (!Prop[type][vartype].ContainsKey(apexv)) Prop[type][vartype].Add(apexv, k);
        }



        public void RootSenescence(List<SoilHorizon> soil, double plantPerMeter, double soilDepth, double lengthSegNorm, int killCrown,double cumulTT)
        {

            double[] p = new double[7] { 1.5,1.33,1.45,1.35,1.35,1.1,1.15 };
            
            if (killCrown >= 1 )
            {

                
                TT += cumulTT;
                cTT += cumulTT;

                if (TT >= 100)
                //if (TT >= 75)
                {
                    TT = 0;
                    //prop -= 3;
                    prop += 1;
                }
                // prop = Math.Max(2, prop);
                prop = Math.Min(4, prop);
                if (cTT >= 656)
                {
                    prop = 5;
                }
                if (cTT >= 756)
                {
                    prop = 6;
                }
                List<int> apiceskc = new List<int>(tipVertices);
                tipVertices = new List<int>();

                foreach (int apexv in apiceskc)
                {



                        string label = GetProperties(apexv, "label");
                    double sl = GetProperties(apexv, "seg_length");
                    double age_tip = GetProperties(apexv, "age");

                    int pair = 0;
                    if (label.Contains("killCrown"))
                    {
                        pair = Int32.Parse(label.Split('_')[3]);
                    }
                    if (label.Contains("adv"))
                    {
                        pair = Int32.Parse(label.Split('_')[2]);
                    }

                    //int cd = -999;
                    //if (Coord_alive.ContainsKey(apexv)) cd = Coord_alive[apexv].Count;

                        if ( ((label.Contains("killCrown") && pair >= nbramCown- (int)((TT / 100) * (nbramCown /( p[(int)prop])) ))||( label.Contains("adv") && pair >= nbAdv- (int)((TT / 100) * (nbAdv /( p[(int)prop])))))  /*&& ((int)(((decimal)Math.Round((double)(pair/p[(int)prop]),1)%1)*10)==0 || pair==0)/*&& cumulTT <= 1000*/)
                    {

                        if (Coord_alive.ContainsKey(apexv))
                        {

                            double diam = GetProperties(apexv, "diametre_tip");

                            for (int j = 0; j < Coord_alive[apexv].Count; j++)
                            {
                                CalcRootDensity(new double[] { 0, 0, 0 }, Coord_alive[apexv][j], diam, soil, plantPerMeter, soilDepth, sl, GetProperties(apexv, "label"), true, N_alive[apexv][j].Item2);

                            }

                            N_dead.Add(apexv, N_alive[apexv].Count());

                            if (label.Contains("ram"))
                            {


                                Build_Dist("ram", "X_dead", GetProperties(apexv, "coordinates")[0], 10);
                                Build_Dist("ram", "Y_dead", GetProperties(apexv, "coordinates")[1], 10);
                                Build_Dist("ram", "Z_dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);


                            }
                            else if (label.Contains("adv"))
                            {

                                Build_Dist("adv", "X_dead", GetProperties(apexv, "coordinates")[0], 10);
                                Build_Dist("adv", "Y_dead", GetProperties(apexv, "coordinates")[1], 10);
                                Build_Dist("adv", "Z_dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);


                            }
                            else if (label.Contains("sem"))
                            {
                                Build_Dist("sem", "X_dead", GetProperties(apexv, "coordinates")[0], 10);
                                Build_Dist("sem", "Y_dead", GetProperties(apexv, "coordinates")[1], 10);
                                Build_Dist("sem", "Z_dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);


                            }


                            N_alive.Remove(apexv);
                            SetProperties(apexv, "is_dead", true);
                            Coord_alive.Remove(apexv);


                        }


                    }
                    else tipVertices.Add(apexv);
                   
                }

            }
        
                    List<int> apices = new List<int>(tipVertices);
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {

                    bool isSenile = GetProperties(apexv, "is_senile");

                #region Remove Dead roots

                if (IsAxisSenescing(apexv))
                {
                    double diam = GetProperties(apexv, "diametre_tip");


                    string label = GetProperties(apexv, "label");


                    double sl = GetProperties(apexv, "seg_length");



                    if (Coord_alive.ContainsKey(apexv)) { 

                    bool hasRamif = false;


                  
                    for (int j = 0; j < Coord_alive[apexv].Count; j++)
                    {

                            if (N_alive[apexv][j].Item1 > -999)
                            {
                                hasRamif = true;
                                break;
                            }


                    }



                        if (!hasRamif)
                        {

                            for (int j = 0; j < Coord_alive[apexv].Count; j++)
                            {
                                CalcRootDensity(new double[] { 0, 0, 0 }, Coord_alive[apexv][j], diam, soil, plantPerMeter, soilDepth, sl, GetProperties(apexv, "label"), true, N_alive[apexv][j].Item2);

                            }


                            foreach (int apexv0 in apices)
                            {

                                if (Coord_alive.ContainsKey(apexv0)) { 

                                //Console.WriteLine(apexv0);
                                for (int j = 0; j < Coord_alive[apexv0].Count; j++)
                                {


                                    if (N_alive[apexv0][j].Item1 == apexv)
                                    {

                                        N_alive[apexv0][j] = new Tuple<int, double>(-999, -999);

                                    }

                                }
                            }

                            }




                            N_dead.Add(apexv, N_alive[apexv].Count());

                            if (label.Contains("ram"))
                            {


                                Build_Dist("ram", "X_dead", GetProperties(apexv, "coordinates")[0], 10);
                                Build_Dist("ram", "Y_dead", GetProperties(apexv, "coordinates")[1], 10);
                                Build_Dist("ram", "Z_dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);


                            }
                            else if (label.Contains("adv"))
                            {

                                Build_Dist("adv", "X_dead", GetProperties(apexv, "coordinates")[0], 10);
                                Build_Dist("adv", "Y_dead", GetProperties(apexv, "coordinates")[1], 10);
                                Build_Dist("adv", "Z_dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);


                            }
                            else if (label.Contains("sem"))
                            {
                                Build_Dist("sem", "X_dead", GetProperties(apexv, "coordinates")[0], 10);
                                Build_Dist("sem", "Y_dead", GetProperties(apexv, "coordinates")[1], 10);
                                Build_Dist("sem", "Z_dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);


                            }


                            N_alive.Remove(apexv);
                            SetProperties(apexv, "is_dead", true);
                            Coord_alive.Remove(apexv);
                       

                        }
                        else tipVertices.Add(apexv);

                    }
                    else tipVertices.Add(apexv);


                }
                else tipVertices.Add(apexv);


                #endregion

            }
            
        }

        #region Utilities Root Senecsence

        private bool IsAxisSenescing(int tipID)
        {


            return GetProperties(tipID, "is_senile");
        }

        #endregion

        #region Ulitilies Grow Root

        private void GrowAxis(double phase,int apexv, int iv, int time, double cumulTT, double dltTT, double P_TMD,
            double lengthSegNorm,
            List<SoilHorizon> soil, int P_DirTropismTrend, double P_TropismIntensity, double soilDepth,
            double creationDelay, double segMinLenght,
            double P_diamMin, double P_slopeSpeedDiam,
            List<double> biomMax,
            double P_distRamif, double P_slopeDurationGrowDiam2, double P_ageMaturityTip, double P_propDiamSem, double P_diamMax,
            double P_propDiamRamif, double P_coeffVarDiamRamif, double P_angLat, double plantPerMeter, double CumulTT, double P_TT1, double P_KN, double P_RamDmin)
        {

            bool mature = GetProperties(apexv, "is_mature");
            bool stop = GetProperties(apexv, "has_stop");
            bool senile = GetProperties(apexv, "is_senile");
            double diametre = GetProperties(apexv, "diametre_tip");
            double[] coord = GetProperties(apexv, "coordinates");
            List<double> rate = GetProperties(GetRootId(seedAxisId), "rate");
            //double ratem = GetProperties(GetRootId(seedAxisId), "mean_rate");


            if (Math.Abs(coord[0]) <= 1000.0 / 2.0 && Math.Abs(coord[1]) <= 1000.0 / 2.0 && coord[2] < (soilDepth-5.0))
            {
                double age = GetProperties(apexv, "age");
                double tipLength = GetProperties(apexv, "length");
                double elongation = 0;
                //if (Math.Round(P_KN,2) > Math.Round(P_TT1, 2)) elongation = Math.Min(1.0, ((1 / (P_TT1 - P_KN)) * age - (P_KN / (P_TT1 - P_KN))))/**rate[time - 1]*/ * Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diametre, coord);
                //else if (Math.Round(P_TT1, 2) > Math.Round(P_KN, 2)) elongation = Math.Min(1.0, ((1 / (P_KN - P_TT1)) * age - (P_TT1 / (P_KN - P_TT1)))) /** rate[time - 1] **/* Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diametre, coord);
                /*else*/ elongation = rate[time - 1]/*rate[0]*/ * Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diametre, coord);
                //double elongation = ratem * Tip.CalcTipElongation(soil, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diametre, coord);


                tipLength += elongation;
                SetProperties(apexv, "length", tipLength);
                SetProperties(apexv, "rate", rate[time - 1]);
                //SetProperties(apexv, "rate", rate[0]);

                while (tipLength >= lengthSegNorm)
                {

                    //if (GetProperties(apexv, "is_dead")) continue;

                    //Debug
                    //traversal t = new traversal();
                    //foreach (int iv0 in t.IterativePreOrder(this, apexv))
                    //{
                    //Debug
                    double tipTime = GetProperties(apexv, "time_tip");
                    SetProperties(apexv, "time_tip", GetProperties(apexv, "time_tip") + dltTT);
                    if (tipTime > creationDelay)
                    {
                        GrowSegments(/*Debug*/apexv/*Debug*//*iv0*/, iv, time, cumulTT, lengthSegNorm, diametre, P_DirTropismTrend, P_TropismIntensity, soilDepth, soil, plantPerMeter, dltTT, P_RamDmin);
                        RamifAxis(/*Debug*/apexv/*Debug*//*iv0*/, P_diamMin, P_distRamif, P_slopeDurationGrowDiam2, P_ageMaturityTip, P_propDiamSem, P_diamMax, soil, soilDepth, P_propDiamRamif, P_coeffVarDiamRamif, P_angLat, lengthSegNorm, CumulTT, P_RamDmin);
                        //Debug
                        ///}
                        //Debug

                        tipLength = GetProperties(apexv, "length");
                        tipLength -= lengthSegNorm;
                        SetProperties(apexv, "length", tipLength);
                    }
                }


            }

        }

      
        private void GrowTip(int apexv, double dltTT, double P_ageMaturityTip, double P_slopeDurationGrowDiam2, double P_slopeDurationLifeDiamTMD, double P_TMD)
        {
            double age = GetProperties(apexv, "age") + dltTT;
            SetProperties(apexv, "age", age);

            bool mature = GetProperties(apexv, "is_mature");
            bool stop = GetProperties(apexv, "has_stop");
            bool senile = GetProperties(apexv, "is_senile");
            double diametre = GetProperties(apexv, "diametre_tip");

            if ((!mature) && (age > P_ageMaturityTip))
            {
                SetProperties(apexv, "is_mature", true);
                //SetProperties(apexv, "age", 0);

            }

            if ((mature) && (!stop) && (age > (P_ageMaturityTip + P_slopeDurationGrowDiam2 * diametre * diametre)))
            {
                SetProperties(apexv, "has_stop", true);
                //SetProperties(apexv, "age", 0);
            }

            if ((mature) && (stop) && (!senile) && (age > ((P_ageMaturityTip + P_slopeDurationGrowDiam2 * diametre * diametre) + P_slopeDurationLifeDiamTMD * diametre * P_TMD)))////
            {
                SetProperties(apexv, "is_senile", true);
            }

        }

        private void GrowSegments(int apexv, int iv, int time, double cumulTT, double elongation, double diametre, int P_DirTropismTrend, double P_TropismIntensity, double soilDepth, List<SoilHorizon> soil, double plantPerMeter, double dltTT, double P_RamDmin)
        {


            double[] dirInit = GetProperties(apexv, "direction");
            double[] coord = GetProperties(apexv, "coordinates");
            double[] dirGrow0 = GetProperties(apexv, "growth_direction");
            double[] dirGrow = Tip.TipDir(elongation, soil, P_DirTropismTrend, P_TropismIntensity, soilDepth, dirInit, diametre, coord, dirGrow0, seed);
            SetProperties(apexv, "growth_direction", dirGrow);

            ////Console.WriteLine(dirCroiss[0] +" "+ dirCroiss[1] + " " + dirCroiss[2]);

            double distPrimInit = GetProperties(apexv, "distance_prim_initial");
            double distPrimInitout;
            coord = Tip.MovePointe(elongation, dirGrow, coord, distPrimInit, out distPrimInitout);

            ////Console.WriteLine(coord[0] + " " + coord[1] + " " + coord[2]);
            SetProperties(apexv, "distance_prim_initial", distPrimInitout);
            SetProperties(apexv, "coordinates", coord);

            bool isSegComp = GetProperties(apexv, "completed");
            string label = GetProperties(apexv, "label");
            int apParId = TipParent(apexv);
            double[] org = new double[3];

            if (isSegComp)
            {

                org = GetProperties(seedId, "origin_seed");

                double ls = Segment.SegLength(org, coord);
                bool isRamif = (GetProperties(apexv, "edge_type") == "+") ? true : false;
                if (isRamif) SetProperties(apexv, "edge_type", "<");
                double diam = GetProperties(apexv, "diametre_tip");

                SetProperties(apexv, "seg_length", elongation);
                CalcRootDensity(org, coord, diametre, soil, plantPerMeter, soilDepth, elongation,GetProperties(apexv, "label"));

                if (!N_alive.ContainsKey(apexv)) N_alive[apexv] =new List<Tuple<int, double>>();
               N_alive[apexv].Add(new Tuple<int,double>(-999,elongation));



                if (!Coord_alive.ContainsKey(apexv)) Coord_alive.Add(apexv, new List<double[]>());
                Coord_alive[apexv].Add(coord);




                if (label.Contains("ram"))
                {


                    Build_Dist("ram", "Z", GetProperties(apexv, "coordinates")[2], 10, N_alive[apexv].Count());
                    //Build_Dist("ram", "Dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);

                }
                else if (label.Contains("adv"))
                {

                    Build_Dist("adv", "Z", GetProperties(apexv, "coordinates")[2], 10, N_alive[apexv].Count());
                    // Build_Dist("adv", "Dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);

                }
                else if (label.Contains("sem"))
                {

                    Build_Dist("sem", "Z", GetProperties(apexv, "coordinates")[2], 10, N_alive[apexv].Count());
                    // Build_Dist("sem", "Dead", GetProperties(apexv, "coordinates")[2], 10, N_dead[apexv]);

                }



            }
            else
            {
                //org = GetProperties(seedId, "origin_seed");
                SetProperties(apexv, "completed", true);
                //if (!label.Contains("seed")) SetProperties(apParId, "extrem_position", coord);
                //else SetProperties(apParId, "extrem_position", org);
                // SetProperties(TipParent(apexv), "time", time);

            }


        }

        private void RamifAxis(int tipv, double P_diamMin, double P_distRamif, double P_slopeDurationGrowDiam2, double P_ageMaturityTip,
            double P_propDiamSem, double P_diamMax,
            List<SoilHorizon> soil, double SoilDepth,
            double P_propDiamRamif, double P_coeffVarDiamRamif,
            double P_angLat, double lengthSegNorm, double CumulTT, double P_RamDmin)
        {
            //Debug
            bool isRam = IsRamif(tipv, P_diamMin, P_distRamif, P_slopeDurationGrowDiam2, P_ageMaturityTip, GetProperties(/*Debug*/tipv/*Debug*//*apexv*/, "distance_prim_initial"), P_RamDmin);
            //bool isRam = true;
            //Debug
            int tipv0 = tipv;

            //Debug
            //List<int> apices = tipVertices;
            //tipVertices = new List<int>();
            int countR = 0;
            //foreach (int apexv in apices)
            //{

            //traversal t = new traversal();
            //foreach (int iv0 in t.IterativePreOrder(this, RootDMtipv0))
            //{
            while (isRam)
            //for(int i=0;i<2;i++)
            //if (isRam)
            {

                //string label = GetProperties(iv0, "label");

                //if (!label.Contains("tip")) continue;
                //int apexv = iv0;
                //isRam = IsRamif(apexv, P_diamMin, P_distRamif, P_slopeDurationGrowDiam2, P_ageMaturityTip);
                //if (isRam)
                //{

                //Debug
                double distPrim = GetProperties(/*Debug*/tipv0/*Debug*//*apexv*/, "distance_prim_initial");
                double[] coord = GetProperties(/*Debug*/tipv0/*Debug*//*apexv*/, "coordinates");
                double distInterRamP = RootAxe.DistRamifTip(soil, P_distRamif, SoilDepth, coord);
                double nDistPrim = distPrim - distInterRamP;
                ////Console.WriteLine(nDistPrim + " " + distPrim + " " + distInterRamP);
                //Debug
                SetProperties(tipv0, "distance_prim_initial", nDistPrim);

                //Debug
                double diametre = GetProperties(/*Debug*/tipv0/*Debug*//*apexv*/, "diametre_tip");
                double diamRamif = RootAxe.RandomDiamDaugtherTip(P_propDiamRamif, P_diamMin, P_coeffVarDiamRamif, diametre);



                if (diamRamif > P_diamMin)
                {
                    //for (int cr = 0; cr < 2; cr++)
                    //{
                    countR++;
                    ////Console.WriteLine(countR);
                    double[] dirCroiss = GetProperties(/*Debug*/tipv0/*Debug*//*apexv*/, "growth_direction");

                    //double[] orgRam = RootAxe.RamifOrigin(coord, dirCroiss, nDistPrim);
                    double[] dirRam = RootAxe.RamifDirection(P_angLat, dirCroiss, seed);
                    ////Console.WriteLine(orgRam[0] + " " + orgRam[1] + " " + orgRam[2]);
                    //int currentAxisId = GetAxisId(/*Debug*/tipv0/*Debug*//*apexv*/);

                    //int axisId=AddAxis(currentAxisId, "ramif");
                    //List<dynamic> segId = GetRamifSegId(/*distPrim*//*distInterRamP*/nDistPrim, /*Debug*/tipv0/*Debug*//*apexv*/, lengthSegNorm, orgRam);

                    //Debug
                    //AddTip(/*axisId,*/ segId[0], diamRamif, segId[1], dirRam, true);
                    double[] ext = new double[3] { 0.0, 0.0, 0.0 };
                    int sId = seedId;

                    //if (!GetProperties((int)Parent(tipv0), "label").Contains("seed"))
                    {
                        //ext = GetProperties((int)Parent(tipv0), "extrem_position");
                        ext = GetProperties(tipv0, "coordinates");
                        //sId = TipParent(tipv0);

                        string type = "ramif";
                        if (GetProperties(tipv0, "label").Contains("adv")) type = "ramif_killCrown";
                        int idram = AddTip(/*axisId,*/ tipv0, CumulTT, diamRamif, ext, type, dirRam, lengthSegNorm/*, GetProperties(tipv0, "distance_prim_initial")*/);

                        //Console.WriteLine(tipv0);
                        if(!N_alive.ContainsKey(tipv0)) N_alive.Add(tipv0, new List <Tuple<int, double>>());
                        N_alive[tipv0].Add(new Tuple<int,double>(idram,-999));


                        if (!Coord_alive.ContainsKey(tipv0)) Coord_alive.Add(tipv0, new List<double[]>());
                        Coord_alive[tipv0].Add(coord);


                        //Console.WriteLine(tipv0);
                        //if (!N_alive.ContainsKey(idram)) N_alive.Add(idram, new List<Tuple<int, double>>());
                        //N_alive[idram].Add(new Tuple<int, double>(-777, 0));


                        //if (!Coord_alive.ContainsKey(idram)) Coord_alive.Add(idram, new List<double[]>());
                        //Coord_alive[idram].Add(coord);


                        //SetProperties(idram, "distance_prim_initial", GetProperties(tipv0, "distance_prim_initial")+ nDistPrim);
                        nbRam++;
                    }
                    /* else
                     {
                         ext = GetProperties((int)Parent(tipv0), "origin_seed");
                         sId = seedId;
                     }*/
                    //AddTip(/*axisId,*/ sId, diamRamif, ext, dirRam, true);
                    //Debug
                    //Debug
                    //tipv0 = GetCursor();
                    //Debug
                    //nbRam++;
                    //Debug
                    //Console.WriteLine("===== Ramif =====");
                    //Console.WriteLine(nbRam);
                    //Console.WriteLine(ext[0]);
                    //Console.WriteLine(ext[1]);
                    //Console.WriteLine(ext[2]);
                    //Console.WriteLine(nDistPrim);
                    //Console.WriteLine("===== Ramif =====");
                    isRam = IsRamif(tipv0, P_diamMin, P_distRamif, P_slopeDurationGrowDiam2, P_ageMaturityTip, nDistPrim, P_RamDmin);
                    //Debug
                    //}
                }
                //Debug
                //else isRam = false;
                else isRam = true;
                //Debug
            }
            //Debug
            //tipVertices.Add(apexv);
            //Debug
            //}
        }

       /* private List<dynamic> GetRamifSegId(double distPrim, int tipId, double lengthSegNorm, double[] orgSeg)
        {

            List<dynamic> results = new List<dynamic>();
            /*
            int nbseg = (int)Math.Floor((distPrim / lengthSegNorm));
            ////Console.WriteLine(distPrim + " "+ nbseg);
            int id = tipId;
            if (nbseg == 0) id = seedId;
            for (int i = 0; i < nbseg; i++) id = (int)this.Parent(id);
            results.Add(id);
            ////Console.WriteLine(GetProperties(id, "label"));
            */
       /*
            int id = seedId;
            int pId = tipId;
            double pevDist = 0;

            string edge_type = "";
            string prev_edge_type = "";

            while (!GetProperties(pId, "label").Contains("seed"))
            {
                if (prev_edge_type == "+") break;
                prev_edge_type = edge_type;
                pId = (int)Parent(pId);
                if (!GetProperties(pId, "label").Contains("seed")) edge_type = GetProperties(pId, "edge_type");

                if (pId == seedId) continue;
                double[] extSeg = GetProperties(pId, "extrem_position");
                double dist = Math.Sqrt(Math.Pow(extSeg[0] - orgSeg[0], 2) + Math.Pow(extSeg[1] - orgSeg[1], 2) + Math.Pow(extSeg[2] - orgSeg[2], 2));
                if (pevDist > dist && pevDist != 0) continue;
                pevDist = dist;
                id = pId;


            }


            results.Add(id);
            if (!GetProperties(id, "label").Contains("seed")) results.Add(GetProperties(id, "extrem_position"));
            else results.Add(GetProperties(id, "origin_seed"));

            return results;
        }*/


        private bool IsRamif(int tipv, double P_diamMin, double P_distRamif, double P_slopeDurationGrowDiam2, double P_ageMaturityTip, double distPrimInit, double P_RamDmin)
        {
            double diam = GetProperties(tipv, "diametre_tip");
            double age = GetProperties(tipv, "age");
            //double distPrimInit = GetProperties(tipv, "distance_prim_initial");

            return ((diam > P_RamDmin * P_diamMin) && (distPrimInit > P_distRamif) && ((  age) < P_ageMaturityTip+ (P_slopeDurationGrowDiam2 * diam * diam)));

        }

     
        private void CalcVolBiomassSR(double time, double dltTT, double P_TMD,/**/List<SoilHorizon> soil, double P_diamMin, double P_slopeSpeedDiam, double soilDepth)
        {
            double oldVol = GetProperties(GetRootId(seedAxisId), "volume");
            List<double> bioMass = GetProperties(GetRootId(seedAxisId), "biomass");
            //Debug
            //double vol = oldVol;
            double vol = 0.0;
            double vol_sem = 0.0;
            double vol_adv = 0.0;
            double vol_ram = 0.0;
            double vol_ram_crown = 0.0;
            //Debug
            double biom = 0;


            #region PreOrder Iteration

            List<int> apices = new List<int>(tipVertices);
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {
                
                string label = GetProperties(apexv, "label");

                   // if (GetProperties(apexv, "is_dead")) continue;

                    double diam = GetProperties(apexv, "diametre_tip");
                    double l = GetProperties(apexv, "length");
                    // vol += (Math.PI / 4.0) * Math.Pow(diam, 2) * l;

                    double[] coord = GetProperties(apexv, "coordinates");
                    bool mature = GetProperties(apexv, "is_mature");
                    bool stop = GetProperties(apexv, "has_stop");
                    bool senile = GetProperties(apexv, "is_senile");

                if (N_alive.ContainsKey(apexv))
                {

                    for (int j = 0; j < N_alive[apexv].Count; j++)
                    {

                        if (N_alive[apexv][j].Item2 > 0)
                        {

                            vol += (Math.PI / 4.0) * Math.Pow(diam, 2) * N_alive[apexv][j].Item2; /** Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diam, coord, true);*/

                          if(label.Contains("sem"))  vol_sem += (Math.PI / 4.0) * Math.Pow(diam, 2) * N_alive[apexv][j].Item2; /** Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diam, coord, true);*/

                            if (label.Contains("adv")) vol_adv += (Math.PI / 4.0) * Math.Pow(diam, 2) * N_alive[apexv][j].Item2; /** Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diam, coord, true);*/

                            if (label.Contains("ram"))
                            {

                                vol_ram += (Math.PI / 4.0) * Math.Pow(diam, 2) * N_alive[apexv][j].Item2; /** Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diam, coord, true);*/
                            }

                            if (label.Contains("Crown")) vol_ram_crown += (Math.PI / 4.0) * Math.Pow(diam, 2) * N_alive[apexv][j].Item2; /** Tip.CalcTipElongation(soil, dltTT, P_diamMin, P_slopeSpeedDiam, soilDepth, mature, stop, senile, diam, coord, true);*/


                        }

                    }
                    
                }
                tipVertices.Add(apexv);




            }


            #endregion
            RootDM = P_TMD * vol  / 1000.0;
            double dv = vol - oldVol;
            //biom = P_TMD * (vol - oldVol) / 1000.0;
            biom = P_TMD * dv / 1000.0;


            RootDMsem = P_TMD * vol_sem / 1000.0;
            RootDMadv = P_TMD * vol_adv / 1000.0;
            RootDMram = P_TMD * vol_ram / 1000.0;
            RootDMram_crown = P_TMD * vol_ram_crown / 1000.0;




            if (biom < 0.0001) biom = 0.0001;
            ////Console.WriteLine(vol + " " + oldVol + " " + dv);
            bioMass.Add(biom);
            oldVol = vol;
            SetProperties(GetRootId(seedAxisId), "volume", vol);
            SetProperties(GetRootId(seedAxisId), "biomass", bioMass);
        }


        /****************************************************************************/
        /****************************************************************************/
        double CalcStatRate(double phase, double plantPerMeter, int time, List<double> biomMax, List<double> biomUsed)
        {
            double rate1, rate2;
            double biomU0, biomU1, biomU2;  // biomass used at different times (-2, -1, actual)
            double biomD1, biomD2;  // biomass available the day before and now

            //if (biomUsed[0] < 0.0002 || biomMax[0] < 0.0002)
                //if (biomMax[time - 1] <= 1E-10)
                //{
                //    // //Console.WriteLine(1.0);
                //    return 1.0;   // The limitation is not managed, at the begining of the developement for exemple 
                //}
                //else
                {
                // biomU0 = 0.0002;
                // if (time > 1) biomU0 = biomUsed[time - 3];   // biomass used at time step - 2, if any
                //biomU1 = biomUsed[time - 2];    // biomass used at time step - 1
                //if (time > 1) biomU0 = biomUsed[time - 1];   // biomass used at time step - 2, if any

                double sU1 = 0;
                for (int j=0;j<Math.Min(biomUsed.Count,7);j++) {
                    sU1 += biomUsed[time - 1 - j];
                }
                biomU1 = sU1 / Math.Min(biomUsed.Count, 7);

                double sD1 = 0;
                for (int j = 0; j < Math.Min(biomMax.Count, 7); j++)
                {
                    sD1 += biomMax[time - 1 - j];
                }
                biomD1 = sD1 / Math.Min(biomMax.Count, 7);


                // if (time > 1) biomU0 = biomUsed[0];   // biomass used at time step - 2, if any
                //  biomU1 = biomUsed[0];

                /*biomU2 = biomU1 * (1 + ((biomU1 - biomU0) / (0.5 * (biomU0 + biomU1))));   // biomass at time step t (not really known, it is in fact a limitation) 
                */
                //biomD1 = biomMax[0];
                 
                //biomD1 = biomMax[time - 2]; // biomass available at time step -1
                /*biomD2 = biomMax[time - 1];   // biomass available at the present time step
                */
                rate1 = biomD1 / biomU1;

                if (rate1 > 1.0) rate1 = 1.0;
                if(biomD1<1E-10) rate1 = 1.0;

                

                if (phase>3 && biomU1<=0.0001) rate1 = 0;

                //rate2 = biomD2 / biomU2;
                //if (rate2 > 1.0) rate2 = 1.0;
            }
            ////Console.WriteLine(Math.Pow(rate1, 4.0));
            ////Console.WriteLine(rate1);
            //return Math.Pow(rate1, 4.0);
            //return rate1;
            //if (rate1<rate2) { return rate1; }
            //  else { return rate2; }  

            //Console.WriteLine("===== rate1 =====");

           //Console.WriteLine("Source {0} Sink {1}  rate {2}", biomD1, biomU1, rate1);
            //Console.WriteLine("{0} {1}", biomD1, biomU1);

            //Console.WriteLine("===== rate1 =====");

            source = biomD1;
            sink = biomU1;
            rate = rate1;

            return rate1;
            //return 1.0;

        }


        private double CalcTSatisMeanSR(int time, List<double> tSatis)
        {
            /* Calcul of the mean satisfaction rate over the ellpsed period (from 0 to time-1) */

            double tSatisCum = 0.0;

            for (int date = 0; date < time; date++) /* Loop on the ellapsed period */
            {
                tSatisCum += tSatis[date];
            }
            double tSatisMean = tSatisCum / time;

            return tSatisMean;

        }


        #endregion


        #region Utilites Export data root system


        
        public Dictionary<double, double> rld { get; set; }
        public Dictionary<double, double> rld_s { get; set; }
        public Dictionary<double, double> rld_ram { get; set; }
        public Dictionary<double, double> rld_sem { get; set; }
        public Dictionary<double, double> rld_nod { get; set; }
        public Dictionary<double, double> rsd { get; set; }
        public Dictionary<double, double> rld_ram_s { get; set; }
        public Dictionary<double, double> rld_sem_s { get; set; }
        public Dictionary<double, double> rld_nod_s { get; set; }


        public double InitSoil(List<SoilHorizon> soil)
        {

            //Console.WriteLine("********* rld **********");
            //foreach (double d in rld.Keys) Console.WriteLine("depth " + d + " rld " + rld[d]);
            //Console.WriteLine("***********************");

            double dpth = 0.0;

            foreach (SoilHorizon sh in soil)
            {
                dpth += sh.thickness;

                if (!rld.ContainsKey(Math.Round(dpth, 2))) rld.Add(Math.Round(dpth, 2), 0.0);

                if (!rld_s.ContainsKey(Math.Round(dpth, 2))) rld_s.Add(Math.Round(dpth, 2), 0.0);
                if (!rld_ram_s.ContainsKey(Math.Round(dpth, 2))) rld_ram_s.Add(Math.Round(dpth, 2), 0.0);
                if (!rld_sem_s.ContainsKey(Math.Round(dpth, 2))) rld_sem_s.Add(Math.Round(dpth, 2), 0.0);
                if (!rld_nod_s.ContainsKey(Math.Round(dpth, 2))) rld_nod_s.Add(Math.Round(dpth, 2), 0.0);
                if (!rld_ram.ContainsKey(Math.Round(dpth, 2))) rld_ram.Add(Math.Round(dpth, 2), 0.0);
                if (!rld_sem.ContainsKey(Math.Round(dpth, 2))) rld_sem.Add(Math.Round(dpth, 2), 0.0);
                if (!rld_nod.ContainsKey(Math.Round(dpth, 2))) rld_nod.Add(Math.Round(dpth, 2), 0.0);

                if (!rsd.ContainsKey(Math.Round(dpth, 2))) rsd.Add(Math.Round(dpth, 2), 0.0);


            }
            return dpth;

        }




        //public Dictionary<double,double> CalcRootLengthDensity(List<SoilHorizon> soil,double plantPerMeter, double soilDepth, out double surfSR)
        public void CalcRootDensity(double[] posO, double[] posE, double diam, List<SoilHorizon> soil, double plantPerMeter, double soilDepth/*, out double surfSR*/, double lengthSegNorm, string type, bool is_dead = false, double n_dead = 0)
        {


            double thick = soil[0].thickness; //m


            double dpth = Math.Round(soilDepth, 2) / 1000.0;//mm->m

            /*            double vol = 0.0;

                                vol = thick * 1.0 * 1.0;//m ^3*/

            double d = Math.Min(Math.Round(dpth, 2), Math.Round((int)(posE[2] / (1000.0 * thick)) * thick + thick, 2));


            /*------------------------------------------------------------------------
             * --------------------------------------------------------------------*/

            //---------------------------------------------------------------------------------------------------
            //   Mujica 2022
            //
            //rld should be in cm/m2 /lengthSegNorm is in mm so I just shoul divide by 10 to have centimeters instead 10000000.0     <<<<<<<<<<<<<<<<<<<<!!!!
            //
            //plantPermeter=300 is seeds per square meter
            //----------------------------------------------------------------------------------------------------------
            if (d > dpth-0.05)
            {
                d = Math.Round(dpth-0.05,2);

            }
            if (!is_dead)
            {
                rld[d] += plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) * (lengthSegNorm / 10.0);//Mujica 2022
                rsd[d] += plantPerMeter * Math.PI * (diam / 10) / (100.0 * 100.0 * (thick * 100.0)) * (lengthSegNorm / 10.0);//Mujica 2022

               if(type.Contains("ram")) rld_ram[d] += plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) * (lengthSegNorm / 10.0);//Mujica 2022
               if (type.Contains("sem")) rld_sem[d] += plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) * (lengthSegNorm / 10.0);//Mujica 2022
               if (type.Contains("adv")) rld_nod[d] += plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) * (lengthSegNorm / 10.0);//Mujica 2022

            }
            else
            {
                if(n_dead>0) rld[d] -= (n_dead/10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022
                if (n_dead>0) rsd[d] -= (n_dead/10.0) * plantPerMeter * Math.PI * (diam / 10) / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022

                if (type.Contains("ram") && n_dead > 0) rld_ram[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022
                if (type.Contains("sem") && n_dead > 0) rld_sem[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022
                if (type.Contains("adv") && n_dead > 0) rld_nod[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022

                if (n_dead > 0) rld_s[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022

                if (type.Contains("ram") && n_dead > 0) rld_ram_s[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022
                if (type.Contains("sem") && n_dead > 0) rld_sem_s[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022
                if (type.Contains("adv") && n_dead > 0) rld_nod_s[d] -= (n_dead / 10.0) * plantPerMeter / (100.0 * 100.0 * (thick * 100.0)) /** (lengthSegNorm / 10.0)*/; //Mujica 2022

            }

            rld_s[d] = Math.Min(rld_s[d], 0);
            rld_ram_s[d] = Math.Min(rld_ram_s[d], 0);
            rld_sem_s[d] = Math.Min(rld_sem_s[d], 0);
            rld_nod_s[d] = Math.Min(rld_nod_s[d], 0);

            rld_ram[d] = Math.Max(rld_ram[d], 0);
            rld_sem[d] = Math.Max(rld_sem[d], 0);
            rld_nod[d] = Math.Max(rld_nod[d], 0);

            rld[d] = Math.Max(rld[d], 0);
            rsd[d] = Math.Max(rsd[d], 0);

        }





        #region Utilities

        private double distHorSeg(double[] posE, double[] posO)
        /* Calcul the horizotal distance of a segment */
        {
            return Math.Sqrt(((posE[0] + posO[0]) * (posE[0] + posO[0]) / 4.0) +
                        ((posE[1] + posO[1]) * (posE[1] + posO[1]) / 4.0));

        }

        #endregion


        #endregion


    }
}
