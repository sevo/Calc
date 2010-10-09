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

    /// <summary>
    /// typ uzlu stromu vyrazu
    /// </summary>
    enum Type {number, paranthesis }//root,number,+,-,...,(

    /// <summary>
    /// uzol stromu s vyrazom
    /// </summary>
    class EvaluationTreeNode
    {
        Type type;
        float value;
        List<EvaluationTreeNode> accessors = new List<EvaluationTreeNode>();
        public EvaluationTreeNode() {}

        /// <summary>
        /// vyhodnotenie vyrazu zo stromu
        /// </summary>
        /// <returns></returns>
        public float Evaluate() 
        {
        switch (type) 
            {
                case Type.number: return value;
                case Type.paranthesis: return ((EvaluationTreeNode)accessors[0]).Evaluate();
                default: return -1.0f;
            }
        }
        /// <summary>
        /// vytvori strom pre zadany vyraz
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Create(String input)
        {

            if (input.CompareTo("") == 0) { type = Type.number; value = 0; }
            try 
            {
                value = float.Parse(input);
                type = Type.number;
            }
            catch (FormatException e)
            {
                if (input[0] == '(') 
                {
                    type = Type.paranthesis;
                    EvaluationTreeNode accessor = new EvaluationTreeNode();
                    accessors.Add(accessor);
                    if (input.LastIndexOf(")") == input.Length - 1) 
                        accessor.Create(input.Substring(1, input.Length - 2));
                    else 
                    {
                        type = Type.number;
                        return 1;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// spracuje vyraz zo vstupu na ozatvorkovanu formu
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        String ProcessUserInput(String input)
        {
        return "";
        }
    }

    /// <summary>
    /// obalenie stromu vyrazu
    /// </summary>
    class EvaluationTree
    {
        EvaluationTreeNode root;
        public EvaluationTree() { root = new EvaluationTreeNode(); }
        /// <summary>
        /// vytvorenie stromu
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Create(String input) { return root.Create(input); }

        /// <summary>
        /// vyhodnotenie stromu
        /// </summary>
        /// <returns></returns>
        public float Evaluate() { return root.Evaluate(); }
    }
}
