using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
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
            TreeNode<string> root = new TreeNode<string>(path);
            BuildTree(path, root, printFiles);
            FormattedOutput(_output, root, new Stack<string>());
        }

        private void BuildTree(string directory, TreeNode<string> node, bool includeFiles)
        {
            try
            {
                if (includeFiles)
                {
                    foreach (var filePath in Directory.GetFiles(directory))
                    {
                        var length = new FileInfo(filePath).Length;
                        var fileSize = length != 0 ? $"({length}b)" : "(empty)";
                        node.AddChild(Path.GetFileName(filePath) + " " + fileSize);
                    }
                }
                foreach (var dirPath in Directory.GetDirectories(directory))
                {
                    var childNode = node.AddChild(Path.GetFileName(dirPath));
                    BuildTree(dirPath, childNode, includeFiles);
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.ToString());
            }
        }

        private void FormattedOutput(TextWriter output, TreeNode<string> node, Stack<string> tabScheme)
        {
            var sortedChildren = node.Children.OrderBy(child => child.Data).ToArray();

            for (int i = 0; i < sortedChildren.Length; ++i)
            {
                if (i == sortedChildren.Length - 1)
                {
                    output.Write(Tabs(tabScheme, true));
                    output.Write(sortedChildren[i].Data);
                    tabScheme.Push("\t");
                }
                else
                {
                    output.Write(Tabs(tabScheme, false));
                    output.Write(sortedChildren[i].Data);
                    tabScheme.Push("│\t");
                }
                output.Write("\r\n");
                FormattedOutput(output, sortedChildren[i], tabScheme);
                tabScheme.Pop();
            }
            
        }

        private string Tabs(Stack<string> tabScheme, bool last)
        {
            string output = last ? string.Concat(tabScheme.Reverse()) + "└───" : 
                string.Concat(tabScheme.Reverse()) + "├───";

            return output;
        }

    }
}