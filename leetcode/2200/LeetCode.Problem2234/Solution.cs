public class Solution
{
    private static long FindLeftmostTargetIndex(int[] flowers, long n, long target)
    {
        long left = 0;
        long right = n - 1;
        while (left <= right)
        {
            long mid = left + (right - left) / 2;
            if (flowers[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return left;
    }

    private static long[] ComputePrefixSums(int[] flowers, long count)
    {
        long[] prefixSums = new long[count];
        prefixSums[0] = flowers[0];
        for (long i = 1; i < count; i++)
        {
            prefixSums[i] = prefixSums[i - 1] + flowers[i];
        }
        return prefixSums;
    }

    private static long CalculateMaxScore(int[] flowers, long newFlowers, int target, int full, int partial, long[] prefixSums, long count, long fullGardens)
    {
        long needFlowersToFillFullGardens = fullGardens * target;
        long partialGardensCount = count - fullGardens;

        long flowersInLastGardens = partialGardensCount != 0
            ? prefixSums[count - 1] - prefixSums[count - fullGardens - 1]
            : prefixSums[count - 1];

        newFlowers -= needFlowersToFillFullGardens - flowersInLastGardens;
        if (newFlowers < 0)
        {
            return 0; // imbossible to fill full gardens
        }


        if (partialGardensCount == 0)
        {
            return fullGardens * full; // there are no partial gardens
        }

        if (newFlowers == 0)
        {
            // there are no flowers anymore to add to partial gardens, so just taking lowest value
            return fullGardens * full + ((long)flowers[0]) * partial;
        }

        long minPartial;
        long left = 1;
        long right = target - 1;

        bool exact = false;
        while (left <= right)
        {
            long mid = left + (right - left) / 2;

            long index = FindLeftmostTargetIndex(flowers, partialGardensCount, mid);
            long flowersNeed = 0;
            if (index != 0)
            {
                long fullTarget = mid * index;
                long existingFlowers = prefixSums[index - 1];
                flowersNeed = fullTarget - existingFlowers;
            }

            if (newFlowers == flowersNeed)
            {
                left = mid;
                exact = true;
                break;
            }

            if (newFlowers < flowersNeed)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        minPartial = exact 
            ? left 
            : left - 1;

        return fullGardens * full + minPartial * partial;
    }

    public long MaximumBeauty(int[] flowers, long newFlowers, int target, int full, int partial)
    {
        Array.Sort(flowers);

        // count of partially filled gardens
        long count = FindLeftmostTargetIndex(flowers, flowers.Length, target);

        // static part of score
        long score = (flowers.Length - count) * full;

        if (count == 0)
        {
            return score;
        }

        long[] prefixSums = ComputePrefixSums(flowers, count);

        long max = 0;

        for (long i = 0; i <= count; i++)
        {
            max = Math.Max(max, CalculateMaxScore(flowers, newFlowers, target, full, partial, prefixSums, count, i));
        }

        return max + score;
    }
}