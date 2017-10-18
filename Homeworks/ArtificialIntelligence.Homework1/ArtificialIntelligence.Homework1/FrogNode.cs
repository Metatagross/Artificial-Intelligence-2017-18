using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Homework1
{
    public class FrogNode
    {
        public FrogNode ( )
        {

        }
        public FrogNode ( int numberOfFrogs )
        {
            Frogs = new List<char>();
            for(int i = 0 ; i < numberOfFrogs ; i++)
            {
                Frogs.Add('>');
            }
            Frogs.Add('_');
            for(int i = 0 ; i < numberOfFrogs ; i++)
            {
                Frogs.Add('<');
            }
            Nodes = new List<FrogNode>();

            ZeroPos = numberOfFrogs;
        }
        public FrogNode ( int numberOfFrogs , char left , char right )
        {
            Frogs = new List<char>();
            for(int i = 0 ; i < numberOfFrogs ; i++)
            {
                Frogs.Add(left);
            }
            Frogs.Add('_');
            for(int i = 0 ; i < numberOfFrogs ; i++)
            {
                Frogs.Add(right);
            }
            Nodes = new List<FrogNode>();

            ZeroPos = numberOfFrogs;
        }
        public FrogNode ( FrogNode node )
        {
            Frogs = new List<char>(node.Frogs);
            Nodes = new List<FrogNode>();
            ZeroPos = node.ZeroPos;
        }

        public List<char> Frogs { get; set; }
        public List<FrogNode> Nodes { get; set; }
        public int ZeroPos { get; set; }

        public static FrogNode CreateFrogNodeTree ( int numberOfFrogs )
        {
            FrogNode root = new FrogNode(numberOfFrogs);
            root = FrogNode.CreateFrogPossibilities(root);
            return root;
        }
        static FrogNode CreateFrogPossibilities ( FrogNode node )
        {

            if(node.ZeroPos - 1 >= 0 && node.Frogs[node.ZeroPos - 1] == '>')
            {
                FrogNode newNode = new FrogNode(node);
                newNode.ZeroPos = node.ZeroPos - 1;
                newNode.Frogs[node.ZeroPos] = '>';
                newNode.Frogs[newNode.ZeroPos] = '_';
                node.Nodes.Add(newNode);
            }
            if(node.ZeroPos - 2 >= 0 && node.Frogs[node.ZeroPos - 2] == '>')
            {
                FrogNode newNode = new FrogNode(node);
                newNode.ZeroPos = node.ZeroPos - 2;
                newNode.Frogs[node.ZeroPos] = '>';
                newNode.Frogs[newNode.ZeroPos] = '_';
                node.Nodes.Add(newNode);
            }
            if(node.ZeroPos + 1 < node.Frogs.Count && node.Frogs[node.ZeroPos + 1] == '<')
            {
                FrogNode newNode = new FrogNode(node);
                newNode.ZeroPos = node.ZeroPos + 1;
                newNode.Frogs[node.ZeroPos] = '<';
                newNode.Frogs[newNode.ZeroPos] = '_';
                node.Nodes.Add(newNode);
            }
            if(node.ZeroPos + 2 < node.Frogs.Count && node.Frogs[node.ZeroPos + 2] == '<')
            {
                FrogNode newNode = new FrogNode(node);
                newNode.ZeroPos = node.ZeroPos + 2;
                newNode.Frogs[node.ZeroPos] = '<';
                newNode.Frogs[newNode.ZeroPos] = '_';
                node.Nodes.Add(newNode);
            }
            foreach(FrogNode child in node.Nodes)
            {
                FrogNode.CreateFrogPossibilities(child);
            }
            return node;
        }

        public override string ToString ( )
        {
            StringBuilder sb = new StringBuilder();
            foreach(char item in Frogs)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
        public override bool Equals ( object obj )
        {
            if(!(obj is FrogNode)) return false;

            FrogNode node = obj as FrogNode;

            for(int i = 0 ; i < Frogs.Count ; i++)
            {
                if(Frogs[i] != node.Frogs[i]) return false;
            }
            return true;
        }
        public override int GetHashCode ( )
        {
            return base.GetHashCode();
        }

        public static void PrintPath ( Stack<FrogNode> path )
        {

            List<FrogNode> listified = new List<FrogNode>();
            while(path.Any())
            {
                listified.Add(path.Pop());
            }
            listified.Reverse();
            foreach(FrogNode node in listified)
            {
                Console.WriteLine(node.ToString());
            }
        }
        public static bool CheckIfAllVisited ( List<FrogNode> nodes , List<FrogNode> visited )
        {
            foreach(FrogNode node in nodes)
            {
                if(!visited.Contains(node)) return false;
            }
            return true;
        }

        public static void DFS ( int numberOfFrogs )
        {
            if(numberOfFrogs <= 0)
            {
                Console.WriteLine("No frogs to swap!");
                return;
            }
            FrogNode root = FrogNode.CreateFrogNodeTree(numberOfFrogs);
            FrogNode searched = new FrogNode(numberOfFrogs , '<' , '>');
            Stack<FrogNode> path = new Stack<FrogNode>();
            List<FrogNode> visited = new List<FrogNode>();
            path.Push(root);
            while(path.Any())
            {
                FrogNode top = path.Peek();
                if(top.Equals(searched))
                {
                    FrogNode.PrintPath(path);
                    break;
                }
                if(top.Nodes.Any() && !CheckIfAllVisited(top.Nodes , visited))
                {
                    foreach(FrogNode node in top.Nodes)
                    {
                        if(!visited.Contains(node))
                        {
                            path.Push(node);
                            break;
                        }
                    }
                }
                else
                {
                    visited.Add(path.Pop());
                }

            }


        }
    }
}
