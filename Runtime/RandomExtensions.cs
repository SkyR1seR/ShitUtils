using UnityEngine;

namespace ShitUtils
{
    public static class RandomExtensions
    {
        public static int Random(this Vector2Int range, bool includeMax = false)
        {
            return UnityEngine.Random.Range(range.x, range.y + (includeMax?1:0));
        }
        
        public static float Random(this Vector2 range)
        {
            return UnityEngine.Random.Range(range.x, range.y);
        }
        
        public static float GetRandomWithExclude(float min, float max, float excludeMin, float excludeMax)
        {
            float leftSize = excludeMin - min;
            float rightSize = max - excludeMax;
            float totalSize = leftSize + rightSize;

            if (totalSize <= 0f)
                throw new System.Exception("No available random number excluded.");

            float rand = UnityEngine.Random.Range(0f, totalSize);

            if (rand < leftSize)
                return min + rand;
            else
                return excludeMax + (rand - leftSize);
        }

        public static int GetRandomWithExclude(int min, int max, int excludeMin, int excludeMax)
        {
            int leftSize = excludeMin - min;
            int rightSize = max - excludeMax;

            int totalSize = leftSize + rightSize;

            if (totalSize <= 0)
                throw new System.Exception("No available random number excluded.");

            int rand = UnityEngine.Random.Range(0, totalSize);

            if (rand < leftSize)
                return min + rand;
            else
                return excludeMax + (rand - leftSize);
        }
    }
}