namespace ConsoleApp1
{
    public class ListRandom
    {
        public ListNode Head;

        public ListNode Tail;

        public int Count;

        public void Serialize(Stream s)
        {
            ListRandomSerializer.Serialize(s, this);
        }

        public void Deserialize(Stream s)
        {
            var list = ListRandomSerializer.Deserialize(s);
            Count = list.Count;
            Head = list.Head;
            Tail = list.Tail;
        }
    }
}
