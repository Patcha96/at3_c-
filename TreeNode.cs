namespace AT3
{
    class TreeNode<T>
    {
        private T element;
        private TreeNode<T> left;
        private TreeNode<T> Right;
        private int height;

        public TreeNode(T _element)
        {
            element = _element;
            left = null;
            Right = null;
            height = 1;
        }
        public T getElement()
        {
            return element;
        }
        public void setElement(T _element)
        {
            element = _element;
        }
        public TreeNode<T> getLeft()
        {
            return left;
        }
        public void setLeft(TreeNode<T> _left)
        {
            left = _left;
        }
        public TreeNode<T> getRight()
        {
            return Right;
        }
        public void setRight(TreeNode<T> _right)
        {
            Right = _right;
        }
        
        public int getHeight()
        {
            return height;
        }
        public void setHeight(int _height)
        {
            height = _height;
        }

        public override string ToString()
        {
            string nodeString = "[" + element + " ";

            if (left == null && Right == null)
            {
                nodeString += " (Leaf) ";
            }

            if (left != null)
            {
                nodeString += "Left: " + left.ToString();
            }

            if (Right != null)
            {
                nodeString += "Right: " + Right.ToString();
            }

            nodeString += "] ";

            return nodeString;
        }
    }
}
