using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasSpecial
{
    /// <summary>
    /// A possible solution object
    /// </summary>
    public class SolutionObject
    {
        public int Ratio;
        public List<int> Distances;
        public List<int> Gears;
        public List<int> Solution;

        bool needToFillGaps = false;
        bool canSkip = false;
        List<List<int>> forcaseSolutions = new List<List<int>>();
        public SolutionObject(List<int> gears, List<int> distances, int ratio)
        {
            if (gears != null)
                this.Gears = new List<int>(gears);
            else
                this.Gears = new List<int>();

            this.Distances = new List<int>(distances);
            this.Ratio = ratio;
            //If gears are less than distances privided this flag to fill the gaps
            needToFillGaps = this.Gears.Count < Distances.Count + 1;
            //Flag to check if we can skip some gears
            canSkip = this.Gears.Count > Distances.Count + 1;
            this.Solution = new List<int>(new int[distances.Count + 1]);
        }
        public SolutionObject(SolutionObject sol) : this(sol.Gears, sol.Distances, sol.Ratio)
        {
            this.Solution = new List<int>(sol.Solution);
        }

        /// <summary>
        /// It will fix itself and return true if succeeded
        /// </summary>
        /// <returns></returns>
        public bool FixSelf()
        {
            var isSolution = true;
            for (var i = 0; i < Distances.Count; i++)
            {
                for (var j = 0; j < Solution.Count || Solution[Solution.Count - 1] == 0; j++)
                {
                    if (!isSolution)
                    {
                        if (needToFillGaps)
                        {
                            Solution[j] = Distances[i] - Solution[j - 1];
                            if (Solution[j] <= 0)
                                return false;
                            i++;
                            if (i >= Distances.Count)
                                break;
                        }
                        else
                        {
                            break;
                        }
                    }
                        
                    isSolution = false;
                    for (var k = 0; k < Gears.Count ; k++)
                    {
                        if (Solution[j] + Gears[k] == Distances[i])
                        {
                            this.Solution[j + 1] = Gears[k];
                            Gears.RemoveAt(k);
                            isSolution = true;
                            i++;
                            break;
                        }
                    }
                }
            }
            var first = Solution[0];
            var last = Solution[Solution.Count - 1];
            var isRatio = ((double)Math.Max(first, last) / (double)Math.Min(first, last)) == Ratio;
            //before last should not be zero (as initiated)
            //ratio should also be good
            return Solution[Solution.Count - 2] != 0 && isRatio;
        }

    }
}
