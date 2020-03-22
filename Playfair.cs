using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication
{
    public class Playfair
    {
        //A B C D E
        //F G H I K
        //L M N O P
        //Q R S T U
        //V W X Y Z
        public char[,] alphabetTab = new char[5, 5];
        string standardAlphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        string alphabetWithKey = "";
        string inputText = "";
        public char[] outputText;

        public Playfair(string input, short whatToDo)
        {
            inputText = input;
            fillAlphabetTab(standardAlphabet);
            if (whatToDo == 0) encrypt();
            else decrypt();
        }

        public Playfair(string key, string input, short whatToDo)
        {
            inputText = input;
            alphabetWithKey = RemoveDuplicatedCharsFromKey(key.ToUpper());
            alphabetWithKey = alphabetWithKey.ToUpper();
            alphabetWithKey += standardAlphabet;
            alphabetWithKey = RemoveDuplicatedCharsFromKey(alphabetWithKey);
            fillAlphabetTab(alphabetWithKey);
            if (whatToDo == 0) encrypt();
            else decrypt();
        }

        private void fillAlphabetTab(string alphabet)
        {
            int counter = 0;
            for (int i = 0; i < alphabetTab.GetLength(0); i++)
            {
                for (int j = 0; j < alphabetTab.GetLength(1); j++)
                {
                    alphabetTab[i, j] = alphabet[counter];
                    counter++;
                }
            }
        }

        private string RemoveDuplicatedCharsFromKey(string key)
        {
            var toRemove = new char[] {'J', ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '+', '*', '/'}; //remove these chars from key
            return new string(key.ToCharArray().Distinct().Where( x => !toRemove.Contains(x)).ToArray());
        }

        private int[] charCords(char letter)
        {
            int[] cords = {9, 9};
            for (int i = 0; i < alphabetTab.GetLength(0); i++)
            {
                for (int j = 0; j < alphabetTab.GetLength(1); j++)
                {
                    if (alphabetTab[i,j] == letter)
                    {
                        cords[0] = i;
                        cords[1] = j;
                        return cords;
                    }
                }
            }
            return cords;
        }

        private void encrypt()
        {
            outputText = inputText.ToCharArray();
            for (int i = 0; i < inputText.Length; i++)
            {
                int j = i + 1;
                int[] firstLetterCords = new int[2];
                int[] secondLetterCords = new int[2];

                Start:
                if (inputText[i] == ' ')
                {
                    i++;
                    goto Start;
                }
                if (j == i) j++;
                if (j >= inputText.Length)
                {
                    firstLetterCords = charCords(inputText[i]);
                    secondLetterCords = charCords('X');
                    Array.Resize<char>(ref outputText, outputText.Length + 1);
                    goto Code;
                }
                if (inputText[j] == ' ')
                {
                    j++;
                    goto Start;
                }
                firstLetterCords = charCords(inputText[i]);
                secondLetterCords = charCords(inputText[j]);
                if (inputText[i] == inputText[j]) //case when letter are same, ex. aa
                {
                    if (firstLetterCords[0] == 4 && firstLetterCords[1] == 4)
                    {
                        outputText[i] = alphabetTab[0, 0];
                        outputText[j] = alphabetTab[0, 0];
                        i = j;
                        continue;
                    }
                    if (firstLetterCords[1] == 4)
                    {
                        outputText[i] = alphabetTab[firstLetterCords[0] + 1, 0];
                        outputText[j] = alphabetTab[firstLetterCords[0] + 1, 0];
                        i = j;
                        continue;
                    }
                    if (firstLetterCords[0] == 4)
                    {
                        outputText[i] = alphabetTab[0, firstLetterCords[1] + 1];
                        outputText[j] = alphabetTab[0, firstLetterCords[1] + 1];
                        i = j;
                        continue;
                    }
                    else
                    {
                        outputText[i] = alphabetTab[firstLetterCords[0] + 1, firstLetterCords[1] + 1];
                        outputText[j] = alphabetTab[firstLetterCords[0] + 1, firstLetterCords[1] + 1];
                        i = j;
                        continue;
                    }
                }
                Code:
                if (firstLetterCords[0] == secondLetterCords[0]) //same row
                {
                    if (firstLetterCords[1] == 4) outputText[i] = alphabetTab[firstLetterCords[0],0];
                    else outputText[i] = alphabetTab[firstLetterCords[0], firstLetterCords[1] + 1];
                    if (secondLetterCords[1] == 4) outputText[j] = alphabetTab[secondLetterCords[0], 0];
                    else outputText[j] = alphabetTab[secondLetterCords[0], secondLetterCords[1] + 1];
                }
                else if (firstLetterCords[1] == secondLetterCords[1]) //same col
                {
                    if (firstLetterCords[0] == 4) outputText[i] = alphabetTab[0, firstLetterCords[1]];
                    else outputText[i] = alphabetTab[firstLetterCords[0] + 1, firstLetterCords[1]];
                    if (secondLetterCords[0] == 4) outputText[j] = alphabetTab[0, secondLetterCords[1]];
                    else outputText[j] = alphabetTab[secondLetterCords[0] + 1, secondLetterCords[1]];
                }
                else //diffrent col & row
                {
                    outputText[i] = alphabetTab[firstLetterCords[0],secondLetterCords[1]];
                    outputText[j] = alphabetTab[secondLetterCords[0],firstLetterCords[1]];
                }
                i = j;
            }
        }

        private void decrypt()
        {
            outputText = inputText.ToCharArray();
            for (int i = 0; i < inputText.Length; i++)
            {
                int j = i + 1;
                int[] firstLetterCords = new int[2];
                int[] secondLetterCords = new int[2];

            Start:
                if (inputText[i] == ' ')
                {
                    i++;
                    j++;
                    goto Start;
                }
                if (j == i) j++;
                if (j >= inputText.Length)
                {
                    //firstLetterCords = charCords(inputText[i]);
                    //secondLetterCords = charCords('X');
                    Array.Resize<char>(ref outputText, outputText.Length + 1);
                    goto Code;
                }
                if (inputText[j] == ' ')
                {
                    j++;
                    goto Start;
                }
                firstLetterCords = charCords(inputText[i]);
                secondLetterCords = charCords(inputText[j]);
                if (inputText[i] == inputText[j]) //case when letter are same, ex. aa
                {
                    if (firstLetterCords[0] == 0 && firstLetterCords[1] == 0)
                    {
                        outputText[i] = alphabetTab[4, 4];
                        outputText[j] = alphabetTab[4, 4];
                        i = j;
                        continue;
                    }
                    if (firstLetterCords[1] == 0)
                    {
                        outputText[i] = alphabetTab[firstLetterCords[0] - 1, 4];
                        outputText[j] = alphabetTab[firstLetterCords[0] - 1, 4];
                        i = j;
                        continue;
                    }
                    if (firstLetterCords[0] == 0)
                    {
                        outputText[i] = alphabetTab[4, firstLetterCords[1] - 1];
                        outputText[j] = alphabetTab[4, firstLetterCords[1] - 1];
                        i = j;
                        continue;
                    }
                    else
                    {
                        outputText[i] = alphabetTab[firstLetterCords[0] - 1, firstLetterCords[1] - 1];
                        outputText[j] = alphabetTab[firstLetterCords[0] - 1, firstLetterCords[1] - 1];
                        i = j;
                        continue;
                    }
                }
            Code:
                if (firstLetterCords[0] == secondLetterCords[0]) //same row
                {
                    if (firstLetterCords[1] == 0) outputText[i] = alphabetTab[firstLetterCords[0], 4];
                    else outputText[i] = alphabetTab[firstLetterCords[0], firstLetterCords[1] - 1];
                    if (secondLetterCords[1] == 0) outputText[j] = alphabetTab[secondLetterCords[0], 4];
                    else outputText[j] = alphabetTab[secondLetterCords[0], secondLetterCords[1] - 1];
                }
                else if (firstLetterCords[1] == secondLetterCords[1]) //same col
                {
                    if (firstLetterCords[0] == 0) outputText[i] = alphabetTab[4, firstLetterCords[1]];
                    else outputText[i] = alphabetTab[firstLetterCords[0] - 1, firstLetterCords[1]];
                    if (secondLetterCords[0] == 0) outputText[j] = alphabetTab[4, secondLetterCords[1]];
                    else outputText[j] = alphabetTab[secondLetterCords[0] - 1, secondLetterCords[1]];
                }
                else //diffrent col & row
                {
                    outputText[i] = alphabetTab[firstLetterCords[0], secondLetterCords[1]];
                    outputText[j] = alphabetTab[secondLetterCords[0], firstLetterCords[1]];
                }
                i = j;
            }
        }
    }
}
