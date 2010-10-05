using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Calc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    class Parser
    { 
    public String Parse (String input)
        {
            EvaluationTree tree = new EvaluationTree();
            tree.Create(input);
            return tree.Evaluate().ToString();
        }
    public Boolean IsValid(String input)
        {
            EvaluationTree tree = new EvaluationTree();
            if (tree.Create(input)==0) return true;
            return false;
        }
    }

    enum Type {number, paranthesis }//root,number,+,-,...,(

    class EvaluationTreeNode
    {
        Type type;
        float value;
        //ArrayList accessors=new ArrayList();
        List<EvaluationTreeNode> accessors = new List<EvaluationTreeNode>();
        public EvaluationTreeNode() {}
        public float Evaluate() 
        {
        switch (type) 
            {
                case Type.number: return value;
                case Type.paranthesis: return accessors.Count;//return ((EvaluationTreeNode)accessors[0]).Evaluate();
                default: return -1.0f;
            }
        }
        public int Create(String input)
        {

            if (input.CompareTo("") == 0) { type = Type.number; value = 0; }
            else if (input.IndexOf("(") == 0) 
            {
                type = Type.paranthesis;
                
            
            }
            else if (float.Parse("2").ToString().CompareTo("2") == 0) 
            {
                value = float.Parse(input);
                type = Type.number;
            }
            else if (input[0] == '(') 
            {
                type = Type.paranthesis;
                EvaluationTreeNode accessor = new EvaluationTreeNode();
                accessors.Insert(1, accessor);
                if (input.LastIndexOf(")") == input.Length - 1) accessor.Create(input.Substring(1, input.Length - 2));
                else 
                {
                    type = Type.number;
                    return 1;
                }
            }



            return 0;
        }
    }

    class EvaluationTree
    {
        EvaluationTreeNode root;
        public EvaluationTree() { root = new EvaluationTreeNode(); }
        public int Create(String input) { return root.Create(input); }
        public float Evaluate() { return root.Evaluate(); }
    }
}
