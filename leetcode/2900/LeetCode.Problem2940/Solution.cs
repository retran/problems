using System;
using System.Linq;

public class Solution
{
    private class SegmentTreeNode
    {
        public readonly int Start;
        public readonly int End;
        public readonly int MaxHeight;
        public readonly SegmentTreeNode? Left;
        public readonly SegmentTreeNode? Right;
        public readonly bool IsUniform;

        public SegmentTreeNode(int start, int end, int maxHeight, SegmentTreeNode? left, SegmentTreeNode? right, bool isUniform)
        {
            Start = start;
            End = end;
            MaxHeight = maxHeight;
            Left = left;
            Right = right;
            IsUniform = isUniform;
        }
    }

    private SegmentTreeNode BuildSegmentTree(int[] heights, int start, int end)
    {
        if (start == end)
        {
            return new SegmentTreeNode(start, end, heights[start], left: null, right: null, isUniform: true);
        }

        int mid = start + (end - start) / 2;
        SegmentTreeNode leftChild = BuildSegmentTree(heights, start, mid);
        SegmentTreeNode rightChild = BuildSegmentTree(heights, mid + 1, end);

        bool isUniform = leftChild.MaxHeight == rightChild.MaxHeight && leftChild.IsUniform && rightChild.IsUniform;
        int maxHeight = Math.Max(leftChild.MaxHeight, rightChild.MaxHeight);

        return new SegmentTreeNode(start, end, maxHeight, leftChild, rightChild, isUniform);
    }

    private bool DoIntervalsOverlap(int segStart, int segEnd, int queryStart, int queryEnd)
    {
        return segStart <= queryEnd && queryStart <= segEnd;
    }

    private bool IsCompletelyWithin(int segStart, int segEnd, int queryStart, int queryEnd)
    {
        return segStart >= queryStart && segEnd <= queryEnd;
    }

    private int Query(SegmentTreeNode node, int queryStart, int queryEnd, int threshold)
    {
        if (!DoIntervalsOverlap(node.Start, node.End, queryStart, queryEnd))
        {
            return -1;
        }

        if (node.IsUniform)
        {
            return IsCompletelyWithin(node.Start, node.End, queryStart, queryEnd) && node.MaxHeight > threshold
                ? node.Start
                : -1;
        }

        if (node.Left != null && node.Left.MaxHeight > threshold)
        {
            int leftResult = Query(node.Left, queryStart, queryEnd, threshold);
            if (leftResult != -1)
            {
                return leftResult;
            }
        }

        if (node.Right != null && node.Right.MaxHeight > threshold)
        {
            return Query(node.Right, queryStart, queryEnd, threshold);
        }

        return -1;
    }

    public int[] LeftmostBuildingQueries(int[] heights, int[][] queries)
    {
        if (heights == null || heights.Length == 0)
        {
            return Array.Empty<int>();
        }

        SegmentTreeNode root = BuildSegmentTree(heights, 0, heights.Length - 1);
        int[] answers = new int[queries.Length];

        for (int i = 0; i < queries.Length; i++)
        {
            int alicePos = queries[i][0];
            int bobPos = queries[i][1];

            if (alicePos > bobPos)
            {
                (alicePos, bobPos) = (bobPos, alicePos);
            }

            if (alicePos == bobPos)
            {
                answers[i] = alicePos;
                continue;
            }

            int aliceHeight = heights[alicePos];
            int bobHeight = heights[bobPos];

            if (aliceHeight < bobHeight)
            {
                answers[i] = bobPos;
                continue;
            }

            if (bobPos == heights.Length - 1)
            {
                answers[i] = -1;
                continue;
            }

            int threshold = Math.Max(aliceHeight, bobHeight);
            answers[i] = Query(root, bobPos + 1, heights.Length - 1, threshold);
        }

        return answers;
    }
}
