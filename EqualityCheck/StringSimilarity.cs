using System;
using System.Threading;

namespace EqualityCheck
{
    public static class StringSimilarity
    {
        /// <summary>
        /// Compute the Damerau-Levenshtein distance between two strings.
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="target">Targer</param>
        /// <returns>The Damerau-Levenshtein distance</returns>
        private static int ComputeDamerauLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
            {
                if (string.IsNullOrEmpty(target))
                {
                    return 0;
                }
                return target.Length;
            }
            if (string.IsNullOrEmpty(target))
            {
                return source.Length;
            }

            // Create a 2D array to store the scores for each possible alignment of the two strings
            var score = new int[source.Length + 2, target.Length + 2];

            // Set the initial scores for the first row and column
            var infinity = source.Length + target.Length;
            score[0, 0] = infinity;
            for (var i = 0; i <= source.Length; i++)
            {
                score[i + 1, 1] = i;
                score[i + 1, 0] = infinity;
            }
            for (var j = 0; j <= target.Length; j++)
            {
                score[1, j + 1] = j;
                score[0, j + 1] = infinity;
            }

            // Create a sorted dictionary to store the last index of each character in the source and target strings
            var sortedDict = new System.Collections.Generic.SortedDictionary<char, int>();
            foreach (var letter in (source + target))
            {
                if (!sortedDict.ContainsKey(letter))
                    sortedDict.Add(letter, 0);
            }

            // Calculate the scores for each possible alignment of the two strings
            for (var i = 1; i <= source.Length; i++)
            {
                var db = 0;
                for (var j = 1; j <= target.Length; j++)
                {
                    var i1 = sortedDict[target[j - 1]];
                    var j1 = db;

                    if (source[i - 1] == target[j - 1])
                    {
                        score[i + 1, j + 1] = score[i, j];
                        db = j;
                    }
                    else
                    {
                        score[i + 1, j + 1] = Math.Min(score[i, j], Math.Min(score[i + 1, j], score[i, j + 1])) + 1;
                    }

                    score[i + 1, j + 1] = Math.Min(score[i + 1, j + 1], score[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }

                sortedDict[source[i - 1]] = i;
            }

            return score[source.Length + 1, target.Length + 1];
        }

        /// <summary>
        /// Calculates the similarity between two strings using the Damerau-Levenshtein distance algorithm
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="target">Target</param>
        /// <returns>The similarity between the two strings</returns>
        public static float ComputeSimilarity(string source, string target, bool caseSensitive = false)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
            {
                throw new Exception("The inputs could not be null or white space.");
            }

            if (!caseSensitive)
            {
                source = source.ToLower();
                target = target.ToLower();
            }

            float distance = ComputeDamerauLevenshteinDistance(source, target);

            // Compute the maximum length of the two strings
            float maxLength = source.Length;
            if (maxLength < target.Length)
            {
                maxLength = target.Length;
            }
            if (maxLength == 0.0F)
            {
                return 1.0F;
            }
            return 1.0F - distance / maxLength;
        }

        /// <summary>
        /// Calculates the similarity percentage between two strings using the Damerau-Levenshtein distance algorithm
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="target">Target</param>
        /// <returns>The similarity percentage between the two strings</returns>
        public static float ComputeSimilarityPercentage(this string source, string target, bool caseSensitive = false)
        {
            return ComputeSimilarity(source, target, caseSensitive) * 100;
        }
    }
}
