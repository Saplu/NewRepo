using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public class TargetSetter
    {
        public List<int> setTargets(int index, int targets, int EnemyCount)
        {
            var result = new List<int>();

            if (targets == 1 || EnemyCount == 1)
                result.Add(index);
            else if (targets == 2 || EnemyCount == 2)
                result = setTwoTargets(index);
            else if (targets == 3 || EnemyCount == 3)
                result = setThreeTargets(index);
            else if (targets == 4)
                result = setFourTargets(index);

            return result;
        }

        public List<int> setTargets(int index, int targets)
        {
            var result = new List<int>();

            if (targets == 1)
                result.Add(index);
            else if (targets == 2)
                result = setTwoTargets(index);
            else if (targets == 3)
                result = setThreeTargets(index);
            else if (targets == 4)
                result = setFourTargets(index);

            return result;
        }

        private List<int> setTwoTargets(int index)
        {
            var result = new List<int>();
            if (index == 1 || index == 2 || index == 3 || index == 5)
            {
                result.Add(index);
                result.Add(index + 1);
            }
            else if (index == 4 || index == 6 || index == 7 || index == 8)
            {
                result.Add(index);
                result.Add(index - 1);
            }
            return result;
        }

        private List<int> setThreeTargets(int index)
        {
            var result = new List<int>();
            if (index == 1 || index == 2 || index == 5)
            {
                result.Add(index);
                result.Add(index + 1);
                result.Add(index + 2);
            }
            else if (index == 3 || index == 4 || index == 7 || index == 8)
            {
                result.Add(index);
                result.Add(index - 1);
                result.Add(index - 2);
            }
            else if (index == 6)
            {
                result.Add(index);
                result.Add(index + 1);
                result.Add(index - 1);
            }
            return result;
        }

        private List<int> setFourTargets(int index)
        {
            var result = new List<int>();
            if (index < 5)
            {
                result.Add(1);
                result.Add(2);
                result.Add(3);
                result.Add(4);
            }
            else
            {
                result.Add(5);
                result.Add(6);
                result.Add(7);
                result.Add(8);
            }
            return result;
        }
    }
}
