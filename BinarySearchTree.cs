using System;
using System.Collections.Generic;
using System.Linq;

namespace AT3
{
    class BinarySearchTree<T>
    {
        /// <summary>
        /// List of staff
        /// </summary>
        private List<string> staff;
        /// <summary>
        /// Tree Node generic object called Root
        /// </summary>
        private TreeNode<T> root;

        /// <summary>
        /// Binary Search Tree Constructor
        /// </summary>
        public BinarySearchTree()
        {
            root = null;
            staff = new List<string>();
        }
        public TreeNode<T> getRoot()
        {
            return root;
        }
        public void setRoot(TreeNode<T> _root)
        {
            root = _root;
        }

        /// <summary>
        /// Returns the amount of staff in the staff list
        /// </summary>
        public int CountStaff()
        {
            return staff.Count();
        }

        /// <summary>
        /// Returns the staff name at passed staff list index.
        /// </summary>
        public string ElementStaff(int i)
        {
            return staff.ElementAt(i);
        }

        /// <summary>
        /// Adds to the staff list.
        /// </summary>
        public void AddStaff(string _name)
        {
            staff.Add(_name);
        }

        /// <summary>
        /// Clears the staff list.
        /// </summary>
        public void ClearStaff()
        {
            staff.Clear();
        }

        /// <summary>
        /// Inserts the data passed to this function into the tree
        /// </summary>
        public void Insert(T data)
        {
            root = Insert(data, root);
        }

        /// <summary>
        /// Deletes the TreeNode<T> containing data passed to this function.
        /// </summary>
        public void Delete(T data)
        {
            root = Delete(root, data);
        }

        /// <summary>
        /// Returns the element found at bottom left of the tree.
        /// </summary>
        public T FindMin()
        {
            return ElementAt(FindMin(root));
        }

        /// <summary>
        /// Searches the tree for a branch containing passed data.
        /// </summary>
        public T Find(T data)
        {
            return ElementAt(Find(data, root));
        }

        /// <summary>
        /// Returns the data element of passed TreeNode<T> object/
        /// </summary>
        private T ElementAt(TreeNode<T> branch)
        {
            if (branch == null)
            { 
                return default(T);
            }
            else
            {
                return branch.getElement();
            }

        }

        /// <summary>
        /// Finds the tree node containing the data object passed, starting from the TreeNode<T> passed to this function
        /// and returns the TreeNode<T> containing said data.
        /// </summary>
        private TreeNode<T> Find(T data, TreeNode<T> branch)
        {
            while (branch != null)
            {
                if ((data as IComparable).CompareTo(branch.getElement()) < 0)
                {
                    branch = branch.getLeft();
                }
                else if ((data as IComparable).CompareTo(branch.getElement()) > 0)
                {
                    branch = branch.getRight();
                }
                else
                {
                    return branch;
                }
            }
            return null;
        }

        /// <summary>
        /// Finds bottom left node starting from the TreeNode<T> provided
        /// </summary>
        private TreeNode<T> FindMin(TreeNode<T> branch)
        {
            if (branch != null)
            {
                while (branch.getLeft() != null)
                {
                    branch = branch.getLeft();
                }
            }
            return branch;
        }

        /// <summary>
        /// Adds each node from the tree to the staff list recursively, alphabetically arranged
        /// </summary>
        public void InOrder(TreeNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            InOrder(node.getLeft());
            staff.Add(node.getElement().ToString());
            InOrder(node.getRight());
        }

        /// <summary>
        /// Inserts the data into the tree, alphabetically arranged and balanced.
        /// </summary>
        public TreeNode<T> Insert(T data, TreeNode<T> branch)
        {
            if (branch == null)
            {
                branch = new TreeNode<T>(data);
                staff.Add(branch.getElement().ToString());
            }
            else if ((data as IComparable).CompareTo(branch.getElement()) < 0)
            {
                branch.setLeft(Insert(data, branch.getLeft()));
                branch.setHeight(UpdateHeight(branch));
                branch = BalanceNode(branch, data);
            }
            else if ((data as IComparable).CompareTo(branch.getElement()) > 0)
            {
                branch.setRight(Insert(data, branch.getRight()));
                branch.setHeight(UpdateHeight(branch));
                branch = BalanceNode(branch, data);
            }
            else
            {
                throw new Exception("Duplicate item");
            }

            return branch;
        }

        /// <summary>
        /// Updates the height of the TreeNode<T>
        /// </summary>
        public int UpdateHeight(TreeNode<T> node)
        {
            if (node.getLeft() != null && node.getRight() != null)
            {
                return 1 + Math.Max(node.getLeft().getHeight(), node.getRight().getHeight());
            }
            else if (node.getLeft() != null && node.getRight() == null)
            {
                return node.getLeft().getHeight() + 1;
            }
            else if (node.getLeft() == null && node.getRight() != null)
            {
                return node.getRight().getHeight() + 1;
            }
            else
            {
                return 1;
            }

        }

        /// <summary>
        /// Balances the TreeNode<T> if the tree height on the left of the tree is higher than the right and vice-versa, by rotating its nodes.
        /// </summary>
        public TreeNode<T> BalanceNode(TreeNode<T> node, T data)
        {
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (GetBalance(node.getLeft()) >= 0)
                {
                    node = RightRotate(node);
                }
                else
                {
                    node.setLeft(LeftRotate(node.getLeft()));
                    node = RightRotate(node);
                }
            }
            else if (balance < -1)
            {
                if (GetBalance(node.getRight()) <= 0)
                {
                    node = LeftRotate(node);
                }
                else
                {
                    node.setRight(RightRotate(node.getRight()));
                    node = LeftRotate(node);
                }
            }
            return node;
        }

        /// <summary>
        /// Checks whether the tree is balanced.
        /// </summary>
        private int GetBalance(TreeNode<T> branch)
        {
            int height = 0;
            int left = 0;
            int right = 0;
            if (branch != null)
            {
                if (branch.getLeft() != null)
                    left = branch.getLeft().getHeight();
                if (branch.getRight() != null)
                    right = branch.getRight().getHeight();
                height = left - right;
            }
            return height;
        }

        /// <summary>
        /// Rotates TreeNode<T> passed down(height) and to the left of finalNode.
        /// </summary>
        public TreeNode<T> LeftRotate(TreeNode<T> currentNode)
        {
            TreeNode<T> finalNode = currentNode.getRight();
            TreeNode<T> subTree = finalNode.getLeft();

            finalNode.setLeft(currentNode);
            currentNode.setRight(subTree);

            currentNode.setHeight(UpdateHeight(currentNode));
            finalNode.setHeight(UpdateHeight(finalNode));

            return finalNode;
        }

        /// <summary>
        /// Rotates TreeNode<T> passed down(height) and to the right of finalNode.
        /// </summary>
        public TreeNode<T> RightRotate(TreeNode<T> currentNode)
        {
            TreeNode<T> finalNode = currentNode.getLeft();
            TreeNode<T> subTree = finalNode.getRight();

            finalNode.setRight(currentNode);
            currentNode.setLeft(subTree);

            currentNode.setHeight(UpdateHeight(currentNode));
            finalNode.setHeight(UpdateHeight(finalNode));

            return finalNode;
        }

        /// <summary>
        /// Recursively moves through the tree to find the node to be deleted.
        /// When it finds the node it tests to see if the node has zero, one or two children.
        /// If the node has one or zero children it deletes the node.
        /// If the node has two children it finds the minimum node of the right sub-tree which it sets as a temp node, then
        /// sets the current node's data equal to the temp node's.
        /// Recursively moves through the current node's sub-tree and deletes the minimum node.
        /// </summary>
        public TreeNode<T> Delete(TreeNode<T> branch, T data)
        {
            // if Current Node is null
            if (branch == null) return branch;

            // else iterate down (left or right)
            else if ((data as IComparable).CompareTo(branch.getElement()) < 0)
            {
                branch.setLeft(Delete(branch.getLeft(), data));
            }
            else if ((data as IComparable).CompareTo(branch.getElement()) > 0)
            {
                branch.setRight(Delete(branch.getRight(), data));
            }
            // else Node has been found
            else
            {
                // Case 1: No Children
                if (branch.getLeft() == null && branch.getRight() == null)
                    branch = null;

                // Case 2: One Child
                else if (branch.getLeft() == null)
                    branch = branch.getRight();
                else if (branch.getRight() == null)
                    branch = branch.getLeft();
                // Case 3: Two Children
                else
                {
                    TreeNode<T> temp = FindMin(branch.getRight());
                    branch.setElement(temp.getElement());
                    branch.setRight(Delete(branch.getRight(), temp.getElement()));
                }
            }
            // Set the new height & balance
            if (branch != null)
            {
                branch.setHeight(UpdateHeight(branch));
                branch = BalanceNode(branch, data);
            }
            return branch;
        }

        // Prints the Balanced BST as a string
        public override string ToString()
        {
            return root.ToString();
        }
    }
}
