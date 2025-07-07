using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace csMTG
{
    public class traversal : PropertyTree
    {
        #region Preorder

        /// <summary>
        /// Iteratively traverse the tree in preorder starting from a vertex.
        /// (Equivalent of pre_order2 in Python file)
        /// </summary>
        /// <param name="tree"> The tree to be traversed. </param>
        /// <param name="vertexId"> The identifier of the starting vertex. </param>
        /// <returns> Returns an iterator. </returns>
        public IEnumerable<int> IterativePreOrder(mtg tree, int vertexId, int complex = -1)
        {

            if (complex != -1 && tree.Complex(vertexId) != complex)
                yield break;

            Dictionary<int, dynamic> edgeType = tree.Property("edge_type");

            Stack<int> stack = new Stack<int>();
            stack.Push(vertexId);

            while (stack.Count != 0)
            {
                List<int> plus = new List<int>();
                List<int> successor = new List<int>();

                vertexId = stack.Pop();
                yield return vertexId;

                foreach (int vid in tree.Children(vertexId))
                {
                    if (complex != -1 && tree.Complex(vid) != complex)
                        continue;

                    if (!edgeType.ContainsKey(vid))
                        successor.Add(vid);
                    else
                    {
                        if (edgeType[vid].Equals("<"))
                            successor.Add(vid);
                        else
                            plus.Add(vid);

                    }
                }

                plus.AddRange(successor);

                List<int> child = plus;

                child.Reverse();

                child.ForEach(o => stack.Push(o));
            }
        }

        /// <summary>
        /// Recursively traverse the tree in preorder starting from a vertex.
        /// (Equivalent of pre_order in Python file)
        /// </summary>
        /// <param name="tree"> The tree to be traversed. </param>
        /// <param name="vertexId"> The identifier of the starting vertex. </param>
        /// <returns> Returns an iterator. </returns>
        public IEnumerable<int> RecursivePreOrder(mtg tree, int vertexId, int complex = -1)
        {
            if (complex != -1 && tree.Complex(vertexId) != complex)
                yield break;

            Dictionary<int, dynamic> edgeType = tree.Property("edge_type");

            List<int> successor = new List<int>();
            yield return vertexId;

            foreach (int vid in tree.Children(vertexId))
            {
                if (edgeType[vid].Equals("<"))
                {
                    successor.Add(vid);
                    continue;
                }

                foreach (int node in RecursivePreOrder(tree, vid, complex))
                {
                    yield return node;
                }
            }

            foreach (int vid in successor)
            {
                foreach (int node in RecursivePreOrder(tree, vid, complex))
                    yield return node;
            }

        }

        #endregion

        #region PostOrder

        /// <summary>
        /// Iteratively traverse the tree in postorder starting from a vertex.
        /// (Equivalent of post_order2 in Python file)
        /// </summary>
        /// <param name="tree"> The tree to be traversed. </param>
        /// <param name="vertexId"> The identifier of the starting vertex. </param>
        /// <returns> Returns an iterator. </returns>
        public IEnumerable<int> IterativePostOrder(mtg tree, int vertexId, int complex = -1)
        {
            Dictionary<int, dynamic> edgeType = tree.Property("edge_type");
            Dictionary<int, dynamic> emptyDictionary = new Dictionary<int, dynamic>();

            // Internal function
            Func<int, List<int>> OrderChildren = new Func<int, List<int>>(vid =>
            {
                List<int> plus = new List<int>();
                List<int> successor = new List<int>();

                foreach(int v in tree.Children(vid))
                {
                    if (complex != -1 && tree.Complex(v) != complex)
                        continue;

                    if (!edgeType.ContainsKey(v))
                        successor.Add(v);
                    else
                    {
                        if (edgeType[v].Equals("<"))
                            successor.Add(v);
                        else
                            plus.Add(v);
                    }

                }

                plus.AddRange(successor);
                List<int> child = plus;

                return child;
            }
            );

            List<int> visited = new List<int>();

            Stack<int> stack = new Stack<int>();
            stack.Push(vertexId);

            while(stack.Count != 0)
            {
                vertexId = stack.Peek();

                List<int> listOfChildren = OrderChildren(vertexId);

                if (listOfChildren.Count != 0 && (listOfChildren.Intersect(visited).Count() != listOfChildren.Count()) )
                {
                    foreach (int vid in listOfChildren)
                    {
                        if (!visited.Contains(vid))
                        {
                            stack.Push(vid);
                            break;
                        }
                    }
                }
                else
                    {
                        visited.Add(vertexId);
                        stack.Pop();
                        yield return vertexId;
                    }
            }

        }

        /// <summary>
        /// Recursively traverse the tree in postorder starting from a vertex.
        /// (Equivalent of function post_order in Python file).
        /// </summary>
        /// <param name="tree"> The tree to be traversed. </param>
        /// <param name="vertexId"> The identifier of the starting vertex. </param>
        /// <returns> Returns an iterator. </returns>
        public IEnumerable<int> RecursivePostOrder(mtg tree, int vertexId, int complex = -1)
        {
            if (complex != -1 && tree.Complex(vertexId) != complex)
                yield break;

            Dictionary<int, dynamic> edgeType = tree.Property("edge_type");

            List<int> successor = new List<int>();

            foreach (int v in tree.Children(vertexId))
            {
                if (edgeType[v].Equals("<"))
                {
                    successor.Add(v);
                    continue;
                }

                foreach (int node in RecursivePostOrder(tree, v, complex))
                {
                    yield return node;
                }
            }

            foreach (int vid in successor)
            {
                foreach (int node in RecursivePostOrder(tree, vid, complex))
                    yield return node;
            }

            yield return vertexId;
            
        }
        
        #endregion

        #region Save to file

        /// <summary>
        /// Saves the PropertyTree/MTG into a file (.tlp)
        /// </summary>
        /// <param name="mtg"> The property tree/mtg to save. </param>
        /// /// <param name="ismtgsaved"> true: save mtg, false save property tree. </param>
        /// <param name="path"> Contains the path and the name of the file (Do not mention the extension). </param>
        public void SaveToFile(mtg mtg, string path, bool ismtgsaved=false)
        {

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@path + ".tlp"))
            {

                file.WriteLine("(tlp \"2.0\")");

                // List of all nodes
                int NbVert = 0;
                foreach (int s in mtg.Scales()) NbVert += mtg.Vertices(s).Count;

                file.WriteLine("(nb_nodes " + NbVert + ")");

                int upperbound = NbVert - 1;
                file.WriteLine("(nodes " + "0.." + upperbound + ")");

                // List of all edges
                int countEdges = 0;

                foreach (int s in mtg.Scales())
                {
                    foreach (int edge in mtg.Vertices(s))
                    {
                        if (edge != 0)
                        {
                            countEdges++;

                            if (((int?)mtg.Parent(edge)) != null && (int)mtg.Parent(edge) != -1) file.WriteLine("(edge " + countEdges + " " + (int)mtg.Parent(edge) + " " + edge + ")");


                        }
                    }
                }

                // List of scales
                //Dictionary<int, List<int>> Dict = new Dictionary<int, List<int>>();

                //foreach (int scale in mtg.Scales())
                //{

                //        List<int> keysList = new List<int>();
                //        foreach (int key in mtg.scale.Keys)
                //        {
                //            if (mtg.scale[key] == scale)
                //            {
                //                keysList.Add(key);
                //            }
                //        }

                //    Dict.Add(scale, keysList);

                //}


                //int ScaleID = 1;
                //foreach (int key in Dict.Keys)
                //{
                //    file.WriteLine("(scale " + key);
                //    file.Write("(nodes");
                //    foreach (int node in Dict[key])
                //    {
                //        file.Write(" " + node);
                //    }
                //    file.WriteLine("))");

                //    ScaleID++;
                //}

                // List of all clusters
                int clusterId = 1;

                foreach (string name in mtg.PropertyNames())
                {

                    file.WriteLine("(cluster " + clusterId + " " + name);

                    file.Write("(nodes");
                    foreach (int node in mtg.Property(name).Keys)
                    {
                        file.Write(" " + node);
                    }
                    file.WriteLine("))");

                    clusterId++;
                }


                //List of Complex

                //foreach (int key in mtg.complex.Keys)
                //{

                //    file.WriteLine("(complex " + mtg.complex[key] + " " + key + ")");

                //}
                // List of all properties
                int propertyId = 1;

                foreach (string name in mtg.PropertyNames())
                {
                    file.WriteLine("(property " + propertyId + " string \"" + name + "\"");

                    Dictionary<int, dynamic> property = mtg.Property(name);

                    foreach (int node in property.Keys)
                    {
                        file.WriteLine("(node " + node + " " + "\"" + property[node] + "\"" + ")");
                    }

                    file.WriteLine(")");
                    propertyId++;
                }

 


            }
        }



        public void SaveToJSONFile(mtg mtg, string path, bool ismtgsaved = false)
        {

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@path + ".json"))
            {

                var jss = new JavaScriptSerializer();
                jss.MaxJsonLength = 20000000;

                file.WriteLine("{");
                file.WriteLine("\"root\":0,");


                mtg.metadata["file"]["name"] = path.Split('\\')[path.Split('\\').Length-1];
                var dict = jss.Serialize(mtg.metadata);
                file.WriteLine("\"metadata\":");
                file.Write(dict);

                string verticeList = "";

                foreach (int s in mtg.Scales()) {
                    foreach (int iv in mtg.Vertices(s)) {
                        if (iv == 1) verticeList = iv.ToString(); else verticeList = verticeList + "," + iv.ToString();
                    }
                }


                file.WriteLine(",\n\"vertices\":");
                file.WriteLine("["+verticeList+"],");


                Dictionary<string, int> parentString = new Dictionary<string, int>();
                foreach (int key in mtg.parent.Keys) parentString.Add(key.ToString(), mtg.parent[key]);
                dict = jss.Serialize(parentString);
                file.WriteLine("\"parent\":");
                file.Write(dict);

                Dictionary<string, List<int>> childrenString = new Dictionary<string, List<int>>();
                foreach (int key in mtg.children.Keys) childrenString.Add(key.ToString(), mtg.children[key]);

                dict = jss.Serialize(childrenString);
                file.WriteLine(",\n\"children\":");
                file.Write(dict);

                Dictionary<string, int> complexString = new Dictionary<string, int>();
                foreach (int key in mtg.complex.Keys) complexString.Add(key.ToString(), mtg.complex[key]);

                dict = jss.Serialize(complexString);
                file.WriteLine(",\n\"complex\":");
                file.Write(dict);

                Dictionary<string, List<int>> componentString = new Dictionary<string, List<int>>();
                foreach (int key in mtg.components.Keys) componentString.Add(key.ToString(), mtg.components[key]);

                dict = jss.Serialize(componentString);
                file.WriteLine(",\n\"components\":");
                file.Write(dict);

                Dictionary<string,int> scaleString=new Dictionary<string,int>();

                foreach(int key in mtg.scale.Keys) scaleString.Add(key.ToString(),mtg.scale[key]);

                dict = jss.Serialize(scaleString);
                file.WriteLine(",\n\"scales\":");
                file.Write(dict);

                Dictionary<string, Dictionary<string, dynamic>> propertiesString = new Dictionary<string, Dictionary<string, dynamic>>();

                foreach (string key in mtg.properties.Keys){
                    Dictionary<string, dynamic> TampProp=new Dictionary<string,dynamic>();
                    foreach (int keydict in mtg.properties[key].Keys) TampProp.Add(keydict.ToString(), mtg.properties[key][keydict]);
                    propertiesString.Add(key, TampProp);

                }
                dict = jss.Serialize(propertiesString);
                file.WriteLine(",\n\"properties\":");
                file.Write(dict);
                file.WriteLine("\n}");
                

            }

           /* using (System.IO.StreamReader r = new System.IO.StreamReader(@path + ".json"))
            {
                string json = r.ReadToEnd();
                var jss = new JavaScriptSerializer();
                Dictionary<int, Dictionary<int, int>> items = jss.Deserialize<Dictionary<int, Dictionary<int, int>>>(json);
            }*/

        }



        #endregion

        #region MTG iterators

        /// <summary>
        /// Iterate all components of the vertexId.
        /// </summary>
        /// <param name="tree"> The MTG. </param>
        /// <param name="vertexId"> The vertex from which the iteration begins. </param>
        /// <returns> Iterator on components of the MTG starting from a vertex. </returns>
        public IEnumerable<int> MtgIterator(mtg tree, int vertexId)
        {
            Dictionary<int, bool> visited = new Dictionary<int, bool>();
            visited.Add(vertexId, true);

            int complexId = vertexId;

            int maxScale = tree.MaxScale();

            yield return vertexId;

            foreach(int vertex in tree.ComponentRootsAtScale(complexId,maxScale))
            {
                foreach(int vid in IterativePreOrder(tree,vertex))
                {
                    foreach (int node in ScaleIterator(tree, vid, complexId, visited))
                        yield return node;
                }
            }

        }

        /// <summary>
        /// Internal method used by MtgIterator.
        /// </summary>
        IEnumerable<int> ScaleIterator(mtg tree, int vertexId, int complexId, Dictionary<int, bool> visited)
        {
            if(vertexId != -1 && !visited.ContainsKey(vertexId) && ( tree.ComplexAtScale(vertexId,(int)tree.Scale(complexId)) == complexId))
            {
                foreach (int v in ScaleIterator(tree, (int)tree.Complex(vertexId), complexId, visited))
                {
                    yield return v;
                }

                visited.Add(vertexId, true);
                yield return vertexId;
            }
        }

        #endregion

    }

    public class Visitor
    {
        public void PreOrder(int vertexId)
        {

        }

        public void PostOrder(int vertexId)
        {

        }
    }
}
