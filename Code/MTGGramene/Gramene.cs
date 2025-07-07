using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace csMTG
{
    public class Gramene : mtg
    {
        #region Attributes

        public Dictionary<int, string> labelsOfScales;

        internal int cursor = 0;
        internal int nbPlants = 0;


        #endregion

        #region Constructor and copy constructor

        /// <summary>
        /// Constructor of Gramene:
        /// * Sets the labels of the 6 scales of an mtg.
        /// </summary>
        public Gramene()
        {
            // define the 6 scales
            
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
            foreach (int complex in grameneToCopyFrom.complex.Keys)
                this.complex.Add(complex, (int)grameneToCopyFrom.Complex(complex));

            // Copy of the components
            foreach (int components in grameneToCopyFrom.components.Keys)
            {
                /*if (grameneToCopyFrom.Components(complex) != null)*/
                this.components.Add(components, grameneToCopyFrom.components[components]);

                //else this.components = new Dictionary<int, List<int>>();
            }

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
        internal int GetPlantId()
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

        

        #endregion

        #region Property utilities

        //public dynamic GetProperties(int Id = 0, string prop = null)
        //{
        //    dynamic value = null;
        //   if(prop!=null) value = GetVertexProperties(Id)[prop];
        //   else value = GetVertexProperties(Id);

        //    return value;
        //}

        //public void SetProperties(int Id = 0, string prop = "origin", dynamic valToAdd = null)
        //{

        //    Dictionary<string, dynamic> namesValues = GetVertexProperties(Id);

        //    if (namesValues.ContainsKey(prop)) namesValues[prop] = valToAdd;
        //    else namesValues.Add(prop, valToAdd);

        //    AddVertexProperties(Id, namesValues);

        //}

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
        internal int PlantHasRoot(int plantId)
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



        #endregion

        #region High level functions (Wheat)

        /// <summary>
        /// A function that the final user can use to create a Wheat plant composed of a number of leaves they will choose.
        /// </summary>
        /// <param name="NbLeaves"> The number of leaves desired in the plant. </param>
        /// <returns> The plant structure. </returns>
        public void CreateBasicWheat()
        {
            this.AddCanopy("wheat");
            this.AddPlant();
            


        }

        #region Save

        public void SaveToFile(string path, bool isJSONSaved)
        {
            traversal t = new traversal();

            FillMetadataDictionary();

            if (isJSONSaved) t.SaveToJSONFile(this, path, true);
            else t.SaveToFile(this, path, true);

            //if (isJSONSaved) t.SaveToJSONFile(FatMtg(this, false), path, true);
            //else t.SaveToFile(FatMtg(this, false), path, true);
        }

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
        }*/

        #endregion
        
        static void Main(String[] args)
        {

        }

    }
}
