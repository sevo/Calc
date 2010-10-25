using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calc
{
    public partial class Form1 : Form
    {
        public void InitializeTooltips()
        {

            string str;

            // *** fibonacci ***
            str="FORMAT: fibbonaci(X)\n";
            str += "\t X -> integer\n";
            str += "\n";
            str += "INFO: Fibonacci numbers are used in the analysis\n";
            str += "of financial markets, in strategies such as\n";
            str += "Fibonacci retracement, and are used in computer\n";
            str += "algorithms such as the Fibonacci search technique\n";
            str += "and the Fibonacci heap data structure.";
            //Tooltipy.Add(fibonacciButton.Text, str);
            toolTip1.SetToolTip(fibonacciButton, str);

            // *** pytagor ***
            str = "FORMAT: pytagoras(A,B)\n";
            str += "\t A -> side of triangle\n";
            str += "\t B -> side of trangle\n";
            str += "\n";
            str += "INFO: In any right triangle, the area of the square\n";
            str += "whose side is the hypotenuse (the side opposite the\n";
            str += "right angle) is equal to the sum of the areas of the\n";
            str += "squares whose sides are the two legs (the two sides\n";
            str += "that meet at a right angle).";
            toolTip1.SetToolTip(pytagorasButton, str);

            // *** sum *** 
            str = "FORMAT: sum(A,B,C,...)\n";
            str += "\t A,B,C -> numbers\n";
            str += "\n";
            str += "INFO: returns sum of all numbers";
            toolTip1.SetToolTip(sumButton, str);

            // *** factorial***
            str = "FORMAT: factorial(X)\n";
            str += "\t X -> integer\n";
            str += "\n";
            str += "INFO: returns factorial of te number. \n";
            str += "The factorial operation is encountered in many\n";
            str += "different areas of mathematics, notably in\n";
            str += "combinatorics, algebra and mathematical analysis";
            toolTip1.SetToolTip(factorialButton, str);

            // *** mod ***
            str = "FORMAT: A mod B\n";
            str += "\t A -> divident\n";
            str += "\t B -> divisor\n";
            str += "\n";
            str += "INFO: returns remainder after dividing.";
            toolTip1.SetToolTip(modButton, str);

            // *** prime ***
            str = "FORMAT: prime(A)\n";
            str += "\t A -> integer\n";
            str += "\n";
            str += "INFO: returns 1 if the number is prime number,\n otherwise returns 0.";
            toolTip1.SetToolTip(primeButton, str);

            // *** random ***
            str = "FORMAT: random()\n";
            str += "\t A -> upper limit\n";
            str += "\n";
            str += "INFO: returns a random number betwen 0 and A. To obtain \n";
            str += "numbers betwen N and N+A use random(A)+N\n";
            toolTip1.SetToolTip(randomButton, str);

            foreach (object j in tabPage5.Controls)
            {
                if (j is Button)
                {
                    Button b = j as Button;
                    toolTip1.SetToolTip(b, b.Text + "\n\n*** NYO :( ***");

                }
            }
        }
    }
}
