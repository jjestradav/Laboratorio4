using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Laboratorio4
{
    public class MainPageModel : BindableObject, INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        private MainPage mainPage;
        private Card previousCard = null;
        private int previousIndex = -1;
        private object[] letters = Helper.getList().ToArray();
        private int pairNumber = 0;
        private string answer = "";
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Answer
        {
            get { return answer; }
            set
            {
                if (value != null)
                {
                    answer = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public MainPageModel(MainPage mainPage)
        {
            this.mainPage = mainPage;
            AddItems();
        }

        private void AddItems()
        {

            int index = 0;
            foreach(var letter in letters)
            {
                Card card = new Card()
                {
                    Letter = '#',
                    Index = index
                };
                index++;
                this._items.Add(card);
            }
             //   Items.Add(string.Format("{0}", letter));
            

        }

        private ObservableCollection<Card> _items = new ObservableCollection<Card>();
        public ObservableCollection<Card> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                if (_items != value)
                {
                    this._items = value;
                    OnPropertyChanged(nameof(_items));
                }
            }
        }


        public Command ItemTappedCommand
        {
            get
            {
                return new Command((data) =>
                {



                    Card card = (Card)data;
                    int index = _items.IndexOf(card);
                    var ColoredCard = new Card()
                    {
                        Index = card.Index,
                        IsDiscovered = card.IsDiscovered,
                        Letter = (char)letters[index],
                        BGColor = Color.Yellow

                    };

                    if (!card.IsDiscovered)
                    {
                        bool flag = false;

                        Console.WriteLine("CURRENT  " + card.IsDiscovered);
                        if (previousIndex != -1)
                        {
                            Console.WriteLine("PREVIOUS  " + _items[previousIndex].IsDiscovered);
                        }

                        Card aux = new Card();
                        aux.Index = card.Index;
                        //Console.WriteLine("AUX INDEX= " + aux.Index);
                        aux.Letter = (char)letters[card.Index];
                        aux.IsDiscovered = true;
                        aux.BGColor = Color.Yellow;
                        
                        _items[index] = aux;

                        OnPropertyChanged(nameof(_items));
                        // _items.CollectionChanged += Items_CollectionChanged;
                        if (this.previousCard != null)
                        {
                            if (previousCard.Index != card.Index)
                            {
                                int _auxIndex = previousCard.Index;

                                if (!_items[_auxIndex].IsDiscovered && !card.IsDiscovered)
                                {
                                    Console.WriteLine("ENTRO= " + previousCard.Index);
                                    if (previousCard.Index != _items[index].Index)
                                    {
                                        if (previousCard.Letter == _items[index].Letter)
                                        {
                                            flag = true;
                                            Card newCard = new Card();
                                            newCard.Index = previousCard.Index;
                                            newCard.Letter = _items[index].Letter;
                                            newCard.IsDiscovered = true;
                                            card.IsDiscovered = true;
                                            //newCard.BGColor = Color.White;
                                            //previousCard.IsDiscovered = true;
                                            _items[previousCard.Index] = newCard;
                                            _items[index] = newCard;
                                            OnPropertyChanged(nameof(_items));
                                            previousIndex = previousCard.Index;
                                            
                                        }
                                        else
                                        {
                                            Card dummy = new Card
                                            {
                                                Index = previousCard.Index,
                                                Letter = '#',
                                                BGColor = Color.Green
                                            };
                                            Console.WriteLine("NO IGUALES" + previousCard.Index);
                                            _items[previousCard.Index] = dummy;
                                            OnPropertyChanged(nameof(_items));
                                        }

                                    }

                                }

                            }

                        }
                        if (!flag)
                        {
                            //card.BGColor = Color.Purple;
                            aux.IsDiscovered = false;
                           // aux.BGColor = Color.Green;
                            this.previousCard = aux;

                        }
                        else
                        {
                            Card dummy = new Card();
                            dummy.Index = previousCard.Index;
                            dummy.Letter = (char)this.letters[dummy.Index];
                            dummy.IsDiscovered = true;
                            dummy.BGColor = Color.White;
                            _items[previousCard.Index] = dummy;
                            _items[index] = dummy;
                            OnPropertyChanged(nameof(_items));
                            this.pairNumber++;
                            previousCard = null;

                        }


                    }

                    if (pairNumber == 12)
                    {
                        mainPage.DisplayAlert("¡Haz Ganado!", "¡Usted ha Ganado!", "Ok");
                    }

                });

            }
        }
        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine(e.OldItems);
            //throw new NotImplementedException();
        }

        public Command Reset
        {
            get
            {
                return new Command(() =>
                {
                    this.letters = Helper.getList().ToArray();
                    _items.Clear();
                    int index = 0;
                    foreach (var letter in letters)
                    {
                        Card card = new Card()
                        {
                            Letter = '#',
                            Index = index
                        };
                        index++;
                        this._items.Add(card);
                    }
                    this.pairNumber = 0;
                    Answer = "";
                });
            }
        }

        public Command VerRespuesta { get {
                return new Command(() =>
                {
                    int count = 0;
                    foreach(var letter in letters)
                    {
                        if (count == 4)
                        {
                            Answer += letter.ToString()+" " + "\n";
                            count = 0;
                        }
                        else
                        {
                            Answer += letter.ToString() + " ";
                            count++;
                        }
                        
                    }
                });
            
            } }
    }
}
