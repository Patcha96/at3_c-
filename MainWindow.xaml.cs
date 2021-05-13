using System;
using System.Windows;
using System.Windows.Controls;

namespace AT3
{
    /// <summary>
    /// Unresizable TextBox class
    /// Prevent Hash TextBox from changing size when 
    /// hash is generated.
    /// </summary>
    public class UnresizableTextBox : TextBox
    {
        protected override Size MeasureOverride(Size constraint)
        {
            return new Size(0, 0);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            toolTip.Content = "Please add an entry to the list first.";
            txtHash.ToolTip = toolTip;
        }

        private BinarySearchTree<string> tree = new BinarySearchTree<string>();
        ToolTip toolTip = new ToolTip();

        /// <summary>
        /// Add Staff Button, adds staff name from the txtAdd Text Box to Tree and List Box.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtAdd.Text))
            {
                try
                {
                    tree.Insert(txtAdd.Text);
                    lstStaff.Items.Clear();
                    tree.ClearStaff();
                    tree.InOrder(tree.getRoot());
                    for (int i = 0; i < tree.CountStaff(); i++)
                    {
                        lstStaff.Items.Add(tree.ElementStaff(i));
                    }
                    toolTip.Content = "Please select an entry to the list first.";
                    txtHash.ToolTip = toolTip;
                }
                catch
                {
                    MessageBox.Show("Staff member already exists!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        /// <summary>
        /// Remove Staff Button, removing staff name inside the txtSearchRemove Text Box from the lstStaff List Box and the Binary Search Tree.
        /// </summary>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            for (int i = 0; i < lstStaff.Items.Count; i++)
            {
                if (lstStaff.Items[i].ToString().Equals(tree.Find(txtSearchRemove.Text)))
                {
                    tree.Delete(txtSearchRemove.Text);
                    lstStaff.Items.Clear();
                    tree.ClearStaff();
                    tree.InOrder(tree.getRoot());
                    for (int j = 0; j < tree.CountStaff(); j++)
                    {
                        lstStaff.Items.Add(tree.ElementStaff(j));
                    }
                    found = true;
                }

            }
            if (found == false)
            {
                MessageBox.Show("Staff member not found!", "Not found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Search Staff Button, searching for staff name inside the txtSearchRemove Text Box, selecting it inside the lstStaff List Box 
        /// by searching for it inside the Binary Search Tree and matching names in the Tree with the names in the List Box.
        /// </summary>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            lstStaff.SelectedItem = -1;
            for (int i = 0; i < lstStaff.Items.Count; i++)
            {
                if (lstStaff.Items[i].ToString().Equals(tree.Find(txtSearchRemove.Text)))
                {
                    lstStaff.SelectedIndex = i;
                    found = true;
                }
            }
            if (found == false || lstStaff.Items.Count < 1)
            {
                MessageBox.Show("Staff member not found!", "Not found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Automatically populates the Search/Remove Text box with the selected item from list
        /// </summary>
        private void lstStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstStaff.SelectedIndex > -1 && lstStaff.Items.Count > 0)
            {
                string selectedStaff = lstStaff.SelectedItem.ToString();
                txtSearchRemove.Text = selectedStaff;
                string hashedStaff = BCrypt.Net.BCrypt.HashPassword(selectedStaff);
                txtHash.Text = hashedStaff;
                toolTip.Content = "Double click to copy hash value.";
                txtHash.ToolTip = toolTip;
            }
            else
            {
                txtAdd.Clear();
                txtSearchRemove.Clear();
                txtHash.Clear();
                if (lstStaff.Items.Count > 0)
                {
                    toolTip.Content = "Please select an entry to the list first.";
                    txtHash.ToolTip = toolTip;
                }
                else if (lstStaff.Items.Count == 0)
                {
                    toolTip.Content = "Please add an entry to the list first.";
                    txtHash.ToolTip = toolTip;
                }
            }
        }

        /// <summary>
        /// Copying hash value to clipboard on TextBox double click
        /// </summary>
        private void txtHash_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Clipboard.SetText(txtHash.Text);
            toolTip.Content = "Hash value copied to clipboard.";
            txtHash.ToolTip = toolTip;
            if (txtHash.SelectedText.Length > 0)
            {
                txtHash.SelectionLength = 0;
            }
        }
    }
}
