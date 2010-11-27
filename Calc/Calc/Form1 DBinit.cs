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


            // *** binLeft ***
            str = "FORMAT: A << B\n";
            str += "\t A -> integer\n";
            str += "\t B -> integer\n";
            str += "\n";
            str += "INFO: returns number A binary moved B-times to left side\n";
            toolTip1.SetToolTip(binLeftButton, str);

            // *** binNeg ***
            str = "FORMAT: A!\n";
            str += "\t A -> integer\n";
            str += "\n";
            str += "INFO: returns number A binary negated\n";
            toolTip1.SetToolTip(binLeftButton, str);

            // *** binNeg ***
            str = "FORMAT: A!\n";
            str += "\t A -> integer\n";
            str += "\n";
            str += "INFO: returns number thats in binary have 0 changed\n";
            str += "to 1 and 1 changed to 0\n";
            toolTip1.SetToolTip(binRightButton, str);

            // *** And ***
            str = "FORMAT: A and B\n";
            str += "\t A -> integer\n";
            str += "\t B -> integer\n";
            str += "\n";
            str += "INFO: returns number which in binary have 1 where\n";
            str += "A and B had one, otherwise 0\n";
            toolTip1.SetToolTip(andButton, str);

            // *** Or ***
            str = "FORMAT: A or B\n";
            str += "\t A -> integer\n";
            str += "\t B -> integer\n";
            str += "\n";
            str += "INFO: returns number which in binary have 1 where\n";
            str += "A, B or Both had 1, otherwise 0\n";
            toolTip1.SetToolTip(orButton, str);

            // *** Xor ***
            str = "FORMAT: A or B\n";
            str += "\t A -> integer\n";
            str += "\t B -> integer\n";
            str += "\n";
            str += "INFO: returns number which in binary have 1 where\n";
            str += "A or B had 1,not both, otherwise 0\n";
            toolTip1.SetToolTip(xorButton, str);
            //TODO: Coel, frac atd
            // *** Flor ***
            str = "FORMAT: floor(A)\n";
            str += "\t A -> number\n";
            str += "\n";
            str += "INFO: returns integer rounded down\n";
            toolTip1.SetToolTip(floorButton, str);

            // *** mod ***
            str = "FORMAT: mod(A, B)\n";
            str += "\t A -> integer\n";
            str += "\t B -> integer\n";
            str += "\n";
            str += "INFO: returns rest after dividing A by B\n"; //CHECK!!!!!!!!!!!!!!!!!!!!
            toolTip1.SetToolTip(modButton, str);

            // *** sin ***
            str = "FORMAT: sin(A)\n";
            str += "\t A -> radians\n";
            str += "\n";
            str += "INFO: returns sine of A.\n";
            str += "Sine is trigonometric function of angle\n";
            toolTip1.SetToolTip(sinButton, str);

            // *** cos ***
            str = "FORMAT: cos(A)\n";
            str += "\t A -> radians\n";
            str += "\n";
            str += "INFO: returns cosine of A. \n";
            str += "Cosine is trigonometric function of angle\n";
            toolTip1.SetToolTip(cosButton, str);

            // *** tg ***
            str = "FORMAT: tg(A)\n";
            str += "\t A -> radians\n";
            str += "\n";
            str += "INFO: returns Tangent of A.\n";
            str += "Tangent isnt defined for 1PI, 2PI etc \n";
            toolTip1.SetToolTip(tgButton, str);

            // *** cotg ***
            str = "FORMAT: cotg(A)\n";
            str += "\t A -> radians\n";
            str += "\n";
            str += "INFO: returns Cotangent of A.\n";
            str += "Cotangent isnt defined for 1.5PI, 2.5PI etc \n";
            toolTip1.SetToolTip(cotgButton, str);

            // *** arcsin ***
            str = "FORMAT: arcsin(A)\n";
            str += "\t A -> number\n";
            str += "\n";
            str += "INFO: returns Arcsine of A.\n";
            str += "Arcsine is reverse function os sine\n";
            toolTip1.SetToolTip(arcsinButton, str);

            // *** arccos ***
            str = "FORMAT: arccos(A)\n";
            str += "\t A -> number\n";
            str += "\n";
            str += "INFO: returns Arccosine of A.\n";
            str += "Arccosine is reverse function os cosine\n";
            toolTip1.SetToolTip(arcosButton, str);

            // *** arctg ***
            str = "FORMAT: arctg(A)\n";
            str += "\t A -> number\n";
            str += "\n";
            str += "INFO: returns Arctangent of A.\n";
            str += "Arctangent is reverse function os tangent\n";
            toolTip1.SetToolTip(arctgButton, str);

            // *** arccotg ***
            str = "FORMAT: arctg(A)\n";
            str += "\t A -> number\n";
            str += "\n";
            str += "INFO: returns Arccotangent of A.\n";
            str += "Arcotangent is reverse function os cotangent\n";
            toolTip1.SetToolTip(arcotgButton, str);

            // *** power ***
            str = "FORMAT: power(A, B)\n";
            str += "\t A, B -> number\n";
            str += "\n";
            str += "INFO: returns number A powered by B\n";
            toolTip1.SetToolTip(powerButton, str);
            //TODO: hyper voloviny
            // *** exp ***
            str = "FORMAT: exp(A)\n";
            str += "\t A -> number\n";
            str += "\n";
            str += "INFO: returns number A powered by 2\n";
            toolTip1.SetToolTip(expButton, str);

            // *** 2nd root ***
            str = "FORMAT: 2throot(A)\n";
            str += "\t A -> positive number \n";
            str += "\n";
            str += "INFO: returns square root of A\n";
            toolTip1.SetToolTip(rootButton, str);

            // *** xth root ***
            str = "FORMAT: xthroot(A, B)\n";
            str += "\t A, B -> positive number \n";
            str += "\n";
            str += "INFO: returns Bth root of A\n";
            toolTip1.SetToolTip(xrootButton, str);

            // *** lg ***
            str = "FORMAT: lg(A)\n";
            str += "\t A -> positive number \n";
            str += "\n";
            str += "INFO: returns binary logarithm of A\n";
            toolTip1.SetToolTip(lgButton, str);

            // *** ln ***
            str = "FORMAT: ln(A)\n";
            str += "\t A -> positive number \n";
            str += "\n";
            str += "INFO: returns natural logarithm of A\n";
            toolTip1.SetToolTip(lnButton, str);

            // *** log ***
            str = "FORMAT: log(A, B)\n";
            str += "\t A -> positive number \n";
            str += "\n";
            str += "INFO: returns  logarithm with base B of A\n";
            toolTip1.SetToolTip(logButton, str);

            // *** permutation ***
            str = "FORMAT: permutation(A)\n";
            str += "\t A -> positive integer \n";
            str += "\n";
            str += "INFO: returns permutation of A\n";
            toolTip1.SetToolTip(permuButton, str);

            // *** combination ***
            str = "FORMAT: permutation(A, B)\n";
            str += "\t A, B -> positive integer \n";
            str += "\n";
            str += "INFO: returns A above B\n";
            toolTip1.SetToolTip(combButton, str);

            // *** variation ***
            str = "FORMAT: permutation(A, B)\n";
            str += "\t A, B -> positive integer \n";
            str += "\n";
            str += "INFO: returns variation of B items from A items\n";
            toolTip1.SetToolTip(varButton, str);

            // *** median ***
            str = "FORMAT: median(A, B, C, ...)\n";
            str += "\t A, B, C -> numbers \n";
            str += "\n";
            str += "INFO: returns middle value B\n";
            toolTip1.SetToolTip(medianButton, str);

            // *** average ***
            str = "FORMAT: average(A, B, C, ...)\n";
            str += "\t A, B, C -> numbers \n";
            str += "\n";
            str += "INFO: returns average value B\n";
            toolTip1.SetToolTip(avgButton, str);

            // *** disperse ***
            str = "FORMAT: disperse(A, B, C, ...)\n";
            str += "\t A, B, C -> numbers \n";
            str += "\n";
            str += "INFO: returns average differnce betwen numbers and\n their averge value B\n";
            toolTip1.SetToolTip(dispButton, str);

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
