function HtmlEntityModule() {
	HtmlEntityModule.$parent.call(this);
	
	this.value = "";
	
	this.render = function(writer) {
		if (!this.value) {
			throw new ModuleException("The \"value\" property must be set to a valid HTML entity");
		}
		
		if (!/^[a-zA-Z0-9#]+$/.test(this.value)) {
			throw new ModuleException("\"" + this.value + "\" is not a valid HTML entity");
		}
		
		writer.write("&" + this.value + ";");
	};
}

extend(Module, HtmlEntityModule);