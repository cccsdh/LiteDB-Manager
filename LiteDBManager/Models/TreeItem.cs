using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Collections.ObjectModel;
using LiteDB;

namespace LiteDBManager.Models
{
    public class TreeItem
    {
        public TreeItem()
        {
            this.Items = new ObservableCollection<TreeItem>();
        }

        public string Name { get; set; }
        public string Json { get; set; }

        public ObservableCollection<TreeItem> Items { get; set; }
    }
}
