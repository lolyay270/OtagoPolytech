using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Theory_Exam
{
    private class ClassOne
    {
        private int num1 = 3;

        private string MethodOne()
        {
            return "method one string";
        }
    }
    public class ClassTwo
    {
        ClassOne c1 = new();
        public void MethodTwo()
        {
            int num2 = c1.num1;
        }
    }
}
