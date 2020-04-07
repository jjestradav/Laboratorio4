using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Laboratorio4
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    public partial class MainPage : ContentPage
    {
        MainPageModel pageModel;
        public MainPage()
        {
            InitializeComponent();
            pageModel = new MainPageModel(this);
            BindingContext = pageModel;
        }
    }
}
