﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;

namespace QuanLyGara.Components
{
    public class FilteredComboBox : ComboBox
    {
        private string oldFilter = string.Empty;

        private string currentFilter = string.Empty;

        protected TextBox EditableTextBox => GetTemplateChild("PART_EditableTextBox") as TextBox;

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                var view = CollectionViewSource.GetDefaultView(newValue);
                if (view != null)
                {
                    view.Filter += FilterItem;
                }
            }

            if (oldValue != null)
            {
                var oldView = CollectionViewSource.GetDefaultView(oldValue);
                if (oldView != null) oldView.Filter -= FilterItem;
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Tab:
                case Key.Enter:
                    IsDropDownOpen = false;
                    break;
                case Key.Escape:
                    IsDropDownOpen = false;
                    SelectedIndex = -1;
                    Text = currentFilter;
                    break;
                default:
                    if (e.Key == Key.Down) IsDropDownOpen = true;

                    base.OnPreviewKeyDown(e);
                    break;
            }
            
            oldFilter = Text;
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                case Key.Down:
                    break;
                case Key.Tab:
                case Key.Enter:
                    ClearFilter();
                    break;
                default:
                    if (Text != oldFilter)
                    {
                        RefreshFilter();
                        IsDropDownOpen = true;

                        EditableTextBox.SelectionStart = int.MaxValue;
                    }

                    base.OnKeyUp(e);
                    currentFilter = Text;
                    break;
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            currentFilter = "";
            oldFilter = "";
        }

        protected override void OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            ClearFilter();
            var temp = SelectedIndex;
            SelectedIndex = -1;
            Text = string.Empty;
            SelectedIndex = temp;
            base.OnPreviewLostKeyboardFocus(e);
        }

        private void RefreshFilter()
        {
            if (ItemsSource == null) return;

            var view = CollectionViewSource.GetDefaultView(ItemsSource);
            view.Refresh();
        }

        private void ClearFilter()
        {
            currentFilter = string.Empty;
            RefreshFilter();
        }

        private bool FilterItem(object value)
        {
            if (value == null) return false;
            if (Text.Length == 0) return true;

            return value.ToString().ToLower().Contains(Text.ToLower());
        }
    }
}
