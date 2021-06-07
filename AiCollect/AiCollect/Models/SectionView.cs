using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AiCollect.Models
{
    public class SectionView
    {
        public string Key { get; set; }
        public ContentView ContentView { get; set; }
        public bool IsValid { get; set; }
    }
}
