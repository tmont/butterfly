using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ButterflyNet.Parser {

	public class ScopeTree {

		private readonly ICollection<ScopeTreeNode> nodes = new Collection<ScopeTreeNode>();

		public void AddNode(ScopeTreeNode node) {
			nodes.Add(node);
		}

		public IEnumerable<ScopeTreeNode> Nodes { get { return nodes; } }
		public int Count { get { return nodes.Count + nodes.Sum(node => node.Count); } }

		public override string ToString() {
			return string.Format("ScopeTree(Count={0})", Count);
		}
	}

	public static class ScopeTreeExtensions {
		public static ScopeTreeNode GetMostRecentNode(this ScopeTree tree, int depth) {
			return depth > 0 && tree.Nodes.Any() ? GetMostRecentNode(tree.Nodes, depth) : null;
		}

		private static ScopeTreeNode GetMostRecentNode(IEnumerable<ScopeTreeNode> nodes, int depth, int currentDepth = 0) {
			var node = nodes.Last();
			return currentDepth < depth && node.Children.Any() ? GetMostRecentNode(node.Children, depth, ++currentDepth) : node;
		}
	}

}