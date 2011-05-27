function ParseStrategy() {
	var satisfiers = [];
	
	function LambdaDrivenSatisfier(lambda) {
		this.isSatisfiedBy = function(context) {
			return lambda(context);
		};
	}
	
	this.beforeExecute = new Event();
	this.afterExecute = new Event();
	this.priority = ParseStrategy.defaultPriority;
	this.execute = function(context) {
		this.beforeExecute.fire(context);
		this.doExecute(context);
		this.afterExecute.fire(context);
	};
	
	this.addSatisfier = function(satisfier) {
		if (typeof(satisfier) === "function") {
			satisfier = new LambdaDrivenSatisfier(satisfier);
		}
		
		satisfiers.push(satisfier);
	};
	
	this.getSatisfiers = function() {
		return satisfiers.slice(0);
	};
}

extend(Satisfier, ParseStrategy);

ParseStrategy.prototype.doExecute = function(context) {};

ParseStrategy.$override("isSatisfiedBy", function(context) {
	var satisfiers = this.getSatisfiers();
	for (var i = 0; i < satisfiers.length; i++) {
		if (!satisfiers[i].isSatisfiedBy(context)) {
			return false;
		}
	}
	
	return true;
});

ParseStrategy.defaultPriority = 100;