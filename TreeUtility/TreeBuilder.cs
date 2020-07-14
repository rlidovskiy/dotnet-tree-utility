using System;
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
            TreeNode<String> root = new TreeNode<String>(path);
            DirSearch(path, root);

            FormattedOutput(_output, root);

        }

        void DirSearch(string directory, TreeNode<string> node)
        {
            try
            {
  
                foreach (string file in Directory.GetFiles(directory))
                {
                    node.AddLeaf(file);
                }

                foreach (string dir in Directory.GetDirectories(directory))
                {
                    var childNode = node.AddChildNode(dir);
                    DirSearch(directory, childNode);
                }

            }
            catch (System.Exception ex)
            {
                _output.WriteLine(ex.Message);
            }
        }

        void FormattedOutput(TextWriter output, TreeNode<String> node)
        {

            sorting(node.Children);
            foreach (string child in node.Children)
            {
                output.Write(child);
                output.Write("\r\n");
                FormattedOutput(output, child)
            }
        }

    }
}