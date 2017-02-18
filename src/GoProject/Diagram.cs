using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using GoProject.Nodes;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;

namespace GoProject
{
    public class Diagram
    {
        static Diagram()
        {
            TinyMapper.Bind<Node, EventNode>();
            TinyMapper.Bind<Node, DataNode>();
            TinyMapper.Bind<Node, ActivityNode>();
            TinyMapper.Bind<Node, GatewayNode>();
            TinyMapper.Bind<Node, GroupNode>();
            TinyMapper.Bind<Node, PoolNode>();
            TinyMapper.Bind<Node, LaneNode>();
            TinyMapper.Bind<Node, SubProcessNode>();
        }

        public Diagram()
        {
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentCulture ?? CultureInfo.CreateSpecificCulture("en");
        }

        public Diagram(CultureInfo ci)
        {
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = ci;
        }

        [JsonProperty(PropertyName = "class", NullValueHandling = NullValueHandling.Ignore)]
        public string Class { get; set; } = "go.GraphLinksModel";

        [JsonProperty(PropertyName = "copiesArrays", NullValueHandling = NullValueHandling.Ignore)]
        public bool CopiesArrays { get; set; } = true;

        [JsonProperty(PropertyName = "copiesArrayObjects", NullValueHandling = NullValueHandling.Ignore)]
        public bool CopiesArrayObjects { get; set; } = true;

        [JsonProperty(PropertyName = "linkFromPortIdProperty", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkFromPortIdProperty { get; set; }

        [JsonProperty(PropertyName = "linkToPortIdProperty", NullValueHandling = NullValueHandling.Ignore)]
        public string LinkToPortIdProperty { get; set; }

        [JsonIgnore]
        public List<Node> TreeNodes { get; set; }

        [JsonProperty(PropertyName = "nodeDataArray", NullValueHandling = NullValueHandling.Ignore)]
        public List<Node> NodeDataArray
        {
            get
            {
                var result = TreeNodes?.Where(n => n is IGoupNode).SelectMany(x => ((IGoupNode)x).GetNodes()).ToList();// find child nodes
                result?.AddRange(TreeNodes); // top level nodes

                return result;
            }
            set
            {
                TreeNodes = new List<Node>();
                foreach (var node in value)
                {
                    switch (node.Category)
                    {
                        case NodeCategory.Pool: TreeNodes.Add(TinyMapper.Map<PoolNode>(node));
                            break;
                            //TODO:  Other typess
                    }

                }
            }
        }

        [JsonProperty(PropertyName = "linkDataArray", NullValueHandling = NullValueHandling.Ignore)]
        public List<LinkDataArray> LinkDataArray { get; set; }

        [JsonProperty(PropertyName = "modelData", NullValueHandling = NullValueHandling.Ignore)]
        public ModelData ModelData { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty(PropertyName = "isReadonly", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsReadonly { get; set; } = false;
    }
}