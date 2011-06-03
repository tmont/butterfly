using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ButterflyNet.Parser {
	public class ScopeTreeNode {
		private readonly ICollection<ScopeTreeNode> children = new Collection<ScopeTreeNode>();

		public ScopeTreeNode(IScope scope) {
			Scope = scope;
		}

		public ScopeTreeNode Parent { get; private set; }
		public IScope Scope { get; private set; }
		public IEnumerable<ScopeTreeNode> Children { get { return children; } }

		public void AddChild(ScopeTreeNode node) {
			node.Parent = this;
			children.Add(node);
		}

		public int Count { get { return children.Count + children.Sum(node => node.Count); } }
		public int Depth { get { return 1 + (Parent != null ? Parent.Depth : 0); } }

		public override string ToString() {
			return string.Format("ScopeTreeNode(Scope={0}, Count={1}, Depth={2})", Scope, Count, Depth);
		}
	}
}