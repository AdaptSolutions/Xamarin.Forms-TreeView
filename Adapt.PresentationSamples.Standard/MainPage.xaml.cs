using Adapt.Presentation.Controls;
using Adapt.PresentationSamples.Standard.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Adapt.PresentationSamples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private bool _IsLoaded;

        protected override void OnAppearing()
        {
            if (_IsLoaded)
            {
                return;
            }

            _IsLoaded = true;

            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("XamarinFormsTreeView.Resource.XamlItemGroups.xml");
            string xml;
            using (var reader = new StreamReader(stream))
            {
                xml = reader.ReadToEnd();
            }

            var xamlItemGroups = (XamlItemGroup)DeserialiseObject(xml, typeof(XamlItemGroup));

            var rootNodes = ProcessXamlItemGroups(xamlItemGroups);

            foreach (var node in rootNodes)
            {
                var xamlItemGroup = (XamlItemGroup)node.BindingContext;
            }

            TheTreeView.RootNodes = rootNodes;

            base.OnAppearing();
        }

        private static void ProcessXamlItems(TreeViewNode node, XamlItemGroup xamlItemGroup)
        {
            var children = new ObservableCollection<TreeViewNode>();
            foreach (var xamlItem in xamlItemGroup.XamlItems.OrderBy(xi => xi.Key))
            {
                CreateXamlItem(children, xamlItem);
            }
            node.Children = children;
        }

        private static void CreateXamlItem(IList<TreeViewNode> children, XamlItem xamlItem)
        {
            var label = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            label.SetBinding(Label.TextProperty, "Key");

            var xamlItemTreeViewNode = CreateTreeViewNode(xamlItem, label, true);
            children.Add(xamlItemTreeViewNode);
        }

        private static TreeViewNode CreateTreeViewNode(object bindingContext, Label label, bool isItem)
        {
           var node= new TreeViewNode
            {
                BindingContext = bindingContext,
                Content = new StackLayout
                {
                    Children =
                        {
                            new ResourceImage
                            {
                                Resource = isItem? "XamarinFormsTreeView.Resource.Item.png" :"XamarinFormsTreeView.Resource.FolderOpen.png" ,
                                HeightRequest= 16,
                                WidthRequest = 16
                            },
                            label
                        },
                    Orientation = StackOrientation.Horizontal
                }
            };

            //set DataTemplate for expand button content
            node.ExpandButtonTemplate = new DataTemplate(() => new ExpandButtonContent { BindingContext = node});

            return node;
        }


        //set what icons shows for expanded/Collapsed/Leafe Nodes or on request node expand icon (when ShowExpandButtonIfEmpty true).
        public class ExpandButtonContent : ContentView
        {

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                var node = (BindingContext as TreeViewNode);
                bool isLeafNode = (node.Children == null || node.Children.Count == 0);

                //empty nodes have no icon to expand unless showExpandButtonIfEmpty is et to true which will show the expand
                //icon can click and populated node on demand propably using the expand event.
                if ((isLeafNode) && !node.ShowExpandButtonIfEmpty)
                {
                    Content = new ResourceImage
                    {
                        Resource = isLeafNode ? "XamarinFormsTreeView.Resource.Blank.png" : "XamarinFormsTreeView.Resource.FolderOpen.png",
                        HeightRequest = 16,
                        WidthRequest = 16
                    };
                }
                else
                {
                    Content = new ResourceImage
                    {
                        Resource = node.IsExpanded ? "XamarinFormsTreeView.Resource.OpenGlyph.png" : "XamarinFormsTreeView.Resource.CollpsedGlyph.png",
                        HeightRequest = 16,
                        WidthRequest = 16
                    };
                }
            }

        }
            private static ObservableCollection<TreeViewNode> ProcessXamlItemGroups(XamlItemGroup xamlItemGroups)
        {
            var rootNodes = new ObservableCollection<TreeViewNode>();

            foreach (var xamlItemGroup in xamlItemGroups.Children.OrderBy(xig => xig.Name))
            {

                var label = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.Black
                };
                label.SetBinding(Label.TextProperty, "Name");

                var groupTreeViewNode = CreateTreeViewNode(xamlItemGroup, label, false);

                rootNodes.Add(groupTreeViewNode);

                groupTreeViewNode.Children = ProcessXamlItemGroups(xamlItemGroup);

                foreach (var xamlItem in xamlItemGroup.XamlItems)
                {
                    CreateXamlItem(groupTreeViewNode.Children, xamlItem);
                }

            }

            return rootNodes;
        }

        private async void TheTreeView_SelectedItemChanged(object sender, EventArgs e)
        {
            //var selectedItem = TheTreeView.SelectedItem?.BindingContext as Something;
            //if (selectedItem != null)
            //{
            //    await DisplayAlert("Item Selected", $"Selected Content: {selectedItem.TestString}", "OK");
            //}
        }

        public static object DeserialiseObject(string source, Type targetType)
        {
            var serializer = new XmlSerializer(targetType);
            var stream = new StringReader(source);
            return serializer.Deserialize(stream);
        }
    }
}