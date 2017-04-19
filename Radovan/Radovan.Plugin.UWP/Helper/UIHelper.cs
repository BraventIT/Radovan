using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Radovan.Plugin.UWP.Helper
{
    public static class UIHelper
    {
        public static T FindFirstChild<T>(this DependencyObject parent) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindFirstChild<T>(child);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static T FindFirstChild<T>(this DependencyObject parent, string name) where T : FrameworkElement
        {
            // Exec
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T && ((FrameworkElement)child).Name == name)
                    return (T)child;
                else
                {
                    T childOfChild = FindFirstChild<T>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static IEnumerable<T> FindAllChilds<T>(this DependencyObject parent) where T : FrameworkElement
        {
            List<T> childs = new List<T>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                    childs.Add(child as T);
                else
                {
                    childs.AddRange(FindAllChilds<T>(child));
                }
            }

            return childs;
        }


        public static FrameworkElement GetVisualParent(
          this FrameworkElement childElement)
        {
            return VisualTreeHelper.GetParent(childElement) as FrameworkElement;
        }

        public static T FindFirstAncestor<T>
          (this FrameworkElement descendentElement) where T : FrameworkElement
        {
            FrameworkElement parent = descendentElement.GetVisualParent();
            while (parent != null)
            {
                T item = parent as T;
                if (item != null)
                    return item;

                parent = parent.GetVisualParent();
            }
            return null;
        }
    }
}
