function TimestampMacro() {
	TimestampMacro.$parent.call(this);
	
	//only "local" is allowed since i don't feel like implementing an entire 
	this.format = "";
	
	function zeroPad(s) {
		s = s.toString();
		if (s.length === 2) {
			return s;
		}
		
		return "0" + s;
	}
	
	this.getValue = function(writer) {
		var date = new Date();
		var midfix = this.format === "local" ? "" : "UTC";
		return date["get" + midfix + "FullYear"]() + "-" +
			zeroPad(date["get" + midfix + "Month"]() + 1) + "-" +
			zeroPad(date["get" + midfix + "Date"]()) + " " +
			zeroPad(date["get" + midfix + "Hours"]()) + ":" +
			zeroPad(date["get" + midfix + "Minutes"]()) + ":" +
			zeroPad(date["get" + midfix + "Seconds"]()) +
			(midfix === "UTC" ? "Z" : "");
	};
}

extend(Macro, TimestampMacro);