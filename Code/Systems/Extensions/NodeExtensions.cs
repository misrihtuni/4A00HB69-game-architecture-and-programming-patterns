using System.Collections.Generic;
using Godot;

namespace GA.Common
{
	public static class NodeExtensions
	{
		public static TNode GetNode<TNode>(this Node node, bool recursive = true)
			where TNode : Node
		{
			int childCount = node.GetChildCount();

			for (int i = 0; i < childCount; i++)
			{
				Node child = node.GetChild(i);

				if (child is TNode result)
				{
					return result;
				}

				if (recursive && child.GetChildCount() > 0)
				{
					TNode recursiveResult = GetNode<TNode>(child, recursive);
					if (recursiveResult != null)
					{
						return recursiveResult;
					}
				}
			}

			return null;
		}

		public static IList<TNode> GetChildren<TNode>(this Node node, bool recursive = true)
			where TNode : Node
		{
			List<TNode> result = new List<TNode>();

			int childCount = node.GetChildCount();
			for (int i = 0; i < childCount; i++)
			{
				Node child = node.GetChild(i);
				if (child is TNode foundChild)
				{
					result.Add(foundChild);
				}

				if (recursive && child.GetChildCount() > 0)
				{
					result.AddRange(GetChildren<TNode>(child, recursive));
				}
			}

			return result;
		}
	}
}