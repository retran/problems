public class MyLinkedList
{
    private class ListNode
    {
        public int Value;
        public ListNode Next;
        public ListNode Prev;

        public ListNode(int val = 0, ListNode next = null, ListNode prev = null)
        {
            Value = val;
            Next = next;
            Prev = prev;
        }
    }

    private ListNode head;
    private ListNode tail;
    private int size;

    public MyLinkedList()
    {
        head = new ListNode(0);
        tail = new ListNode(0);
        head.Next = tail;
        tail.Prev = head;
        size = 0;
    }

    public int Get(int index)
    {
        if (index < 0 || index >= size)
        {
            return -1;
        }

        var curr = head;
        if (index + 1 < size - index)
        {
            for (int i = 0; i <= index; i++)
            {
                curr = curr.Next;
            }
        }
        else
        {
            curr = tail;
            for (int i = 0; i < size - index; i++)
            {
                curr = curr.Prev;
            }
        }

        return curr.Value;
    }

    public void AddAtHead(int val)
    {
        AddAtIndex(0, val);
    }

    public void AddAtTail(int val)
    {
        AddAtIndex(size, val);
    }

    public void AddAtIndex(int index, int val)
    {
        if (index < 0 || index > size) 
        {
            return;
        }

        ListNode predecessor, successor;
        if (index < size - index)
        {
            predecessor = head;
            for (int i = 0; i < index; i++)
            {
                predecessor = predecessor.Next;
            }
            successor = predecessor.Next;
        }
        else
        {
            successor = tail;
            for (int i = 0; i < size - index; i++)
            {
                successor = successor.Prev;
            }
            predecessor = successor.Prev;
        }

        var newNode = new ListNode(val)
        {
            Next = successor,
            Prev = predecessor
        };

        predecessor.Next = newNode;
        successor.Prev = newNode;

        size++;
    }

    public void DeleteAtIndex(int index)
    {
        if (index < 0 || index >= size) 
        {
            return;
        }

        ListNode predecessor, successor;
        if (index < size - index)
        {
            predecessor = head;
            for (int i = 0; i < index; i++)
            {
                predecessor = predecessor.Next;
            }
            successor = predecessor.Next.Next;
        }
        else
        {
            successor = tail;
            for (int i = 0; i < size - index - 1; i++)
            {
                successor = successor.Prev;
            }
            predecessor = successor.Prev.Prev;
        }

        predecessor.Next = successor;
        successor.Prev = predecessor;

        size--;
    }
}

/**
 * Your MyLinkedList object will be instantiated and called as such:
 * MyLinkedList obj = new MyLinkedList();
 * int param_1 = obj.Get(index);
 * obj.AddAtHead(val);
 * obj.AddAtTail(val);
 * obj.AddAtIndex(index,val);
 * obj.DeleteAtIndex(index);
 */
