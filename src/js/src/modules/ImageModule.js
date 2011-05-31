function ImageModule(baseUrl) {
	ImageModule.$parent.call(this);
	
	this.url = "";
	this.alt = "";
	this.title = "";
	this.width = 0;
	this.height = 0;
	
	this.render = function(writer) {
		if (!this.url) {
			throw new ModuleException("For images, the URL must be specified");
		}
		
		if (!this.alt) {
			this.alt = this.url;
		}
		
		if (!this.title) {
			this.title = this.url;
		}
		
		var otherAttributes = "";
		if (this.width > 0 || this.height > 0) {
			otherAttributes += " ";
			if (this.width > 0) {
				otherAttributes += "width=\"" + this.width + "\" ";
			}
			if (this.height > 0) {
				otherAttributes += "height=\"" + this.height + "\" ";
			}
		}
		
		if (!isExternalUrl(this.url)) {
			this.url = baseUrl + this.url;
		}
		
		writer.write("<img src=\"" + this.url + "\" alt=\"" + this.alt + "\" title=\"" + this.title + "\"" + rtrim(otherAttributes) + " />");
	};
}

extend(Module, ImageModule);