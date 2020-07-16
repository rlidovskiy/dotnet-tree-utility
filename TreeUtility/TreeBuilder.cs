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
            TreeNode<String> root = new TreeNode<String>(path);
            DirSearch(path, root, printFiles);
            FormattedOutput(_output, root, new Stack<String>());
        }

        void DirSearch(string directory, TreeNode<string> node, bool includeFiles)
        {
            try
            {
                if (includeFiles)
                {
                    foreach (string filePath in Directory.GetFiles(directory))
                    {
                        node.AddChild(Path.GetFileName(filePath));
                    }
                }
                foreach (string dirPath in Directory.GetDirectories(directory))
                {
                    var childNode = node.AddChild(Path.GetFileName(dirPath));
                    DirSearch(dirPath, childNode, includeFiles);
                }
            }
            catch (System.Exception ex)
            {
                _output.WriteLine(ex.Message);
            }
        }

        void FormattedOutput(TextWriter output, TreeNode<String> node, Stack<String> tabScheme)
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

        string Tabs(Stack<String> tabScheme, bool last)
        {
            String output;

            if (last)
                output = String.Concat(tabScheme.Reverse()) + "└───";
            else
                output = String.Concat(tabScheme.Reverse()) + "├───";

            return output;
        }

    }
}