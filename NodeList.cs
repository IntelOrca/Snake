using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ted.MySnake
{
	class NodeList : IList<Node>
	{
		List<Node> mNodes = new List<Node>();

		public Node GetLowestFScoreNode()
		{
			int lowestF = Int32.MaxValue;
			Node lowestN = null;
			foreach (Node n in mNodes) {
				if (n.FScore < lowestF) {
					lowestF = n.FScore;
					lowestN = n;
				}
			}

			return lowestN;
		}

		public bool ContainsLocation(Node searchNode)
		{
			foreach (Node n in mNodes) {
				if (n.Location == searchNode.Location)
					return true;
			}

			return false;
		}

		#region Trivial

		#region IList<Node> Members

		public int IndexOf(Node item)
		{
			return mNodes.IndexOf(item);
		}

		public void Insert(int index, Node item)
		{
			mNodes.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			mNodes.RemoveAt(index);
		}

		public Node this[int index]
		{
			get
			{
				return mNodes[index];
			}
			set
			{
				mNodes[index] = value;
			}
		}

		#endregion

		#region ICollection<Node> Members

		public void Add(Node item)
		{
			mNodes.Add(item);
		}

		public void Clear()
		{
			mNodes.Clear();
		}

		public bool Contains(Node item)
		{
			return mNodes.Contains(item);
		}

		public void CopyTo(Node[] array, int arrayIndex)
		{
			mNodes.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return mNodes.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(Node item)
		{
			return mNodes.Remove(item);
		}

		#endregion

		#region IEnumerable<Node> Members

		public IEnumerator<Node> GetEnumerator()
		{
			return mNodes.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return mNodes.GetEnumerator();
		}

		#endregion

		#endregion
	}
}
