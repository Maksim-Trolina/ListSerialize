using ConsoleApp1;

var nodes = new ListNode[5];
nodes[0] = new ListNode()
{
    Data = "1"
};
nodes[1] = new ListNode()
{
    Data = "2"
};
nodes[2] = new ListNode()
{
    Data = "3"
};
nodes[3] = new ListNode()
{
    Data = "4"
};
nodes[4] = new ListNode()
{
    Data = "5"
};

nodes[0].Next = nodes[1];
nodes[0].Random = nodes[3];

nodes[1].Previous = nodes[0];
nodes[1].Next = nodes[2];
nodes[1].Random = nodes[4];

nodes[2].Previous = nodes[1];
nodes[2].Next = nodes[3];
nodes[2].Random = nodes[0];

nodes[3].Previous = nodes[2];
nodes[3].Next = nodes[4];
nodes[3].Random = nodes[0];

nodes[4].Previous = nodes[3];
nodes[4].Random = nodes[4];

var list = new ListRandom()
{
    Head = nodes[0],
    Tail = nodes[4],
    Count = nodes.Length
};
var expectedList = new ListRandom()
{
    Head = nodes[0],
    Tail = nodes[4],
    Count = list.Count
};

string pathToFile = "TestAppFile.txt";

FileStream serializeStream = new(pathToFile, FileMode.OpenOrCreate);
list.Serialize(serializeStream);

FileStream deserializeStream = new(pathToFile, FileMode.OpenOrCreate);
list.Deserialize(deserializeStream);

File.Delete(pathToFile);