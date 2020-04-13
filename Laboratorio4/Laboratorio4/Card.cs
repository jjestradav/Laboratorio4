using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Laboratorio4
{
    public class Card : INotifyPropertyChanged 
    {
        private int index;
        private char letter;
        private bool isDiscovered = false;
        private Color bgColor = Color.Green;
        public Color BGColor
        {
            set
            {
                if (value != null)
                {
                    bgColor = value;
                    NotifyPropertyChanged();
                }
            }
            get
            {
                return bgColor;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public int Index
        {
            get { return index; }
            set { this.index = value; }
        }

        public char Letter
        {
            get { return letter; }
            set 
            { 
                this.letter = value;
                NotifyPropertyChanged();

            }

        }

        public bool IsDiscovered
        {
            get { return this.isDiscovered; }
            set { this.isDiscovered = value; }
        }
        public Card()
        {

        }

        public Card(char letter, int index)
        {
            this.letter = letter;
            this.index = index;
        }



        public override string ToString()
        {
            return letter.ToString();
        }


    }
}
