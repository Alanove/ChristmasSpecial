using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChristmasSpecial
{

    public class Solution
    {

        public Solution()
        {
        }

        /**
         * Given all available gear radii on the ground, the peg distances and
         * the gear ratio to achieve, this method returns the placement of the
         * gears.
         */
        public List<int> GearOrder(List<int> pegDistances, List<int> gearRadii, int gearRatio)
        {
            //Console.WriteLine("pegDistances " + String.Join(", ", pegDistances));
            //Console.WriteLine("gearRadii " + String.Join(", ", gearRadii));
            //Console.WriteLine("gearRatio " + gearRatio.ToString());
            //need to find first and last possible correct lists
            List<SolutionObject> possibleSolutions =
                findPossibleListEnds(gearRadii, gearRatio, pegDistances);
            List<int> ret = new List<int>();
            var solutionFound = false;

            for (var i = 0; i < possibleSolutions.Count && !solutionFound; i++)
            {
                if (possibleSolutions[i].FixSelf())
                {
                    ret = possibleSolutions[i].Solution;
                    break;
                }
            }
            //Console.WriteLine("ret " + String.Join(", ", ret));
            return ret;
        }

        /// <summary>
        /// Finds all possible ends for corresponding inputs
        /// First and last item of possible solutions
        /// </summary>
        /// <param name="gearRadii"></param>
        /// <param name="gearRatio"></param>
        /// <param name="pegDistances"></param>
        /// <returns></returns>
        List<SolutionObject> findPossibleListEnds(List<int> gearRadii, int gearRatio, List<int> pegDistances)
        {
            var ret = new List<SolutionObject>();
            if (gearRadii.Count == 0) //if no gear provided we need to generate ends
            {
                return createEnds(pegDistances, gearRatio);
            }
            var found = new List<string>();
            for (var i = 0; i < gearRadii.Count; i++)
            {
                for (var j = 0; j < gearRadii.Count; j++)
                {
                    if (j == i) //we don't want to add the same indexes
                        continue;
                    if (gearRadii[i] / gearRadii[j] == gearRatio ||
                        gearRadii[j] / gearRadii[i] == gearRatio) // possible match
                    {
                        var val1 = gearRadii[i];
                        var val2 = gearRadii[j];
                        var stringVals = val1 > val2 ? string.Format("{0}{1}", val2, val1) : string.Format("{0}{1}", val1, val2);
                        if (found.Contains(stringVals)) //just checking if we already have this combination
                            continue;
                        found.Add(stringVals);
                        //create a possible solution object
                        SolutionObject sol = new SolutionObject(gearRadii, pegDistances, gearRatio);

                        sol.Solution[0] = gearRadii[i];
                        sol.Solution[sol.Solution.Count - 1] = gearRadii[j];
                        sol.Gears.RemoveAt(i);
                        sol.Gears.RemoveAt(j - 1); //removing at j - 1 because we already removed one item from the list
                        ret.Add(sol);

                        //there are 2 possible solutins for these ends (we just need to reverse the list)
                        var sol1 = new SolutionObject(sol);
                        sol1.Solution.Reverse();
                        ret.Add(sol1);
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// This would create the ends for a possible solution in case gears was not provided
        /// </summary>
        /// <param name="distances"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        List<SolutionObject> createEnds(List<int> distances, int ratio)
        {
            var ret = new List<SolutionObject>();
            var max = distances.Max();
            for (var i = max - max % ratio; i >= ratio; i -= ratio)
            {
                if (i % ratio != 0)
                    continue;
                for (var j = 1; j < i; j++)
                {
                    if (((double)i / (double)j) == (double)ratio)
                    {
                        SolutionObject sol = new SolutionObject(null, distances, ratio);

                        sol.Solution[0] = i;
                        sol.Solution[sol.Solution.Count - 1] = j;
                        ret.Add(sol);

                        var sol1 = new SolutionObject(sol);
                        sol1.Solution.Reverse();
                        ret.Add(sol1);
                        break;
                    }
                }
            }
            return ret;
        }
    }
}
