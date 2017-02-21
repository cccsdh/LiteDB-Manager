using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using LiteDB;
using LiteDBManager.Models;

namespace LiteDBManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiteDatabase _db;
        private string _dbFile;
        private string _json;
        public string jsonText { get { return _json; } }

        public MainWindow()
        {
            InitializeComponent();
       

        }
        private void Open_Database_File(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                _db = new LiteDatabase(openFileDialog.FileName);
            _dbFile = openFileDialog.FileName;

            LoadTreeView();
        }
        private void LoadTreeView()
        {
            var dbName = System.IO.Path.GetFileName(_dbFile);
            var root = new TreeItem() { Json="" , Name= dbName };
            var collections = _db.GetCollectionNames();
            foreach (var name in collections)
            {
                var childItem = new TreeItem() { Json="",Name=name };
                var collection = _db.GetCollection(name);
                var docs = collection.FindAll();
                foreach (var doc in docs)
                {
                    var id = doc["_id"];



                    if (id.AsObjectId != null)
                    {
                        childItem.Items.Add(new TreeItem() { Json = JsonSerializer.Serialize(doc, true), Name = id.AsString });
                    }
                    else
                    {
                        if (id.AsString != null)
                        {
                            childItem.Items.Add(new TreeItem() { Json=JsonSerializer.Serialize(doc, true), Name = id.AsString });
                        }


                    }
                }
                root.Items.Add(childItem);
            }
            dbView.Items.Add(root);
        }


        private void dbView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = dbView.SelectedItem as TreeItem;


                JsonText.Text = item.Json;

           
        }
    }
}
