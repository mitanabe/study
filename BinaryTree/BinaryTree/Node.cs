using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class Node<TValue> where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Public Property
        /// </summary>
        public TValue val;
        public Node<TValue> parent;
        public Node<TValue> left;
        public Node<TValue> right;

        /// <summary>
        /// Public Constructor
        /// </summary>
        Node(TValue newVal)
        {
            // Initialize Properties
            this.val = newVal;
            this.parent = null;
            this.left = null;
            this.right = null;
        }

        /// <summary>
        /// Private Constructor
        /// </summary>
        private Node(TValue newVal, Node<TValue> newParent)
        {
            // Initialize Properties
            this.val = newVal;
            this.parent = newParent;
            this.left = null;
            this.right = null;
        }

        /// <summary>
        /// Public Methods
        /// </summary>
        /// <returns></returns>
        public Boolean HasLeft()
        {
            if (this.left != null)
            {
                // This node has a left node
                return true;
            }
            else
            {
                // This node does not have a left node
                return false;
            }
        }

        public Boolean HasRight()
        {
            if (this.right != null)
            {
                // This node has a right node
                return true;
            }
            else
            {
                // This node does not have a right node
                return false;
            }
        }

        public Boolean IsLeaf()
        {
            if ((!this.HasLeft()) && (!this.HasRight()))
            {
                // This node is leaf
                return true;
            }
            else
            {
                // This node is not leaf
                return false;
            }
        }

        public Boolean IsRoot()
        {
            if ((!this.HasLeft()) && (!this.HasRight()))
            {
                // This node is root
                return true;
            }
            else
            {
                // This node is not root
                return false;
            }
        }

        public void Add(TValue addVal)
        {
            Node<TValue> newNode = new Node<TValue>(addVal, this);

            if (this.val.CompareTo(addVal) >= 0)
            {
                if (!this.HasLeft())
                {
                    // Set the node to the left node
                    this.left = newNode;
                }
                else
                {
                    // Try to add the node to the left node
                    this.left.Add(addVal);
                }
            }
            else
            {
                if (!this.HasRight())
                {
                    // Set the node to the right node
                    this.right = newNode;
                }
                else
                {
                    // Try to add the node to the right node
                    this.right.Add(addVal);
                }
            }
        }

        public Node<TValue> Find(TValue findVal)
        {
            int compareToResult = this.val.CompareTo(findVal);
            if (compareToResult == 0)
            {
                // This node is the one
                return this;
            }
            else if (this.HasLeft() && (compareToResult > 0))
            {
                // Search in the left subtree
                return this.left.Find(findVal);
            }
            else if (this.HasRight() && (compareToResult < 0))
            {
                // Search in the right subtree
                return this.right.Find(findVal);
            }
            else
            {
                // Cannnot find the value
                return null;
            }
        }

        public List<Node<TValue>> FindAll(TValue findVal)
        {
            List<Node<TValue>> resultNodeList = new List<Node<TValue>>();
            Node<TValue> firstNode = this.Find(findVal);
            if(firstNode != null)
            {
                // There exist the node
                resultNodeList.Add(firstNode);

                while (resultNodeList.Last().HasLeft())
                {
                    // Search in the left subtree
                    Node<TValue> lowerNode = resultNodeList.Last().left.Find(findVal);
                    resultNodeList.Add(lowerNode);
                }
            }
            return resultNodeList;
        }

        public Node<TValue> GetMinNode()
        {
            if (this.HasLeft())
            {
                // this node is the one
                return this;
            }
            else
            {
                // search in the left subtree
                return this.left.GetMinNode();
            }
        }

        public void Remove()
        {
            if (this.IsLeaf())
            {
                // The case this node has no children
                if (this.parent.left.val.CompareTo(this.val) == 0)
                {
                    this.parent.left = null;
                }
                else
                {
                    this.parent.right = null;
                }
            }
            else if (this.HasLeft() && !this.HasRight())
            {
                // The case this node has only left child
                if (this.parent.left.val.CompareTo(this.val) == 0)
                {
                    this.parent.left = this.left;
                }
                else
                {
                    this.parent.right = this.left;
                }
            }
            else if (!this.HasLeft() && this.HasRight())
            {
                // The case this node has only left child
                if (this.parent.left.val.CompareTo(this.val) == 0)
                {
                    this.parent.left = this.right;
                }
                else
                {
                    this.parent.right = this.right;
                }
            }
            else
            {
                // The case this node has left and right child
                // Move the minimum node of the right tree
                Node<TValue> rightMinNode = this.right.GetMinNode();
                this.val = rightMinNode.val;
                rightMinNode.Remove();
            }
        }
    }
}
