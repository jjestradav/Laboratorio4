using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Laboratorio4
{
    class Helper
    {
        private static Random _random = new Random();
        private  static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }

        public static ArrayList getList()
        {
            ArrayList result = new ArrayList();
            for(int i=0; i<12; i++)
            {
                char letter = Char.ToUpper(GetLetter());
                result.Add(letter);
                result.Add(letter);

            }
            result.Add(Char.ToUpper(GetLetter()));
          
            return shuffle(result.ToArray());
        }

        private static ArrayList shuffle(object[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int j = _random.Next(i, array.Length);
                char temp = (char)array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return new ArrayList(array);
        }
    }
}
