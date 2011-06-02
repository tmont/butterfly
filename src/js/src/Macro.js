function Macro() {
	Macro.$parent.call(this);
}
Macro.prototype.getValue = function() { return ""; };
extend(Loadable, Macro);