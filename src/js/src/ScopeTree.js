function getCount(nodes) {
	var count = nodes.length;
	for (var i = 0; i < nodes.length; i++) {
		count += nodes[i].count();
	}
	
	return count;
}

function ScopeTree() {
	var nodes = [];
	
	this.addNode = function(node) {
		nodes.push(node);
	};
	
	this.isEmpty = function() { return nodes.length === 0; };
	
	this.getNodes = function() { return nodes.slice(0); };
	this.count = function() { return getCount(nodes); };
}

function ScopeTreeNode(scope) {
	var children = [];
	
	this.scope = scope;
	this.parent = null;
	this.getChildren = function() { return children.slice(0); };
	
	this.addChild = function(node) {
		node.parent = this;
		children.push(node);
	};
	
	this.count = function() {
		return getCount(children);
	};
	
	this.depth = function() {
		return 1 + (this.parent ? this.parent.depth : 0);
	};
}

ScopeTree.prototype = {
	getMostRecentNodes: function() {
		function getMostRecentNode(nodes, depth, currentDepth) {
			var node = nodes[nodes.length - 1];
			var children = node.getChildren();
			return currentDepth < depth && children.length > 0 ? getMostRecentNode(children, depth, ++currentDepth) : node;
		}
		
		return function(depth) {
			return depth > 0 && !this.isEmpty() ? getMostRecentNode(this.getNodes(), depth, 0) : null;
		};
	}()
};