function getCount(nodes) {
	var count = nodes.length;
	for (var i = 0; i < nodes.length; i++) {
		count += nodes[i].count();
	}
	
	return count;
}

function ScopeTree() {
	this.nodes = [];
	
	this.addNode = function(node) {
		this.nodes.push(node);
	};
	
	this.isEmpty = function() { return this.nodes.length === 0; };
	this.count = function() { return getCount(this.nodes); };
}

function ScopeTreeNode(scope) {
	this.scope = scope;
	this.parent = null;
	this.children = [];
	
	this.addChild = function(node) {
		node.parent = this;
		this.children.push(node);
	};
	
	this.count = function() {
		return getCount(this.children);
	};
	
	this.depth = function() {
		return 1 + (this.parent ? this.parent.depth() : 0);
	};
}

ScopeTree.prototype = {
	getMostRecentNode: function() {
		function getMostRecentNode(nodes, depth, currentDepth) {
			var node = nodes[nodes.length - 1];
			return currentDepth < depth && node.children.length > 0 ? getMostRecentNode(node.children, depth, ++currentDepth) : node;
		}
		
		return function(depth) {
			return depth > 0 && !this.isEmpty() ? getMostRecentNode(this.nodes, depth, 0) : null;
		};
	}()
};