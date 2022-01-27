using System.Text;

namespace ConsoleApp1
{
    static internal class ListRandomSerializer
    {
        const int NullId = -1;
        const int FirstElementId = 0;
        public static ListRandom Deserialize(Stream stream)
        {
            var nodesData = CreateNodesData(stream);
            int count = nodesData.Count;
            var nodes = ListNode.InitArray(count);
            int lastElementId = count - 1;

            FillNodes(nodesData, nodes, lastElementId);

            return CreateListRandom(count, nodes, lastElementId);
        }

        public static void Serialize(Stream stream, ListRandom list)
        {
            Dictionary<ListNode, int> listNodesId = CreateListNodesId(list);

            var sb = GetSerializeStrings(listNodesId);

            using (StreamWriter sw = new(stream))
            {
                sw.Write(sb);
            }
        }
        static StringBuilder GetSerializeStrings(Dictionary<ListNode, int> listNodesId)
        {
            StringBuilder sb = new();

            foreach (var listNodeId in listNodesId)
            {
                var node = listNodeId.Key;
                int id = listNodeId.Value;
                int randomId = GetValidId(node.Random, listNodesId);
                string data = node.Data;

                sb.AppendLine(GetSerializeString(id, randomId, data));
            }

            return sb;
        }
        static Dictionary<ListNode, int> CreateListNodesId(ListRandom list)
        {
            Dictionary<ListNode, int> listNodesId = new();
            int currentId = FirstElementId;

            for (var current = list.Head; current != null; current = current.Next, currentId++)
            {
                listNodesId.Add(current, currentId);
            }

            return listNodesId;
        }

        static ListRandom CreateListRandom(int count, ListNode[] nodes, int lastElementId)
        {
            var list = new ListRandom
            {
                Count = count,
                Head = nodes[FirstElementId],
                Tail = nodes[lastElementId]
            };

            return list;
        }

        static Dictionary<int, Tuple<int, string>> CreateNodesData(Stream stream)
        {
            var nodesData = new Dictionary<int, Tuple<int, string>>();
            using (var sr = new StreamReader(stream))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var nodeData = GetNodeData(line);
                    nodesData.Add(nodeData.Key, nodeData.Value);
                }
            }

            return nodesData;
        }

        static void FillNodes(Dictionary<int, Tuple<int, string>> nodesData, ListNode[] nodes, int lastElementId)
        {
            foreach (var nodeData in nodesData)
            {
                FillNode(nodeData, nodes, lastElementId);
            }
        }

        static void FillNode(KeyValuePair<int, Tuple<int, string>> nodeData, ListNode[] nodes, int lastElementId)
        {
            int id = nodeData.Key;
            int randomId = nodeData.Value.Item1;
            string data = nodeData.Value.Item2;

            nodes[id].Data = data;
            nodes[id].Previous = GetPreviousNode(nodes, id);
            nodes[id].Next = GetNextNode(nodes, id, lastElementId);
            nodes[id].Random = GetRandomNode(nodes, id, randomId);
        }

        static KeyValuePair<int, Tuple<int, string>> GetNodeData(string line)
        {
            var properties = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            var id = int.Parse(properties[0]);
            var randomId = int.Parse(properties[1]);
            var data = properties[2];

            return new KeyValuePair<int, Tuple<int, string>>(id, new Tuple<int, string>(randomId, data));
        }

        static string GetSerializeString(int id, int randomId, string data)
        {
            return $"{id};{randomId};{data}";
        }

        static int GetValidId(ListNode node, Dictionary<ListNode, int> listNodesId)
        {
            return node == null ? NullId : listNodesId[node];
        }

        static int GetPreviousNodeId(int currentNodeId)
        {
            return currentNodeId - 1;
        }

        static int GetNextNodeId(int currentNodeId)
        {
            return currentNodeId + 1;
        }

        static ListNode GetPreviousNode(ListNode[] nodes, int id)
        {
            return id == FirstElementId ? null : nodes[GetPreviousNodeId(id)];
        }

        static ListNode GetNextNode(ListNode[] nodes, int id, int lastElementId)
        {
            return id == lastElementId ? null : nodes[GetNextNodeId(id)];
        }

        static ListNode GetRandomNode(ListNode[] nodes, int id, int randomId)
        {
            return id == NullId ? null : nodes[randomId];
        }
    }
}
