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
            Node rootNode = new Node(path);
            //TreeNode<string> root = new TreeNode<string>(path);
            TreeNode<Node> root = new TreeNode<Node>(new Node(path));
            BuildTree(root, printFiles);
            FormattedOutput(_output, root, new Stack<string>());
        }

        private void BuildTree(TreeNode<Node> treeNode, bool includeFiles)
        {
            try
            {
                if (includeFiles)
                {
                    foreach (var filePath in Directory.GetFiles(treeNode.Data.Path))
                    {
                        var length = new FileInfo(filePath).Length;
                        var fileSize = length != 0 ? $"({length}b)" : "(empty)";
                        treeNode.AddChild(new Node((filePath) + " " + fileSize));
                    }
                }
                foreach (var dirPath in Directory.GetDirectories(treeNode.Data.Path))
                {
                    var childNode = treeNode.AddChild(new Node(dirPath));
                    BuildTree(childNode, includeFiles);
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine(ex.ToString());
            }
        }

        private void FormattedOutput(TextWriter output, TreeNode<Node> treeNode, Stack<string> tabScheme)
        {
            var sortedChildren = treeNode.Children.OrderBy(child => child.Data.Name).ToArray();

            for (int i = 0; i < sortedChildren.Length; ++i)
            {
                if (i == sortedChildren.Length - 1)
                {
                    output.Write(Tabs(tabScheme, true));
                    output.Write(sortedChildren[i].Data.Name);
                    tabScheme.Push("\t");
                }
                else
                {
                    output.Write(Tabs(tabScheme, false));
                    output.Write(sortedChildren[i].Data.Name);
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