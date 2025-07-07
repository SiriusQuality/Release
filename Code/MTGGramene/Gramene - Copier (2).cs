using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csMTG.RootSystem;
using csMTG.Utilities;

namespace csMTG
{
    public class Gramene : mtg
    {
        #region Attributes

        public Dictionary<int, string> labelsOfScales;

        int cursor = 0;
        int nbPlants = 0;
        int axisNumber = 0;
        int segmentNb = 0;
        int tipNb = 0;
        int seedId=-1;
        int seedAxisId = -1;

        int nbSem = 0;
        int nbAdv = 0;
        int nbRam = 0;

        int countday = 0;
        int seed;

        public Dictionary<string, List<double>> propertiesToAdd { get; set; }
        public List<int> tipVertices = new List<int>();




        #endregion

        #region Constructor and copy constructor

        /// <summary>
        /// Constructor of Gramene:
        /// * Sets the labels of the 6 scales of an mtg.
        /// </summary>
        public Gramene()
        {
            // define the 6 scales
            RootUtilities.rnd = new Random(Guid.NewGuid().GetHashCode());
            labelsOfScales = new Dictionary<int, string>();
            labelsOfScales.Add(1, "canopy");
            labelsOfScales.Add(2, "plant");
            labelsOfScales.Add(3, "root_system");
            labelsOfScales.Add(4, "axis");
            labelsOfScales.Add(5, "root");

        }


        /// <summary>
        /// Copy constructor of gramene.
        /// It copies the values of: scale, complex, components, parent, children, properties and the class attributes
        /// </summary>
        /// <param name="grameneToCopyFrom"> The gramene we want to copy from. </param>
        /// <param name="copyAll"></param>
        public Gramene(Gramene grameneToCopyFrom, bool copyAll = true)
        {
            // Copy of the scales
            foreach (int vertex in grameneToCopyFrom.scale.Keys)
            {
                if (vertex != 0)
                    this.scale.Add(vertex, (int)grameneToCopyFrom.Scale(vertex));
            }


            // Copy of the complexes
            foreach (int component in grameneToCopyFrom.complex.Keys)
                this.complex.Add(component, (int)grameneToCopyFrom.Complex(component));

            // Copy of the components
            foreach (int complex in grameneToCopyFrom.components.Keys)
                this.components.Add(complex, grameneToCopyFrom.Components(complex));

            // Copy of the parents
            foreach (int child in grameneToCopyFrom.parent.Keys)
            {
                if (child != 0)
                    this.parent.Add(child, (int)grameneToCopyFrom.Parent(child));
            }

            // Copy of the children
            foreach (int parent in grameneToCopyFrom.children.Keys)
                this.children.Add(parent, grameneToCopyFrom.Children(parent));

            // Copy of the properties
            //foreach (string label in grameneToCopyFrom.PropertyNames())
            //{
            //    if (this.properties.ContainsKey(label))
            //        this.properties[label] = grameneToCopyFrom.Property(label);
            //    else
            //        this.properties.Add(label, grameneToCopyFrom.Property(label));
            //}

            this.properties = new Dictionary<string, Dictionary<int, dynamic>>();

            foreach (string name in grameneToCopyFrom.PropertyNames())
            {

                this.properties.Add(name, new Dictionary<int, dynamic>());

                Dictionary<int, dynamic> property = grameneToCopyFrom.Property(name);

                foreach (int node in property.Keys)
                {
                    this.properties[name].Add(node, property[node]);
                }
            }

            cursor = grameneToCopyFrom.cursor;
            nbPlants = grameneToCopyFrom.nbPlants;

            axisNumber = grameneToCopyFrom.axisNumber;
            segmentNb = grameneToCopyFrom.segmentNb;
            tipNb = grameneToCopyFrom.tipNb;
            seedId = grameneToCopyFrom.seedId;
            seedAxisId = grameneToCopyFrom.seedAxisId;

            nbSem = grameneToCopyFrom.nbSem;
            nbAdv = grameneToCopyFrom.nbAdv;
            nbRam = grameneToCopyFrom.nbRam;
            seed = grameneToCopyFrom.seed;

            tipVertices = new List<int>();

            countday = grameneToCopyFrom.countday;

            foreach (int iv in grameneToCopyFrom.tipVertices)
            {
                tipVertices.Add(iv);
            }

            labelsOfScales = grameneToCopyFrom.labelsOfScales;

    }


        #endregion

        #region Cursor

        /// <summary>
        /// Gets the value of the cursor.
        /// </summary>
        /// <returns> Value of the cursor. </returns>
        public int GetCursor()
        {
            return cursor;
        }

        /// <summary>
        /// Updates the value of the cursor.
        /// </summary>
        /// <param name="vertexId"> Vertex identifier on which will be placed the cursor. </param>
        public void SetCursor(int vertexId)
        {
            cursor = vertexId;
        }

        /// <summary>
        /// Retrieve the identifier of the canopy based on the position of the cursor.
        /// The cursor is set to the last item visited. If it's in the right scale (1), it is equal to the canopy's id.
        /// Otherwise, we will iteratively look for the complex of the cursor until scale 1 is reached.
        /// </summary>
        /// <returns> The identifier of the canopy. </returns>
        int GetCanopyId()
        {
            int canopyCursor = cursor;

            while (Scale(canopyCursor) != 1 && canopyCursor != 0)
            {
                canopyCursor = (int)Complex(canopyCursor);
            }

            if (canopyCursor == 0)
                canopyCursor = AddCanopy();

            return canopyCursor;
        }

        /// <summary>
        /// Retrieve the actual plant's identifier.
        /// The cursor's scale should be equal to 2.
        /// In case the cursor's scale is lower than 2, the plant needs to be created first.
        /// In case the cursor's scale is greater than 2, we will iteratively look for the complex until scale 2 is reached.
        /// </summary>
        /// <returns> The identifier of the plant. </returns>
        int GetPlantId()
        {
            int plantId;

            if (Scale(cursor) == 2)
                plantId = cursor;
            else
            {
                if (Scale(cursor) < 2)
                    plantId = AddPlant();
                else
                {
                    plantId = ComplexAtScale(cursor, 2);

                }
            }

            return plantId;
        }

        /// <summary>
        /// Retrieve the actual shoot's identifier.
        /// The cursor's scale should be equal to 3.
        /// In case the cursor's scale is lower than 3, the shoot needs to be created first.
        /// In case the cursor's scale is greater than 2, we will iteratively look for the complex until scale 3 is reached.
        /// </summary>
        /// <returns> The identifier of the shoot. </returns>
        int GetRootId(int iv=-1)
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

        /// <summary>
        /// Retrieve the current segment's identifier.
        /// The cursor should be on scale 5.
        /// If it's lower than that, we create a new segment on the same axis.
        /// If it's greater than that, we iteratively look for the complex at scale 5.
        /// </summary>
        /// <returns> Identifier of the segment. </returns>
        //int GetTipId()
        //{
        //    int tipId = cursor;

        //    if (Scale(tipId) != 5)
        //    {
        //        if (Scale(tipId) > 5)
        //            tipId = ComplexAtScale(tipId, 5);
        //        else
        //            tipId = AddSegment();
        //    }

        //    return tipId;
        //}

        #endregion

        #region Property utilities

        public dynamic GetProperties(int Id = 0, string prop = null)
        {
            dynamic value = null;
           if(prop!=null) value = GetVertexProperties(Id)[prop];
           else value = GetVertexProperties(Id);

            return value;
        }

        public dynamic GetVertexMax(string prop = null)
        {
            double[] value = new double[3];

            if (Properties().ContainsKey(prop))
            {
                List<double> lx = new List<double>();
                List<double> ly = new List<double>();
                List<double> lz = new List<double>();

                foreach (double[] val in this.Properties()[prop].Values)
                {
                    lx.Add(Math.Max(-1E10,val[0]));
                    ly.Add(Math.Max(-1E10, val[1]));
                    lz.Add(Math.Max(-1E10, val[2]));
                }

                value[0] = lx.Max();
                value[1] = ly.Max();
                value[2] = lz.Max();

                return value;
            }
            else
            {
                value[0] = -1E10;
                value[1] = -1E10;
                value[2] = -1E10;

                return value;
            }
        }


        public dynamic GetVertexMin(string prop = null)
        {
            double[] value = new double[3];


            if (Properties().ContainsKey(prop))
            {
                List<double> lx = new List<double>();
                List<double> ly = new List<double>();
                List<double> lz = new List<double>();

                foreach (double[] val in this.Properties()[prop].Values)
                {
                    lx.Add(Math.Min(1E10, val[0]));
                    ly.Add(Math.Min(1E10, val[1]));
                    lz.Add(Math.Min(1E10, val[2]));
                }

                value[0] = lx.Min();
                value[1] = ly.Min();
                value[2] = lz.Min();

                return value;
            }
            else
            {
                value[0] = 1E10;
                value[1] = 1E10;
                value[2] = 1E10;

                return value;
            }
        }

        public void SetProperties(int Id = 0, string prop = "origine", dynamic valToAdd = null)
        {

            Dictionary<string, dynamic> namesValues = GetVertexProperties(Id);

            if (namesValues.ContainsKey(prop)) namesValues[prop] = valToAdd;
            else namesValues.Add(prop, valToAdd);

            AddVertexProperties(Id, namesValues);

        }

        #endregion


        /// <summary>
        /// Returns a list containing the identifiers of all plants.
        /// It is to note that plants are in scale number 2.
        /// </summary>
        /// <returns></returns>
        public List<int> Plants()
        {
            return Vertices(2);
        }


        #region Internal functions

        /// <summary>
        /// Checks if the plants already has a root.
        /// </summary>
        /// <param name="plantId"> The plant to verify. </param>
        /// <returns> The identifier of the root if found. If not, it returns zero. </returns>
        int PlantHasRoot(int plantId)
        {
            int rootExists = 0;

            if (Components(plantId).Count > 0)
            {
                foreach (int component in Components(plantId))
                {
                    if (GetVertexProperties(component)["label"].Substring(0, 4) == "root")
                        rootExists = component;
                }
            }

            return rootExists;
        }

        /// <summary>
        /// Checks if the axis already has a tip.
        /// </summary>
        /// <param name="plantId"> The axis to verify. </param>
        /// <returns> The identifier of the tip if found. If not, it returns zero. </returns>
        protected int AxisHasTip(int axisId)
        {
            int tipExists = 0;

            if (Components(axisId).Count > 0)
            {
                foreach (int component in Components(axisId))
                {
                    if (GetVertexProperties(component)["label"].Contains("tip"))
                        tipExists = component;
                }
            }

            return tipExists;
        }

        /// <summary>
        /// Checks if the tip already has an segment or not.
        /// </summary>
        /// <param name="tipId"> Identifier of the tip. </param>
        /// <returns> Identifier of the segment if found. If not, it returns zero. </returns>
        int TipHasSegement(int tipId)
        {
            int segmentId = 0;

            if (Children(tipId).Count > 0)
            {
                foreach (int children in Children(tipId))
                {
                    if (GetVertexProperties(children)["label"].Length > 7)
                    {
                        string label = GetVertexProperties(children)["label"].Substring(0, 7);

                        if (label == "segment")
                        {
                            segmentId = children;
                        }
                    }
                }
            }

            return segmentId;
        }

        int TipParent(int tipId)
        {
            int segmentId = 0;

            if (Parent(tipId)!=null)
            {
                segmentId = (int)Parent(tipId);
            }

            return segmentId;
        }

        /// <summary>
        /// Checks if the tip already has an segment or not.
        /// </summary>
        /// <param name="tipId"> Identifier of the segment. </param>
        /// <returns> Identifier of the internode if found. If not, it returns zero. </returns>
        int SegmentHasSegement(int segmentId)
        {
            int segmentIdParent = 0;

            if (Components(segmentId).Count > 0)
            {
                foreach (int component in Components(segmentId))
                {
                    if (GetVertexProperties(component)["label"].Length > 7)
                    {
                        string label = GetVertexProperties(component)["label"].Substring(0, 7);

                        if (label == "segment")
                            segmentIdParent = component;
                    }
                }
            }

            return segmentIdParent;
        }

        #endregion

        #region Editing functions (AddCanopy, AddPlant,  AddRoot, AddAxis, AddSegment AddTip)

        /// <summary>
        /// Adds a canopy which will contain all plants.
        /// </summary>
        /// <param name="label"> Optional parameter. 
        /// Specified in case all plants are of the same botanical variety. </param>
        /// <returns> Identifier of the canopy added. </returns>
        protected int AddCanopy(string label = "canopy")
        {
            int canopy;
            Dictionary<string, dynamic> canopyLabel = new Dictionary<string, dynamic>();

            canopyLabel.Add("label", label);

            canopy = AddComponent(0, canopyLabel);

            SetCursor(canopy);

            return canopy;
        }

        /// <summary>
        /// Add a plant to the canopy.
        /// It is to note that the plant is labelled plant+number of the plant (e.g: plant0, plant1, ..).
        /// </summary>
        /// <returns> Identifier of the plant. </returns>
        protected int AddPlant()
        {

            int canopy = GetCanopyId();

            Dictionary<string, dynamic> plantLabel = new Dictionary<string, dynamic>();
            plantLabel.Add("label", "plant" + nbPlants);
            plantLabel.Add("edge_type", "/");

            int plantId = AddComponent(canopy, namesValues: plantLabel);

            nbPlants++;

            SetCursor(plantId);

            return plantId;
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

            seed = Guid.NewGuid().GetHashCode();
            string plantNb = GetVertexProperties(plantId)["label"].Substring(5);
            double orient = 2.0 * Math.PI * RootUtilities.dRandUnif(seed);

            Dictionary<string, dynamic> rootLabel = new Dictionary<string, dynamic>();
            rootLabel.Add("label", "root" + plantNb);
            rootLabel.Add("edge_type", "/");
            rootLabel.Add("orientation", orient);
            rootLabel.Add("origineSR", orgRS);
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

            switch (axeType) {
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

                        axisLabel.Add("label", "axis_adv" + nbAdv);
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
        protected int AddTip(/*int axisId,*/ int parentId, double tipDiam = 0, double[] origine = null, double[] direction = null,bool isRamif=false)
        {

           /// int axisId = GetAxisId();

            //int axisHasTip = AxisHasTip(axisId);

            Dictionary<string, dynamic> tipLabel = new Dictionary<string, dynamic>();
            tipLabel.Add("label", "tip" + tipNb);
            if(!isRamif) tipLabel.Add("edge_type", "<");
            else tipLabel.Add("edge_type", "+");
            tipLabel.Add("length", 0.0);
            tipLabel.Add("time_tip", 0);
            tipLabel.Add("direction", direction);
            tipLabel.Add("origine_tip", origine);
            tipLabel.Add("direction_croissance", direction);
            tipLabel.Add("diametre_tip", tipDiam);
            tipLabel.Add("coordinates", origine);
            tipLabel.Add("distance_prim_initial", 0.0);
            tipLabel.Add("completed", false);
            tipLabel.Add("is_mature", false);
            tipLabel.Add("has_stop", false);
            tipLabel.Add("is_senile", false);
            tipLabel.Add("age", 0);
            tipLabel.Add("is_dead", false);


            //int tipId = AddChildAndComplex(parentId, -1,axisId, tipLabel)[0];
            int tipId = AddChild(parentId, tipLabel);
            //InsertParent(tipId, axisId);
            //InsertParent(tipId, parentId);


            tipNb++;
            SetCursor(tipId);
            tipVertices.Add(tipId);
            return tipId;
        }

        /// <summary>
        /// Adds a segment to the current tip.
        /// The new segment is added between the last created sgment and the tip.
        /// </summary>
        /// <returns> The identifier of the tip. </returns>
        protected int AddSegment(int tipId, int time = 0, double diam = 0, /*double[] origine = null,*/ double[] extrem = null, bool isRamif=false)
        {

            //int tipId = GetTipId();
           // int parentSegID = TipHasSegement(tipId);

            Dictionary<string, dynamic> segmentLabel = new Dictionary<string, dynamic>();
            segmentLabel.Add("label", "segment" + segmentNb);
            if(!isRamif) segmentLabel.Add("edge_type", "<");
            else segmentLabel.Add("edge_type", "+");
            segmentLabel.Add("time", time);
            segmentLabel.Add("necrose", false);
            segmentLabel.Add("diametre", diam);
            //segmentLabel.Add("origine_position", origine);
            segmentLabel.Add("extrem_position", extrem);
            segmentLabel.Add("is_senescencing", false);
            segmentLabel.Add("is_dead", false);

            int prevSeg=(int)Parent(tipId);
            int segmentId = InsertParent(tipId, -1, segmentLabel);
            //AddComponent(GetAxisId(tipId),GetProperties(segmentId), segmentId);
            AddChild(prevSeg, segmentId);

            segmentNb++;
            //Console.WriteLine(GetAxisId(segmentId) + " " + time + " " + diam + " " + origine[0] + " " + origine[1] + " " + origine[2] + " " + extrem[0] + " " + extrem[1] + " " + extrem[2]);
            SetCursor(segmentId);
            return segmentId;
        }

        protected void Remove(int vertexId, bool reparent)
        {

            int newParentID = RemoveVertex(vertexId, reparent);
            RemoveVertexProperties(vertexId);
            if(reparent) SetCursor(newParentID);

        }

        protected void AddSeed(double[] orgSR)
        {
            int rootId = GetRootId();
            Dictionary<string, dynamic> seedAxislabel = new Dictionary<string, dynamic>();
            seedAxislabel.Add("label", "axis_seed");
            seedAxisId = AddComponent(rootId, seedAxislabel);
            Dictionary<string, dynamic> seedlabel = new Dictionary<string, dynamic>();
            seedlabel.Add("label", "seed");
            seedlabel.Add("origine_seed", orgSR);
            seedId = AddComponent(seedAxisId, seedlabel);
        }

        #endregion

        #region High level functions (Wheat)

        /// <summary>
        /// A function that the final user can use to create a Wheat plant composed of a number of leaves they will choose.
        /// </summary>
        /// <param name="NbLeaves"> The number of leaves desired in the plant. </param>
        /// <returns> The plant structure. </returns>
        public void CreateBasicWheat(double[] orgSR)
        {
            this.AddCanopy("wheat");
            this.AddPlant();
            this.AddRoot(orgSR);
            this.AddSeed(orgSR);


        }

        public void CreateSeminal(int temps, double P_vitEmissionSem, int P_nbMaxSem,
                                    double P_angInitMoyVertSem, double P_angInitETVertSem,
                                    double P_propDiamSem, double P_diamMax)
        {
            int nbSemToEmit = RootAxe.calcNouvNbSem(temps, P_vitEmissionSem, P_nbMaxSem) - nbSem;

            double[] vInit = new double[3];
            double[] dirInit = new double[3];
            double angRot, angI;

            /* Calcul de la direction initiale de l'axe */
            double[] orgSR = GetProperties(seedId, "origine_seed"); //Root System orientattion
            double angDep = GetProperties(GetRootId(seedAxisId), "orientation"); //Root System orientattion

            for (int isem = 0; isem < nbSemToEmit; isem++)
            {
                //seed = Guid.NewGuid().GetHashCode();

                if (nbSem == 0) angI = RootUtilities.tireGaussien(0.0, 0.06,seed); // émission de la radicule proche verticale (gravitropisme initial fort)
                else angI = RootUtilities.tireGaussien(P_angInitMoyVertSem, P_angInitETVertSem,seed); // angle par rapport Ã  la verticale
                vInit[0] = Math.Sin(angI);
                vInit[1] = 0.0;
                vInit[2] = Math.Cos(angI);
                angRot = angDep + RootUtilities.tireAngRad(seed);
                dirInit = RootUtilities.rotZ(vInit, angRot);

                //int axisId = AddAxis(seedAxisId, "seminal");
                AddTip(/*axisId,*/seedId,P_propDiamSem * P_diamMax, orgSR, dirInit);


                nbSem++;
            }

        }

        public void CreateAdventice(int temps, double P_vitEmissionAdv, double P_ageEmissionAdv, int P_nbMaxAdv, double P_dBaseMaxAdv, double P_angInitMoyVertAdv, double P_angInitETVertAdv, double P_propDiamAdv, double P_diamMax)
        {
            int nbAdvToEmit = RootAxe.calcNouvNbAdv(temps, P_vitEmissionAdv, P_ageEmissionAdv, P_nbMaxAdv) - nbAdv;

            for (int iadv = 0; iadv < nbAdvToEmit; iadv++)
            {
                //seed = Guid.NewGuid().GetHashCode();

                double dBaseAdv = RootUtilities.dRandUnif(seed) * P_dBaseMaxAdv;
                double currentDBase = 0;
                double[] vInit = new double[3];
                //int parentAxisId = GetAxisId();

                double[] posE = new double[3];
                double[] posO = new double[3];

                traversal t = new traversal();

                List<int> apices = tipVertices;
                tipVertices = new List<int>();
                List<int> VerticesVisisted = new List<int>();

                //foreach (int tipID in apices)
                //{

                //List<int> ancestorsTip = new List<int>(Ancestors(tipID));

                //foreach (int iv in ancestorsTip)
                foreach (int iv in t.IterativePreOrder(this, seedId))
                {
                    //if (VerticesVisisted.Contains(iv)) continue; 
                    ///VerticesVisisted.Add(iv);

                        int parentiv = (int)this.Parent(iv);
                        string label = GetProperties(iv, "label");
                        

                        if (label.Contains("segment"))
                        {
                            posE = GetProperties(iv, "extrem_position");

                        string labelParent = GetProperties(parentiv, "label");
                        if (labelParent.Contains("segment")) posO = GetProperties(parentiv, "extrem_position");
                        else if (labelParent.Contains("seed")) posO = GetProperties(seedId, "origine_seed");

                            //parentAxisId = GetAxisId(iv);
                            currentDBase += Segment.longSeg(posE, posO);

                            if (currentDBase >= dBaseAdv) break;
                        }

                    //}
                    //tipVertices.Add(tipID);

                    #endregion
                }
                
                double[] posInit=Segment.origineAdv(posO, posE,seed);
                double angI = RootUtilities.tireGaussien(P_angInitMoyVertAdv, P_angInitETVertAdv, seed);
                vInit[0] = Math.Sin(angI);
                vInit[1] = 0.0;
                vInit[2] = Math.Cos(angI);
                double angRot = RootUtilities.tireAngRad(seed);
                double[] dirInit=RootUtilities.rotZ(vInit,angRot);

                //int axisId=AddAxis(/*seedAxisId*/parentAxisId, "adventice");
                AddTip(/*axisId,*/ seedId, P_propDiamAdv * P_diamMax, posInit, dirInit);


            }

        }

        public void GrowRoots(int temps, double P_TMD,
            double lengthSegNorm,
            List<SoilHorizon> sol, int P_tendanceDirTropisme, double P_intensiteTropisme, double soilDepth,
            double creationDelay, double longSegMin,
            double P_diamMin, double P_penteVitDiam,
            List<double> biomMax,
            double P_distRamif, double P_penteDureeCroissDiam2, double P_ageMaturitePointe, double P_propDiamSem, double P_diamMax, double P_propDiamRamif, double P_coeffVarDiamRamif, double P_angLat,
            double P_penteDureeVieDiamTMD)
        {
            calcVolumesBiomassesSR(temps, P_TMD);

            List<double> rate = GetProperties(GetRootId(seedAxisId), "rate");
            List<double> Biom = GetProperties(GetRootId(seedAxisId), "biomass");
            double r = calcTauxSatis(temps, biomMax, Biom);
            rate.Add(r);
            double rm = calcTSatisMoySR(temps, rate);
            SetProperties(GetRootId(seedAxisId), "mean_rate", rm);
            SetProperties(GetRootId(seedAxisId), "rate", rate);

            List<int> apices = tipVertices;
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {


                int iv = 0;

                GrowAxis(apexv, iv, temps, P_TMD, lengthSegNorm, sol, P_tendanceDirTropisme, P_intensiteTropisme, soilDepth, creationDelay, longSegMin, P_diamMin, P_penteVitDiam, biomMax, P_distRamif, P_penteDureeCroissDiam2,
                    P_ageMaturitePointe, P_propDiamSem, P_diamMax, P_propDiamRamif, P_coeffVarDiamRamif, P_angLat);

                GrowTip(apexv, P_ageMaturitePointe, P_penteDureeCroissDiam2, P_penteDureeVieDiamTMD, P_TMD);

                tipVertices.Add(apexv);

            }

        }

        public void RadialGrowth(double P_coeffCroissRad)
        {
            #region children iteration
            
            List<int> apices = tipVertices;
            tipVertices = new List<int>();

            #region Debug

            foreach (int apexv in apices)
            {
                double tipRad = GetProperties(apexv, "diametre_tip");

                #region Ancestor Iterate
                traversal t = new traversal();
                //List<int> ancestorsTip = new List<int>(Ancestors(apexv));
                //List<int> VerticesVisisted = new List<int>();
                //foreach (int segv in ancestorsTip)
                foreach (int segv in t.IterativePreOrder(this, apexv))
                {
                    if (GetProperties(segv, "label").Contains("tip") && apexv != segv) break;

                    //if (VerticesVisisted.Contains(segv)) continue;
                    //VerticesVisisted.Add(segv);

                    string label = GetProperties(segv, "label");
                    if (label.Contains("segment"))
                    {
                        SetProperties(segv, "diametre", tipRad);
                    }
                }

                #endregion

                tipVertices.Add(apexv);
            }
            #endregion

            #region Debug

            apices = tipVertices;
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {

                bool isMature = GetProperties(apexv, "is_mature");
                bool isSenile = GetProperties(apexv, "is_senile");

                #region Ancestor Iterate

                //List<int> ancestorsTip = new List<int>(Ancestors(apexv));
                //List<int> VerticesVisisted = new List<int>();
                traversal t = new traversal();
                foreach (int segv in t.IterativePreOrder(this, apexv))
                //foreach (int segv in ancestorsTip)
                {
                    if (GetProperties(segv, "label").Contains("tip") && apexv != segv) break;

                    //if (VerticesVisisted.Contains(segv)) continue;
                    //VerticesVisisted.Add(segv);

                    if (GetProperties(segv, "label").Contains("tip") && apexv != segv) break;

                    string label = GetProperties(segv, "label");

                    if (label.Contains("segment"))
                    {
                        if (isMature && !isSenile)
                        {

                            double tipRad = GetProperties(apexv, "diametre_tip");
                            double segRad = GetProperties(segv, "diametre");
                            double section = (Math.PI * Math.Pow(segRad, 2) / 4.0) + (Math.PI * P_coeffCroissRad * Math.Pow(tipRad, 2) / 4.0);
                            double diamSeg = Math.Sqrt(4.0 * section / Math.PI);
                            SetProperties(segv, "diametre", diamSeg);

                        }
                    }
                }

                tipVertices.Add(apexv);
            }

            #endregion

            #endregion


        }

        public void RootSenescence()
        {
            List<int> apices = tipVertices;
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {
                bool isSenile= GetProperties(apexv, "is_senile");

                #region Ancestor iteration

                //List<int> ancestorsTip = new List<int>(Ancestors(apexv));
                //List<int> VerticesVisisted = new List<int>();
                traversal t = new traversal();
                foreach (int segv in t.IterativePreOrder(this, apexv))
                //foreach (int segv in ancestorsTip)
                {
                    if (GetProperties(segv, "label").Contains("tip") && apexv != segv) break;

                    //if (VerticesVisisted.Contains(segv)) continue;
                    //VerticesVisisted.Add(segv);

                    if (GetProperties(apexv, "is_dead")) continue;

                    string label = GetProperties(segv, "label");
                    if (label.Contains("segment"))
                    {
                        if (isSenile) SetProperties(segv, "is_senescencing", true);
                        else SetProperties(segv, "is_senescencing", false);
                    }

                }

                #endregion

                tipVertices.Add(apexv);
            }

            apices = tipVertices;
            tipVertices = new List<int>();

            foreach (int apexv in apices)
            {
                

                bool isSenile = GetProperties(apexv, "is_senile");

                #region Remove Dead roots

                if (IsAxeSenescing(apexv))
                {

                    #region ancestor iteration

                    //List<int> ancestorsTip = new List<int>(Ancestors(apexv));
                    //List<int> VerticesVisisted = new List<int>();
                    traversal t = new traversal();
                    foreach (int segv in t.IterativePreOrder(this, apexv))
                    //foreach (int segv in ancestorsTip)
                    {
                        if (GetProperties(segv, "label").Contains("tip") && apexv != segv) break;

                        //if (VerticesVisisted.Contains(segv)) continue;
                        //VerticesVisisted.Add(segv);

                        if (GetProperties(segv, "is_dead")) continue;

                        string label1 = GetProperties(segv, "label");

                        if (label1.Contains("tip")) SetProperties(apexv, "is_dead", true);
                        if (label1.Contains("segment")) SetProperties(apexv, "is_dead", true);
                        //if (label1.Contains("tip")) this.Remove(apexv,false);
                        //if (label1.Contains("segment")) this.Remove(apexv, false);

                    }

                    //string label = GetProperties(GetAxisId(apexv), "label");
                    //if (label.Contains("axis")) SetProperties(apexv, "is_dead", true);
                    //if (label.Contains("axis")) this.Remove(apexv, false);


                    #endregion

                }
                else tipVertices.Add(apexv);
                    
                    #endregion
                    
                    
                }

            }

        #region Utilities Root Senecsence

        private bool IsAxeSenescing(int tipID)
        {
            bool isS = true;

            bool isSenile = GetProperties(tipID, "is_senile");
            if (!isSenile) isS = false;

            #region Ancestor iteration


            //List<int> ancestorsTip = new List<int>(Ancestors(tipID));

            //List<int> VerticesVisisted = new List<int>();
            traversal t = new traversal();
            foreach (int segv in t.IterativePreOrder(this, tipID))
                //foreach (int segv in ancestorsTip)
            {
                //if (VerticesVisisted.Contains(segv)) continue;
                //VerticesVisisted.Add(segv);

                if (GetProperties(segv, "label").Contains("tip") && tipID != segv) break;

                string label = GetProperties(segv, "label");

                if (label.Contains("segment"))
                {

                    bool isSegSen = GetProperties(segv, "is_senescencing");

                    if (!isSegSen) isS = false;
                }

            }

            #endregion

            return isS;
        }

        #endregion

        #region Ulitilies Grow Root

        private void GrowAxis(int apexv, int iv, int temps, double P_TMD,
            double lengthSegNorm,
            List<SoilHorizon> sol, int P_tendanceDirTropisme, double P_intensiteTropisme, double soilDepth,
            double creationDelay, double longSegMin,
            double P_diamMin, double P_penteVitDiam,
            List<double> biomMax,
            double P_distRamif, double P_penteDureeCroissDiam2, double P_ageMaturitePointe, double P_propDiamSem, double P_diamMax, double P_propDiamRamif, double P_coeffVarDiamRamif, double P_angLat)
        {

            bool mature = GetProperties(apexv, "is_mature");
            bool stop = GetProperties(apexv, "has_stop");
            bool senile = GetProperties(apexv, "is_senile");
            double diametre = GetProperties(apexv, "diametre_tip");
            double[] coord = GetProperties(apexv, "coordinates");
            List<double> rate = GetProperties(GetRootId(seedAxisId), "rate");

            double tipLength = GetProperties(apexv, "length");
            double elongation = rate[temps - 1] * Tip.calcElongationPointe(sol, P_diamMin, P_penteVitDiam, soilDepth, mature, stop, senile, diametre, coord);

            
            tipLength += elongation;
            SetProperties(apexv, "length", tipLength);


            while (tipLength > lengthSegNorm)
            {

                tipLength = GetProperties(apexv, "length");
                tipLength -= lengthSegNorm;
                SetProperties(apexv, "length", tipLength);

                if (GetProperties(apexv, "is_dead")) continue;

                GrowSegments(apexv, iv, temps, lengthSegNorm, diametre, P_tendanceDirTropisme, P_intensiteTropisme, soilDepth, sol);
                RamifAxis(apexv, P_diamMin, P_distRamif, P_penteDureeCroissDiam2, P_ageMaturitePointe, P_propDiamSem, P_diamMax, sol, soilDepth, P_propDiamRamif, P_coeffVarDiamRamif, P_angLat, lengthSegNorm);

            }

            double tipTime = GetProperties(apexv, "time_tip");

            if ((temps - tipTime > creationDelay) && tipLength > longSegMin && !GetProperties(apexv, "is_dead"))
            {

                GrowSegments(apexv, iv, temps, tipLength, diametre, P_tendanceDirTropisme, P_intensiteTropisme, soilDepth, sol);
                SetProperties(apexv, "length", 0.0);
                RamifAxis(apexv, P_diamMin, P_distRamif, P_penteDureeCroissDiam2, P_ageMaturitePointe, P_propDiamSem, P_diamMax, sol, soilDepth, P_propDiamRamif, P_coeffVarDiamRamif, P_angLat, lengthSegNorm);
            }
           
        }


        private void GrowTip(int apexv, double P_ageMaturitePointe, double P_penteDureeCroissDiam2,double P_penteDureeVieDiamTMD, double P_TMD)
        {
            double age = GetProperties(apexv, "age") + 1;
            SetProperties(apexv, "age", age);

            bool mature = GetProperties(apexv, "is_mature");
            bool stop = GetProperties(apexv, "has_stop");
            bool senile = GetProperties(apexv, "is_senile");
            double diametre = GetProperties(apexv, "diametre_tip");

            if ((!mature) && (age > P_ageMaturitePointe))
            {
                SetProperties(apexv, "is_mature",true);
                SetProperties(apexv, "age", 0);

            }

            if ((mature) && (!stop) && (age > (P_penteDureeCroissDiam2 * diametre * diametre)))
            {
                SetProperties(apexv, "has_stop",true);
            }

            if ((mature) && (stop) && (!senile) && (age > (P_penteDureeVieDiamTMD * diametre * P_TMD)))
            {
                SetProperties(apexv, "is_senile",true);
            }

        }

        private void GrowSegments(int apexv,int iv, int temps, double elongation, double diametre, int P_tendanceDirTropisme, double P_intensiteTropisme, double soilDepth, List<SoilHorizon> sol)
        {
            SetProperties(apexv, "time_tip", temps);

            //seed = Guid.NewGuid().GetHashCode();

            double[] dirInit = GetProperties(apexv, "direction");
            double[] coord = GetProperties(apexv, "coordinates");
            double[] dirCroiss0 = GetProperties(apexv, "direction_croissance");
            double[] dirCroiss = Tip.ReorientePointe(elongation, sol, P_tendanceDirTropisme, P_intensiteTropisme, soilDepth, dirInit, diametre, coord, dirCroiss0,seed);
            SetProperties(apexv, "direction_croissance", dirCroiss);

            //Console.WriteLine(dirCroiss[0] +" "+ dirCroiss[1] + " " + dirCroiss[2]);

            double distPrimInit = GetProperties(apexv, "distance_prim_initial");
            List<dynamic> l=Tip.deplacePointe(elongation, dirCroiss, coord, distPrimInit);
            coord = l[0];
            distPrimInit = l[1];
            //Console.WriteLine(coord[0] + " " + coord[1] + " " + coord[2]);
            SetProperties(apexv, "distance_prim_initial", distPrimInit);
            SetProperties(apexv, "coordinates", coord);

            bool isSegComp = GetProperties(apexv, "completed");
            string label = GetProperties(TipParent(apexv), "label");
            int apParId = TipParent(apexv);
            double[] org = new double[3];

            if (isSegComp)
            {



                if (!label.Contains("seed"))
                {
                    org = GetProperties(apParId, "extrem_position");

                    double ls = Segment.longSeg(org, coord);
                    AddSegment(apexv, temps, diametre, /*org,*/ coord);
                }
                else
                {
                    org = GetProperties(seedId, "origine_seed");

                    //double[] binf = GetProperties(GetRootId(seedAxisId), "lower_limit");
                    //for (int i = 0; i < 3; i++) org[i] -= binf[i];

                    double ls = Segment.longSeg(org, coord);
                    bool isRamif = (GetProperties(apexv, "edge_type") == "+") ? true : false;
                    if (isRamif) SetProperties(apexv, "edge_type", "<");
                    AddSegment(apexv, temps, diametre, /*org,*/ coord, isRamif);
                }




                //SetProperties(apexv, "completed", false);
                //Console.WriteLine(org[0] + " " + org[1] + " " + org[2]);

            }
            else
            {
                //org = GetProperties(seedId, "origine_seed");
                SetProperties(apexv, "completed", true);
                if (!label.Contains("seed")) SetProperties(apParId, "extrem_position", coord);
                //else SetProperties(apParId, "extrem_position", org);
                SetProperties(TipParent(apexv), "time", temps);

            }
            

        }

        private void RamifAxis(int tipv, double P_diamMin, double P_distRamif, double P_penteDureeCroissDiam2, double P_ageMaturitePointe,
            double P_propDiamSem, double P_diamMax,
            List<SoilHorizon> sol, double SoilDepth,
            double P_propDiamRamif, double P_coeffVarDiamRamif,
            double P_angLat,double lengthSegNorm)
        {
             bool isRam = IsRamif(tipv, P_diamMin, P_distRamif, P_penteDureeCroissDiam2, P_ageMaturitePointe);

            int tipv0 = tipv;

            while (isRam)
            {

                //seed= seed = Guid.NewGuid().GetHashCode();

                double distPrim = GetProperties(tipv0, "distance_prim_initial");
                double[] coord = GetProperties(tipv0, "coordinates");
                double nDistPrim = distPrim - RootAxe.distInterRamifPointe(sol, P_distRamif, SoilDepth, coord);
                SetProperties(tipv0, "distance_prim_initial", nDistPrim);

                double diametre = GetProperties(tipv0, "diametre_tip");
                double diamRamif = RootAxe.tireDiamPointeFille(P_propDiamRamif, P_diamMin, P_coeffVarDiamRamif, diametre,seed);



                if (diamRamif > P_diamMin)
                {

                    double[] dirCroiss = GetProperties(tipv0, "direction_croissance");

                    double[] orgRam =  RootAxe.origineRamif(coord, dirCroiss, nDistPrim);
                    double[] dirRam = RootAxe.orienteRamif(P_angLat, dirCroiss,seed);
                    //Console.WriteLine(orgRam[0] + " " + orgRam[1] + " " + orgRam[2]);
                    //int currentAxisId = GetAxisId(tipv0);
                    
                    //int axisId=AddAxis(currentAxisId, "ramif");
                    int segId = GetRamifSegId(distPrim, tipv0, lengthSegNorm, orgRam);
                    AddTip(/*axisId,*/ segId, diamRamif, orgRam, dirRam,true);
                    tipv0 = GetCursor();
                    nbRam++;
                    isRam = IsRamif(tipv0, P_diamMin, P_distRamif, P_penteDureeCroissDiam2, P_ageMaturitePointe);

                }
                else isRam = false;
            }
        }

        private int GetRamifSegId(double distPrim, int tipId, double lengthSegNorm, double[] orgSeg)
        {

            //double lengthSegs = 0;

            int nbseg = (int)Math.Floor((distPrim / lengthSegNorm) + 0.5);
            int id = tipId;
            for (int i = 0; i < nbseg; i++) id = (int) this.Parent(id);


            //Dictionary<int, double> distSeg = new Dictionary<int, double>();

            //List<int> ancestorsTip = new List<int>(Ancestors(tipId));
            //List<int> VerticesVisisted = new List<int>();

            //int id = seedId;
            //if (ancestorsTip.Count > 2) id = ancestorsTip[1];
            //else if (ancestorsTip.Count == 1) id = ancestorsTip[0];

            //foreach (int iv in ancestorsTip)
            //{

            //    if (VerticesVisisted.Contains(iv)) continue;
            //    VerticesVisisted.Add(iv);

            //    //string edge = GetProperties(iv, "edge_type");
            //    //if (edge == "+") break;

            //    string label = GetProperties(iv, "label");
            //    if (label.Contains("tip")) continue;

            //    if (label.Contains("segment"))
            //    {

            //        //double[] posO = GetProperties(iv, "origine_position");
            //        double[] posO = GetProperties(iv, "extrem_position");

            //        distSeg.Add(iv, Math.Sqrt(Math.Pow(posO[0] - orgSeg[0], 2) + Math.Pow(posO[1] - orgSeg[1], 2) + Math.Pow(posO[2] - orgSeg[2], 2)));
            //        /*

            //                        double[] posE = GetProperties(iv, "extrem_position");
            //                            lengthSegs += Segment.longSeg(posE, posO);
            //                            */
            //    }
            //    //if (label.Contains("segment")) lengthSegs += lengthSegNorm;
            //    /*
            //                    if (lengthSegs>= distPrim)
            //                    {
            //                        id = iv;
            //                        break;
            //                    }
            //                    */
            //}
            //if (distSeg.Count > 0)
            //{

            //    double min = distSeg.Values.Min();
            //    foreach (int k in distSeg.Keys) if (distSeg[k] == min) { id = k; break; }
            //}

            return id;
        }


        private bool IsRamif(int tipv,double P_diamMin, double P_distRamif,double P_penteDureeCroissDiam2, double P_ageMaturitePointe)
        {
            double diam = GetProperties(tipv, "diametre_tip");
            double age = GetProperties(tipv, "age");
            double distPrimInit = GetProperties(tipv, "distance_prim_initial");

            return ((diam > 1.8 * P_diamMin) && (distPrimInit > P_distRamif) && (((P_penteDureeCroissDiam2 * diam * diam) - age) > P_ageMaturitePointe));

        }


        private void calcVolumesBiomassesSR(int temps, double P_TMD)
        {
            double oldVol = GetProperties(GetRootId(seedAxisId), "volume");
            List<double> bioMass = GetProperties(GetRootId(seedAxisId), "biomass");
            double vol = oldVol;
            double biom = 0;

            traversal t = new traversal();



            #region PreOrder Iteration

            //List<int> apices = tipVertices;
            //tipVertices = new List<int>();
            //List<int> VerticesVisited = new List<int>();

            foreach (int iv in t.IterativePreOrder(this, seedId))
            //foreach (int apexv in apices)
            {

                //List<int> ancestorsTip = new List<int>(Ancestors(apexv));

                //foreach (int iv in ancestorsTip)
                {
                    //if (VerticesVisited.Contains(iv)) continue;
                    //VerticesVisited.Add(iv);
                    //foreach (int iv in t.IterativePreOrder(this, seedId))
                    //{
                    string label = GetProperties(iv, "label");

                    if (label.Contains("tip"))
                    {
                        if (GetProperties(iv, "is_dead")) continue;

                        double diam = GetProperties(iv, "diametre_tip");
                        vol += (Math.PI / 4.0) * Math.Pow(diam, 3);
                    }

                    if (label.Contains("segment"))
                    {
                        if (GetProperties(iv, "is_dead")) continue;

                        double[] posE = GetProperties(iv, "extrem_position");
                        //double[] posO = GetProperties(iv, "origine_position");
                        double[] posO = new double[3];
                        int parentiv = (int)this.Parent(iv);
                        string labelParent = GetProperties(parentiv, "label");

                        posE = GetProperties(iv, "extrem_position");

                        if (labelParent.Contains("segment")) posO = GetProperties(parentiv, "extrem_position");
                        else if (labelParent.Contains("seed")) posO = GetProperties(seedId, "origine_seed");

                        double diametre = GetProperties(iv, "diametre");

                        vol += Segment.volTotalSeg(posE, posO, diametre);
                    }
                }
                //tipVertices.Add(apexv);
            }

        #endregion


        biom = P_TMD * (vol - oldVol) / 1000.0;
            if (biom < 0.0001) biom = 0.0001;

            bioMass.Add(biom);

            SetProperties(GetRootId(seedAxisId), "volume", vol);
            SetProperties(GetRootId(seedAxisId), "biomass", bioMass);
        }




        public void SaveToFile(string path,bool isJSONSaved)
        {
            traversal t = new traversal();

            FillMetadataDictionary();
            
            if (isJSONSaved) t.SaveToJSONFile(this, path, true);
            t.SaveToFile(this, path, true);

            //if (isJSONSaved) t.SaveToJSONFile(FatMtg(this, false), path, true);
            //else t.SaveToFile(FatMtg(this, false), path, true);
        }

        /****************************************************************************/
        /****************************************************************************/
        double calcTauxSatis(int temps, List<double> biomMax,List<double> biomUtilisee)
        {
            double taux1, taux2;
            double biomU0, biomU1, biomU2;  // biomasses utilisées aux différents pas (-2, -1, actuel)
            double biomD1, biomD2;  // biomasses disponibles aux pas précédent et actuel
            ////Debug
            //return 1.0;
            ////Debug

            if (biomUtilisee[temps - 1] < 0.0002) return 1.0;   // en début de croissance notamment, on ne gère pas la limitation éventuelle

            else
            {
                biomU0 = 0.0002;
                if (temps > 1) biomU0 = biomUtilisee[temps - 3];   // biomasse utilisée au pas de temps - 2, s'il existe
                biomU1 = biomUtilisee[temps - 2];    // biomasse utilisée au pas de temps - 1
                biomU2 = biomU1 * (1 + ((biomU1 - biomU0) / (0.5 * (biomU0 + biomU1))));   // biomasse au pas de temps t (non connue, c'est juste une estimation) 

                biomD1 = biomMax[temps - 2]; // biomasse disponible au pas de temps -1
                biomD2 = biomMax[temps - 1];   // biomasse disponible au pas de temps actuel

                taux1 = biomD1 / biomU1;
                if (taux1 > 1.0) taux1 = 1.0;

                taux2 = biomD2 / biomU2;
                if (taux2 > 1.0) taux2 = 1.0;
            }
            //Console.Write(Math.Pow(taux1, 4.0));
            return Math.Pow(taux1, 4.0);
            ////  if (taux1<taux2) { return taux1; }
            ////	            else { return taux2; }  // on renvoie le plus petit des 2 taux calculés


        } /* Fonction calcTauxSatis */
          /****************************************************************************/
          /****************************************************************************/
        private double calcTSatisMoySR(int temps,List<double> tSatis)
        {
            /* Calcul du taux de satisfaction moyen sur la période écoulée (de 1 Ã  temps) */

            double tSatisCum = 0.0;

            for (int date = 0; date < temps; date++) /* Boucle sur la période écoulée */
            {
                tSatisCum += tSatis[date];
            }
            double tSatisMoy = tSatisCum / temps;

            return tSatisMoy;

        }  /* Fonction calcTSatisMoySR */

        #endregion

        #endregion

        #region Utilites Export data root system
        
        
        public void CalcLimiteSR(/*out double volPrim,*/ double d2, out double volTot, out double secPointe, out double[] binf, out double[] bsup,out double diamMax,
            out double distMax,out double profMax,out double longueur,out double distMoy,out double profMoy)
        {        
            //Console.WriteLine("===============" + countday + "===============");
            countday++;

            //seed = Guid.NewGuid().GetHashCode();

            if (countday == 101)
            {
               Console.WriteLine("===============");
            }

            volTot = 0.0; // volume total
            //volPrim = 0.0; // volume des structures primaires
            secPointe = 0.0;  // section totale des pointes actives
            diamMax = -1.0E10; // diamètre maximal, du plus gros segment
            distMax = -1.0E10;  // extension maximale
            profMax = -1.0E10;  // profondeur maximale
            longueur = 0.0;  // longueur
            double distHorLong = 0.0;
            double profLong = 0.0;

            binf = new double[3];
            bsup = new double[3];

            //binf[0] = +1.0E10; binf[1] = +1.0E10; binf[2] = +1.0E10; // initialisation des valeurs
            //bsup[0] = -1.0E10; bsup[1] = -1.0E10; bsup[2] = -1.0E10;

            if (segmentNb > 0)
            {
                //binf[0] = Math.Min(GetVertexMin("extrem_position")[0], GetVertexMin("origine_position")[0]);
                //binf[1] = Math.Min(GetVertexMin("extrem_position")[1], GetVertexMin("origine_position")[1]);
                //binf[2] = Math.Min(GetVertexMin("extrem_position")[2], GetVertexMin("origine_position")[2]);
                //bsup[0] = Math.Max(GetVertexMax("extrem_position")[0], GetVertexMax("origine_position")[0]);
                //bsup[1] = Math.Max(GetVertexMax("extrem_position")[1], GetVertexMax("origine_position")[1]);
                //bsup[2] = Math.Max(GetVertexMax("extrem_position")[2], GetVertexMax("origine_position")[2]);
            }
            else
            {
                binf[0] = GetVertexMin("origine_tip")[0];
                binf[1] = GetVertexMin("origine_tip")[1];
                binf[2] = GetVertexMin("origine_tip")[2];
                bsup[0] = GetVertexMax("origine_tip")[0];
                bsup[1] = GetVertexMax("origine_tip")[1];
                bsup[2] = GetVertexMax("origine_tip")[2];
            }
            traversal t = new traversal();

            #region PreOrder iteration
            //List<int> apices = tipVertices;
            //tipVertices = new List<int>();
            //List<int> VerticesVisited = new List<int>();

            ///foreach (int apexv in apices)
            //{

                //List<int> ancestorsTip = new List<int>(Ancestors(apexv));

                //foreach (int iv in ancestorsTip)
                //{
                //if (VerticesVisited.Contains(iv)) continue;

                //VerticesVisited.Add(iv);
                foreach (int iv in t.IterativePreOrder(this, seedId))
                {
                    string label = GetProperties(iv, "label");

                    if (label.Contains("tip"))
                    {
                        //Debug
                        if (GetProperties(iv, "is_dead")) continue;
                        //Debug

                        double diam = GetProperties(iv, "diametre_tip");
                        double length = GetProperties(iv, "length");

                        volTot += Math.PI * Math.Pow(diam, 2) * length / 4.0;
                        // volPrim += Math.PI * Math.Pow(diam, 2) * length / 4.0;

                        bool isMat = GetProperties(iv, "is_mature");
                        bool isSen = GetProperties(iv, "is_senile");

                        if (isMat && !isSen) secPointe += Math.PI * diam * diam / 4.0;

                    //double[] posE = GetProperties(iv, "origine_tip");
                    //double[] posO = GetProperties(iv, "origine_tip");

                    //for (int i = 0; i < 3; i++)
                    //{
                    //    if (posO[i] < binf[i]) binf[i] = posO[i];
                    //    if (posE[i] < binf[i]) binf[i] = posE[i];
                    //    if (posO[i] > bsup[i]) bsup[i] = posO[i];
                    //    if (posE[i] > bsup[i]) bsup[i] = posE[i];
                    //}
                    if (countday == 101)
                    {
                        //Console.WriteLine(iv + " " + posO[0] + " " + posO[1] + " " + posO[2] + " " + posE[0] + " " + posE[1] + " " + posE[2] + " " + diametre + " " + Segment.longSeg(posO, posE));
                        //if (GetProperties((int)this.Parent(iv), "label").Contains("segment") && GetProperties(iv, "label").Contains("tip"))
                        {
                            //Console.WriteLine(iv + " " + GetProperties(iv, "origine_tip")[0] + " " + GetProperties(iv, "origine_tip")[1] + " " + GetProperties(iv, "origine_tip")[2] + " " + (int)this.Parent(iv) + " " + GetProperties((int)this.Parent(iv), "extrem_position")[0] + " " + GetProperties((int)this.Parent(iv), "extrem_position")[1] + " " + GetProperties((int)this.Parent(iv), "extrem_position")[2]);
                        }
                    }
                }


                if (label.Contains("segment"))
                {
                    //Debug
                    if (GetProperties(iv, "is_dead")) continue;
                    //Debug

                    double[] posE = GetProperties(iv, "extrem_position");
                    //double[] posO = GetProperties(iv, "origine_position");
                    double[] posO = new double[3];
                    int parentiv = (int)this.Parent(iv);
                    string labelParent = GetProperties(parentiv, "label");

                    posE = GetProperties(iv, "extrem_position");

                    if (labelParent.Contains("segment")) posO = GetProperties(parentiv, "extrem_position");
                    else if (labelParent.Contains("seed")) posO = GetProperties(seedId, "origine_seed");

                        //for (int i = 0; i < 3; i++)
                        //{
                        //    if (posO[i] < binf[i]) binf[i] = posO[i];
                        //    if (posE[i] < binf[i]) binf[i] = posE[i];
                        //    if (posO[i] > bsup[i]) bsup[i] = posO[i];
                        //    if (posE[i] > bsup[i]) bsup[i] = posE[i];
                        //}


                        //Console.WriteLine(posO[0] + " " + posO[1] + " " + posO[2]);
                        //Console.WriteLine(posE[0] + " " + posE[1] + " " + posE[2]);
                        double diametre = GetProperties(iv, "diametre");

                    if (countday == 101)
                    {
                        Console.WriteLine(iv + " " + posO[0] + " " + posO[1] + " " + posO[2] + " " + posE[0] + " " + posE[1] + " " + posE[2] + " " + diametre + " " + Segment.longSeg(posO, posE));
                        if (GetProperties((int)this.Parent(iv), "label").Contains("segment") && GetProperties(iv, "label").Contains("segment"))
                        {
                            //Console.WriteLine(iv + " " + posO[0] + " " + posO[1] + " " + posO[2] + " " + (int)this.Parent(iv) + " " + GetProperties((int)this.Parent(iv), "extrem_position")[0] + " " + GetProperties((int)this.Parent(iv), "extrem_position")[1] + " " + GetProperties((int)this.Parent(iv), "extrem_position")[2]);
                        }
                    }


                        if (diametre > diamMax) diamMax = diametre;

                        double distHor = distHorSeg(posE, posO);
                        if (distHor > distMax) { distMax = distHor; }

                        double profS = 0.0;
                        if (posO[2] > posE[2]) profS = posO[2]; else profS = posE[2];
                        if (profS > profMax) profMax = profS;


                        volTot += Segment.volTotalSeg(posE, posO, diametre);
                        //volPrim += Segment.volPrimSeg(posE, posO, double tipDiam)
                        double longS = Segment.longSeg(posE, posO);
                        longueur += longS;
                        distHorLong += distHor * longS;
                        profLong += profS * longS;


                    }
                    //}
                    //tipVertices.Add(apexv);
                }

            #endregion

            if (countday == 101)
            {
                Console.WriteLine("===============");
            }

            for (int i = 0; i < 3; i++) { binf[i] = binf[i] - d2; bsup[i] = bsup[i] + d2; }

            double amplMax = 0.0;
            for(int i=0;i<3;i++) if ((bsup[i] - binf[i]) > amplMax) amplMax = bsup[i] - binf[i];

            if (longueur > 0.0)
            {
                distMoy = distHorLong /  longueur;
                profMoy =  profLong / longueur;
            }
            else
            {
                distMoy = 0.0;
                profMoy = 0.0;
            }
            SetProperties(GetRootId(seedAxisId), "lower_limit", binf);
            SetProperties(GetRootId(seedAxisId), "upper_limit", bsup);
            //Console.WriteLine(binf[0] + " " + binf[1] + " " + binf[2] +" "+ bsup[0] + " " + bsup[1] + " " + bsup[2]);

        }

                #region Utilities

                private double distHorSeg(double[] posE,double[] posO)
        /* Calcule la distance horizontale d'un segment */
        {
            return Math.Sqrt(((posE[0] + posO[0]) * (posE[0] + posO[0]) / 4.0) +
                        ((posE[1] + posO[1]) * (posE[1] + posO[1]) / 4.0));

        }  /* Fonction distHorSeg */

        #endregion


        #endregion

        #region Metadata


        private void FillMetadataDictionary()
        {
            
            this.metadata = new Dictionary<string, dynamic>();

            
            //Scales
            Dictionary<string, dynamic> Dict = new Dictionary<string, dynamic>();
            /*foreach (int key in labelsOfScales.Keys) Dict.Add(labelsOfScales[key], key.ToString());
            this.metadata.Add("scales", Dict);*/

            //Version
            this.metadata.Add("version", "SiriusQuality2.5");

            //Name of file
            Dict = new Dictionary<string, dynamic>();
            Dict.Add("name","XXX");
            Dict.Add("captured", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
            this.metadata.Add("file", Dict);
            /*
            //Property definition 
            //labels
            Dict = new Dictionary<string, dynamic>();
            List<string> list = new List<string>();
            list.Add("leaf_number");
            list.Add("last_axis_fraction");
            list.Add("axis_fraction");
            list.Add("leaf_fraction");
            list.Add("internode_length");
            list.Add("sheath_areaindex");
            list.Add("laminae_areaindex");
            list.Add("sheath_potentialspecificN");
            list.Add("lamina_structN");
           
            //ect..
            Dict.Add("label", list);
            //type
            list = new List<string>();
            list.Add("double");
            list.Add("double");
            list.Add("double");
            list.Add("double");
            list.Add("double");
            list.Add("double");
            list.Add("double");
            list.Add("double");
            list.Add("double");
            //ect..
            Dict.Add("type", list);
            //units
            list = new List<string>();
            list.Add("dimensionless");
            list.Add("dimensionless");
            list.Add("dimensionless");
            list.Add("dimensionless");
            list.Add("cm");
            list.Add("m²/m²");
            list.Add("gN");
            list.Add("gN");
            //ect..
            Dict.Add("unit", list);
            this.metadata.Add("properties", Dict);

            //Functions definition 
            //labels
            Dict = new Dictionary<string, dynamic>();
            list = new List<string>();
            list.Add("None");
            list.Add("None");

            //ect..
            Dict.Add("label", list);
            //type
            list = new List<string>();
            list.Add("none");
            list.Add("none");
            //ect..
            Dict.Add("type", list);
            this.metadata.Add("functions", Dict);
            */
        }


        //public string ToLongDateString();

        //public string ToLongTimeString();

       // public long ToFileTime();

        //public long ToFileTimeUtc();

        //public DateTime ToLocalTime();

        #endregion

        /*  #region Testing functions

        public int TestAddCanopy(string label = "canopy")
        {
            return AddCanopy(label);
        }

        public int TestAddPlant()
        {
            return AddPlant();
        }

        public int TestAddShoot()
        {
            return AddShoot();
        }

        public int TestAddRoot()
        {
            return AddRoot();
        }

        public int TestAddAxis()
        {
            return AddAxis();
        }

        public int TestAddMetamer()
        {
            return AddMetamer();
        }

        public int TestAddInternode()
        {
            return AddInternode();
        }

        public int TestAddSheath()
        {
            return AddSheath();
        }

        public int TestAddBlade()
        {
            return AddBlade();
        }

        #endregion
        */
        static void Main(String[] args)
        {

        }

    }
}
