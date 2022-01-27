namespace ConsoleApp1
{
    public class ListNode
    {
        public ListNode Previous;

        public ListNode Next;

        public ListNode Random;

        public string Data;

        public static ListNode[] InitArray(int count)
        {
            var nodes = new ListNode[count];

            for (int i = 0; i < count; i++)
            {
                nodes[i] = new ListNode();
            }

            return nodes;
        }
    }
}
