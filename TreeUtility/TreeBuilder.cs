using System.IO;
using System.Linq;

namespace TreeUtility
{
    public class TreeBuilder
    {
        private readonly TextWriter _output;

        public TreeBuilder(TextWriter output)
        {
            _output = output;
        }

        public void Build(string path, bool printFiles)
        {
            //put your solution here
            //_output.Write(...);


        }



        void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        lstFilesFound.Items.Add(f);
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception ex)
            {
                _output.WriteLine(ex.Message);
            }
        }


        static IEnumerable<string> GetFiles(string path)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }
    }
}