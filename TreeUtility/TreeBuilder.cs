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
            DirSearch(path, root, printFiles);
            FormattedOutput(_output, root, new Stack<string>());
        }

        void DirSearch(string directory, TreeNode<string> node, bool includeFiles)
        {
            try
            {
                if (includeFiles)
                {
                    foreach (var filePath in Directory.GetFiles(directory))
                    {
                        var length = new FileInfo(filePath).Length;
                        var fileSize = length != 0 ? new string($"({length}b)") : new string("(empty)");
                        node.AddChild(Path.GetFileName(filePath) + " " + fileSize);
                    }
                }
                foreach (var dirPath in Directory.GetDirectories(directory))
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

        void FormattedOutput(TextWriter output, TreeNode<string> node, Stack<string> tabScheme)
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

        string Tabs(Stack<string> tabScheme, bool last)
        {
            string output;

            if (last)
                output = string.Concat(tabScheme.Reverse()) + "└───";
            else
                output = string.Concat(tabScheme.Reverse()) + "├───";

            return output;
        }

    }
}