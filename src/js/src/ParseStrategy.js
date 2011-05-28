function ParseStrategy() {
	var satisfiers = [];
	
	function LambdaDrivenSatisfier(lambda) {
		this.isSatisfiedBy = function(context) {
			return lambda(context);
		};
	}
	
	this.priority = ParseStrategy.defaultPriority;
	
	function advanceInputForToken(token) {
		return function(context) {
			if (token.length <= 1) {
				return;
			}
			
			context.advanceInput(token.length - 1);
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
	
	this.setAsTokenTransformer = function(token) {
		this.addSatisfier(new ExactCharMatchSatisfier(token));
		this.beforeExecute.attach(advanceInputForToken(token));
	};
}

extend(Satisfier, ParseStrategy);

ParseStrategy.prototype.doExecute = function(context) {};
ParseStrategy.prototype.executeIfSatisfied = function(context) {
	if (this.isSatisfiedBy(context)) {
		this.execute(context);
	}
};

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