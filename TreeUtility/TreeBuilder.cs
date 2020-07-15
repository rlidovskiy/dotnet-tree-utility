using System;
using System.Collections.Immutable;
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
                    node.AddChild(file);
                }
                foreach (string dir in Directory.GetDirectories(directory))
                {
                    var childNode = node.AddChild(dir);
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
            var sortedChildren = node.Children.OrderBy(child => child.Data);
            foreach (var child in sortedChildren)
            {
                output.Write(child.Data);
                output.Write("\r\n");
                FormattedOutput(output, child);
            }
        }

    }
}