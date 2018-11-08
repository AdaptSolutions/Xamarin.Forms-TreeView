﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Xamarin.Forms
{
    public class TreeView : ScrollView
    {
        #region Fields
        private readonly StackLayout _StackLayout = new StackLayout { Orientation = StackOrientation.Vertical };

        //TODO: This initialises the list, but there is nothing listening to INotifyCollectionChanged so no nodes will get rendered
        private IList<TreeViewNode> _RootNodes = new ObservableCollection<TreeViewNode>();
        private TreeViewNode _SelectedItem;
        #endregion

        #region Public Properties

        /// <summary>
        /// The item that is selected in the tree
        /// TODO: Make this two way - and maybe eventually a bindable property
        /// </summary>
        public TreeViewNode SelectedItem
        {
            get => _SelectedItem;

            set
            {
                if (_SelectedItem == value)
                {
                    return;
                }

                if (_SelectedItem != null)
                {
                    _SelectedItem.IsSelected = false;
                }

                _SelectedItem = value;

                SelectedItemChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// The colour of the selected item
        /// TODO: Make this a bindable property for styling purposes
        /// </summary>
        public Color SelectedBackgroundColour { get; } = Color.Blue;


        /// <summary>
        /// The opacity of the box that sits over the top of the selected item
        /// TODO: Make this a bindable property for styling purposes
        /// </summary>
        public double SelectedBackgroundOpacity { get; } = .5;

        public IList<TreeViewNode> RootNodes
        {
            get => _RootNodes;
            set
            {
                _RootNodes = value;

                if (value is INotifyCollectionChanged notifyCollectionChanged)
                {
                    notifyCollectionChanged.CollectionChanged += (s, e) =>
                    {
                        RenderNodes(_RootNodes, _StackLayout, e, null);
                    };
                }

                RenderNodes(_RootNodes, _StackLayout, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset), null);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user selects a TreeViewItem
        /// </summary>
        public event EventHandler SelectedItemChanged;
        #endregion

        #region Constructor
        public TreeView()
        {
            Content = _StackLayout;
        }
        #endregion

        #region Private Methods
        private void RemoveSelectionRecursive(IEnumerable<TreeViewNode> nodes)
        {
            foreach (var treeViewItem in nodes)
            {
                if (treeViewItem != SelectedItem)
                {
                    treeViewItem.IsSelected = false;
                }

                RemoveSelectionRecursive(treeViewItem.Children);
            }
        }
        #endregion

        #region Private Static Methods
        private static void AddItems(IEnumerable<TreeViewNode> childTreeViewItems, StackLayout parent, TreeViewNode parentTreeViewItem)
        {
            foreach (var childTreeNode in childTreeViewItems)
            {
                if (!parent.Children.Contains(childTreeNode))
                {
                    parent.Children.Add(childTreeNode);
                }

                childTreeNode.ParentTreeViewItem = parentTreeViewItem;
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// TODO: A bit stinky but better than bubbling an event up...
        /// </summary>
        internal void ChildSelected(TreeViewNode child)
        {
            SelectedItem = child;
            child.IsSelected = true;
            child.SelectionBoxView.Color = SelectedBackgroundColour;
            child.SelectionBoxView.Opacity = SelectedBackgroundOpacity;
            RemoveSelectionRecursive(RootNodes);
        }
        #endregion

        #region Internal Static Methods
        internal static void RenderNodes(IEnumerable<TreeViewNode> childTreeViewItems, StackLayout parent, NotifyCollectionChangedEventArgs e, TreeViewNode parentTreeViewItem)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
            {
                //TODO: Reintate this...
                //parent.Children.Clear();
                AddItems(childTreeViewItems, parent, parentTreeViewItem);
            }
            else
            {
                AddItems(e.NewItems.Cast<TreeViewNode>(), parent, parentTreeViewItem);
            }
        }
        #endregion
    }
}