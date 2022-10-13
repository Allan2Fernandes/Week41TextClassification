using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextClassification.Domain
{
    public class FileLists
    {
        private List<string> _a;
        private List<string> _b;
        private List<string> testa;
        private List<string> testb;

        public FileLists(){

        }

        public void SetA(List<string> a)
        {
            _a = a;
        }

        public void SetB(List<string> b)
        {
            _b = b;
        }

        public void SetTestA(List<string> a)
        {
            testa = a;
        }

        public void SetTestB(List<string> b)
        {
            testb = b;
        }

        public List<string> GetA()
        {
            return _a;
        }

        public List<string> GetB()
        {
            return _b;
        }

        public List<string> GetTestA()
        {
            return testa;
        }

        public List<string> GetTestB()
        {
            return testb;
        }

        public List<string> GetBoth()
        {
            return _a.Concat(_b).ToList(); ;
        }

    }
}
